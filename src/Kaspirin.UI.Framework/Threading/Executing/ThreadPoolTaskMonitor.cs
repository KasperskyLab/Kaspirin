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

#if NET6_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kaspirin.UI.Framework.Threading.Executing;

/// <summary>
///     Provides a monitor for tasks registered in this class.
/// </summary>
/// <remarks>
///     The class stores weak references to tracked tasks. If the ThreadPoolTaskInterceptionGenerator
///     code generator is linked to the project, then all tasks created using <see cref="Task.Run(Action)" /> are registered in this class.
/// </remarks>
public static class ThreadPoolTaskMonitor
{
    /// <summary>
    ///     Registers a task <see cref="Task" /> for monitoring.
    /// </summary>
    /// <param name="task">
    ///     The task that needs to be tracked.
    /// </param>
    public static void Register(Task task)
    {
        Guard.ArgumentIsNotNull(task);

        lock (_sync)
        {
            // Used to clean up collected or completed tasks.
            GetActiveTasks();

            _tasks.Add(new WeakReference<Task>(task));
        }
    }

    /// <summary>
    ///     Returns an array of all active (unfinished) tasks registered in the monitor.
    /// </summary>
    /// <remarks>
    ///     The method also cleans up the internal task list by removing links pointing to completed or collected tasks.
    /// </remarks>
    /// <returns>
    ///     An array of <see cref="Task" /> objects that have not been completed yet.
    /// </returns>
    public static Task[] GetActiveTasks()
    {
        lock (_sync)
        {
            var liveTasks = _tasks
                .Select(wr =>
                {
                    wr.TryGetTarget(out var t);
                    return t;
                })
                .WhereNotNull()
                .Where(t => !t.IsCompleted)
                .ToArray();

            _tasks.Clear();
            _tasks.AddRange(liveTasks.Select(t => new WeakReference<Task>(t)));

            return liveTasks;
        }
    }

    private static readonly List<WeakReference<Task>> _tasks = new();
    private static readonly object _sync = new();
}
#endif
