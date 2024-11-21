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

#pragma warning disable CA1416 // This call site is reachable on all platforms

using System;

#if WINDOWS10_0_17763_0_OR_GREATER
using Windows.System;
#endif

namespace Kaspirin.UI.Framework.WinRT
{
    /// <summary>
    ///     Provides an implementation of <see cref="IWinRTLauncher" />.
    /// </summary>
    /// <remarks>
    ///     It is supported only on Windows 10 RS5 and above.
    /// </remarks>
    public sealed class WinRTLauncher : IWinRTLauncher
    {
        /// <inheritdoc cref="IWinRTLauncher.LaunchUriAsync" />
        public void LaunchUriAsync(Uri uri)
        {
#if WINDOWS10_0_17763_0_OR_GREATER
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed.
            Launcher.LaunchUriAsync(uri);
#pragma warning restore CS4014
#endif
        }
    }
}

