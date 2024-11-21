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
#pragma warning disable KCAIDE0002 // Enum has incorrect suffix

using System;

namespace Kaspirin.UI.Framework.NativeMethods.Api.Comctl32.Enums
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/controls/imagelistdrawflags">Learn more</seealso>.
    /// </summary>
    [Flags]
    public enum ImageListDrawFlags : uint
    {
        ILD_NORMAL = 0x000000000,
        ILD_TRANSPARENT = 0x000000001,
        ILD_BLEND25 = 0x000000002,
        ILD_FOCUS = ILD_BLEND25,
        ILD_BLEND50 = 0x000000004,
        ILD_SELECTED = ILD_BLEND50,
        ILD_BLEND = ILD_BLEND50,
        ILD_MASK = 0x0000000010,
        ILD_IMAGE = 0x0000000020,
        ILD_ROP = 0x0000000040,
        ILD_OVERLAYMASK = 0x0000000F00,
        ILD_PRESERVEALPHA = 0x0000001000,
        ILD_SCALE = 0x0000002000,
        ILD_DPISCALE = 0x0000004000,
        ILD_ASYNC = 0x0000008000
    }
}
