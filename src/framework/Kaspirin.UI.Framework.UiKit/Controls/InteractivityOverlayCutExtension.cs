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
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class InteractivityOverlayCutExtension : MarkupExtension
    {
        public Enum? Id { get; set; }
        public bool? AllowsInteraction { get; set; }
        public double? ClipCornerRadius { get; set; }
        public bool? CloseOnMouseClick { get; set; }
        public bool? CloseOnMouseWheel { get; set; }
        public Thickness? ClipExtent { get; set; }
        public object? Decorator { get; set; }
        public DataTemplate? DecoratorTemplate { get; set; }
        public double? DecoratorHorizontalOffset { get; set; }
        public InteractivityOverlayCutDecoratorPosition? DecoratorPosition { get; set; }
        public double? DecoratorVerticalOffset { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            Guard.IsNotNull(Id);

            var overlayCut = new InteractivityOverlayCut()
            {
                Decorator = Decorator,
                DecoratorTemplate = DecoratorTemplate,
            };

            if (AllowsInteraction != null)
            {
                overlayCut.AllowsInteraction = AllowsInteraction.Value;
            }

            if (ClipCornerRadius != null)
            {
                overlayCut.ClipCornerRadius = ClipCornerRadius.Value;
            }

            if (ClipExtent != null)
            {
                overlayCut.ClipExtent = ClipExtent.Value;
            }

            if (CloseOnMouseClick != null)
            {
                overlayCut.CloseOnMouseClick = CloseOnMouseClick.Value;
            }

            if (CloseOnMouseWheel != null)
            {
                overlayCut.CloseOnMouseWheel = CloseOnMouseWheel.Value;
            }

            if (DecoratorPosition != null)
            {
                overlayCut.DecoratorPosition = DecoratorPosition.Value;
            }

            if (DecoratorHorizontalOffset != null)
            {
                overlayCut.DecoratorHorizontalOffset = DecoratorHorizontalOffset.Value;
            }

            if (DecoratorVerticalOffset != null)
            {
                overlayCut.DecoratorVerticalOffset = DecoratorVerticalOffset.Value;
            }

            return new InteractivityOverlayCutCollection()
            {
                { Id, overlayCut }
            };
        }
    }
}
