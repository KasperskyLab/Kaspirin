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

namespace Kaspirin.UI.Framework.UiKit.Fonts;

/// <summary>
///     The base class of markup extensions for setting a text brush by enumeration value <typeparamref name="TFontBrushEnum" />.
/// </summary>
/// <typeparam name="TFontBrushEnum">
///     The type of enumeration of text brushes.
/// </typeparam>
[MarkupExtensionReturnType(typeof(UIKitFontBrushSettings))]
public abstract class UIKitFontBrushExtension<TFontBrushEnum> : MarkupExtension
    where TFontBrushEnum : struct, Enum
{
    /// <summary>
    ///     ID of the text brush.
    /// </summary>
    public TFontBrushEnum Id { get; set; }

    /// <summary>
    ///     Returns the text brush settings <see cref="UIKitFontBrushSettings" />.
    /// </summary>
    /// <param name="serviceProvider">
    ///     Not used.
    /// </param>
    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        return new UIKitFontBrushSettings(Map(Id));
    }

    /// <summary>
    ///     Converts the enumeration value to the brush resource key.
    /// </summary>
    /// <param name="foregroundEnum">
    ///     The value being converted.
    /// </param>
    /// <returns>
    ///     Returns the key of the brush resource.
    /// </returns>
    protected abstract string Map(TFontBrushEnum foregroundEnum);
}
