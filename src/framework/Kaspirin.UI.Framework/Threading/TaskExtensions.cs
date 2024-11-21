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
    ///     Extension methods for <see cref="Task" />.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        ///     Schedules the execution of the delegate <paramref name="continuation" /> in the UI thread as
        ///     a continuation of the task <paramref name="task" />.
        /// </summary>
        /// <typeparam name="TResult">
        ///     The type of the value returned by the task.
        /// </typeparam>
        /// <param name="task">
        ///     A task.
        /// </param>
        /// <param name="continuation">
        ///     A delegate to execute.
        /// </param>
        /// <param name="priority">
        ///     Priority of execution.
        /// </param>
        /// <param name="onlyOnRanToCompletion">
        ///     Specifies that the <paramref name="continuation" /> delegate will be executed only after the
        ///     successful completion of the task.
        /// </param>
        /// <returns>
        ///     Continuation of the task <paramref name="task" />.
        /// </returns>
        public static Task ContinueOnUi<TResult>(
            this Task<TResult> task,
            Action continuation,
            DispatcherPriority priority = DispatcherPriority.Normal,
            bool onlyOnRanToCompletion = true)
            => task.ContinueOnUi(_ => continuation(), priority, onlyOnRanToCompletion);

        /// <summary>
        ///     Schedules the execution of the delegate <paramref name="continuation" /> in the UI thread as
        ///     a continuation of the task <paramref name="task" />.
        /// </summary>
        /// <param name="task">
        ///     A task.
        /// </param>
        /// <param name="continuation">
        ///     A delegate to execute.
        /// </param>
        /// <param name="priority">
        ///     Priority of execution.
        /// </param>
        /// <param name="onlyOnRanToCompletion">
        ///     Specifies that the <paramref name="continuation" /> delegate will be executed only after the
        ///     successful completion of the task.
        /// </param>
        /// <returns>
        ///     Continuation of the task <paramref name="task" />.
        /// </returns>
        public static Task ContinueOnUi(
            this Task task,
            Action continuation,
            DispatcherPriority priority = DispatcherPriority.Normal,
            bool onlyOnRanToCompletion = true)
        {
            Guard.ArgumentIsNotNull(task);
            Guard.ArgumentIsNotNull(continuation);

            return task.ContinueWith(
                parentTask => continuation.InUiAsync(priority),
                (onlyOnRanToCompletion ? TaskContinuationOptions.OnlyOnRanToCompletion : 0) | TaskContinuationOptions.ExecuteSynchronously);
        }

        /// <summary>
        ///     Schedules the execution of the delegate <paramref name="continuation" /> in the UI thread as
        ///     a continuation of the task <paramref name="task" />. The result of the task is passed to <paramref name="continuation" />.
        /// </summary>
        /// <typeparam name="TResult">
        ///     The type of the value returned by the task.
        /// </typeparam>
        /// <param name="task">
        ///     A task.
        /// </param>
        /// <param name="continuation">
        ///     A delegate to execute.
        /// </param>
        /// <param name="priority">
        ///     Priority of execution.
        /// </param>
        /// <param name="onlyOnRanToCompletion">
        ///     Specifies that the <paramref name="continuation" /> delegate will be executed only after the
        ///     successful completion of the task.
        /// </param>
        /// <returns>
        ///     Continuation of the task <paramref name="task" />.
        /// </returns>
        public static Task ContinueOnUi<TResult>(
            this Task<TResult> task,
            Action<TResult> continuation,
            DispatcherPriority priority = DispatcherPriority.Normal,
            bool onlyOnRanToCompletion = true)
        {
            Guard.ArgumentIsNotNull(task);
            Guard.ArgumentIsNotNull(continuation);

            return task.ContinueWith(
                parentTask => Executers.InUiAsync(() => continuation(parentTask.Result), priority),
                (onlyOnRanToCompletion ? TaskContinuationOptions.OnlyOnRanToCompletion : 0) | TaskContinuationOptions.ExecuteSynchronously);
        }
    }
}
