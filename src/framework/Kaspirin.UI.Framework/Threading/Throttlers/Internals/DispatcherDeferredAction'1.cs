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

namespace Kaspirin.UI.Framework.Threading.Throttlers.Internals;

internal sealed class DispatcherDeferredAction<TArgument> : BaseDeferredAction<TArgument>
{
    public DispatcherDeferredAction(DeferredActionParameters<TArgument> parameters, DispatcherPriority priority, IDispatcherExecutor executor)
        : base(parameters)
    {
        _executor = Guard.EnsureArgumentIsNotNull(executor);
        _priority = priority;
    }

    public DispatcherDeferredAction(DeferredActionParameters parameters, DispatcherPriority priority, IDispatcherExecutor executor)
        : base(parameters)
    {
        _executor = Guard.EnsureArgumentIsNotNull(executor);
        _priority = priority;
    }

    protected override Task StartAction(Action action, TimeSpan delay, CancellationToken token)
    {
        return delay > TimeSpan.Zero
            ? _executor.ExecuteAsyncWithDelay(action, delay, _priority, token)
            : _executor.ExecuteAsync(action, _priority, token);
    }

    private readonly DispatcherPriority _priority;
    private readonly IDispatcherExecutor _executor;
}
