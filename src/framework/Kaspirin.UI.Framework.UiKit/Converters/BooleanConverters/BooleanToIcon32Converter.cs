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

using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Converters.BooleanConverters
{
    /// <summary>
    ///     Provides an implementation of <see cref="BaseBooleanConverter{T}" /> in which the conversion
    ///     result is of type <see cref="UIKitIcon_32" />.
    /// </summary>
    /// <remarks>
    ///     By default, it converts <see langword="true" /> and <see langword="false" /> to <see cref="UIKitIcon_32.UIKitUnset" />.
    /// </remarks>
    [ValueConversion(typeof(bool), typeof(UIKitIcon_32))]
    public sealed class BooleanToIcon32Converter : BaseBooleanConverter<UIKitIcon_32>
    {
        /// <summary>
        ///     Creates an object <see cref="BooleanToIcon32Converter" />.
        /// </summary>
        public BooleanToIcon32Converter() : base(UIKitIcon_32.UIKitUnset, UIKitIcon_32.UIKitUnset) { }
    }
}
