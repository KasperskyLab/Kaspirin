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
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Navigation;

public sealed class RegionView
{
    internal RegionView(string viewName, Func<FrameworkElement> viewFactory, Region region)
    {
        Name = Guard.EnsureArgumentIsNotNullOrEmpty(viewName);

        _viewFactory = Guard.EnsureArgumentIsNotNull(viewFactory);
        _region = Guard.EnsureArgumentIsNotNull(region);

        SubscribeOnCacheUpdate(_region.Cache);

    }

    public string Name { get; }

    public FrameworkElement View => _view ??= _viewFactory();

    public object? ViewModel => View.DataContext;

    public NavigationOptions Options => GetOptions();

    public override string ToString() => Name;

    internal void SetParameters<TParameters>(TParameters? parameters)
    {
        if (parameters == null)
        {
            return;
        }

        Execute<INavigationAware<TParameters>>(View, v => v.SetParameters(parameters));
        Execute<INavigationAware<TParameters>>(ViewModel, vm => vm.SetParameters(parameters));
    }

    internal void OnNavigatedTo(RegionView? targetView, NavigationDirection direction)
    {
        var context = new NavigationContext(_region, direction, targetView?.Name, targetView, _region.ActiveView);

        _tracer.TraceInformation($"Notifying view {Name} about navigation to it");

        Execute<INavigationAware>(View, v => v.OnNavigatedTo(context));
        Execute<INavigationAware>(ViewModel, vm => vm.OnNavigatedTo(context));
    }

    internal void OnNavigatedFrom(RegionView? targetView, NavigationDirection direction)
    {
        var context = new NavigationContext(_region, direction, targetView?.Name, targetView, _region.ActiveView);

        _tracer.TraceInformation($"Notifying view {Name} about navigation from it");

        Execute<INavigationAware>(View, v => v.OnNavigatedFrom(context));
        Execute<INavigationAware>(ViewModel, vm => vm.OnNavigatedFrom(context));
    }

    private NavigationOptions GetOptions()
    {
        var builder = new NavigationOptionsBuilder();

        Execute<INavigationAware>(View, v => v.BuildOptions(builder));
        Execute<INavigationAware>(ViewModel, vm => vm.BuildOptions(builder));

        return builder.Build();
    }

    private static void Execute<T>(object? obj, Action<T> action) where T : class
    {
        if (obj is T typedObj)
        {
            action.Invoke(typedObj);
        }
    }

    private void SubscribeOnCacheUpdate(RegionCache regionCache)
    {
        regionCache.Added += WeakEventHandlerFactory.Wrap(
            handler: OnCacheAdded,
            cleanup: h => regionCache.Added -= h);

        regionCache.Removed += WeakEventHandlerFactory.Wrap(
            handler: OnCacheRemoved,
            cleanup: h => regionCache.Removed -= h);
    }

    private void OnCacheAdded(RegionView view)
    {
        if (view == this)
        {
            DisposablePrepare();
        }
    }

    private void OnCacheRemoved(RegionView view)
    {
        if (view == this)
        {
            DisposableInvoke();
        }
    }

    private void DisposablePrepare()
    {
        if (_view == null)
        {
            var viewFactory = _viewFactory;

            _viewFactory = () =>
            {
                var view = viewFactory();

                DispositionContext.Initialize(view);

                return view;
            };
        }
        else
        {
            DispositionContext.Initialize(_view);
        }
    }

    private void DisposableInvoke()
    {
        if (_view != null)
        {
            DispositionContext.Dispose(_view);
        }
    }

    private FrameworkElement? _view;
    private Func<FrameworkElement> _viewFactory;

    private readonly Region _region;

    private static readonly ComponentTracer _tracer = ComponentTracer.Get(UIKitComponentTracers.Navigation);
}