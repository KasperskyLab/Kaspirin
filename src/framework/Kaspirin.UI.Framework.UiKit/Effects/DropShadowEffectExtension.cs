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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Kaspirin.UI.Framework.UiKit.Effects
{
    public sealed class DropShadowEffectExtension : MarkupExtension
    {
        public double BlurRadius { get; set; } = 10;
        public Color Color { get; set; } = Colors.Black;
        public double Direction { get; set; } = 270;
        public double Opacity { get; set; } = 0.3;
        public RenderingBias RenderingBias { get; set; } = RenderingBias.Quality;
        public double ShadowDepth { get; set; } = 2;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new DropShadowEffect
            {
                BlurRadius = BlurRadius,
                Color = Color,
                Direction = Direction,
                Opacity = Opacity,
                RenderingBias = RenderingBias,
                ShadowDepth = ShadowDepth,
            };
        }
    }
}
