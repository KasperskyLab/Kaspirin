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
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using Kaspirin.UI.Framework.Threading.Timers.Internals;

namespace Kaspirin.UI.Framework.Tests.Mocks;

internal sealed class MockTimerFactory : ITimerFactory
{
    public ITimer CreateOnTp(Action onTimer, TimeSpan? interval, string name, TimerOptions options)
    {
        var timer = new MockTimer(this, onTimer, interval, name, options);
        _timers.Add(timer);
        return timer;
    }

    public ITimer CreateOnUi(Action onTimer, TimeSpan? interval, string name, TimerOptions options, DispatcherPriority priority)
    {
        var timer = new MockTimer(this, onTimer, interval, name, options);
        _timers.Add(timer);
        return timer;
    }

    public void AdvanceTime(double millisecondsElapsed)
    {
        var changeDelta = 5;

        while (millisecondsElapsed > 0)
        {
            millisecondsElapsed -= changeDelta;

            CurrentTime += TimeSpan.FromMilliseconds(changeDelta).Ticks;
            foreach (var timer in _timers.ToList())
            {
                timer.ProcessTicks();
            }
        }
    }

    public long CurrentTime { get; private set; }

    private readonly List<MockTimer> _timers = new();

    private sealed class MockTimer : BaseTimer
    {
        private readonly MockTimerFactory _factory;
        private long _nextTickTime;
        private bool _isRunning;

        public MockTimer(MockTimerFactory factory, Action tickAction, TimeSpan? interval, string name, TimerOptions options)
            : base(tickAction, interval, name, options)
        {
            _factory = factory;
        }

        protected override void TimerStart(TimeSpan delay)
        {
            _nextTickTime = _factory.CurrentTime + delay.Ticks;
            _isRunning = true;
        }

        protected override void TimerStop()
        {
            _isRunning = false;
        }

        protected override void TimerDispose()
        {
            _factory._timers.Remove(this);
        }

        protected override void TimerResetInterval()
        {
            if (Interval.HasValue && _isRunning)
            {
                _nextTickTime = _factory.CurrentTime + Interval.Value.Ticks;
            }
        }

        internal void ProcessTicks()
        {
            if (!_isRunning)
            {
                return;
            }

            var currentTime = _factory.CurrentTime;
            if (currentTime < _nextTickTime)
            {
                return;
            }

            if (Interval.HasValue)
            {
                while (currentTime >= _nextTickTime)
                {
                    OnTimerTick();
                    _nextTickTime += Interval.Value.Ticks;
                }
            }
            else
            {
                OnTimerTick();
                _isRunning = false;
            }
        }
    }
}
