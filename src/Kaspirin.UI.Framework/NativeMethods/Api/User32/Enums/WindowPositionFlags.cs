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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos">Learn more</seealso>.
/// </summary>
[Flags]
public enum WindowPositionFlags : uint
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,

    /// <summary>
    ///     The SWP_NOSIZE constant.
    /// </summary>
    NoSize = 0x0001,

    /// <summary>
    ///     The SWP_NOMOVE constant.
    /// </summary>
    NoMove = 0x0002,

    /// <summary>
    ///     The SWP_NOZORDER constant.
    /// </summary>
    NoZOrder = 0x0004,

    /// <summary>
    ///     The SWP_NOREDRAW constant.
    /// </summary>
    NoRedraw = 0x0008,

    /// <summary>
    ///     The SWP_NOACTIVATE constant.
    /// </summary>
    NoActivate = 0x0010,

    /// <summary>
    ///     The SWP_DRAWFRAME constant.
    /// </summary>
    DrawFrame = 0x0020,

    /// <summary>
    ///     The SWP_HIDEWINDOW constant.
    /// </summary>
    HideWindow = 0x0080,

    /// <summary>
    ///     The SWP_SHOWWINDOW constant.
    /// </summary>
    ShowWindow = 0x0040,

    /// <summary>
    ///     The SWP_NOCOPYBITS constant.
    /// </summary>
    NoCopyBits = 0x0100,

    /// <summary>
    ///     The SWP_NOOWNERZORDER constant.
    /// </summary>
    NoOwnerZOrder = 0x0200,

    /// <summary>
    ///     The SWP_NOSENDCHANGING constant.
    /// </summary>
    NoSendChanging = 0x0400,

    /// <summary>
    ///     The SWP_DEFERERASE constant.
    /// </summary>
    DeferErase = 0x2000,

    /// <summary>
    ///     The SWP_ASYNCWINDOWPOS constant.
    /// </summary>
    AsyncWindowPos = 0x4000,
}