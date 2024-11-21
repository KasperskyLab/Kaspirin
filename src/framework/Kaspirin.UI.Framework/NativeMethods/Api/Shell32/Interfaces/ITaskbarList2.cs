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
using System.Runtime.InteropServices;

namespace Kaspirin.UI.Framework.NativeMethods.Api.Shell32.Interfaces
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shobjidl_core/nn-shobjidl_core-itaskbarlist2">Learn more</seealso>.
    /// </summary>
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid(ShlGuids.IidITaskbarList2)]
    public interface ITaskbarList2 : ITaskbarList
    {
        #region ITaskbarList redeclaration

        new void HrInit();

        new void AddTab(
            IntPtr hwnd);

        new void DeleteTab(
            IntPtr hwnd);

        new void ActivateTab(
            IntPtr hwnd);

        new void SetActiveAlt(
            IntPtr hwnd);

        #endregion

        void MarkFullscreenWindow(
            IntPtr hwnd,
            [MarshalAs(UnmanagedType.Bool)] bool fFullscreen);
    }
}
