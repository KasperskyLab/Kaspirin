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
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.Threading.Throttlers.Internals;

internal sealed class DispatcherDeferredAction : IDeferredAction
{
    public DispatcherDeferredAction(DeferredActionParameters parameters, DispatcherPriority priority, IDispatcherExecutor executor)
        => _deferredAction = new DispatcherDeferredAction<object>(parameters, priority, executor);

    public DeferredActionState State => _deferredAction.State;

    public DeferredActionMode Mode => _deferredAction.Mode;

    public DeferredActionOptions Options => _deferredAction.Options;

    public TimeSpan DelayBeforeAction => _deferredAction.DelayBeforeAction;

    public TimeSpan DelayAfterAction => _deferredAction.DelayAfterAction;

    public string Name => _deferredAction.Name;

    public void Execute()
        => _deferredAction.Execute(null);

    public void Cancel()
        => _deferredAction.Cancel();

    public void Dispose()
        => _deferredAction.Dispose();

    private readonly IDeferredAction<object> _deferredAction;
}
