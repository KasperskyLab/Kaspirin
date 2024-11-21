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
    public sealed class Regions : IEnumerable<Region>
    {
        public Regions(
            IRegionViewFactory regionViewFactory,
            IRegionBehaviorsRegistry regionBehaviorsRegistry)
        {
            _regionViewFactory = Guard.EnsureArgumentIsNotNull(regionViewFactory);
            _regionBehaviorsRegistry = Guard.EnsureArgumentIsNotNull(regionBehaviorsRegistry);
        }

        public event EventHandler<RegionCreatedEventArgs> RegionCreated = (o, e) => { };

        public event EventHandler<ActiveViewChangedEventArgs> ActiveViewChanged = (o, e) => { };

        public event EventHandler<NavigateEventArgs> Navigating = (o, e) => { };

        public event EventHandler<NavigateEventArgs> Navigated = (o, e) => { };

        #region RegionName

        public static string GetRegionName(DependencyObject obj)
            => (string)obj.GetValue(RegionNameProperty);

        public static void SetRegionName(DependencyObject obj, string value)
            => obj.SetValue(RegionNameProperty, value);

        public static readonly DependencyProperty RegionNameProperty = DependencyProperty.RegisterAttached(
            "RegionName",
            typeof(string),
            typeof(Regions),
            new PropertyMetadata(default(string), OnRegionNameChanged));

        private static void OnRegionNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ServiceLocator.Instance.GetService<Regions>().CreateRegion(d);

        #endregion

        #region IEnumerable

        public IEnumerator<Region> GetEnumerator()
        {
            return ((IEnumerable<Region>)_regions.ToList()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_regions.ToList()).GetEnumerator();
        }

        #endregion

        private void CreateRegion(DependencyObject control)
        {
            var regionHost = Guard.EnsureArgumentIsInstanceOfType<ContentControl>(control);
            var regionName = Guard.EnsureArgumentIsNotNullOrEmpty(GetRegionName(control));

            var region = _regions.FirstOrDefault(region => region.Name == regionName);
            if (region == null)
            {
                region = new Region(regionName, regionHost, _regionViewFactory, _regionBehaviorsRegistry);

                region.Navigated += OnNavigated;
                region.Navigating += OnNavigating;
                region.ActiveViewChanged += OnActiveViewChanged;

                _regions.Add(region);

                _tracer.TraceInformation($"Region {regionName} created");

                RegionCreated.Invoke(this, new RegionCreatedEventArgs(region));
            }
            else
            {
                _tracer.TraceInformation($"Region {regionName} already exists");
            }
        }

        private void OnNavigating(object? sender, NavigateEventArgs args)
        {
            _tracer.TraceInformation($"Navigating to view {args.TargetViewName} in region {args.RegionName}");

            Navigating.Invoke(sender, args);
        }

        private void OnNavigated(object? sender, NavigateEventArgs args)
        {
            _tracer.TraceInformation($"Navigated to view {args.ActiveViewName} in region {args.RegionName}");

            Navigated.Invoke(sender, args);
        }

        private void OnActiveViewChanged(object? sender, ActiveViewChangedEventArgs args)
        {
            _tracer.TraceInformation($"Active view changed in region {args.RegionName}: old view {args.OldView}, new view {args.NewView}");

            ActiveViewChanged.Invoke(sender, args);
        }

        private readonly List<Region> _regions = new();
        private readonly IRegionViewFactory _regionViewFactory;
        private readonly IRegionBehaviorsRegistry _regionBehaviorsRegistry;

        private static readonly ComponentTracer _tracer = ComponentTracer.Get(UIKitComponentTracers.Navigation);
    }
}
