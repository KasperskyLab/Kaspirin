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

public sealed class Regions : IEnumerable<Region>
{
    public Regions(
        IRegionViewFactory regionViewFactory,
        IRegionBehaviorsRegistry regionBehaviorsRegistry)
    {
        _regionViewFactory = Guard.EnsureArgumentIsNotNull(regionViewFactory);
        _regionBehaviorsRegistry = Guard.EnsureArgumentIsNotNull(regionBehaviorsRegistry);
    }

    public event EventHandler<RegionEventArgs> RegionCreated = EmptyDelegate.For<EventHandler<RegionEventArgs>>();

    public event EventHandler<RegionEventArgs> RegionRemoved = EmptyDelegate.For<EventHandler<RegionEventArgs>>();

    public event EventHandler<ViewChangedEventArgs> ActiveViewChanged = EmptyDelegate.For<EventHandler<ViewChangedEventArgs>>();

    public event EventHandler<NavigateEventArgs> Navigating = EmptyDelegate.For<EventHandler<NavigateEventArgs>>();

    public event EventHandler<NavigateEventArgs> Navigated = EmptyDelegate.For<EventHandler<NavigateEventArgs>>();

    public Region this[string regionName] => GetRegion(regionName);

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
        => ServiceLocator.GetService<Regions>().CreateRegion(d);

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

    public Region GetRegion(string regionName)
    {
        Guard.ArgumentIsNotNullOrEmpty(regionName);

        var region = TryGetRegion(regionName);
        if (region == null)
        {
            throw new InvalidOperationException($"Region with name '{regionName}' does not exist.");
        }

        return region;
    }

    public Region? TryGetRegion(string regionName)
    {
        Guard.ArgumentIsNotNullOrEmpty(regionName);

        return _regions.FirstOrDefault(r => r.Name.Equals(regionName, StringComparison.OrdinalIgnoreCase));
    }

    public void WhenRegionCreated(string regionName, Action<Region> action)
    {
        Guard.ArgumentIsNotNullOrEmpty(regionName);
        Guard.ArgumentIsNotNull(action);

        var region = TryGetRegion(regionName);
        if (region != null)
        {
            action.Invoke(region);

            return;
        }

        _pendingRegionCallbacks.Add((regionName, action));

        _tracer.TraceInformation($"Region {regionName} does not exist yet. Callback deferred until region creation.");
    }

    private void CreateRegion(DependencyObject control)
    {
        var regionHost = Guard.EnsureArgumentIsInstanceOfType<ContentControl>(control);
        var regionName = Guard.EnsureArgumentIsNotNullOrEmpty(GetRegionName(control));

        var regionExists = TryGetRegion(regionName) != null;
        if (regionExists)
        {
            Guard.Fail($"Region with name '{regionName}' already exists.");
        }

        var region = new Region(regionName, regionHost, _regionViewFactory, _regionBehaviorsRegistry);

        regionHost.WhenUnloaded(() => RemoveRegion(region));

        AddRegion(region);

        var matchedCallbacks = _pendingRegionCallbacks
            .Where(pending => pending.RegionName.Equals(region.Name, StringComparison.OrdinalIgnoreCase))
            .ToList();

        matchedCallbacks.ForEach(matched => _pendingRegionCallbacks.Remove(matched));
        matchedCallbacks.ForEach(matched => matched.Action.Invoke(region));
    }

    private void AddRegion(Region region)
    {
        if (_regions.Contains(region))
        {
            return;
        }

        region.Navigated += OnNavigated;
        region.Navigating += OnNavigating;
        region.ActiveViewChanged += OnActiveViewChanged;

        _regions.Add(region);

        _tracer.TraceInformation($"Region {region.Name} added");

        RegionCreated.Invoke(this, new(region));
    }

    private void RemoveRegion(Region region)
    {
        if (_regions.Contains(region) is false)
        {
            return;
        }

        region.Navigated -= OnNavigated;
        region.Navigating -= OnNavigating;
        region.ActiveViewChanged -= OnActiveViewChanged;

        _regions.Remove(region);

        _tracer.TraceInformation($"Region {region.Name} removed. Host control was unloaded.");

        RegionRemoved.Invoke(this, new(region));
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

    private void OnActiveViewChanged(object? sender, ViewChangedEventArgs args)
    {
        _tracer.TraceInformation($"Active view changed in region {args.RegionName}: old view {args.OldView}, new view {args.NewView}");

        ActiveViewChanged.Invoke(sender, args);
    }

    private readonly List<Region> _regions = new();
    private readonly List<(string RegionName, Action<Region> Action)> _pendingRegionCallbacks = new();
    private readonly IRegionViewFactory _regionViewFactory;
    private readonly IRegionBehaviorsRegistry _regionBehaviorsRegistry;

    private static readonly ComponentTracer _tracer = ComponentTracer.Get(UIKitComponentTracers.Navigation);
}
