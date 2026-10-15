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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Kaspirin.UI.Framework.UiKit.Navigation;

public sealed class Region : IEnumerable<RegionView>
{
    public Region(
        string regionName,
        ContentControl regionHost,
        IRegionViewFactory viewFactory,
        IRegionBehaviorsRegistry behaviorsRegistry)
    {
        Name = Guard.EnsureArgumentIsNotNullOrEmpty(regionName);
        _viewFactory = Guard.EnsureArgumentIsNotNull(viewFactory);
        _behaviorsRegistry = Guard.EnsureArgumentIsNotNull(behaviorsRegistry);

        Host.SetHost(regionHost);

        Cache.Added += OnCacheViewAdded;
        Cache.Removed += OnCacheViewRemoved;

        _behaviorsRegistry.Attach(this);

        ActiveViewChanged += OnActiveViewChanged;
    }

    public string Name { get; }

    public RegionHistory History { get; } = new();

    public RegionHost Host { get; } = new();

    public RegionCache Cache { get; } = new();

    public RegionContext Context { get; } = new();

    public RegionView? ActiveView
    {
        get => _activeView ?? _pendingView;
        private set => _activeView = value;
    }

    public event EventHandler<ViewChangedEventArgs> ActiveViewChanged = EmptyDelegate.For<EventHandler<ViewChangedEventArgs>>();

    public event EventHandler<ViewAddedEventArgs> ViewAdded = EmptyDelegate.For<EventHandler<ViewAddedEventArgs>>();

    public event EventHandler<ViewRemovedEventArgs> ViewRemoved = EmptyDelegate.For<EventHandler<ViewRemovedEventArgs>>();

    public event EventHandler<NavigateEventArgs> Navigating = EmptyDelegate.For<EventHandler<NavigateEventArgs>>();

    public event EventHandler<NavigateEventArgs> Navigated = EmptyDelegate.For<EventHandler<NavigateEventArgs>>();

    #region IEnumerable

    public IEnumerator<RegionView> GetEnumerator()
    {
        return Cache.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Cache.GetEnumerator();
    }

    #endregion

    #region Navigate

    public RegionView? Navigate<TTargetView>(
        Action<NavigationResult>? callback = null)
        where TTargetView : FrameworkElement
    {
        return Navigate<TTargetView, object>(parameters: default, callback);
    }

    public RegionView? Navigate<TTargetView, TParameters>(
        TParameters? parameters = default,
        Action<NavigationResult>? callback = null)
        where TTargetView : FrameworkElement
    {
        var targetViewName = typeof(TTargetView).FullName!;

        return Navigate(targetViewName, parameters, callback);
    }

    public RegionView? Navigate<TParameters>(
        string targetViewName,
        TParameters? parameters,
        Action<NavigationResult>? callback)
    {
        var isInHistory = History.Contains(targetViewName);
        if (isInHistory)
        {
            return NavigateBack(targetViewName, parameters, callback);
        }
        else
        {
            return NavigateForward(targetViewName, parameters, callback);
        }
    }

    #endregion

    #region NavigateForward

    public RegionView? NavigateForward<TTargetView>(
        Action<NavigationResult>? callback = null)
        where TTargetView : FrameworkElement
    {
        return NavigateForward<TTargetView, object>(parameters: default, callback);
    }

    public RegionView? NavigateForward<TTargetView, TParameters>(
        TParameters? parameters = default,
        Action<NavigationResult>? callback = null)
        where TTargetView : FrameworkElement
    {
        var targetViewName = typeof(TTargetView).FullName!;

        return NavigateForward(targetViewName, parameters, callback);
    }

    public RegionView? NavigateForward<TParameters>(
        string targetViewName,
        TParameters? parameters,
        Action<NavigationResult>? callback)
    {
        if (ActiveView?.Name == targetViewName)
        {
            _tracer.TraceInformation($"Already navigated to view {targetViewName} in region {Name}");

            return CompleteNavigation(NavigationOutcome.NoOp, callback);
        }

        return GateNavigateFrom(targetViewName, NavigationDirection.Forward, callback, () =>
        {
            _tracer.TraceInformation($"Navigating forward to view {targetViewName} in region {Name}.");

            History.Push(targetViewName);

            var targetView = FindOrCreateView(targetViewName, parameters);

            return SetActiveView(targetView, targetViewName, NavigationDirection.Forward, parameters, callback);
        });
    }

