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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Comctl32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/controls/imagelistdrawflags">Learn more</seealso>.
/// </summary>
[Flags]
public enum ImageListDrawFlags : uint
{
    /// <summary>
    ///     The ILD_NORMAL constant.
    /// </summary>
    Normal = 0x000000000,

    /// <summary>
    ///     The ILD_TRANSPARENT constant.
    /// </summary>
    Transparent = 0x000000001,

    /// <summary>
    ///     The ILD_BLEND25 constant.
    /// </summary>
    Blend25 = 0x000000002,

    /// <summary>
    ///     The ILD_FOCUS constant.
    /// </summary>
    Focus = Blend25,

    /// <summary>
    ///     The ILD_BLEND50 constant.
    /// </summary>
    Blend50 = 0x000000004,

    /// <summary>
    ///     The ILD_SELECTED constant.
    /// </summary>
    Selected = Blend50,

    /// <summary>
    ///     The ILD_BLEND constant.
    /// </summary>
    Blend = Blend50,

    /// <summary>
    ///     The ILD_MASK constant.
    /// </summary>
    Mask = 0x0000000010,

    /// <summary>
    ///     The ILD_IMAGE constant.
    /// </summary>
    Image = 0x0000000020,

    /// <summary>
    ///     The ILD_ROP constant.
    /// </summary>
    Rop = 0x0000000040,

    /// <summary>
    ///     The ILD_OVERLAYMASK constant.
    /// </summary>
    OverlayMask = 0x0000000F00,

    /// <summary>
    ///     The ILD_PRESERVEALPHA constant.
    /// </summary>
    PreserveAlpha = 0x0000001000,

    /// <summary>
    ///     The ILD_SCALE constant.
    /// </summary>
    Scale = 0x0000002000,

    /// <summary>
    ///     The ILD_DPISCALE constant.
    /// </summary>
    DpiScale = 0x0000004000,

    /// <summary>
    ///     The ILD_ASYNC constant.
    /// </summary>
    Async = 0x0000008000
}
