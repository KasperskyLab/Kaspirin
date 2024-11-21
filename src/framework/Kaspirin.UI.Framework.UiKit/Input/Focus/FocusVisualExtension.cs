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

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;

namespace Kaspirin.UI.Framework.UiKit.Input.Focus
{
    public sealed class FocusVisualExtension : MarkupExtension
    {
        public FocusVisualType Type { get; set; }
        public LocalizationMarkupBase? Brush { get; set; }
        public CornerRadius CornerRadius { get; set; }
        public Thickness Margin { get; set; }
        public double Thickness { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            FrameworkElementFactory focusElement;

            if (Type == FocusVisualType.Ellipse)
            {
                focusElement = new FrameworkElementFactory(typeof(Ellipse));
                focusElement.SetValue(Shape.StrokeThicknessProperty, Thickness);
                focusElement.SetValue(Shape.StrokeProperty, Brush);
            }
            else
            {
                focusElement = new FrameworkElementFactory(typeof(Border));
                focusElement.SetValue(Border.BorderBrushProperty, Brush);
                focusElement.SetValue(Border.CornerRadiusProperty, CornerRadius);
                focusElement.SetValue(Border.BorderThicknessProperty, new Thickness(Thickness));
            }

            focusElement.SetValue(FrameworkElement.MarginProperty, Margin);
            focusElement.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Stretch);
            focusElement.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Stretch);

            var focusTemplate = new ControlTemplate
            {
                VisualTree = focusElement
            };

            var focusStyle = new Style();
            focusStyle.Setters.Add(new Setter
            {
                Property = Control.TemplateProperty,
                Value = focusTemplate
            });

            return focusStyle;
        }
    }
}