    #endregion

    #region NavigateBack

    public RegionView? NavigateBack(
        Action<NavigationResult>? callback = null)
    {
        return NavigateBack<object>(parameters: default, callback);
    }

    public RegionView? NavigateBack<TParameters>(
        TParameters? parameters = default,
        Action<NavigationResult>? callback = null)
    {
        var targetViewName = History.Previous ?? string.Empty;

        return NavigateBack(targetViewName, parameters, callback);
    }

    public RegionView? NavigateBack<TTargetView, TParameters>(
        TParameters? parameters = default,
        Action<NavigationResult>? callback = null)
        where TTargetView : FrameworkElement
    {
        var targetViewName = typeof(TTargetView).FullName!;

        return NavigateBack(targetViewName, parameters, callback);
    }

    public RegionView? NavigateBack<TParameters>(
        string targetViewName,
        TParameters? parameters,
        Action<NavigationResult>? callback)
    {
        if (!History.Contains(targetViewName) && targetViewName != EmptyView)
        {
            _tracer.TraceInformation($"Cant navigate back to view {targetViewName} in region {Name}. Target view does not present in history.");

            return CompleteNavigation(NavigationOutcome.NoOp, callback);
        }

        if (ActiveView?.Name == targetViewName)
        {
            _tracer.TraceInformation($"Already navigated to view {targetViewName} in region {Name}");

            return CompleteNavigation(NavigationOutcome.NoOp, callback);
        }

        return GateNavigateFrom(targetViewName, NavigationDirection.Back, callback, () =>
        {
            _tracer.TraceInformation($"Navigating back to view {targetViewName} in region {Name}.");

            var activeViewName = History.Pop();
            var activeView = Cache.TryFind(activeViewName);
            if (activeView != null)
            {
                RemoveView(activeView);
            }

            if (targetViewName == EmptyView)
            {
                History.Clear();
                Cache.Clear();
            }

            var targetViewIndex = History.IndexOf(targetViewName);
            if (targetViewIndex > 0)
            {
                History.PopRange(targetViewIndex)
                    .Select(Cache.TryFind)
                    .WhereNotNull()
                    .ToList()
                    .ForEach(RemoveView);
            }

            var targetView = Cache.TryFind(targetViewName);

            return SetActiveView(targetView, targetViewName, NavigationDirection.Back, parameters, callback);
        });
    }

    #endregion

    public void Clear(Action<NavigationResult>? callback = null)
    {
        NavigateBack<object>(EmptyView, parameters: null, callback);
    }

    public bool CanNavigateForward()
    {
        return ActiveView == null || ActiveView.Options.CanNavigateFrom != NavigateFromPermission.Deny;
    }

    public bool CanNavigateBack()
    {
        return ActiveView != null && ActiveView.Options.CanNavigateFrom != NavigateFromPermission.Deny;
    }

    private RegionView? GateNavigateFrom(string targetViewName, NavigationDirection direction, Action<NavigationResult>? callback, Func<RegionView?> proceed)
    {
        var permission = ActiveView?.Options?.CanNavigateFrom ?? NavigateFromPermission.Allow;

        switch (permission)
        {
            case NavigateFromPermission.Deny:
                _tracer.TraceInformation($"Navigation to view {targetViewName} in region {Name} denied by Options.");

                return CompleteNavigation(NavigationOutcome.Cancelled, callback);

            case NavigateFromPermission.Ask:
                var confirmationCallback = ActiveView?.Options.ConfirmNavigation;
                if (confirmationCallback == null)
                {
                    throw new InvalidOperationException("ConfirmNavigation is not set for Ask permission.");
                }

                var contextTargetViewName = targetViewName == EmptyView ? null : targetViewName;
                var contextTargetView = Cache.TryFind(targetViewName);

                var context = new NavigationContext(this, direction, contextTargetViewName, contextTargetView, ActiveView);

                confirmationCallback.Invoke(context, confirmed =>
                {
                    if (confirmed)
                    {
                        proceed();
                    }
                    else
                    {
                        _tracer.TraceInformation($"Navigation to view {targetViewName} in region {Name} cancelled by confirmation.");
                        CompleteNavigation(NavigationOutcome.Cancelled, callback);
                    }
                });

                return ActiveView;

            case NavigateFromPermission.Allow:
            default:
                return proceed();
        }
    }

