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

using System;

namespace Kaspirin.UI.Framework.NativeMethods.Api.User32.Constants
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowpos">Learn more</seealso>.
    /// </summary>
    public static class KnownHwnd
    {
        public static readonly IntPtr HWND_BOTTOM = new(1);
        public static readonly IntPtr HWND_NOTOPMOST = new(-2);
        public static readonly IntPtr HWND_TOP = new(0);
        public static readonly IntPtr HWND_TOPMOST = new(-1);
    }
}
