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

namespace Kaspirin.UI.Framework.UiKit.Navigation
{
    public sealed class Region : IEnumerable<RegionView>
    {
        public Region(
            string regionName,
            ContentControl regionHost,
            IRegionViewFactory viewFactory,
            IRegionBehaviorsRegistry regionBehaviorsRegistry)
        {
            Name = Guard.EnsureArgumentIsNotNullOrEmpty(regionName);
            _viewHost = new(Guard.EnsureArgumentIsNotNull(regionHost));
            _viewFactory = Guard.EnsureArgumentIsNotNull(viewFactory);
            _behaviors = Guard.EnsureArgumentIsNotNull(regionBehaviorsRegistry).CreateBehaviors();

            _behaviors.ForEach(b => b.Attach(this));

            ActiveViewChanged += OnActiveViewChanged;
        }

        public string Name { get; }

        public RegionView? ActiveView
        {
            get => _activeView ?? _pendingView;
            private set => _activeView = value;
        }

        public event EventHandler<ActiveViewChangedEventArgs> ActiveViewChanged = (o, e) => { };

        public event EventHandler<ViewAddedEventArgs> ViewAdded = (o, e) => { };

        public event EventHandler<ViewRemovedEventArgs> ViewRemoved = (o, e) => { };

        public event EventHandler<NavigateEventArgs> Navigating = (o, e) => { };

        public event EventHandler<NavigateEventArgs> Navigated = (o, e) => { };

        #region IEnumerable

        public IEnumerator<RegionView> GetEnumerator()
        {
            return ((IEnumerable<RegionView>)_views.ToList()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_views.ToList()).GetEnumerator();
        }

        #endregion

        public RegionView? Navigate<TTargetView>()
            where TTargetView : FrameworkElement
        {
            return Navigate<TTargetView, object>();
        }

        public RegionView? Navigate<TTargetView, TParameters>(TParameters? parameters = default)
            where TTargetView : FrameworkElement
        {
            var targetViewName = typeof(TTargetView).FullName!;

            return Navigate(targetViewName, parameters);
        }

        public RegionView? NavigateForward<TTargetView>()
            where TTargetView : FrameworkElement
        {
            return NavigateForward<TTargetView, object>();
        }

        public RegionView? NavigateForward<TTargetView, TParameters>(TParameters? parameters = default)
            where TTargetView : FrameworkElement
        {
            var targetViewName = typeof(TTargetView).FullName!;

            return NavigateForward(targetViewName, parameters);
        }

        public RegionView? NavigateBack()
        {
            return NavigateBack<object>();
        }

        public RegionView? NavigateBack<TParameters>(TParameters? parameters = default)
        {
            var targetViewName = _history.Skip(1).FirstOrDefault() ?? string.Empty;

            return NavigateBack(targetViewName, parameters);
        }

        public RegionView? NavigateBack<TTargetView, TParameters>(TParameters? parameters = default)
            where TTargetView : FrameworkElement
        {
            var targetViewName = typeof(TTargetView).FullName!;

            return NavigateBack(targetViewName, parameters);
        }

        public void Clear()
        {
            NavigateBack<object>(EmptyView, null);
        }

        public bool CanNavigateForward()
        {
            return ActiveView?.Options.CanNavigateFrom == true || ActiveView == null;
        }

        public bool CanNavigateBack()
        {
            return ActiveView?.Options.CanNavigateFrom == true;
        }

        private RegionView? Navigate<TParameters>(string targetViewName, TParameters? parameters)
        {
            var isInHistory = _history.Contains(targetViewName);
            if (isInHistory)
            {
                return NavigateBack(targetViewName, parameters);
            }
            else
            {
                return NavigateForward(targetViewName, parameters);
            }
        }

        private RegionView? NavigateForward<TParameters>(string targetViewName, TParameters? parameters)
        {
            if (!CanNavigateForwardInternal(targetViewName))
            {
                return ActiveView;
            }

            _tracer.TraceInformation($"Navigating forward to view {targetViewName} in region {Name}.");

            _history.Push(targetViewName);

            var targetView = FindOrCreateView(targetViewName);

            return SetActiveView(targetView, targetViewName, parameters);
        }

        private RegionView? NavigateBack<TParameters>(string targetViewName, TParameters? parameters)
        {
            if (!CanNavigateBackInternal(targetViewName))
            {
                return ActiveView;
            }

            _tracer.TraceInformation($"Navigating back to view {targetViewName} in region {Name}.");

            var activeViewName = _history.Pop();
            var activeView = TryFindView(activeViewName);
            if (activeView != null)
            {
                RemoveView(activeView);
            }

            if (targetViewName == EmptyView)
            {
                _history.Clear();
                _views.Clear();
            }

            var targetViewIndex = _history.IndexOf(targetViewName);
            if (targetViewIndex > 0)
            {
                _history.PopRange(targetViewIndex)
                    .Select(TryFindView)
                    .WhereNotNull()
                    .ToList()
                    .ForEach(RemoveView);
            }

            var targetView = TryFindView(targetViewName);

            return SetActiveView(targetView, targetViewName, parameters);
        }

