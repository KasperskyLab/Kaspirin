// Copyright © 2025 AO Kaspersky Lab.
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
using System.Windows.Interop;
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.Threading
{
    /// <summary>
    ///     Implements a mechanism for asynchronous delegate execution in the UI thread, with the execution
    ///     of the current stack suspended.
    /// </summary>
    public sealed class DispatcherFrameAction
    {
        /// <summary>
        ///     Asynchronously executes the delegate <paramref name="action" /> in the UI thread. In this case,
        ///     the execution of the current stack is suspended until the <see cref="DispatcherFrameContext.CloseFrame" /> method is called.
        /// </summary>
        /// <param name="action">
        ///     A delegate to execute.
        /// </param>
        /// <param name="priority">
        ///     Priority of execution.
        /// </param>
        /// <remarks>
        ///     This method does not block the UI thread and must be called in the UI thread.
        /// </remarks>
        public static void Run(Action<DispatcherFrameContext> action, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            new DispatcherFrameAction(action, priority).Execute();
        }

        private DispatcherFrameAction(Action<DispatcherFrameContext> action, DispatcherPriority priority = DispatcherPriority.Normal)
        {
            _action = Guard.EnsureArgumentIsNotNull(action);
            _priority = priority;
            _tracer = ComponentTracer.Get(ComponentTracers.Threading, this);
        }

        private void Execute()
        {
            var executer = Executers.Implementation;

            Guard.Assert(executer.IsAvailable);
            Guard.Assert(executer.IsUiThread);

            ComponentDispatcher.PushModal();
            _tracer.TraceMethodDebug("Modal state entered.");

            try
            {
                var dispatcherFrame = new DispatcherFrame();
                var dispatchedFrameContext = new DispatcherFrameContext(dispatcherFrame);

                void DispatcherCallback() => _action(dispatchedFrameContext);

                executer.ExecuteInUiThreadAsync(DispatcherCallback, _priority);

                Dispatcher.PushFrame(dispatcherFrame);
            }
            finally
            {
                ComponentDispatcher.PopModal();
                _tracer.TraceMethodDebug("Modal state exited.");
            }
        }

        private readonly ComponentTracer _tracer;
        private readonly Action<DispatcherFrameContext> _action;
        private readonly DispatcherPriority _priority;
    }
}