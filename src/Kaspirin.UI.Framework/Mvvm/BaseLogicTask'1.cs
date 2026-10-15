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
///     Asynchronous operation of the application's business logic that returns a result.
/// </summary>
/// <typeparam name="TResult">
///     The type of the asynchronous operation result.
/// </typeparam>
public sealed class BaseLogicTask<TResult> : BaseLogicTask
{
    internal BaseLogicTask(BaseLogic owner, Task<TResult> task, CancellationTokenSource taskCts)
        : base(owner, task, taskCts)
    {
        _task = task;
    }

    /// <summary>
    ///     Schedules the continuation of the asynchronous business logic operation, which returns the result.
    /// </summary>
    /// <typeparam name="TNewResult">
    ///     The type of result of the new continuation.
    /// </typeparam>
    /// <param name="continuationAction">
    ///     The continuation delegate.
    /// </param>
    /// <returns>
    ///     An instance of the task representing the continuation.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Called if the owner of this asynchronous operation is not available.
    /// </exception>
    public BaseLogicTask<TNewResult> ContinueWith<TNewResult>(Func<TNewResult> continuationAction)
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

            return continuationAction();
        });
    }

    /// <summary>
    ///     Schedules a continuation of the asynchronous business logic operation, returning the result
    ///     and accepting the result of the previous operation.
    /// </summary>
    /// <typeparam name="TNewResult">
    ///     The type of result of the new continuation.
    /// </typeparam>
    /// <param name="continuationAction">
    ///     The continuation delegate.
    /// </param>
    /// <returns>
    ///     An instance of the task representing the continuation.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     Called if the owner of this asynchronous operation is not available.
    /// </exception>
    public BaseLogicTask<TNewResult> ContinueWith<TNewResult>(Func<TResult, TNewResult> continuationAction)
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

            return continuationAction(antecedent.Result);
        });
    }

    /// <summary>
    ///     Schedules a continuation of the asynchronous business logic operation that accepts the result of the previous operation.
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
    public BaseLogicTask ContinueWith(Action<TResult> continuationAction)
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

            continuationAction(antecedent.Result);
        });
    }

    private readonly Task<TResult> _task;
}
