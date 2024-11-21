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
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.Threading
{
    /// <summary>
    ///     An interface for executing delegates in the UI thread.
    /// </summary>
    public interface IUiThreadExecutor
    {
        /// <summary>
        ///     The <paramref name="action" /> delegate executes synchronously in the UI thread.
        /// </summary>
        /// <param name="action">
        ///     A delegate to execute.
        /// </param>
        /// <param name="priority">
        ///     Priority of execution.
        /// </param>
        void ExecuteInUiThreadSync(Action action, DispatcherPriority priority = DispatcherPriority.Normal);

        /// <summary>
        ///     Synchronously executes the <paramref name="action" /> delegate in the UI thread and returns the result.
        /// </summary>
        /// <param name="action">
        ///     A delegate to execute.
        /// </param>
        /// <param name="priority">
        ///     Priority of execution.
        /// </param>
        /// <returns>
        ///     The result of the delegate execution.
        /// </returns>
        TResult ExecuteInUiThreadSync<TResult>(Func<TResult> action, DispatcherPriority priority = DispatcherPriority.Normal);

        /// <summary>
        ///     Asynchronously executes the <paramref name="action" /> delegate in the UI thread.
        /// </summary>
        /// <param name="action">
        ///     A delegate to execute.
        /// </param>
        /// <param name="priority">
        ///     Priority of execution.
        /// </param>
        /// <returns>
        ///     The task in which the delegate will be executed.
        /// </returns>
        Task ExecuteInUiThreadAsync(Action action, DispatcherPriority priority = DispatcherPriority.Normal);
    }
}
