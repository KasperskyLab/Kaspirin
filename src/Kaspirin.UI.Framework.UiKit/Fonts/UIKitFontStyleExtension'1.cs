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
///     The base class of markup extensions for setting the font style by enumeration value <typeparamref name="TFontStyleEnum" />.
/// </summary>
/// <typeparam name="TFontStyleEnum">
///     The type of font styles enumeration.
/// </typeparam>
[MarkupExtensionReturnType(typeof(UIKitFontStyleSettings))]
public abstract class UIKitFontStyleExtension<TFontStyleEnum> : MarkupExtension
    where TFontStyleEnum : struct, Enum
{
    /// <summary>
    ///     ID of the font style.
    /// </summary>
    public TFontStyleEnum Id { get; set; }

    /// <summary>
    ///     Returns font style settings <see cref="UIKitFontStyleSettings" />.
    /// </summary>
    /// <param name="serviceProvider">
    ///     Not used.
    /// </param>
    public override object? ProvideValue(IServiceProvider serviceProvider)
    {
        return new UIKitFontStyleSettings(Map(Id));
    }

    /// <summary>
    ///     Converts the enumeration value to the font style resource key.
    /// </summary>
    /// <param name="style">
    ///     The value being converted.
    /// </param>
    /// <returns>
    ///     Returns the key of the font style resource.
    /// </returns>
    protected abstract string Map(TFontStyleEnum style);
}