    private RegionView? SetActiveView<TParameters>(RegionView? view, string viewName, NavigationDirection direction, TParameters? parameters, Action<NavigationResult>? callback)
    {
        if (Host.IsInitialized is false)
        {
            _tracer.TraceInformation($"ViewHost is not set.");

            return CompleteNavigation(NavigationOutcome.NoOp, callback);
        }

        if (Host.IsLoaded is false)
        {
            _pendingView = view;
        }

        Host.WhenLoaded(() =>
        {
            Exception? error = null;

            try
            {
                _pendingView = null;

                var navigationArgs = new NavigateEventArgs(this, viewName);
                var activeViewChangeArgs = new ViewChangedEventArgs(Name, ActiveView, view);

                Navigating.Invoke(this, navigationArgs);

                ActiveView?.OnNavigatedFrom(view, direction);
                ActiveView = view;

                Host.SetHostView(ActiveView);

                ActiveView?.SetParameters(parameters);
                ActiveView?.OnNavigatedTo(view, direction);

                ActiveViewChanged.Invoke(this, activeViewChangeArgs);

                Navigated.Invoke(this, navigationArgs);
            }
            catch (Exception exception)
            {
                error = exception;

                _tracer.TraceError($"Navigation to view {viewName} in region {Name} failed. {exception}");
            }

            var result = error == null
                ? new NavigationResult(NavigationOutcome.Success, view)
                : new NavigationResult(NavigationOutcome.Failed, view, error);

            callback?.Invoke(result);
        });

        return view;
    }

    private RegionView? CompleteNavigation(NavigationOutcome outcome, Action<NavigationResult>? callback)
    {
        callback?.Invoke(new NavigationResult(outcome, ActiveView));

        return ActiveView;
    }

    private void RemoveView(RegionView view)
    {
        if (History.Contains(view.Name))
        {
            _tracer.TraceInformation(
                $"Removing {view.Name} from cache in region {Name} skipped. " +
                $"View present in navigation history.");

            return;
        }

        if (view.Options.KeepInCache)
        {
            _tracer.TraceInformation(
                $"Removing {view.Name} from cache in region {Name} skipped. " +
                $"View has KeepInCache option.");

            return;
        }

        Cache.Remove(view.Name);
    }

    private RegionView FindOrCreateView<TParameters>(string viewName, TParameters? parameters)
    {
        var regionView = Cache.TryFind(viewName);
        if (regionView == null)
        {
            var viewFactory = () => _viewFactory.CreateView(viewName, parameters);

            regionView = new RegionView(viewName, viewFactory, this);

            Cache.Add(regionView);

            _tracer.TraceInformation($"Created new view {viewName} in region {Name}");
        }
        else
        {
            _tracer.TraceInformation($"Found existing view {viewName} in region {Name}");
        }

        return regionView;
    }

    private void OnCacheViewAdded(RegionView view)
    {
        _tracer.TraceInformation($"Add {view.Name} to cache in region {Name}");

        ViewAdded?.Invoke(this, new ViewAddedEventArgs(Name, view));
    }

    private void OnCacheViewRemoved(RegionView view)
    {
        _tracer.TraceInformation($"Remove {view.Name} from cache in region {Name}");

        ViewRemoved?.Invoke(this, new ViewRemovedEventArgs(Name, view));
    }

    private void OnActiveViewChanged(object? sender, ViewChangedEventArgs e)
    {
        var oldView = e.OldView;
        if (oldView?.Options.KeepInHistory == false)
        {
            History.Remove(oldView.Name);

            RemoveView(oldView);
        }
    }

    private const string EmptyView = ":EmptyView:";
    private RegionView? _activeView;
    private RegionView? _pendingView;

    private readonly IRegionViewFactory _viewFactory;
    private readonly IRegionBehaviorsRegistry _behaviorsRegistry;

    private static readonly ComponentTracer _tracer = ComponentTracer.Get(UIKitComponentTracers.Navigation);
}
