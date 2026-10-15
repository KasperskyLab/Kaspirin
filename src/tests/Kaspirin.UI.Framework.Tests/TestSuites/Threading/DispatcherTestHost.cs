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
using System.Threading;
using WpfDispatcher = System.Windows.Threading.Dispatcher;

namespace Kaspirin.UI.Framework.Tests.TestSuites.Threading;

internal sealed class DispatcherTestHost : IDisposable
{
    public DispatcherTestHost()
    {
        using var dispatcherCreated = new ManualResetEvent(false);

        _thread = new Thread(() =>
        {
            _dispatcher = WpfDispatcher.CurrentDispatcher;
            dispatcherCreated.Set();

            WpfDispatcher.Run();
        })
        {
            IsBackground = true,
            Name = nameof(DispatcherTestHost),
        };

        _thread.Start();
        dispatcherCreated.WaitOne();
    }

    public WpfDispatcher Dispatcher => _dispatcher!;

    public void Dispose()
    {
        _dispatcher?.InvokeShutdown();
        _thread.Join(TimeSpan.FromSeconds(5));
    }

    private readonly Thread _thread;

    private WpfDispatcher? _dispatcher;
}
