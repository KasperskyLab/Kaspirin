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
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.Threading
{
    /// <summary>
    ///     Performs an action in the UI thread once after the specified time interval.
    /// </summary>
    public sealed class DeferredAction
    {
        /// <summary>
        ///     Executes the delegate <paramref name="action" /> after a time interval <paramref name="delay" />.
        /// </summary>
        /// <param name="action">
        ///     The delegate being executed.
        /// </param>
        /// <param name="delay">
        ///     The time interval after which to execute <paramref name="action" />.
        /// </param>
        /// <remarks>
        ///     
        /// <paramref name="action" /> is executed in the UI thread.
        /// </remarks>
        /// <summary>
        ///     An instance of <see cref="DeferredAction" /> executing the delegate.
        /// </summary>
        public static DeferredAction Execute(Action action, TimeSpan delay)
        {
            var deferredAction = new DeferredAction(action, delay);
            deferredAction.Execute();

            return deferredAction;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DeferredAction" /> class.
        /// </summary>
        /// <param name="action">
        ///     The delegate being executed.
        /// </param>
        /// <remarks>
        ///     
        /// <paramref name="action" /> is executed in the UI thread.
        /// </remarks>
        public DeferredAction(Action action) : this(action, TimeSpan.Zero)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DeferredAction" /> class.
        /// </summary>
        /// <param name="action">
        ///     The delegate being executed.
        /// </param>
        /// <param name="delay">
        ///     The time interval after which to execute <paramref name="action" />.
        /// </param>
        /// <remarks>
        ///     
        /// <paramref name="action" /> is executed in the UI thread.
        /// </remarks>
        public DeferredAction(Action action, TimeSpan delay)
        {
            Guard.ArgumentIsNotNull(action);

            _action = action;
            _timer = WeakDispatcherTimer.Create(OnTimer, delay);
        }

        /// <summary>
        ///     Indicates whether the delayed execution timer is running.
        /// </summary>
        public bool IsEnabled => _timer.IsEnabled;

        /// <summary>
        ///     Starts the delayed execution timer.
        /// </summary>
        /// <remarks>
        ///     When the method is called again, the timer will be restarted.
        /// </remarks>
        public void Execute()
        {
            _timer.Stop();
            _timer.Start();
        }

        /// <summary>
        ///     Starts the delayed execution timer.
        /// </summary>
        /// <param name="delay">
        ///     The time interval after which the actions must be performed.
        /// </param>
        /// <remarks>
        ///     When the method is called again, the timer will be restarted.
        /// </remarks>
        public void Execute(TimeSpan delay)
        {
            _timer.Stop();
            _timer.Interval = delay;
            _timer.Start();
        }

        /// <summary>
        ///     Stops the delayed execution timer.
        /// </summary>
        public void Cancel()
        {
            _timer.Stop();
        }

        private void OnTimer(object? sender, EventArgs e)
        {
            _action();
            _timer.Stop();
        }

        private readonly Action _action;
        private readonly DispatcherTimer _timer;
    }
}
