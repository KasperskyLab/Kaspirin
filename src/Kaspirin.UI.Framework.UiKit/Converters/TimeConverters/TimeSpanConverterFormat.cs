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

public enum TimeSpanConverterFormat
{
    /// <summary>
    ///     Custom format with the following templates:
    ///     <br /> • 15 seconds - if the value is less than 1 minute,
    ///     <br /> • 30 minutes 15 seconds - if the value is less than 1 hour,
    ///     <br /> • 12 hours 30 minutes - if the value is less than 1 day,
    ///     <br /> • 2 days 12 hours - in the rest cases.
    /// </summary>
    /// <remarks>
    ///     The format is localized, as the templates used are defined in the resource file. UiKit.lt .
    /// </remarks>
    TimePassed,

    /// <summary>
    ///     Custom format with the following templates:
    ///     <br /> • Less than a minute left - if the value is less than 1 minute,
    ///     <br /> • About 30 minutes left - if the value is less than 1 hour,
    ///     <br /> • About 12 hours left - if the value is less than 1 day,
    ///     <br /> • About 2 days left - in all other cases.
    /// </summary>
    /// <remarks>
    ///     The format is localized, as the templates used are defined in the resource file. UiKit.lt .
    /// </remarks>
    TimeLeft,

    /// <summary>
    ///     The standard short time format.
    ///     <br /> Corresponds to the standard time pattern "t". Example for en-US:
    ///     <para />1:45 PM.
    /// </summary>
    /// <remarks>
    ///     
    /// <see cref="LocalizationManager.FormatCulture" /> is used as the IFormatProvider.
    /// </remarks>
    ShortTime,

    /// <summary>
    ///     Custom format with the "h:mm" template. Example for en-US:
    ///     <para />7:12.
    /// </summary>
    /// <remarks>
    ///     The format is localized, as the templates used are defined in the resource file. UiKit.lt .
    /// </remarks>
    TimeSpan,

    /// <summary>
    ///     Custom format with the "mm:ss" template. Example for en-US:
    ///     <para />08:36.
    /// </summary>
    /// <remarks>
    ///     The format is localized, as the templates used are defined in the resource file. UiKit.lt .
    /// </remarks>
    TimeSpanSeconds,

    /// <summary>
    ///     Custom format with the following templates:
    ///     <br /> • m minutes - if the value is less than 1 hour,
    ///     <br /> • h hours - otherwise.
    /// </summary>
    /// <remarks>
    ///     The format is localized, as the templates used are defined in the resource file. UiKit.lt .
    /// </remarks>
    ShortTimePast
}
