// Copyright © 2024 AO Kaspersky Lab.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals;

internal static class ImageInternals
{
    #region ImageObserver

    public static bool GetImageObserver(DependencyObject element)
        => (bool)element.GetValue(ImageObserverProperty);
    public static void SetImageObserver(DependencyObject element, bool value)
        => element.SetValue(ImageObserverProperty, value);

    public static readonly DependencyProperty ImageObserverProperty = DependencyProperty.RegisterAttached(
        "ImageObserver",
        typeof(bool),
        typeof(ImageInternals),
        UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(Image), nameof(ImageObserverProperty), OnImageObserverChanged));

    #endregion

    #region SvgBrush

    public static Brush GetSvgBrush(DependencyObject element)
        => (Brush)element.GetValue(SvgBrushProperty);

    public static void SetSvgBrush(DependencyObject element, Brush value)
        => element.SetValue(SvgBrushProperty, value);

    public static readonly DependencyProperty SvgBrushProperty = DependencyProperty.RegisterAttached(
        "SvgBrush",
        typeof(Brush),
        typeof(ImageInternals),
        UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(Image), nameof(SvgBrushProperty), OnSvgBrushChanged));

    #endregion

    #region PropertyChangeNotifier

    private static PropertyChangeNotifier<Image, ImageSource> GetPropertyChangeNotifier(DependencyObject element)
        => (PropertyChangeNotifier<Image, ImageSource>)element.GetValue(_propertyChangeNotifierProperty);

    private static void SetPropertyChangeNotifier(DependencyObject element, PropertyChangeNotifier<Image, ImageSource> value)
        => element.SetValue(_propertyChangeNotifierProperty, value);

    private static readonly DependencyProperty _propertyChangeNotifierProperty = DependencyProperty.RegisterAttached(
        "PropertyChangeNotifier",
        typeof(PropertyChangeNotifier<Image, ImageSource>),
        typeof(ImageInternals),
        new PropertyMetadata(default(PropertyChangeNotifier<Image, ImageSource>)));

    #endregion

    private static void OnImageObserverChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var image = (Image)d;

        ApplyBrush(image);

        var changeNotifier = GetPropertyChangeNotifier(image);
        if (changeNotifier == null)
        {
            changeNotifier = new PropertyChangeNotifier<Image, ImageSource>(image, Image.SourceProperty);
            changeNotifier.ValueChanged += OnSourceChanged;
            SetPropertyChangeNotifier(image, changeNotifier);
        }
    }

    private static void OnSvgBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var image = (Image)d;
        ApplyBrush(image);
    }

    private static void OnSourceChanged(Image image, ImageSource? oldValue, ImageSource? newValue)
        => ApplyBrush(image);

    private static void ApplyBrush(Image image)
    {
        var drawing = (image.Source as DrawingImage)?.Drawing;
        if (drawing == null)
        {
            return;
        }

        var brush = GetSvgBrush(image);
        if (brush != null)
        {
            ApplyBrush(drawing, brush);
        }
    }

    private static void ApplyBrush(Drawing drawing, Brush brush)
    {
        if (drawing is DrawingGroup drawingGroup)
        {
            foreach (var child in drawingGroup.Children)
            {
                ApplyBrush(child, brush);
            }

            return;
        }

        if (drawing is GeometryDrawing geometryDrawing)
        {
            if (geometryDrawing.Brush != null && !IsTransparent(geometryDrawing.Brush))
            {
                geometryDrawing.SetCurrentValue(GeometryDrawing.BrushProperty, brush);
            }

            var pen = geometryDrawing.Pen;
            if (pen?.Brush != null && !IsTransparent(pen.Brush))
            {
                pen.SetCurrentValue(Pen.BrushProperty, brush);
            }
        }
    }

    private static bool IsTransparent(Brush brush)
    {
        if (brush is SolidColorBrush solid)
        {
            return solid.Color.A == 0;
        }
        else if (brush is GradientBrush gradient)
        {
            return gradient.GradientStops.All(s => s.Color.A == 0);
        }

        return false;
    }
}