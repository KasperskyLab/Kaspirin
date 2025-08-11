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


using System;

namespace Kaspirin.UI.Framework.NativeMethods.Api.User32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-flashwinfo">Learn more</seealso>.
/// </summary>
[Flags]
public enum FlashWindowFlags : uint
{
    /// <summary>
    ///     The FLASHW_STOP constant.
    /// </summary>
    Stop = 0x00000000,

    /// <summary>
    ///     The FLASHW_CAPTION constant.
    /// </summary>
    Caption = 0x00000001,

    /// <summary>
    ///     The FLASHW_TRAY constant.
    /// </summary>
    Tray = 0x00000002,

    /// <summary>
    ///     The FLASHW_ALL constant.
    /// </summary>
    All = 0x00000003,

    /// <summary>
    ///     The FLASHW_TIMER constant.
    /// </summary>
    Timer = 0x00000004,

    /// <summary>
    ///     The FLASHW_TIMERNOFG constant.
    /// </summary>
    TimerNoFG = 0x0000000C,
}