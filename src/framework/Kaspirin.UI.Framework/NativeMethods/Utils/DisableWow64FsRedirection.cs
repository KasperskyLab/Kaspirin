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

namespace Kaspirin.UI.Framework.NativeMethods.Utils
{
    /// <summary>
    ///     Allows you to disable the WOW64 forwarding mechanism for this process.
    /// </summary>
    public sealed class DisableWow64FsRedirection : IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DisableWow64FsRedirection" /> class.
        /// </summary>
        /// <param name="isRedirectionDisabled">
        ///     The initial value of the <see cref="IsRedirectionDisabled" /> property.
        /// </param>
        public DisableWow64FsRedirection(bool isRedirectionDisabled = true)
        {
            IsRedirectionDisabled = isRedirectionDisabled;
        }

        /// <summary>
        ///     Specifies whether to disable the WOW64 forwarding mechanism.
        /// </summary>
        public bool IsRedirectionDisabled
        {
            get => _isRedirectionDisabled;
            set
            {
                if (!Environment.Is64BitOperatingSystem)
                {
                    return;
                }

                if (_isRedirectionDisabled == value)
                {
                    return;
                }

                _isRedirectionDisabled = value;

                if (_isRedirectionDisabled)
                {
                    Kernel32Dll.Wow64DisableWow64FsRedirection(ref _wow64Value);
                }
                else
                {
                    Kernel32Dll.Wow64RevertWow64FsRedirection(_wow64Value);
                }
            }
        }

        /// <summary>
        ///     Enables the operation of the WOW64 forwarding mechanism, if it has been disabled.
        /// </summary>
        public void Dispose()
        {
            IsRedirectionDisabled = false;
        }

        private IntPtr _wow64Value = IntPtr.Zero;
        private bool _isRedirectionDisabled;
    }
}
