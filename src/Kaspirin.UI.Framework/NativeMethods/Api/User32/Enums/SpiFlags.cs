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

namespace Kaspirin.UI.Framework.NativeMethods.Api.User32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-systemparametersinfow">Learn more</seealso>.
/// </summary>
[Flags]
public enum SpiFlags : uint
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,

    /// <summary>
    ///     The SPIF_UPDATEINIFILE constant.
    /// </summary>
    UpdateIniFile = 0x01,

    /// <summary>
    ///     The SPIF_SENDCHANGE constant.
    /// </summary>
    SendChange = 0x02,

    /// <summary>
    ///     The SPIF_SENDWININICHANGE constant.
    /// </summary>
    SendWinIniChange = 0x02,
}
