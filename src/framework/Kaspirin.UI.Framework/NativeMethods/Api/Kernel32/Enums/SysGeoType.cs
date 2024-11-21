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

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Kaspirin.UI.Framework.NativeMethods.Api.Kernel32.Enums
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winnls/ne-winnls-sysgeotype">Learn more</seealso>.
    /// </summary>
    public enum SysGeoType : ushort
    {
        GEO_NATION = 0x0001,
        GEO_LATITUDE = 0x0002,
        GEO_LONGITUDE = 0x0003,
        GEO_ISO2 = 0x0004,
        GEO_ISO3 = 0x0005,
        GEO_RFC1766 = 0x0006,
        GEO_LCID = 0x0007,
        GEO_FRIENDLYNAME = 0x0008,
        GEO_OFFICIALNAME = 0x0009,
        GEO_TIMEZONES = 0x000A,
        GEO_OFFICIALLANGUAGES = 0x000B,
        GEO_ISO_UN_NUMBER = 0x000C,
        GEO_PARENT = 0x000D,
        GEO_DIALINGCODE = 0x000E,
        GEO_CURRENCYCODE = 0x000F,
        GEO_CURRENCYSYMBOL = 0x0010,
        GEO_NAME = 0x0011,
        GEO_ID = 0x0012,
    }
}
