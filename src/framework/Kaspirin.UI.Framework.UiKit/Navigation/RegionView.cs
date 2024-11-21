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

namespace Kaspirin.UI.Framework.UiKit.Navigation
{
    public sealed class RegionView
    {
        public RegionView(string viewName, IRegionViewFactory viewFactory, Region region)
        {
            Name = Guard.EnsureArgumentIsNotNullOrEmpty(viewName);

            _region = Guard.EnsureArgumentIsNotNull(region);
            _viewFactory = Guard.EnsureArgumentIsNotNull(viewFactory);
            _view = new(() => Guard.EnsureIsNotNull(_viewFactory.CreateView(viewName)));
        }

        public string Name { get; }

        public FrameworkElement View => _view.Value;

        public object? ViewModel => _view.Value?.DataContext;

        public NavigationOptions Options => GetOptions();

        public override string ToString() => Name;

        internal void SetParameters<TParameters>(TParameters? parameters)
        {
            if (parameters == null)
            {
                return;
            }

            Guard.Assert(View is INavigationAware<TParameters> ||
                         ViewModel is INavigationAware<TParameters>,
                         $"View or ViewModel must implement {nameof(INavigationAware<TParameters>)} " +
                         $"when navigating to this View with parameters.");

            Execute<INavigationAware<TParameters>>(View, v => v.SetParameters(parameters));
            Execute<INavigationAware<TParameters>>(ViewModel, vm => vm.SetParameters(parameters));
        }

        internal void OnNavigatedTo(RegionView? targetView)
        {
            var context = new NavigationContext(targetView, _region.ActiveView);

            _tracer.TraceInformation($"Notifying view {Name} about navigation to it");

            Execute<INavigationAware>(View, v => v.OnNavigatedTo(context));
            Execute<INavigationAware>(ViewModel, vm => vm.OnNavigatedTo(context));
        }

        internal void OnNavigatedFrom(RegionView? targetView)
        {
            var context = new NavigationContext(targetView, _region.ActiveView);

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

        private readonly Lazy<FrameworkElement> _view;
        private readonly Region _region;
        private readonly IRegionViewFactory _viewFactory;

        private static readonly ComponentTracer _tracer = ComponentTracer.Get(UIKitComponentTracers.Navigation);
    }
}