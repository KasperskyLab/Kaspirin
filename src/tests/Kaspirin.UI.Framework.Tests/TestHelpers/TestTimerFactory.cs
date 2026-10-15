// Copyright © 2026 AO Kaspersky Lab.
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
using Kaspirin.UI.Framework.Threading.Timers.Internals;
using ITimer = Kaspirin.UI.Framework.Threading.Timers.ITimer;

namespace Kaspirin.UI.Framework.Tests.TestHelpers;

internal sealed class TestTimerFactory : ITimerFactory
{
    public TestTimerFactory(Dispatcher dispatcher)
        => _dispatcher = dispatcher;

    public ITimer CreateOnTp(Action? onTimer, TimeSpan? interval, string name, TimerOptions options)
        => new ThreadPoolUnifiedTimer(onTimer, interval, name, options);

    public ITimer CreateOnUi(Action? onTimer, TimeSpan? interval, string name, TimerOptions options, DispatcherPriority priority)
        => new DispatcherUnifiedTimer(onTimer, interval, name, options, _dispatcher, priority);

    private readonly Dispatcher _dispatcher;
}
