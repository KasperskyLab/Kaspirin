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

#pragma warning disable  CA1416 // This call site is reachable on all platforms

using System;
using System.Globalization;
using System.Threading;

namespace Kaspirin.UI.Framework.Threading
{
    /// <summary>
    ///     Starts the main thread of the WPF application (UI thread).
    /// </summary>
    public sealed class WpfThread
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WpfThread" /> class.
        /// </summary>
        /// <param name="startAction">
        ///     A delegate to start the UI thread.
        /// </param>
        /// <param name="threadCulture">
        ///     Passed to <see cref="Thread.CurrentCulture" /> for the UI stream being created.
        /// </param>
        /// <param name="threadUICulture">
        ///     Passed to <see cref="Thread.CurrentUICulture" /> for the UI stream being created.
        /// </param>
        /// <param name="threadName">
        ///     Passed to <see cref="Thread.Name" /> for the UI stream being created.
        /// </param>
        public WpfThread(
            Action startAction,
            CultureInfo threadCulture,
            CultureInfo threadUICulture,
            string threadName = "WPF")
        {
            Guard.ArgumentIsNotNull(startAction);
            Guard.ArgumentIsNotNull(threadCulture);
            Guard.ArgumentIsNotNull(threadUICulture);

            _wpfThread = new Thread(() => startAction())
            {
                Name = threadName,
                CurrentCulture = threadCulture,
                CurrentUICulture = threadUICulture
            };

            _wpfThread.SetApartmentState(ApartmentState.STA);

            Guard.SetUiThreadId(_wpfThread.ManagedThreadId);
        }

        /// <summary>
        ///     Starts the UI thread.
        /// </summary>
        public void Start()
            => _wpfThread.Start();

        /// <summary>
        ///     Blocks the calling thread until the UI thread is completed.
        /// </summary>
        public void Join()
            => _wpfThread.Join();

        private readonly Thread _wpfThread;
    }
}