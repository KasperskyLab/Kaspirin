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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls.Properties
{
    public static class ScrollViewerProps
    {
        #region CanMouseWheelScroll

        public static bool GetCanMouseWheelScroll(DependencyObject obj)
            => (bool)obj.GetValue(CanMouseWheelScrollProperty);

        public static void SetCanMouseWheelScroll(DependencyObject obj, bool value)
            => obj.SetValue(CanMouseWheelScrollProperty, value);

        public static readonly DependencyProperty CanMouseWheelScrollProperty = DependencyProperty.RegisterAttached(
            "CanMouseWheelScroll",
            typeof(bool),
            typeof(ScrollViewerProps),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ScrollViewer), nameof(CanMouseWheelScrollProperty), OnCanMouseWheelScrollChanged, defaultValue: true));

        private static void OnCanMouseWheelScrollChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => d.SetValue(ScrollViewerInternals.CanMouseWheelScrollProperty, e.NewValue);

        #endregion

        #region OuterVerticalScrollBar

        public static bool GetOuterVerticalScrollBar(DependencyObject obj)
            => (bool)obj.GetValue(OuterVerticalScrollBarProperty);

        public static void SetOuterVerticalScrollBar(DependencyObject obj, bool value)
            => obj.SetValue(OuterVerticalScrollBarProperty, value);

        public static readonly DependencyProperty OuterVerticalScrollBarProperty = DependencyProperty.RegisterAttached(
            "OuterVerticalScrollBar",
            typeof(bool),
            typeof(ScrollViewerProps),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ScrollViewer), nameof(OuterVerticalScrollBarProperty), defaultValue: false));

        #endregion

        #region OuterHorizontalScrollBar

        public static bool GetOuterHorizontalScrollBar(DependencyObject obj)
            => (bool)obj.GetValue(OuterHorizontalScrollBarProperty);

        public static void SetOuterHorizontalScrollBar(DependencyObject obj, bool value)
            => obj.SetValue(OuterHorizontalScrollBarProperty, value);

        public static readonly DependencyProperty OuterHorizontalScrollBarProperty = DependencyProperty.RegisterAttached(
            "OuterHorizontalScrollBar",
            typeof(bool),
            typeof(ScrollViewerProps),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ScrollViewer), nameof(OuterHorizontalScrollBarProperty), defaultValue: true));

        #endregion

        #region IsBorderFadeEnabled

        public static bool GetIsBorderFadeEnabled(DependencyObject obj)
            => (bool)obj.GetValue(IsBorderFadeEnabledProperty);

        public static void SetIsBorderFadeEnabled(DependencyObject obj, bool value)
            => obj.SetValue(IsBorderFadeEnabledProperty, value);

        public static readonly DependencyProperty IsBorderFadeEnabledProperty = DependencyProperty.RegisterAttached(
            "IsBorderFadeEnabled",
            typeof(bool),
            typeof(ScrollViewerProps),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ScrollViewer), nameof(IsBorderFadeEnabledProperty), OnIsBorderFadeEnabledChanged));

        private static void OnIsBorderFadeEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
            => d.SetValue(ScrollViewerInternals.IsBorderFadeEnabledProperty, e.NewValue);

        #endregion

        #region MouseWheelScrollOrientation

        public static Orientation GetMouseWheelScrollOrientation(DependencyObject obj)
            => (Orientation)obj.GetValue(MouseWheelScrollOrientationProperty);

        public static void SetMouseWheelScrollOrientation(DependencyObject obj, Orientation value)
            => obj.SetValue(MouseWheelScrollOrientationProperty, value);

        public static readonly DependencyProperty MouseWheelScrollOrientationProperty = DependencyProperty.RegisterAttached(
            "MouseWheelScrollOrientation",
            typeof(Orientation),
            typeof(ScrollViewerProps),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ScrollViewer), nameof(MouseWheelScrollOrientationProperty), OnMouseWheelScrollOrientationChanged, Orientation.Vertical));

        private static void OnMouseWheelScrollOrientationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => d.SetValue(ScrollViewerInternals.MouseWheelScrollOrientationProperty, e.NewValue);

        #endregion

        #region BorderFadeWidth

        public static double GetBorderFadeWidth(DependencyObject obj)
            => (double)obj.GetValue(BorderFadeWidthProperty);

        public static void SetBorderFadeWidth(DependencyObject obj, double value)
            => obj.SetValue(BorderFadeWidthProperty, value);

        public static readonly DependencyProperty BorderFadeWidthProperty = DependencyProperty.RegisterAttached(
            "BorderFadeWidth",
            typeof(double),
            typeof(ScrollViewerProps),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ScrollViewer), nameof(BorderFadeWidthProperty), OnBorderFadeWidthChanged, UIKitConstants.ScrollViewerBorderFadeLength));

        private static void OnBorderFadeWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) 
            => d.SetValue(ScrollViewerInternals.BorderFadeWidthProperty, e.NewValue);

        #endregion

        #region BorderFadeHeight

        public static double GetBorderFadeHeight(DependencyObject obj)
            => (double)obj.GetValue(BorderFadeHeightProperty);

        public static void SetBorderFadeHeight(DependencyObject obj, double value)
            => obj.SetValue(BorderFadeHeightProperty, value);

        public static readonly DependencyProperty BorderFadeHeightProperty = DependencyProperty.RegisterAttached(
            "BorderFadeHeight",
            typeof(double),
            typeof(ScrollViewerProps),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ScrollViewer), nameof(BorderFadeHeightProperty), OnBorderFadeHeightChanged, UIKitConstants.ScrollViewerBorderFadeLength));

        private static void OnBorderFadeHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => d.SetValue(ScrollViewerInternals.BorderFadeHeightProperty, e.NewValue);

        #endregion

        #region BorderFadeGradient

        public static GradientStopCollection GetBorderFadeGradient(DependencyObject obj)
            => (GradientStopCollection)obj.GetValue(BorderFadeGradientProperty);

        public static void SetBorderFadeGradient(DependencyObject obj, GradientStopCollection value)
            => obj.SetValue(BorderFadeGradientProperty, value);

        public static readonly DependencyProperty BorderFadeGradientProperty = DependencyProperty.RegisterAttached(
            "BorderFadeGradient",
            typeof(GradientStopCollection),
            typeof(ScrollViewerProps),
            UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(ScrollViewer), nameof(BorderFadeGradientProperty), OnBorderFadeGradientChanged, UIKitConstants.ScrollViewerBorderFadeGradient));

        private static void OnBorderFadeGradientChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => d.SetValue(ScrollViewerInternals.BorderFadeGradientProperty, e.NewValue);

        #endregion
    }
}
