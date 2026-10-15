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

namespace Kaspirin.UI.Framework.UiKit.Effects;

/// <summary>
///     A pixel shader effect that makes pixels of a given color transparent.
/// </summary>
public sealed class ColorKeyAlphaEffect : ShaderEffect
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ColorKeyAlphaEffect" /> class.
    /// </summary>
    public ColorKeyAlphaEffect()
    {
        var effectsLocalizer = LocalizationManager.GetLocalizer<IFileLocalizer>(EffectsScope);
        var effectUri = effectsLocalizer.GetUri(EffectKey);

        PixelShader = new PixelShader
        {
            UriSource = effectUri,
        };

        UpdateShaderValue(InputProperty);
        UpdateShaderValue(ColorKeyProperty);
        UpdateShaderValue(ToleranceProperty);

        Tolerance = 1;
    }

    #region Input

    /// <summary>
    ///     The input image to which the effect is applied.
    /// </summary>
    public Brush Input
    {
        get => (Brush)GetValue(InputProperty);
        set => SetValue(InputProperty, value);
    }

    /// <summary>
    ///     Identifies the dependency property <see cref="Input" />.
    /// </summary>
    public static readonly DependencyProperty InputProperty = RegisterPixelShaderSamplerProperty(
        nameof(Input),
        typeof(ColorKeyAlphaEffect),
        0);

    #endregion

    #region ColorKey

    /// <summary>
    ///     The color that becomes transparent.
    /// </summary>
    public Color ColorKey
    {
        get => (Color)GetValue(ColorKeyProperty);
        set => SetValue(ColorKeyProperty, value);
    }

    /// <summary>
    ///     Identifies the dependency property <see cref="ColorKey" />.
    /// </summary>
    public static readonly DependencyProperty ColorKeyProperty = DependencyProperty.Register(
        nameof(ColorKey),
        typeof(Color),
        typeof(ColorKeyAlphaEffect),
        new UIPropertyMetadata(Colors.White, PixelShaderConstantCallback(0)));

    #endregion

    #region Tolerance

    /// <summary>
    ///     Acceptable deviation of the pixel color from <see cref="ColorKey" />.
    /// </summary>
    public double Tolerance
    {
        get => (double)GetValue(ToleranceProperty);
        set => SetValue(ToleranceProperty, value);
    }

    /// <summary>
    ///     Identifies the dependency property <see cref="Tolerance" />.
    /// </summary>
    public static readonly DependencyProperty ToleranceProperty = DependencyProperty.Register(
        nameof(Tolerance),
        typeof(double),
        typeof(ColorKeyAlphaEffect),
        new UIPropertyMetadata(default(double), PixelShaderConstantCallback(1)));

    #endregion

    private const string EffectsScope = "effects";
    private const string EffectKey = "color_key_alpha_effect.ps";
}
