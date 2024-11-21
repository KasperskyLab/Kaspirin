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
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Kaspirin.UI.Framework.UiKit.Effects
{
    public sealed class ColorKeyAlphaEffect : ShaderEffect
    {
        public ColorKeyAlphaEffect()
        {
            var effectsLocalizer = LocalizationManager.Current.LocalizerFactory.Resolve<IFileLocalizer>(EffectsScope);
            var effectUri = effectsLocalizer.GetFileUri(EffectKey);

            PixelShader = new PixelShader
            {
                UriSource = effectUri,
            };

            UpdateShaderValue(InputProperty);
            UpdateShaderValue(ColorKeyProperty);
            UpdateShaderValue(ToleranceProperty);
            UpdateShaderValue(CenterProperty);
            UpdateShaderValue(BlurAmountProperty);

            Tolerance = 1;
        }

        #region Input

        public Brush Input
        {
            get { return (Brush)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }
        }

        public static readonly DependencyProperty InputProperty =
            RegisterPixelShaderSamplerProperty("Input", typeof(ColorKeyAlphaEffect), 0);

        #endregion

        #region ColorKey

        public Color ColorKey
        {
            get { return (Color)GetValue(ColorKeyProperty); }
            set { SetValue(ColorKeyProperty, value); }
        }

        public static readonly DependencyProperty ColorKeyProperty =
            DependencyProperty.Register("ColorKey", typeof(Color), typeof(ColorKeyAlphaEffect),
                new UIPropertyMetadata(Colors.White, PixelShaderConstantCallback(0)));

        #endregion

        #region Tolerance

        public double Tolerance
        {
            get { return (double)GetValue(ToleranceProperty); }
            set { SetValue(ToleranceProperty, value); }
        }

        public static readonly DependencyProperty ToleranceProperty =
            DependencyProperty.Register("Tolerance", typeof(double), typeof(ColorKeyAlphaEffect),
                new UIPropertyMetadata(0D, PixelShaderConstantCallback(1)));

        #endregion

        #region Center

        public Point Center
        {
            get { return (Point)GetValue(CenterProperty); }
            set { SetValue(CenterProperty, value); }
        }

        public static readonly DependencyProperty CenterProperty =
            DependencyProperty.Register("Center", typeof(Point), typeof(ColorKeyAlphaEffect),
                new UIPropertyMetadata(new Point(0D, 0D), PixelShaderConstantCallback(0)));

        #endregion

        #region BlurAmount

        public double BlurAmount
        {
            get { return (double)GetValue(BlurAmountProperty); }
            set { SetValue(BlurAmountProperty, value); }
        }

        public static readonly DependencyProperty BlurAmountProperty =
            DependencyProperty.Register("BlurAmount", typeof(double), typeof(ColorKeyAlphaEffect),
                new UIPropertyMetadata(0D, PixelShaderConstantCallback(1)));

        #endregion

        private const string EffectsScope = "effects";
        private const string EffectKey = "color_key_alpha_effect.ps";
    }
}
