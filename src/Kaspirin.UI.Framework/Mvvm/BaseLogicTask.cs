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
using System.Threading.Tasks;

namespace Kaspirin.UI.Framework.Mvvm;

/// <summary>
///     Asynchronous operation of the business logic of the application.
/// </summary>
public class BaseLogicTask
{
    internal BaseLogicTask(BaseLogic owner, Task task, CancellationTokenSource taskCts)
    {
        OwnerType = owner.GetType();
        OwnerWeakRef = new WeakReference<BaseLogic>(owner);

        _task = task;
        _taskCts = taskCts;
    }

    /// <summary>
    ///     Cancels the asynchronous operation.
    /// </summary>
    public void Cancel()
    {
        _taskCts.Cancel();
    }

    /// <summary>
    ///     The current status of the asynchronous operation.
    /// </summary>
    public TaskStatus Status => _task.Status;

    /// <summary>
    ///     Plans to continue asynchronous business logic operations.
    /// </summary>
    /// <param name="continuationAction">
    ///     The continuation delegate.
    /// </param>
    /// <returns>
    ///     An instance of the task representing the continuation.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Called if the owner of this asynchronous operation is not available.
    /// </exception>
    public BaseLogicTask ContinueWith(Action continuationAction)
    {
        Guard.ArgumentIsNotNull(continuationAction);

        if (!OwnerWeakRef.TryGetTarget(out var owner))
        {
            throw new InvalidOperationException($"Task owner of type {OwnerType} is not available.");
        }

        var antecedent = _task;

        return owner.PushTask(() =>
        {
            ObserveAntecedent(antecedent);

            continuationAction();
        });
    }

    /// <summary>
    ///     Observes the completed task <paramref name="antecedent" />: on exception or cancellation, it
    ///     skips them further, otherwise it does nothing.
    /// </summary>
    /// <remarks>
    ///     It is called from the continuation, which the sequential dispatcher starts after the completion
    ///     of <paramref name="antecedent" />, so the call does not block the flow.
    /// </remarks>
    private protected static void ObserveAntecedent(Task antecedent)
    {
#if NETCOREAPP
        antecedent.GetAwaiter().GetResult();
#else
        if (antecedent.IsCanceled)
        {
            throw new OperationCanceledException();
        }

        if (antecedent.IsFaulted)
        {
            throw antecedent.Exception!;
        }
#endif
    }

    /// <summary>
    ///     The type of the issue owner.
    /// </summary>
    protected Type OwnerType { get; }

    /// <summary>
    ///     A weak reference to the owner of the task.
    /// </summary>
    protected WeakReference<BaseLogic> OwnerWeakRef { get; }

    private readonly Task _task;
    private readonly CancellationTokenSource _taskCts;
}