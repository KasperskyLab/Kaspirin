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
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Kaspirin.UI.Framework.Threading
{
    /// <summary>
    ///     Implements the logic of sequential and asynchronous execution of transmitted delegates.
    /// </summary>
    public sealed class ActionQueue
    {
        /// <summary>
        ///     Adds a delegate to the execution queue.
        /// </summary>
        /// <param name="action">
        ///     A delegate to execute.
        /// </param>
        /// <remarks>
        ///     The delegate is executed asynchronously in a separate thread.
        /// </remarks>
        public void Enqueue(Action action)
        {
            Guard.ArgumentIsNotNull(action);

            lock (_syncRoot)
            {
                _actionQueue.Enqueue(action);

                if (_queueProcessor == null)
                {
                    StartProcessing();
                }
            }
        }

        private void StartProcessing()
        {
            _queueProcessor = Task.Factory.StartNew(ProcessQueue);
        }

        private void ProcessQueue()
        {
            while (_actionQueue.TryDequeue(out var action))
            {
                action();
            }

            lock (_syncRoot)
            {
                _queueProcessor = null;
                if (_actionQueue.Count > 0)
                {
                    StartProcessing();
                }
            }
        }

        private readonly object _syncRoot = new();
        private readonly ConcurrentQueue<Action> _actionQueue = new();
        private Task? _queueProcessor;
    }
}
