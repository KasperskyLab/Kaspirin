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
using Kaspirin.UI.Framework.Tests.TestHelpers;
using Kaspirin.UI.Framework.Threading.Timers.Internals;
using ITimer = Kaspirin.UI.Framework.Threading.Timers.ITimer;

namespace Kaspirin.UI.Framework.Tests.TestSuites.Threading;

[TestClass]
public sealed class DispatcherServiceTimerTests : BaseServiceTimerTests
{
    [TestInitialize]
    public void InitializeTests()
    {
        _host = new DispatcherTestHost();
        _previousFactory = TimerFactory.Implementation;
        TimerFactory.Implementation = new TestTimerFactory(_host.Dispatcher);
    }

    [TestCleanup]
    public void CleanupTests()
    {
        if (_previousFactory != null)
        {
            TimerFactory.Implementation = _previousFactory;
        }

        _host?.Dispose();
    }

    protected override ITimer CreateServiceTimer(object owner, Action tickAction, MutableParameter<TimeSpan> interval)
        => new DispatcherServiceTimer(owner, tickAction, interval);

    private DispatcherTestHost? _host;
    private ITimerFactory? _previousFactory;
}
