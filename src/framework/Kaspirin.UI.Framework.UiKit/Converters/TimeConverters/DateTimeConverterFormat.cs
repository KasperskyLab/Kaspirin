// Copyright © 2025 AO Kaspersky Lab.
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

namespace Kaspirin.UI.Framework.UiKit.Converters.TimeConverters;

public enum DateTimeConverterFormat
{
    /// <summary>
    ///     Standard short date format.
    ///     <br/> Corresponds to the "d" standard date time pattern.
    ///     <para/> Examples:
    ///     <br/> • (en-US) 6/15/2024
    ///     <br/> • (ja-JP) 2024/06/15
    ///     <br/> • (pt-PT) 15/06/2024
    ///     <br/> • (ru-RU) 15.06.2024
    /// </summary>
    /// <remarks>
    ///     <see cref="LocalizationManager.FormatCulture"/> is used as IFormatProvider.
    /// </remarks>
    Date,

    /// <summary>
    ///     Standard short time format.
    ///     <br/> Corresponds to the "t" standard date time pattern.
    ///     <para/> Examples:
    ///     <br/> • (en-US) 1:45 PM
    ///     <br/> • (ja-JP) 13:45
    ///     <br/> • (pt-PT) 13:45
    ///     <br/> • (ru-RU) 13:45
    /// </summary>
    /// <remarks>
    ///     <see cref="LocalizationManager.FormatCulture"/> is used as IFormatProvider.
    /// </remarks>
    ShortTime,

    /// <summary>
    ///     Standard long time format.
    ///     <br/> Corresponds to the "T" standard date time pattern.
    ///     <para/> Examples:
    ///     <br/> • (en-US) 1:45:30 PM
    ///     <br/> • (ja-JP) 13:45:30
    ///     <br/> • (pt-PT) 13:45:30
    ///     <br/> • (ru-RU) 13:45:30
    /// </summary>
    /// <remarks>
    ///     <see cref="LocalizationManager.FormatCulture"/> is used as IFormatProvider.
    /// </remarks>
    LongTime,

    /// <summary>
    ///     Standard general date/time format (short time).
    ///     <br/> Corresponds to the "g" standard date time pattern.
    ///     <para/> Examples:
    ///     <br/> • (en-US) 6/15/2024 1:45 PM
    ///     <br/> • (ja-JP) 2024/06/15 13:45
    ///     <br/> • (pt-PT) 15/06/2024 13:45
    ///     <br/> • (ru-RU) 15.06.2024 13:45
    /// </summary>
    /// <remarks>
    ///     <see cref="LocalizationManager.FormatCulture"/> is used as IFormatProvider.
    /// </remarks>
    DateShortTime,

    /// <summary>
    ///     Standard general date/time format (long time).
    ///     <br/> Corresponds to the "G" standard date time pattern.
    ///     <para/> Examples:
    ///     <br/> • (en-US) 6/15/2024 1:45:30 PM
    ///     <br/> • (ja-JP) 2024/06/15 13:45:30
    ///     <br/> • (pt-PT) 15/06/2024 13:45:30
    ///     <br/> • (ru-RU) 15.06.2024 13:45:30
    /// </summary>
    /// <remarks>
    ///     <see cref="LocalizationManager.FormatCulture"/> is used as IFormatProvider.
    /// </remarks>
    DateLongTime,
}
