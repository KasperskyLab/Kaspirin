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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-mouseinput">Learn more</seealso>.
/// </summary>
[Flags]
public enum MouseInputFlags : uint
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,

    /// <summary>
    ///     The MOUSEEVENTF_MOVE constant.
    /// </summary>
    Move = 0x0001,

    /// <summary>
    ///     The MOUSEEVENTF_LEFTDOWN constant.
    /// </summary>
    LeftDown = 0x0002,

    /// <summary>
    ///     The MOUSEEVENTF_LEFTUP constant.
    /// </summary>
    LeftUp = 0x0004,

    /// <summary>
    ///     The MOUSEEVENTF_RIGHTDOWN constant.
    /// </summary>
    RightDown = 0x0008,

    /// <summary>
    ///     The MOUSEEVENTF_RIGHTUP constant.
    /// </summary>
    RightUp = 0x0010,

    /// <summary>
    ///     The MOUSEEVENTF_MIDDLEDOWN constant.
    /// </summary>
    MiddleDown = 0x0020,

    /// <summary>
    ///     The MOUSEEVENTF_MIDDLEUP constant.
    /// </summary>
    MiddleUp = 0x0040,

    /// <summary>
    ///     The MOUSEEVENTF_XDOWN constant.
    /// </summary>
    XDown = 0x0080,

    /// <summary>
    ///     The MOUSEEVENTF_XUP constant.
    /// </summary>
    Xup = 0x0100,

    /// <summary>
    ///     The MOUSEEVENTF_WHEEL constant.
    /// </summary>
    Wheel = 0x0800,

    /// <summary>
    ///     The MOUSEEVENTF_HWHEEL constant.
    /// </summary>
    HWheel = 0x1000,

    /// <summary>
    ///     The MOUSEEVENTF_MOVE_NOCOALESCE constant.
    /// </summary>
    MoveNoCoalesce = 0x2000,

    /// <summary>
    ///     The MOUSEEVENTF_VIRTUALDESK constant.
    /// </summary>
    VirtualDesk = 0x4000,

    /// <summary>
    ///     The MOUSEEVENTF_ABSOLUTE constant.
    /// </summary>
    Absolute = 0x8000,
}
