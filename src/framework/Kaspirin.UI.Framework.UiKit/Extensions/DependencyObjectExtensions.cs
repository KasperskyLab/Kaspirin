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
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Kaspirin.UI.Framework.Services.Internals;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Extensions;

public static class DependencyObjectExtensions
{
    public static bool HasLogicalParent(this DependencyObject dependencyObject, DependencyObject parent)
        => dependencyObject.TraverseLogicalParents().Any(p => p == parent);

    public static bool HasVisualParent(this DependencyObject dependencyObject, DependencyObject parent)
        => dependencyObject.TraverseVisualParents().Any(p => p == parent);

    public static T? FindLogicalParent<T>(this DependencyObject dependencyObject)
        where T : DependencyObject
        => dependencyObject.TraverseLogicalParents().OfType<T>().FirstOrDefault();

    public static T? FindLogicalParent<T>(this DependencyObject dependencyObject, Func<T, bool> condition)
        where T : DependencyObject
        => dependencyObject.TraverseLogicalParents().OfType<T>().FirstOrDefault(condition);

    public static IEnumerable<T> FindLogicalParents<T>(this DependencyObject dependencyObject)
        where T : DependencyObject
        => dependencyObject.TraverseLogicalParents().OfType<T>();

    public static IEnumerable<T> FindLogicalParents<T>(this DependencyObject dependencyObject, Func<T, bool> condition)
        where T : DependencyObject
        => dependencyObject.TraverseLogicalParents().OfType<T>().Where(condition);

    public static T? FindVisualParent<T>(this DependencyObject dependencyObject)
        where T : DependencyObject
        => dependencyObject.TraverseVisualParents().OfType<T>().FirstOrDefault();

    public static T? FindVisualParent<T>(this DependencyObject dependencyObject, Func<T, bool> condition)
        where T : DependencyObject
        => dependencyObject.TraverseVisualParents().OfType<T>().FirstOrDefault(condition);

    public static IEnumerable<T> FindVisualParents<T>(this DependencyObject dependencyObject)
        where T : DependencyObject
        => dependencyObject.TraverseVisualParents().OfType<T>();

    public static IEnumerable<T> FindVisualParents<T>(this DependencyObject dependencyObject, Func<T, bool> condition)
        where T : DependencyObject
        => dependencyObject.TraverseVisualParents().OfType<T>().Where(condition);

    public static T? FindVisualChild<T>(this DependencyObject dependencyObject)
        where T : DependencyObject
        => TraverseService.TraverseVisualChildren(dependencyObject, null, TraverseChildrenOptions.None).OfType<T>().FirstOrDefault();

    public static T? FindVisualChild<T>(this DependencyObject dependencyObject, Func<T, bool> condition)
        where T : DependencyObject
        => TraverseService.TraverseVisualChildren(dependencyObject, null, TraverseChildrenOptions.None).OfType<T>().FirstOrDefault(condition);

    public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject dependencyObject)
        where T : DependencyObject
        => TraverseService.TraverseVisualChildren(dependencyObject, null, TraverseChildrenOptions.None).OfType<T>();

    public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject dependencyObject, Func<T, bool> condition)
        where T : DependencyObject
        => TraverseService.TraverseVisualChildren(dependencyObject, null, TraverseChildrenOptions.None).OfType<T>().Where(condition);

    public static IEnumerable<DependencyObject> TraverseVisualChildren(
       this DependencyObject dependencyObject,
       Func<DependencyObject, bool>? traverseCondition = null)
        => TraverseService.TraverseVisualChildren(dependencyObject, traverseCondition, TraverseChildrenOptions.None);

    public static IEnumerable<DependencyObject> TraverseVisualParents(
        this DependencyObject dependencyObject,
        Func<DependencyObject, bool>? traverseCondition = null)
        => TraverseService.TraverseParents(dependencyObject, traverseCondition, TraverseParentOptions.VisualTraverse);

    public static IEnumerable<DependencyObject> TraverseLogicalParents(
        this DependencyObject dependencyObject,
        Func<DependencyObject, bool>? traverseCondition = null)
        => TraverseService.TraverseParents(dependencyObject, traverseCondition, TraverseParentOptions.LogicalTraverse);

    public static object? GetDataContext(this DependencyObject dependencyObject)
    {
        Guard.ArgumentIsNotNull(dependencyObject);

        return new FrameworkObject(dependencyObject).DataContext;
    }

    public static Window GetWindow(this DependencyObject dependencyObject)
    {
        Guard.ArgumentIsNotNull(dependencyObject);

        return Window.GetWindow(dependencyObject);
    }

    public static T GetValue<T>(this DependencyObject dependencyObject, DependencyProperty dependencyProperty)
    {
        Guard.ArgumentIsNotNull(dependencyObject);
        Guard.ArgumentIsNotNull(dependencyProperty);

        return Guard.EnsureIsInstanceOfType<T>(dependencyObject.GetValue(dependencyProperty));
    }

    public static void SetResource(this DependencyObject dependencyObject, DependencyProperty dependencyProperty, BaseLocalizationMarkupExtension resource)
    {
        Guard.ArgumentIsNotNull(dependencyObject);
        Guard.ArgumentIsNotNull(dependencyProperty);
        Guard.ArgumentIsNotNull(resource);

        BindingOperations.SetBinding(dependencyObject, dependencyProperty, resource.ProvideBinding());
    }

    public static void WhenLoaded(this DependencyObject dependencyObject, Action action)
    {
        var fo = new FrameworkObject(dependencyObject);

        EventSubscriber.OnceOrNow(
            source: dependencyObject,
            condition: depObj => fo.IsLoaded,
            subscribeCallback: eh => fo.Loaded += eh,
            unsubscribeCallback: eh => fo.Loaded -= eh,
            action: action);
    }

    public static void WhenInitialized(this DependencyObject dependencyObject, Action action)
    {
        var fo = new FrameworkObject(dependencyObject);

        EventSubscriber.OnceOrNow(
            source: dependencyObject,
            condition: depObj => fo.IsInitialized,
            subscribeCallback: eh => fo.Initialized += eh,
            unsubscribeCallback: eh => fo.Initialized -= eh,
            action: action);
    }

    public static void WhenUnloaded(this DependencyObject dependencyObject, Action action)
    {
        var fo = new FrameworkObject(dependencyObject);

        EventSubscriber.Once(
            source: dependencyObject,
            subscribeCallback: eh => fo.Unloaded += eh,
            unsubscribeCallback: eh => fo.Unloaded -= eh,
            action: action);
    }
}
