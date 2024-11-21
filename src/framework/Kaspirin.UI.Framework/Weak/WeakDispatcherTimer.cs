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
using System.Windows;
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.Weak
{
    /// <summary>
    ///     Provides methods for creating a timer <see cref="DispatcherTimer" />, which stores a weak reference
    ///     to the delegate executed by the timer <see cref="EventHandler" />.
    /// </summary>
    public static class WeakDispatcherTimer
    {
        /// <summary>
        ///     Creates a timer <see cref="DispatcherTimer" /> that stores a weak reference to the delegate
        ///     being executed <paramref name="OnTimer" />.
        /// </summary>
        /// <param name="onTimer">
        ///     The delegate executed by the timer event <see cref="DispatcherTimer.Tick" />.
        /// </param>
        /// <param name="interval">
        ///     The timer interval.
        /// </param>
        /// <param name="dispatcherPriority">
        ///     The priority with which the timer is running.
        /// </param>
        /// <remarks>
        ///     The <paramref name="OnTimer" /> delegate is executed in the UI thread.
        /// </remarks>
        /// <returns>
        ///     An instance of <see cref="DispatcherTimer" />.
        /// </returns>
        public static DispatcherTimer Create(
            EventHandler onTimer,
            TimeSpan interval,
            DispatcherPriority dispatcherPriority = DispatcherPriority.Background)
        {
            var application = Guard.EnsureIsNotNull(Application.Current);

            var timer = new DispatcherTimer(dispatcherPriority, application.Dispatcher)
            {
                Interval = interval
            };

            timer.Tick += WeakEventHandler.Wrap(
                onTimer,
                eh =>
                {
                    timer.Tick -= eh;
                    timer.Stop();
                });

            return timer;
        }

        /// <summary>
        ///     Creates a timer <see cref="DispatcherTimer" /> that stores a weak reference to the executed
        ///     delegate <paramref name="OnTimer" /> and starts it.
        /// </summary>
        /// <param name="onTimer">
        ///     The delegate executed by the timer event <see cref="DispatcherTimer.Tick" />.
        /// </param>
        /// <param name="interval">
        ///     The timer interval.
        /// </param>
        /// <param name="dispatcherPriority">
        ///     The priority with which the timer is running.
        /// </param>
        /// <remarks>
        ///     The <paramref name="OnTimer" /> delegate is executed in the UI thread.
        /// </remarks>
        /// <returns>
        ///     An instance of <see cref="DispatcherTimer" />.
        /// </returns>
        public static DispatcherTimer CreateAndStart(
            EventHandler onTimer,
            TimeSpan interval,
            DispatcherPriority dispatcherPriority = DispatcherPriority.Background)
        {
            var timer = Create(onTimer, interval, dispatcherPriority);
            timer.Start();

            return timer;
        }
    }
}