        private RegionView? SetActiveView<TParameters>(RegionView? view, string viewName, TParameters? parameters)
        {
            if (!_viewHost.TryGetTarget(out var viewHost))
            {
                _tracer.TraceInformation($"ViewHost is not set.");
                return ActiveView;
            }

            if (viewHost.IsLoaded is false)
            {
                _pendingView = view;
            }

            viewHost.WhenLoaded(() =>
            {
                _pendingView = null;

                var navigationArgs = new NavigateEventArgs(this, viewName);
                var activeViewChangeArgs = new ActiveViewChangedEventArgs(Name, ActiveView, view);

                Navigating.Invoke(this, navigationArgs);

                ActiveView?.OnNavigatedFrom(view);
                ActiveView = view;

                viewHost.Content = ActiveView?.View;

                ActiveView?.SetParameters(parameters);
                ActiveView?.OnNavigatedTo(view);

                ActiveViewChanged.Invoke(this, activeViewChangeArgs);

                Navigated.Invoke(this, navigationArgs);
            });

            return view;
        }

        private bool CanNavigateForwardInternal(string targetViewName)
        {
            if (!CanNavigateForward())
            {
                _tracer.TraceInformation($"Cant navigate forward from view {targetViewName} in region {Name}");
                return false;
            }

            if (ActiveView?.Name == targetViewName)
            {
                _tracer.TraceInformation($"Already navigated to view {targetViewName} in region {Name}");
                return false;
            }

            return true;
        }

        private bool CanNavigateBackInternal(string targetViewName)
        {
            if (!CanNavigateBack())
            {
                _tracer.TraceInformation($"Cant navigate back to view {targetViewName} in region {Name}. Back navigation suppressed by Options.");
                return false;
            }

            if (!_history.Contains(targetViewName) && targetViewName != EmptyView)
            {
                _tracer.TraceInformation($"Cant navigate back to view {targetViewName} in region {Name}. Target view does not present in history.");
                return false;
            }

            if (ActiveView?.Name == targetViewName)
            {
                _tracer.TraceInformation($"Already navigated to view {targetViewName} in region {Name}");
                return false;
            }

            return true;
        }

        private void AddView(RegionView view)
        {
            _tracer.TraceInformation($"Add {view.Name} to cache in region {Name}");

            _views.Add(view);

            ViewAdded?.Invoke(this, new ViewAddedEventArgs(Name, view));
        }

        private void RemoveView(RegionView view)
        {
            if (_history.Contains(view.Name))
            {
                _tracer.TraceInformation(
                    $"Removing {view.Name} from cache in region {Name} skipped. " +
                    $"View present in navigation history.");

                return;
            }

            if (view.Options.KeepAlive)
            {
                _tracer.TraceInformation(
                    $"Removing {view.Name} from cache in region {Name} skipped. " +
                    $"View has KeepAlive option.");

                return;
            }

            _tracer.TraceInformation($"Remove {view.Name} from cache in region {Name}");

            _views.Remove(view);

            ViewRemoved?.Invoke(this, new ViewRemovedEventArgs(Name, view));
        }

        private RegionView FindOrCreateView(string viewName)
        {
            var view = TryFindView(viewName);
            if (view == null)
            {
                view = new RegionView(viewName, _viewFactory, this);

                AddView(view);

                _tracer.TraceInformation($"Created new view {viewName} in region {Name}");
            }
            else
            {
                _tracer.TraceInformation($"Found existing view {viewName} in region {Name}");
            }

            return view;
        }

        private RegionView? TryFindView(string? viewName)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                return null;
            }

            return _views.FirstOrDefault(view => view.Name == viewName);
        }

        private void OnActiveViewChanged(object? sender, ActiveViewChangedEventArgs e)
        {
            var oldView = e.OldView;
            if (oldView?.Options.SkipOnBackNavigation == true)
            {
                _history.Remove(oldView.Name);

                RemoveView(oldView);
            }
        }

        private const string EmptyView = ":EmptyView:";
        private RegionView? _activeView;
        private RegionView? _pendingView;
        private WeakReference<ContentControl?> _viewHost = new(null);

        private readonly List<RegionView> _views = new();
        private readonly NavigationHistory _history = new();
        private readonly IRegionViewFactory _viewFactory;
        private readonly IList<IRegionBehavior>? _behaviors;

        private static readonly ComponentTracer _tracer = ComponentTracer.Get(UIKitComponentTracers.Navigation);
    }
}