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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.Tests.TestHelpers;

internal sealed class TestExecutor : IDispatcherExecutor
{
    public bool IsAvailable => true;
    public bool IsDispatcherThread { get; set; } = true;
    public bool ActionRaisedAsync { get; private set; }
    public bool ActionRaisedSync { get; private set; }

    public event EventHandler IsAvailableChanged = EmptyDelegate.ForEventHandler();

    public void ExecuteSync(Action action, DispatcherPriority priority, CancellationToken cancellationToken)
    {
        ActionRaisedSync = true;

        action();
    }

    public Task ExecuteAsync(Action action, DispatcherPriority priority, CancellationToken cancellationToken)
    {
        ActionRaisedAsync = true;

        return Task.Run(action);
    }

    public TResult ExecuteSync<TResult>(Func<TResult> action, DispatcherPriority priority, CancellationToken cancellationToken)
    {
        ActionRaisedSync = true;

        return action();
    }

    public Task<TResult> ExecuteAsync<TResult>(Func<TResult> action, DispatcherPriority priority, CancellationToken cancellationToken)
    {
        ActionRaisedAsync = true;

        return Task.Run(action);
    }

    public Task ExecuteAsyncWithDelay(Action action, TimeSpan delay, DispatcherPriority priority, CancellationToken cancellationToken)
    {
        ActionRaisedAsync = true;

        return Task.Run(action);
    }

    public Task<TResult> ExecuteAsyncWithDelay<TResult>(Func<TResult> action, TimeSpan delay, DispatcherPriority priority, CancellationToken cancellationToken)
    {
        ActionRaisedAsync = true;

        return Task.Run(action);
    }

    public Task ExecuteAsyncWithDelay(Action action, TimeSpan delay, CancellationToken cancellationToken)
    {
        ActionRaisedAsync = true;

        return Task.Run(action);
    }

    public Task<TResult> ExecuteAsyncWithDelay<TResult>(Func<TResult> action, TimeSpan delay, CancellationToken cancellationToken)
    {
        ActionRaisedAsync = true;

        return Task.Run(action);
    }

    public void ExecuteSync(Action action, CancellationToken cancellationToken)
        => ExecuteSync(action, DispatcherPriority.Normal, cancellationToken);

    public TResult ExecuteSync<TResult>(Func<TResult> action, CancellationToken cancellationToken)
        => ExecuteSync(action, DispatcherPriority.Normal, cancellationToken);

    public Task ExecuteAsync(Action action, CancellationToken cancellationToken)
        => ExecuteAsync(action, DispatcherPriority.Normal, cancellationToken);

    public Task<TResult> ExecuteAsync<TResult>(Func<TResult> action, CancellationToken cancellationToken)
        => ExecuteAsync(action, DispatcherPriority.Normal, cancellationToken);

    public bool VerifyThread()
        => IsDispatcherThread;
}
