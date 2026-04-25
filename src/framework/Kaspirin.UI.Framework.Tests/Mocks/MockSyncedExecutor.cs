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

namespace Kaspirin.UI.Framework.Tests.Mocks;

internal sealed class MockSyncedExecutor : IThreadPoolExecutor, IDispatcherExecutor
{
    public MockSyncedExecutor(ITimerFactory mockTimerFactory)
    {
        _timerFactory = mockTimerFactory;
    }

    public bool IsAvailable => true;

    public event EventHandler IsAvailableChanged = (o, e) => { };

    public Task ExecuteAsync(Action action, CancellationToken cancellationToken)
        => ExecuteCore(() => ActionWrap(action), cancellationToken);

    public Task<TResult> ExecuteAsync<TResult>(Func<TResult> action, CancellationToken cancellationToken)
        => ExecuteCore(action, cancellationToken);

    public Task ExecuteAsync(Action action, DispatcherPriority priority, CancellationToken cancellationToken)
        => ExecuteCore(() => ActionWrap(action), cancellationToken);

    public Task<TResult> ExecuteAsync<TResult>(Func<TResult> action, DispatcherPriority priority, CancellationToken cancellationToken)
        => ExecuteCore(action, cancellationToken);

    public void ExecuteSync(Action action, CancellationToken cancellationToken)
        => ExecuteCore(() => ActionWrap(action), cancellationToken);

    public TResult ExecuteSync<TResult>(Func<TResult> action, CancellationToken cancellationToken)
        => ExecuteCore(action, cancellationToken).Result;

    public void ExecuteSync(Action action, DispatcherPriority priority, CancellationToken cancellationToken)
        => ExecuteCore(() => ActionWrap(action), cancellationToken);

    public TResult ExecuteSync<TResult>(Func<TResult> action, DispatcherPriority priority, CancellationToken cancellationToken)
        => ExecuteCore(action, cancellationToken).Result;

    public void ExecuteSync(Action action, TaskCreationOptions options, CancellationToken cancellationToken)
        => ExecuteCore(() => ActionWrap(action), cancellationToken);

    public TResult ExecuteSync<TResult>(Func<TResult> action, TaskCreationOptions options, CancellationToken cancellationToken)
        => ExecuteCore(action, cancellationToken).Result;

    public Task ExecuteAsync(Action action, TaskCreationOptions options, CancellationToken cancellationToken)
        => ExecuteCore(() => ActionWrap(action), cancellationToken);

    public Task<TResult> ExecuteAsync<TResult>(Func<TResult> action, TaskCreationOptions options, CancellationToken cancellationToken)
        => ExecuteCore(action, cancellationToken);

    public Task ExecuteAsyncWithDelay(Action action, TimeSpan delay, TaskCreationOptions options, CancellationToken cancellationToken)
        => ExecuteCoreWithDelay(() => ActionWrap(action), delay, cancellationToken);

    public Task<TResult> ExecuteAsyncWithDelay<TResult>(Func<TResult> action, TimeSpan delay, TaskCreationOptions options, CancellationToken cancellationToken)
        => ExecuteCoreWithDelay(action, delay, cancellationToken);

    public Task ExecuteAsyncWithDelay(Action action, TimeSpan delay, DispatcherPriority priority, CancellationToken cancellationToken)
        => ExecuteCoreWithDelay(() => ActionWrap(action), delay, cancellationToken);

    public Task<TResult> ExecuteAsyncWithDelay<TResult>(Func<TResult> action, TimeSpan delay, DispatcherPriority priority, CancellationToken cancellationToken)
        => ExecuteCoreWithDelay(action, delay, cancellationToken);

    public Task ExecuteAsyncWithDelay(Action action, TimeSpan delay, CancellationToken cancellationToken)
        => ExecuteCoreWithDelay(() => ActionWrap(action), delay, cancellationToken);

    public Task<TResult> ExecuteAsyncWithDelay<TResult>(Func<TResult> action, TimeSpan delay, CancellationToken cancellationToken)
        => ExecuteCoreWithDelay(action, delay, cancellationToken);

    public bool VerifyThread() => true;

    private Task<TResult> ExecuteCore<TResult>(Func<TResult> action, CancellationToken token)
    {
        if (token.IsCancellationRequested)
        {
            return Task.FromResult(default(TResult)!);
        }

        return Task.FromResult(action());
    }

    private Task<TResult> ExecuteCoreWithDelay<TResult>(Func<TResult> action, TimeSpan delay, CancellationToken token)
    {
        var tcs = new TaskCompletionSource<TResult>();

        var timer = _timerFactory.CreateOnTp(
            onTimer: () =>
            {
                var result = ExecuteCore(action, token).Result;
                tcs.SetResult(result);
            },
            interval: null,
            name: string.Empty,
            options: TimerOptions.None);

        timer.Tick += (t) => t.Dispose();
        timer.Start(delay);

        return tcs.Task;
    }

    private bool ActionWrap(Action action)
    {
        action();
        return true;
    }

    private readonly ITimerFactory _timerFactory;
}
