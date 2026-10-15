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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Kernel32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winnls/ne-winnls-sysgeotype">Learn more</seealso>.
/// </summary>
public enum SysGeoType : ushort
{

    /// <summary>
    ///     The GEO_NATION constant.
    /// </summary>
    Nation = 0x0001,

    /// <summary>
    ///     The GEO_LATITUDE constant.
    /// </summary>
    Latitude = 0x0002,

    /// <summary>
    ///     The GEO_LONGITUDE constant.
    /// </summary>
    Longitude = 0x0003,

    /// <summary>
    ///     The GEO_ISO2 constant.
    /// </summary>
    Iso2 = 0x0004,

    /// <summary>
    ///     The GEO_ISO3 constant.
    /// </summary>
    Iso3 = 0x0005,

    /// <summary>
    ///     The GEO_RFC1766 constant.
    /// </summary>
    Rfc1766 = 0x0006,

    /// <summary>
    ///     The GEO_LCID constant.
    /// </summary>
    Lcid = 0x0007,

    /// <summary>
    ///     The GEO_FRIENDLYNAME constant.
    /// </summary>
    FriendlyName = 0x0008,

    /// <summary>
    ///     The GEO_OFFICIALNAME constant.
    /// </summary>
    OfficialName = 0x0009,

    /// <summary>
    ///     The GEO_TIMEZONES constant.
    /// </summary>
    TimeZones = 0x000A,

    /// <summary>
    ///     The GEO_OFFICIALLANGUAGES constant.
    /// </summary>
    OfficialLanguages = 0x000B,

    /// <summary>
    ///     The GEO_ISO_UN_NUMBER constant.
    /// </summary>
    IsoUnNumber = 0x000C,

    /// <summary>
    ///     The GEO_PARENT constant.
    /// </summary>
    Parent = 0x000D,

    /// <summary>
    ///     The GEO_DIALINGCODE constant.
    /// </summary>
    DialingCode = 0x000E,

    /// <summary>
    ///     The GEO_CURRENCYCODE constant.
    /// </summary>
    CurrencyCode = 0x000F,

    /// <summary>
    ///     The GEO_CURRENCYSYMBOL constant.
    /// </summary>
    CurrencySymbol = 0x0010,

    /// <summary>
    ///     The GEO_NAME constant.
    /// </summary>
    Name = 0x0011,

    /// <summary>
    ///     The GEO_ID constant.
    /// </summary>
    Id = 0x0012,
}
