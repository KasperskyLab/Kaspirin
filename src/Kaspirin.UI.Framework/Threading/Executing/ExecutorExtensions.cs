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
using Kaspirin.UI.Framework.Services.Internals;

namespace Kaspirin.UI.Framework.Threading.Executing;

/// <summary>
///     Extension methods for <see cref="IExecutor" />.
/// </summary>
public static class ExecutorExtensions
{
    /// <summary>
    ///     The delegate executes synchronously <paramref name="action" /> if the performer of the actions
    ///     is available, otherwise <paramref name="action" /> will be asynchronously executed only when
    ///     the performer becomes available.
    /// </summary>
    /// <param name="executor">
    ///     The performer of actions.
    /// </param>
    /// <param name="action">
    ///     The action being performed.
    /// </param>
    public static void WhenAvailable(this IExecutor executor, Action action)
    {
        EventSubscriber.OnceOrNow(
            source: executor,
            condition: e => e.IsAvailable,
            subscribeCallback: eh => executor.IsAvailableChanged += eh,
            unsubscribeCallback: eh => executor.IsAvailableChanged -= eh,
            action: action);
    }
}