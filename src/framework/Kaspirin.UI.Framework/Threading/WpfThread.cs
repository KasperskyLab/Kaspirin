// Copyright © 2024 AO Kaspersky Lab.
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

#pragma warning disable  CA1416 // This call site is reachable on all platforms

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.Threading;

/// <summary>
///     Starts the main thread of the WPF application (UI thread).
/// </summary>
public sealed class WpfThread
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="WpfThread" /> class.
    /// </summary>
    /// <remarks>
    ///     This method can only be called once.
    /// </remarks>
    /// <param name="startAction">
    ///     A delegate to start the UI thread.
    /// </param>
    /// <param name="threadCulture">
    ///     Passed to <see cref="Thread.CurrentCulture" /> for the UI stream being created.
    /// </param>
    /// <param name="threadUICulture">
    ///     Passed to <see cref="Thread.CurrentUICulture" /> for the UI stream being created.
    /// </param>
    /// <param name="threadName">
    ///     Passed to <see cref="Thread.Name" /> for the UI stream being created.
    /// </param>
    public static WpfThread Create(
        Action startAction,
        CultureInfo? threadCulture = null,
        CultureInfo? threadUICulture = null,
        string? threadName = null)
    {
        lock (_sync)
        {
            if (Current != null)
            {
                throw new InvalidOperationException("WPF thread is already created.");
            }

            Guard.ArgumentIsNotNull(startAction);

            threadCulture ??= Thread.CurrentThread.CurrentCulture;
            threadUICulture ??= Thread.CurrentThread.CurrentUICulture;
            threadName ??= "WPF";

            Current = new WpfThread(startAction, threadCulture, threadUICulture, threadName);

            _observers?.ForEach(o => o.Created());

            return Current;
        }
    }

    /// <summary>
    ///     Registers an observer <see cref="IWpfThreadObserver" /> to monitor the status of the UI thread.
    /// </summary>
    /// <param name="observer">
    ///     An observer for monitoring the status of the UI thread.
    /// </param>
    public static void RegisterObserver(IWpfThreadObserver observer)
    {
        lock (_sync)
        {
            _observers?.Add(observer);
        }
    }

    /// <summary>
    ///     A global instance of the UI thread.
    /// </summary>
    public static WpfThread? Current { get; private set; }

    private WpfThread(
        Action startAction,
        CultureInfo threadCulture,
        CultureInfo threadUICulture,
        string threadName)
    {
        _tracer = ComponentTracer.Get(ComponentTracers.Threading, this);
        _startAction = startAction;

        _wpfThread = new Thread(Run)
        {
            Name = threadName,
            CurrentCulture = threadCulture,
            CurrentUICulture = threadUICulture
        };

        _wpfThread.SetApartmentState(ApartmentState.STA);
    }

    /// <summary>
    ///     Dispatcher of the main UI thread.
    /// </summary>
    public Dispatcher? CurrentDispatcher
    {
        get => Dispatcher.FromThread(_wpfThread);
    }

    /// <summary>
    ///     The value is <see cref="Thread.CurrentCulture" /> for the UI stream.
    /// </summary>
    public CultureInfo CurrentCulture
    {
        get => _wpfThread.CurrentCulture;
        set => _wpfThread.CurrentCulture = value;
    }

    /// <summary>
    ///     The value is <see cref="Thread.CurrentUICulture" /> for the UI stream.
    /// </summary>
    public CultureInfo CurrentUICulture
    {
        get => _wpfThread.CurrentUICulture;
        set => _wpfThread.CurrentUICulture = value;
    }

    /// <summary>
    ///     Starts the UI thread.
    /// </summary>
    public void Start()
    {
        lock (_sync)
        {
            if (_wpfThread.ThreadState == ThreadState.Running)
            {
                return;
            }

            _wpfThread.Start();
        }
    }

    /// <summary>
    ///     Blocks the calling thread until the UI thread is completed.
    /// </summary>
    public void Join()
        => _wpfThread.Join();

    private void Run()
    {
        var threadId = Thread.CurrentThread.ManagedThreadId;
        var threadCulture = Thread.CurrentThread.CurrentCulture;
        var threadUICulture = Thread.CurrentThread.CurrentUICulture;
        var threadName = Thread.CurrentThread.Name;

        var dispatcher = Dispatcher.CurrentDispatcher;
        dispatcher.ShutdownStarted += OnDispatcherShutdownStarted;
        dispatcher.BeginInvoke(NotifyStarted, DispatcherPriority.Normal);
        dispatcher.BeginInvoke(InvokeStartAction, DispatcherPriority.Normal);

        Guard.SetUiThreadId(threadId);

        _tracer.TraceInformation($"UI-Thread started. [" +
                                 $"ID:0x{threadId:X}]; " +
                                 $"Name:{threadName}; " +
                                 $"Culture:{threadCulture.Name}; " +
                                 $"UICulture:{threadUICulture.Name}]");

        Dispatcher.Run();
    }

    private void NotifyStarted()
    {
        _observers?.ForEach(o => o.Started());
        _observers = null;
    }

    private void InvokeStartAction()
    {
        _startAction?.Invoke();
        _startAction = null;
    }

    private void OnDispatcherShutdownStarted(object? sender, EventArgs e)
    {
        _tracer.TraceInformation($"UI-Thread dispatched shutdown started.");
    }

    private static readonly object _sync = new();
    private static HashSet<IWpfThreadObserver>? _observers = new();

    private Action? _startAction;
    private readonly ComponentTracer _tracer;
    private readonly Thread _wpfThread;
}