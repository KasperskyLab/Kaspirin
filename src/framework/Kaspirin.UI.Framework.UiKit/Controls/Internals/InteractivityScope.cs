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
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals;

internal abstract class InteractivityScope<TInteractivityControl> where TInteractivityControl : FrameworkElement
{
    public InteractivityScope()
    {
        _scopeDictionary = new();

        Tracer = ComponentTracer.Get(UIKitComponentTracers.Interactivity, this);
    }

    #region Scope

    public static InteractivityScope<TInteractivityControl> GetScope(DependencyObject obj)
        => (InteractivityScope<TInteractivityControl>)obj.GetValue(_scopeProperty);

    public static void SetScope(DependencyObject obj, InteractivityScope<TInteractivityControl> value)
        => obj.SetValue(_scopeProperty, value);

    private static readonly DependencyProperty _scopeProperty = DependencyProperty.RegisterAttached(
        "Scope",
        typeof(InteractivityScope<TInteractivityControl>),
        typeof(InteractivityScope<TInteractivityControl>),
        new PropertyMetadata(default(InteractivityScope<TInteractivityControl>)));

    #endregion

    #region AssociatedNotificationView

    public static NotificationView GetAssociatedNotificationView(DependencyObject obj)
        => (NotificationView)obj.GetValue(_associatedNotificationViewProperty);

    public static void SetAssociatedNotificationView(DependencyObject obj, NotificationView value)
        => obj.SetValue(_associatedNotificationViewProperty, value);

    private static readonly DependencyProperty _associatedNotificationViewProperty = DependencyProperty.RegisterAttached(
        "AssociatedNotificationView",
        typeof(NotificationView),
        typeof(InteractivityScope<TInteractivityControl>),
        new PropertyMetadata(default(NotificationView)));

    #endregion

    public void Register(TInteractivityControl control, string scope, Action? continueCallback)
    {
        var collection = GetScopeInstance(control);
        if (collection == null)
        {
            Tracer.TraceMethodWarning($"Control detached from Visual Tree. {control}");
            continueCallback?.Invoke();

            return;
        }

        AssociateNotificationView(control);

        var scopedControls = collection.GetScopedControls(scope);
        if (scopedControls.Contains(control))
        {
            Tracer.TraceMethodWarning($"Control already registered. {control}");
            continueCallback?.Invoke();

            return;
        }

        OnRegister(control, scopedControls, continueCallback);

        collection.SetScopedControls(scope, scopedControls);

        Tracer.TraceMethodInfo($"Control registered. {control}");
    }

    public void Unregister(TInteractivityControl control, string scope, Action? continueCallback)
    {
        var collection = GetScopeInstance(control);
        if (collection == null)
        {
            Tracer.TraceMethodWarning($"Control detached from Visual Tree. {control}");
            continueCallback?.Invoke();

            return;
        }

        AssociateNotificationView(control);

        var scopedControls = collection.GetScopedControls(scope);
        if (!scopedControls.Contains(control))
        {
            Tracer.TraceMethodWarning($"Control is not registered. {control}");
            continueCallback?.Invoke();

            return;
        }

        OnUnregister(control, scopedControls, continueCallback);

        collection.SetScopedControls(scope, scopedControls);

        Tracer.TraceMethodInfo($"Control unregistered. {control}");
    }

    protected ComponentTracer Tracer { get; }

    protected abstract void OnRegister(TInteractivityControl control, List<TInteractivityControl> scopedControls, Action? continueCallback);

    protected abstract void OnUnregister(TInteractivityControl control, List<TInteractivityControl> scopedControls, Action? continueCallback);

    protected abstract InteractivityScope<TInteractivityControl> CreateScopeInstance();

    private List<TInteractivityControl> GetScopedControls(string scope)
    {
        if (_scopeDictionary.TryGetValue(scope, out var scopeCollection))
        {
            _scopeDictionary[scope] = scopeCollection
                .Select(control => GetAssociatedNotificationView(control).FindVisualChild<TInteractivityControl>(d => d.IsVisible))
                .WhereNotNull()
                .ToList();

            var controlsCount = _scopeDictionary[scope].Count;
            if (controlsCount > 0)
            {
                Tracer.TraceMethodDebug(
                    $"Scope {scope} found with {_scopeDictionary[scope].Count} interactivity controls: " +
                    $"{Environment.NewLine}{string.Join(Environment.NewLine, _scopeDictionary[scope])}");
            }
            else
            {
                Tracer.TraceMethodDebug($"Scope {scope} found without interactivity controls.");
            }
        }
        else
        {
            Tracer.TraceMethodDebug($"Scope {scope} created.");

            _scopeDictionary[scope] = new();
        }

        return _scopeDictionary[scope];
    }

    private void SetScopedControls(string scope, List<TInteractivityControl> controls)
    {
        _scopeDictionary[scope] = controls;
    }

    private InteractivityScope<TInteractivityControl>? GetScopeInstance(TInteractivityControl target)
    {
        var scopeCollection = CreateScopeInstance();

        var targetWindow = target.GetWindow();
        if (targetWindow == null)
        {
            var cachedNotification = GetAssociatedNotificationView(target);
            if (cachedNotification != null)
            {
                var visibleControl = cachedNotification.FindVisualChild<TInteractivityControl>(d => d.IsVisible);
                if (visibleControl != null)
                {
                    return GetScopeInstance(visibleControl);
                }
            }
        }
        else if (GetScope(targetWindow) is InteractivityScope<TInteractivityControl> cachedCollection)
        {
            scopeCollection = cachedCollection;
        }
        else
        {
            SetScope(targetWindow, scopeCollection);
        }

        return scopeCollection;
    }

    private void AssociateNotificationView(TInteractivityControl control)
    {
        var controlNotificationView = control.FindVisualParent<NotificationView>();
        if (controlNotificationView != null)
        {
            var savedNotificationView = GetAssociatedNotificationView(control);
            if (savedNotificationView != controlNotificationView)
            {
                Tracer.TraceInformation($"Control associated to new {nameof(NotificationView)}. {control}");
            }

            SetAssociatedNotificationView(control, controlNotificationView);
        }
    }

    private readonly Dictionary<string, List<TInteractivityControl>> _scopeDictionary;
}
