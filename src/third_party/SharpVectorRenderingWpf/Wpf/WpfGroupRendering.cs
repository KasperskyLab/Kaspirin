// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 11/23/2022.
// Scope of modification:
//   - Code adaptation to project requirements.

using System;
using System.Diagnostics;

using System.Windows.Media;
using SharpVectorCore.Utils;
using SharpVectors.Dom.Svg; 
using SharpVectors.Runtime;

namespace SharpVectors.Renderers.Wpf
{
    public sealed class WpfGroupRendering : WpfRendering
    {
        #region Private Fields

        private DrawingGroup _drawGroup;

        #endregion

        #region Constructors and Destructor

        public WpfGroupRendering(SvgElement element)
            : base(element)
        {
        }

        #endregion

        #region Public Methods

        public override void BeforeRender(WpfDrawingRenderer renderer)
        {
            base.BeforeRender(renderer);

            WpfDrawingContext context = renderer.Context;
            _drawGroup = new DrawingGroup();

            string elementId = this.GetElementName();
            if (!string.IsNullOrWhiteSpace(elementId) && !context.IsRegisteredId(elementId))
            {
                SvgObject.SetName(_drawGroup, elementId);

                context.RegisterId(elementId);

                if (context.IncludeRuntime)
                {
                    SvgObject.SetId(_drawGroup, elementId);
                }
            }
            
            string elementClass = this.GetElementClass();
            if (!string.IsNullOrWhiteSpace(elementClass) && context.IncludeRuntime)
            {
                SvgObject.SetClass(_drawGroup, elementClass);
            }

            DrawingGroup currentGroup = context.Peek();

            if (currentGroup == null)
            {
                throw new InvalidOperationException("An existing group is expected.");
            }

            currentGroup.Children.Add(_drawGroup);
            context.Push(_drawGroup); 
        }

        public override void Render(WpfDrawingRenderer renderer)
        {
            if (_drawGroup != null)
            {
                Geometry clipGeom = this.ClipGeometry;
                if (clipGeom != null)
                {
                    _drawGroup.ClipGeometry = clipGeom;
                }

                Transform transform = this.Transform;
                if (transform != null)
                {
                    _drawGroup.Transform = transform;
                }

                float opacityValue = -1;

                SvgGElement element = (SvgGElement)_svgElement;
                string opacity = element.GetAttribute("opacity");
                if (string.IsNullOrWhiteSpace(opacity))
                {
                    opacity = element.GetPropertyValue("opacity");
                }
                if (!string.IsNullOrWhiteSpace(opacity))
                {
                    opacityValue = (float)SvgNumber.ParseNumber(opacity);
                    opacityValue = Math.Min(opacityValue, 1);
                    opacityValue = Math.Max(opacityValue, 0);
                }

                if (NumOperations.GreaterOrEquals(opacityValue, 0) && opacityValue < 1)
                {
                    _drawGroup.Opacity = opacityValue;
                }

                string sVisibility = element.GetPropertyValue("visibility");
                string sDisplay = element.GetPropertyValue("display");
                if (string.Equals(sVisibility, "hidden") || string.Equals(sDisplay, "none"))
                {
                    _drawGroup.Opacity = 0;
                }
            }

            // Register this drawing with the Drawing-Document...
            this.Rendered(_drawGroup);

            base.Render(renderer);
        }

        public override void AfterRender(WpfDrawingRenderer renderer)
        {
            Debug.Assert(_drawGroup != null);

            WpfDrawingContext context = renderer.Context;

            DrawingGroup currentGroup = context.Peek();

            if (currentGroup == null || currentGroup != _drawGroup)
            {
                throw new InvalidOperationException("An existing group is expected.");
            }

            // Remove the added group from the stack...
            context.Pop();

            // If the group is empty, we simply remove it...
            if (_drawGroup.Children.Count == 0 && _drawGroup.ClipGeometry == null &&
                _drawGroup.Transform == null && _drawGroup.Opacity.Equals(1.0))
            {
                currentGroup = context.Peek();
                if (currentGroup != null)
                {
                    currentGroup.Children.Remove(_drawGroup);
                }
            }

            base.AfterRender(renderer);
        }

        #endregion

        #region Protected Methods

        protected override void Initialize(SvgElement element)
        {
            base.Initialize(element);

            _drawGroup = null;
        }

        #endregion
    }
}
