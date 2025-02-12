// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 11/23/2022.
// Scope of modification:
//   - Code adaptation to project requirements.

using System;
using System.Collections.Generic;

using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace SharpVectors.Runtime
{
    /// <summary>
    /// This creates a host for visuals derived from the <see cref="Canvas"/> class.
    /// </summary>
    /// <remarks>
    /// This class provides layout, event handling, and container support for the 
    /// child visual objects.
    /// </remarks>
    public sealed class SvgAnimationLayer : DependencyObject
    {
        #region Private Fields

        private Rect _bounds;

        private Color _colorText;
        private Color _colorLink;
        private Color _colorSelected;
        private Color _colorHover;

        private ToolTip _tooltip;
        private TextBlock _tooltipText;

        private Transform _displayTransform;

        private DrawingGroup _wholeDrawing;
        private DrawingGroup _linksDrawing;

        // Create a collection of child visual objects.
        private List<Drawing> _linkObjects;
        private List<Drawing> _drawObjects;

        private Drawing _hitVisual;
        private Drawing _selectedVisual;

        private SvgAnimator _animator;

        private SvgDrawingCanvas _drawingCanvas;

        private Dictionary<string, Brush> _visualBrushes;

        #endregion

        #region Constructors and Destructor

        public SvgAnimationLayer(SvgDrawingCanvas drawingCanvas)
        {
            _animator       = new SvgAnimator();
            _drawingCanvas  = drawingCanvas;

            _displayTransform = Transform.Identity;

            _colorText      = Colors.Black;
            _colorLink      = Colors.Blue;
            _colorSelected  = Colors.Red;
            _colorHover = (Color)ColorConverter.ConvertFromString("#ffa500");

            _visualBrushes  = new Dictionary<string, Brush>(StringComparer.OrdinalIgnoreCase);

            _drawObjects       = new List<Drawing>();
            _linkObjects       = new List<Drawing>();

            // Create a tooltip and set its position.
            _tooltip                    = new ToolTip();
            _tooltip.Placement          = PlacementMode.MousePoint;
            _tooltip.PlacementRectangle = new Rect(50, 0, 0, 0);
            _tooltip.HorizontalOffset   = 20;
            _tooltip.VerticalOffset     = 20;

            _tooltipText        = new TextBlock();
            _tooltipText.Text   = string.Empty;
            _tooltipText.Margin = new Thickness(6, 0, 0, 0);

            //Create BulletDecorator and set it as the tooltip content.
            Ellipse bullet = new Ellipse();
            bullet.Height  = 10;
            bullet.Width   = 10;
            bullet.Fill    = Brushes.LightCyan;

            BulletDecorator decorator = new BulletDecorator();
            decorator.Bullet = bullet;
            decorator.Margin = new Thickness(0, 0, 10, 0);
            decorator.Child  = _tooltipText;

            _tooltip.Content    = decorator;
            _tooltip.IsOpen     = false;
            _tooltip.Visibility = Visibility.Hidden;

            //Finally, set tooltip on this canvas
            _drawingCanvas.ToolTip    = _tooltip;
            //_drawingCanvas.Background = Brushes.Transparent;
        }

        #endregion

        #region Public Properties

        public Transform DisplayTransform
        {
            get
            {
                return _displayTransform;
            }
            set
            {
                if (value == null)
                {
                    _displayTransform = new MatrixTransform(Matrix.Identity);
                }
                else
                {
                    _displayTransform = value;
                }
            }
        }

        #endregion

        #region Public Methods

        public void LoadDiagrams(DrawingGroup linkGroups, DrawingGroup wholeGroup)
        {
            if (linkGroups == null)
            {
                return;
            }
            DrawingCollection drawings = linkGroups.Children;
            if (drawings == null || drawings.Count == 0)
            {
                return;
            }
            else if (drawings.Count == 1)
            {
                DrawingGroup layerGroup = drawings[0] as DrawingGroup;
                if (layerGroup != null)
                {
                    string elementId = SvgObject.GetId(layerGroup);
                    if (!string.IsNullOrWhiteSpace(elementId) &&
                        string.Equals(elementId, "IndicateLayer", StringComparison.OrdinalIgnoreCase))
                    {
                        this.LoadLayerDiagrams(layerGroup);

                        _wholeDrawing = wholeGroup;
                        _linksDrawing = linkGroups;

                        return;
                    }
                }
            }

            this.UnloadDiagrams();             

            for (int i = 0; i < drawings.Count; i++)
            {
                DrawingGroup childGroup = drawings[i] as DrawingGroup;
                if (childGroup != null)
                {
                    string groupName = SvgLink.GetKey(childGroup);
                    //string groupName = SvgObject.GetName(childGroup);
                    if (string.IsNullOrWhiteSpace(groupName))
                    {
                        if (childGroup.Children != null && childGroup.Children.Count == 1)
                        {
                            this.AddDrawing(childGroup);
                        }
                        else
                        {
                            throw new InvalidOperationException("Error: The link group is in error.");
                        }
                    }
                    else
                    {
                        if (childGroup.Children != null && childGroup.Children.Count == 1)
                        {
                            this.AddDrawing(childGroup, groupName);
                        }
                        else
                        {
                            throw new InvalidOperationException(
                                String.Format("Error: The link group is in error - {0}", groupName));
                        }
                    }
                }
            }

            _wholeDrawing = wholeGroup; 
            _linksDrawing = linkGroups;

            if (_drawingCanvas != null)
            {
                _displayTransform = _drawingCanvas.DisplayTransform;
            }
        }

        public void UnloadDiagrams()
        {
            _displayTransform = Transform.Identity;

            _bounds = new Rect(0, 0, 1, 1);

            //_brushIndex = 0;

            if (_animator != null)
            {
                _animator.Stop();
            }

            this.ClearVisuals();

            if (_tooltip != null)
            {
                _tooltip.IsOpen = false;
                _tooltip.Visibility = Visibility.Hidden;
            }

            //this.RenderTransform = new TranslateTransform(0, 0);

            _wholeDrawing = null;
            _linksDrawing = null;
        }

        #endregion

        #region Public Mouse Methods

        public bool HandleMouseMove(MouseEventArgs e)
        {
            // Retrieve the coordinates of the mouse button event.
            Point pt = e.GetPosition(_drawingCanvas);

            Drawing hitVisual = HitTest(pt);

            if (_selectedVisual != null && hitVisual == _selectedVisual)
            {
                _drawingCanvas.Cursor = Cursors.Hand;

                return true;
            }

            string itemName = null;

            if (hitVisual == null)
            {
                if (_hitVisual != null)
                {
                    itemName = SvgObject.GetName(_hitVisual);
                    if (itemName == null)
                    {
                        _hitVisual = null;
                        return false;
                    }
                    if (_visualBrushes.ContainsKey(itemName) && (_hitVisual != _selectedVisual))
                    {
                        SolidColorBrush brush = _visualBrushes[itemName] as SolidColorBrush;
                        if (brush != null)
                        {
                            brush.Color = _colorLink;
                        }
                    }
                    _hitVisual = null;
                }

                if (_tooltip != null)
                {
                    _tooltip.IsOpen     = false;
                    _tooltip.Visibility = Visibility.Hidden;
                }
 
                return false;
            }
            else
            {
                _drawingCanvas.Cursor = Cursors.Hand;

                if (hitVisual == _hitVisual)
                {
                    return false;
                }

                if (_hitVisual != null)
                {
                    itemName = SvgObject.GetName(_hitVisual);
                    if (itemName == null)
                    {
                        _hitVisual = null;
                        return false;
                    }
                    if (_visualBrushes.ContainsKey(itemName) && (_hitVisual != _selectedVisual))
                    {
                        SolidColorBrush brush = _visualBrushes[itemName] as SolidColorBrush;
                        if (brush != null)
                        {
                            brush.Color = _colorLink;
                        }
                    }
                    _hitVisual = null;
                }

                itemName = SvgObject.GetName(hitVisual);
                if (itemName == null)
                {
                    return false;
                }
                if (_visualBrushes.ContainsKey(itemName))
                {
                    SolidColorBrush brush = _visualBrushes[itemName] as SolidColorBrush;
                    if (brush != null)
                    {
                        brush.Color = _colorHover;
                    }
                }
                _hitVisual = hitVisual;

                string tooltipText          = itemName;
                SvgLinkAction linkAction = SvgLink.GetAction(hitVisual);
                if (linkAction == SvgLinkAction.LinkTooltip && 
                    _tooltip != null && !string.IsNullOrWhiteSpace(tooltipText))
                {
                    Rect rectBounds = hitVisual.Bounds;

                    _tooltip.PlacementRectangle = rectBounds;

                    _tooltipText.Text = tooltipText;

                    if (_tooltip.Visibility == Visibility.Hidden)
                    {
                        _tooltip.Visibility = Visibility.Visible;
                    }

                    _tooltip.IsOpen = true;
                }
                else
                {
                    if (_tooltip != null)
                    {
                        _tooltip.IsOpen     = false;
                        _tooltip.Visibility = Visibility.Hidden;
                    } 
                }
            }

            return true;
        }

        public bool HandleMouseDown(MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition(_drawingCanvas);

            Drawing visual = HitTest(pt);
            if (visual == null)
            {
                if (_tooltip != null)
                {
                    _tooltip.IsOpen     = false;
                    _tooltip.Visibility = Visibility.Hidden;
                }
 
                return false;
            }

            if (_selectedVisual != null && visual == _selectedVisual)
            {
                _drawingCanvas.Cursor = Cursors.Hand;

                return true;
            }

            string itemName = SvgObject.GetName(visual);
            if (itemName == null)
            {
                if (_tooltip != null)
                {
                    _tooltip.IsOpen = false;
                    _tooltip.Visibility = Visibility.Hidden;
                }

                return false;
            }
            SolidColorBrush brush = null;
            if (_visualBrushes.ContainsKey(itemName))
            {
                brush = _visualBrushes[itemName] as SolidColorBrush;

                if (brush != null)
                {
                    brush.Color = _colorSelected;
                }
            }
            if (brush == null)
            {
                if (_tooltip != null)
                {
                    _tooltip.IsOpen     = false;
                    _tooltip.Visibility = Visibility.Hidden;
                }

                return false;
            }
            if (_selectedVisual != null)
            {
                itemName = SvgObject.GetName(_selectedVisual);
                if (itemName == null)
                {
                    return false;
                }
                if (_visualBrushes.ContainsKey(itemName))
                {
                    brush = _visualBrushes[itemName] as SolidColorBrush;

                    if (brush != null)
                    {
                        brush.Color = _colorLink;
                    }
                }
                else
                {
                    return false;
                }
            }

            _selectedVisual = visual;

            if (e.ChangedButton == MouseButton.Left)
            {
                if (brush != null)
                {
                    string brushName = SvgObject.GetName(brush);
                    if (!string.IsNullOrWhiteSpace(brushName))
                    {
                        SvgLinkAction linkAction = SvgLink.GetAction(visual);
                        if (linkAction == SvgLinkAction.LinkHtml ||
                            linkAction == SvgLinkAction.LinkPage)
                        {
                            _animator.Start(brushName, brush);
                        }
                    }
                }
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                _animator.Stop();
            }

            return true;
        }

        public bool HandleMouseLeave()
        {
            if (_tooltip != null)
            {
                _tooltip.IsOpen     = false;
                _tooltip.Visibility = Visibility.Hidden;
            }

            if (_hitVisual == null)
            {
                return false;
            }

            string itemName = SvgObject.GetName(_hitVisual);
            if (itemName == null)
            {
                _hitVisual = null;
                return false;
            }
            if (_visualBrushes.ContainsKey(itemName) && (_hitVisual != _selectedVisual))
            {
                SolidColorBrush brush = _visualBrushes[itemName] as SolidColorBrush;
                if (brush != null)
                {
                    brush.Color = _colorLink;
                }
            }
            _hitVisual = null;

            return true;
        }

        #endregion

        #region Private Methods

        private void ClearVisuals()
        {
            if (_drawObjects != null && _drawObjects.Count != 0)
            {
                _drawObjects.Clear();
            }

            if (_linkObjects != null && _linkObjects.Count != 0)
            {
                _linkObjects.Clear();
            }
        }

        private Drawing HitTest(Point pt)
        {
            Point ptDisplay = _displayTransform.Transform(pt);

            for (int i = 0; i < _linkObjects.Count; i++)
            {
                Drawing drawing = _linkObjects[i];
                if (drawing.Bounds.Contains(ptDisplay))
                {
                    return drawing;
                }
            }

            return null;
        }

        private void AddDrawing(DrawingGroup group)
        {               
        }

        private void AddDrawing(DrawingGroup group, string name)
        {               
        }

        private void AddTextDrawing(DrawingGroup group, string name)
        {
            bool isHyperlink = false;
            if (name == "x11201_1_")
            {
                isHyperlink = true;
            }

            SolidColorBrush textBrush = null;
            if (name.StartsWith("XMLID_", StringComparison.OrdinalIgnoreCase))
            {
                textBrush = new SolidColorBrush(_colorText);
            }
            else
            {
                isHyperlink = true;
                textBrush = new SolidColorBrush(_colorLink);
            }
            string brushName = name + "_Brush";
            SvgObject.SetName(textBrush, brushName);

            DrawingCollection drawings = group.Children;
            int itemCount = drawings != null ? drawings.Count : 0;
            for (int i = 0; i < itemCount; i++)
            {
                DrawingGroup drawing = drawings[i] as DrawingGroup;
                if (drawing != null)
                {
                    for (int j = 0; j < drawing.Children.Count; j++)
                    {
                        GlyphRunDrawing glyphDrawing = drawing.Children[j] as GlyphRunDrawing;
                        if (glyphDrawing != null)
                        {
                            glyphDrawing.ForegroundBrush = textBrush;
                        }
                    }
                }
            }

            if (isHyperlink)
            {
                _visualBrushes[name] = textBrush;

                _linkObjects.Add(group);
            }
        }

        private void AddGeometryDrawing(GeometryDrawing drawing)
        {               
        }

        private static bool IsValidBounds(Rect rectBounds)
        {
            if (rectBounds.IsEmpty || Double.IsNaN(rectBounds.Width) || Double.IsNaN(rectBounds.Height)
                || Double.IsInfinity(rectBounds.Width) || Double.IsInfinity(rectBounds.Height))
            {
                return false;
            }

            return true;
        }

        private static bool TryCast<TBase, TDerived>(TBase baseObj, out TDerived derivedObj)
            where TDerived : class, TBase
        {
            return (derivedObj = baseObj as TDerived) != null;
        }

        private void LoadLayerDiagrams(DrawingGroup layerGroup)
        {
            this.UnloadDiagrams();

            DrawingCollection drawings = layerGroup.Children;

            DrawingGroup drawGroup = null;
            GeometryDrawing drawGeometry = null;
            for (int i = 0; i < drawings.Count; i++)
            {
                Drawing drawing = drawings[i];
                if (TryCast(drawing, out drawGroup))
                {
                    string groupName = SvgLink.GetKey(drawGroup);
                    if (string.IsNullOrWhiteSpace(groupName))
                    {
                        groupName = SvgObject.GetId(drawGroup);
                    }
                    //string groupName = SvgObject.GetName(childGroup);
                    if (string.IsNullOrWhiteSpace(groupName))
                    {
                        LoadLayerGroup(drawGroup);
                    }
                    else
                    {
                        SvgObjectType elementType = SvgObject.GetType(drawGroup);

                        if (elementType == SvgObjectType.Text)
                        {
                            this.AddTextDrawing(drawGroup, groupName);
                        }
                        else
                        {   
                            if (drawGroup.Children != null && drawGroup.Children.Count == 1)
                            {
                                this.AddDrawing(drawGroup, groupName);
                            }
                            else
                            {
                                //throw new InvalidOperationException(
                                //    String.Format("Error: The link group is in error - {0}", groupName));
                            }
                        }
                    }
                }
                else if (TryCast(drawing, out drawGeometry))
                {
                    this.AddGeometryDrawing(drawGeometry);
                }
            }

            if (_drawingCanvas != null)
            {
                _displayTransform = _drawingCanvas.DisplayTransform;
            }
        }

        private void LoadLayerGroup(DrawingGroup group)
        {
            DrawingCollection drawings = group.Children;

            DrawingGroup drawGroup = null;
            GeometryDrawing drawGeometry = null;
            for (int i = 0; i < drawings.Count; i++)
            {
                Drawing drawing = drawings[i];
                if (TryCast(drawing, out drawGroup))
                {
                    string groupName = SvgLink.GetKey(drawGroup);
                    if (string.IsNullOrWhiteSpace(groupName))
                    {
                        groupName = SvgObject.GetId(drawGroup);
                    }
                    //string groupName = SvgObject.GetName(childGroup);
                    if (string.IsNullOrWhiteSpace(groupName))
                    {
                        LoadLayerGroup(drawGroup);
                    }
                    else
                    {
                        SvgObjectType elementType = SvgObject.GetType(drawGroup);

                        if (elementType == SvgObjectType.Text)
                        {
                            this.AddTextDrawing(drawGroup, groupName);
                        }
                        else
                        {   
                            if (drawGroup.Children != null && drawGroup.Children.Count == 1)
                            {
                                this.AddDrawing(drawGroup, groupName);
                            }
                            else
                            {
                                //throw new InvalidOperationException(
                                //    String.Format("Error: The link group is in error - {0}", groupName));
                            }
                        }
                    }
                }
                else if (TryCast(drawing, out drawGeometry))
                {
                    this.AddGeometryDrawing(drawGeometry);
                }
            }
        }


        #endregion
    }
}
