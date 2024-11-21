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

#pragma warning disable CA1401 // P/Invokes should not be visible

using System.Runtime.InteropServices;

namespace Kaspirin.UI.Framework.NativeMethods.Api.IeFrame
{
    /// <summary>
    ///     Provides API methods for functions from ieframe.dll .
    /// </summary>
    public static class IeFrameDll
    {
        #region iepmapi.h

        /// <summary>
        ///     The IEIsProtectedModeURL API method. <br /><seealso href="https://learn.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/platform-apis/aa767961(v=vs.85)">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = false, CharSet = CharSet.Unicode)]
        public static extern int IEIsProtectedModeURL(
            string url);

        /// <summary>
        ///     The IEIsProtectedModeProcess API method. <br /><seealso href="https://learn.microsoft.com/en-us/previous-versions/windows/internet-explorer/ie-developer/platform-apis/ms537316(v=vs.85)">Learn more</seealso>.
        /// </summary>
        [DllImport(DllName, SetLastError = false)]
        public static extern int IEIsProtectedModeProcess(
            ref bool result);

        #endregion

        private const string DllName = "ieframe.dll";
    }
}
