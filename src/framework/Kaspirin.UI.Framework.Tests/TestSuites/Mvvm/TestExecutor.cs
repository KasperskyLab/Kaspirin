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
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.Tests.TestSuites.Mvvm;

internal sealed class TestExecutor : IUiThreadExecutor
{
    public bool IsAvailable => true;
    public bool IsUiThread { get; set; } = true;

    public bool ActionRaisedAsync { get; private set; }
    public bool ActionRaisedSync { get; private set; }

    public bool ThrowIfNotAvailable { get; set; } = true;

    public void ExecuteInUiThreadSync(Action action, DispatcherPriority priority = DispatcherPriority.Normal)
    {
        ActionRaisedSync = true;

        action();
    }

    public Task ExecuteInUiThreadAsync(Action action, DispatcherPriority priority = DispatcherPriority.Normal)
    {
        ActionRaisedAsync = true;

        return Task.Run(action);
    }

    public TResult ExecuteInUiThreadSync<TResult>(Func<TResult> action, DispatcherPriority priority = DispatcherPriority.Normal)
    {
        ActionRaisedSync = true;

        return action();
    }
}
