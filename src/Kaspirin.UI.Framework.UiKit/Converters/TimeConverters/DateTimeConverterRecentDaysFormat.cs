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

public enum DateTimeConverterRecentDaysFormat
{
    /// <summary>
    ///     There is no special formatting for recent days - today and yesterday are treated like all other dates.
    /// </summary>
    None,

    /// <summary>
    ///     Static values are "Today" or "Yesterday".
    /// </summary>
    /// <remarks>
    ///     The format is localized, as the values used are defined in the resource file. UiKit.lt .
    /// </remarks>
    Static,

    /// <summary>
    ///     "Today" or "Yesterday" + the actual value in the standard short date format.
    ///     <br /> Matches the standard date template "d". Example for en-US:
    ///     <para />Today, 6/15/2024.
    /// </summary>
    /// <remarks>
    ///     
    /// <see cref="LocalizationManager.FormatCulture" /> is used as the IFormatProvider.
    ///     <br /> The format is localized, as the templates used are defined in the resource file UiKit.lt .
    /// </remarks>
    Date,

    /// <summary>
    ///     "Today" or "Yesterday" + the actual value in the standard short time format.
    ///     <br /> Corresponds to the standard time pattern "t". Example for en-US:
    ///     <para />Today, 1:45 PM.
    /// </summary>
    /// <remarks>
    ///     
    /// <see cref="LocalizationManager.FormatCulture" /> is used as the IFormatProvider.
    ///     <br /> The format is localized, as the templates used are defined in the resource file UiKit.lt .
    /// </remarks>
    ShortTime,

    /// <summary>
    ///     "Today" or "Yesterday" + the actual value in the standard long time format.
    ///     <br /> Corresponds to the standard time pattern "T". Example for en-US:
    ///     <para />Today, 1:45:30 PM.
    /// </summary>
    /// <remarks>
    ///     
    /// <see cref="LocalizationManager.FormatCulture" /> is used as the IFormatProvider.
    ///     <br /> The format is localized, as the templates used are defined in the resource file UiKit.lt .
    /// </remarks>
    LongTime,

    /// <summary>
    ///     "Today" or "Yesterday" + the actual value in the standard common date/time format (short time).
    ///     <br /> Matches the standard date/time pattern "g". Example for en-US:
    ///     <para />Today, 6/15/2024 1:45 PM.
    /// </summary>
    /// <remarks>
    ///     
    /// <see cref="LocalizationManager.FormatCulture" /> is used as the IFormatProvider.
    ///     <br /> The format is localized, as the templates used are defined in the resource file UiKit.lt .
    /// </remarks>
    DateShortTime,

    /// <summary>
    ///     "Today" or "Yesterday" + the actual value in the standard common date/time format (long time).
    ///     <br /> Matches the standard date/time pattern "G". Example for en-US:
    ///     <para />Today, 6/15/2024 1:45:30 PM.
    /// </summary>
    /// <remarks>
    ///     
    /// <see cref="LocalizationManager.FormatCulture" /> is used as the IFormatProvider.
    ///     <br /> The format is localized, as the templates used are defined in the resource file UiKit.lt .
    /// </remarks>
    DateLongTime,
}
