// Copyright © 2025 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 8/28/2025.
// Scope of modification:
//   - Code adaptation to project requirements.

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the Ms-LPL license. See LICENSE file in the project root for full license information.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kaspirin.UI.Framework.ThirdParty.Threading;

/// <summary>
///     A task scheduler with the specified task execution concurrency level.
/// </summary>
public class LimitedTaskScheduler : TaskScheduler
{
    private const int MaxCountParallelTasks = 1_000_000;

    /// <summary>
    ///     Initializes a new instance of the <see cref="LimitedTaskScheduler" /> class with the specified concurrency level.
    /// </summary>
    /// <param name="maxDegreeOfParallelism">
    ///     The maximum concurrency level supported by this scheduler.
    /// </param>
    public LimitedTaskScheduler(int maxDegreeOfParallelism)
    {
        Guard.Argument(maxDegreeOfParallelism is >= 1 and <= MaxCountParallelTasks);

        _maxDegreeOfParallelism = maxDegreeOfParallelism;
    }

    /// <summary>
    ///     The maximum level of parallelism.
    /// </summary>
    public sealed override int MaximumConcurrencyLevel => _maxDegreeOfParallelism;

    /// <summary>
    ///     Adds a task to the scheduler queue.
    /// </summary>
    /// <param name="task">
    ///     A task to add to the queue.
    /// </param>
    protected override void QueueTask(Task task)
    {
        lock (_tasks)
        {
            _tasks.AddLast(task);

            if (_delegatesQueuedOrRunning < _maxDegreeOfParallelism)
            {
                ++_delegatesQueuedOrRunning;
                NotifyThreadPoolOfPendingWork();
            }
        }
    }

    /// <summary>
    ///     Tries to delete a previously scheduled task from the scheduler.
    /// </summary>
    /// <param name="task">
    ///     The task to delete.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the task was found and deleted, otherwise - <see langword="false" />.
    /// </returns>
    protected override bool TryDequeue(Task task)
    {
        lock (_tasks)
        {
            return _tasks.Remove(task);
        }
    }

    /// <summary>
    ///     Tries to complete the specified task on the current thread.
    /// </summary>
    /// <param name="task">
    ///     A task to complete.
    /// </param>
    /// <param name="taskWasPreviouslyQueued">
    ///     The task was previously added to the queue.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the task can be completed on the current thread, otherwise - <see langword="false" />.
    /// </returns>
    protected sealed override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
    {
        // If this thread isn't already processing a task, we don't support inlining
        if (!_currentThreadIsProcessingItems)
        {
            return false;
        }

        // If the task was previously queued, remove it from the queue
        if (taskWasPreviouslyQueued)
        {
            TryDequeue(task);
        }

        // Try to run the task.
        return TryExecuteTask(task);
    }

    /// <summary>
    ///     Gets a list of tasks that are scheduled for this scheduler.
    /// </summary>
    /// <returns>
    ///     An enumeration of the tasks that are scheduled.
    /// </returns>
    protected sealed override IEnumerable<Task> GetScheduledTasks()
    {
        var lockTaken = false;

        try
        {
            Monitor.TryEnter(_tasks, ref lockTaken);
            if (lockTaken)
            {
                return _tasks.ToArray();
            }
            else
            {
                throw new NotSupportedException();
            }
        }
        finally
        {
            if (lockTaken)
            {
                Monitor.Exit(_tasks);
            }
        }
    }

    /// <summary>
    ///     Notifies the thread pool of the presence of expected work.
    /// </summary>
    private void NotifyThreadPoolOfPendingWork()
    {
        ThreadPool.UnsafeQueueUserWorkItem(QueueUserWorkItemCallBack, state: null);
    }

    private void QueueUserWorkItemCallBack(object? state)
    {
        _currentThreadIsProcessingItems = true;

        try
        {
            while (true)
            {
                Task item;
                lock (_tasks)
                {
                    if (_tasks.Count == 0)
                    {
                        if (_delegatesQueuedOrRunning > 0)
                        {
                            --_delegatesQueuedOrRunning;
                        }

                        break;
                    }

                    item = _tasks.First!.Value;
                    _tasks.RemoveFirst();
                }

                TryExecuteTask(item);
            }
        }
        finally
        {
            _currentThreadIsProcessingItems = false;
        }
    }

    /// <summary>
    ///     Determines whether the current stream is processing items.
    /// </summary>
    [ThreadStatic]
    private static bool _currentThreadIsProcessingItems;

    /// <summary>
    ///     A list of tasks to complete.
    /// </summary>
    private readonly LinkedList<Task> _tasks = new(); // protected by lock(_tasks)

    /// <summary>
    ///     The maximum level of parallelism allowed by this scheduler.
    /// </summary>
    private readonly int _maxDegreeOfParallelism;

    /// <summary>
    ///     Determines whether the scheduler is doing the work.
    /// </summary>
    private int _delegatesQueuedOrRunning; // protected by lock(_tasks)
}
