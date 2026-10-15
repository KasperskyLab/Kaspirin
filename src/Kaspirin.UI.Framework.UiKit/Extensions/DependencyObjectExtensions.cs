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

/// <summary>
///     Extension methods for <see cref="DependencyObject" />.
/// </summary>
public static class DependencyObjectExtensions
{
    /// <summary>
    ///     Checks whether the <paramref name="parent" /> object is included among the logical parents of the element.
    /// </summary>
    /// <param name="dependencyObject">
    ///     The element that is being checked.
    /// </param>
    /// <param name="parent">
    ///     The parent object you are looking for.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the object is found among the logical parents, otherwise - <see langword="false" />.
    /// </returns>
    public static bool HasLogicalParent(this DependencyObject dependencyObject, DependencyObject parent)
        => dependencyObject.TraverseLogicalParents().Any(p => p == parent);

    /// <summary>
    ///     Checks whether the <paramref name="parent" /> object is included among the visual parents of the element.
    /// </summary>
    /// <param name="dependencyObject">
    ///     The element that is being checked.
    /// </param>
    /// <param name="parent">
    ///     The parent object you are looking for.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the object is found among the visual parents, otherwise - <see langword="false" />.
    /// </returns>
    public static bool HasVisualParent(this DependencyObject dependencyObject, DependencyObject parent)
        => dependencyObject.TraverseVisualParents().Any(p => p == parent);

    /// <summary>
    ///     Searches for the nearest parent of type <typeparamref name="T" /> in the logical tree.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of parent you are looking for.
    /// </typeparam>
    /// <param name="dependencyObject">
    ///     The element that starts the search.
    /// </param>
    /// <returns>
    ///     Returns the found parent or <see langword="null" /> if the parent is not found.
    /// </returns>
    public static T? FindLogicalParent<T>(this DependencyObject dependencyObject)
        where T : DependencyObject
        => dependencyObject.TraverseLogicalParents().OfType<T>().FirstOrDefault();

    /// <summary>
    ///     Searches for the nearest parent of type <typeparamref name="T" /> in a logical tree that satisfies
    ///     the condition <paramref name="condition" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of parent you are looking for.
    /// </typeparam>
    /// <param name="dependencyObject">
    ///     The element that starts the search.
    /// </param>
    /// <param name="condition">
    ///     A condition that must be satisfied by the parent in question.
    /// </param>
    /// <returns>
    ///     Returns the found parent or <see langword="null" /> if the parent is not found.
    /// </returns>
    public static T? FindLogicalParent<T>(this DependencyObject dependencyObject, Func<T, bool> condition)
        where T : DependencyObject
        => dependencyObject.TraverseLogicalParents().OfType<T>().FirstOrDefault(condition);

    /// <summary>
    ///     Searches for all parents of type <typeparamref name="T" /> in the logical tree.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of parents you are looking for.
    /// </typeparam>
    /// <param name="dependencyObject">
    ///     The element that starts the search.
    /// </param>
    /// <returns>
    ///     Returns the sequence of found parents.
    /// </returns>
    public static IEnumerable<T> FindLogicalParents<T>(this DependencyObject dependencyObject)
        where T : DependencyObject
        => dependencyObject.TraverseLogicalParents().OfType<T>();

    /// <summary>
    ///     Searches for all parents of type <typeparamref name="T" /> in the logical tree that meet the
    ///     condition <paramref name="condition" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of parents you are looking for.
    /// </typeparam>
    /// <param name="dependencyObject">
    ///     The element that starts the search.
    /// </param>
    /// <param name="condition">
    ///     A condition that must be satisfied by the parents in question.
    /// </param>
    /// <returns>
    ///     Returns the sequence of found parents.
    /// </returns>
    public static IEnumerable<T> FindLogicalParents<T>(this DependencyObject dependencyObject, Func<T, bool> condition)
        where T : DependencyObject
        => dependencyObject.TraverseLogicalParents().OfType<T>().Where(condition);

    /// <summary>
    ///     Searches for the nearest parent of type <typeparamref name="T" /> in the visual tree.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of parent you are looking for.
    /// </typeparam>
    /// <param name="dependencyObject">
    ///     The element that starts the search.
    /// </param>
    /// <returns>
    ///     Returns the found parent or <see langword="null" /> if the parent is not found.
    /// </returns>
    public static T? FindVisualParent<T>(this DependencyObject dependencyObject)
        where T : DependencyObject
        => dependencyObject.TraverseVisualParents().OfType<T>().FirstOrDefault();

    /// <summary>
    ///     Searches for the nearest parent of type <typeparamref name="T" /> in the visual tree that satisfies
    ///     the condition <paramref name="condition" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of parent you are looking for.
    /// </typeparam>
    /// <param name="dependencyObject">
    ///     The element that starts the search.
    /// </param>
    /// <param name="condition">
    ///     A condition that must be satisfied by the parent in question.
    /// </param>
    /// <returns>
    ///     Returns the found parent or <see langword="null" /> if the parent is not found.
    /// </returns>
    public static T? FindVisualParent<T>(this DependencyObject dependencyObject, Func<T, bool> condition)
        where T : DependencyObject
        => dependencyObject.TraverseVisualParents().OfType<T>().FirstOrDefault(condition);

    /// <summary>
    ///     Searches for all parents of type <typeparamref name="T" /> in the visual tree.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of parents you are looking for.
    /// </typeparam>
    /// <param name="dependencyObject">
    ///     The element that starts the search.
    /// </param>
    /// <returns>
    ///     Returns the sequence of found parents.
    /// </returns>
    public static IEnumerable<T> FindVisualParents<T>(this DependencyObject dependencyObject)
        where T : DependencyObject
        => dependencyObject.TraverseVisualParents().OfType<T>();

    /// <summary>
    ///     Searches for all parents of type <typeparamref name="T" /> in the visual tree that meet the
    ///     condition <paramref name="condition" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of parents you are looking for.
    /// </typeparam>
    /// <param name="dependencyObject">
    ///     The element that starts the search.
    /// </param>
    /// <param name="condition">
    ///     A condition that must be satisfied by the parents in question.
    /// </param>
    /// <returns>
    ///     Returns the sequence of found parents.
    /// </returns>
    public static IEnumerable<T> FindVisualParents<T>(this DependencyObject dependencyObject, Func<T, bool> condition)
        where T : DependencyObject
        => dependencyObject.TraverseVisualParents().OfType<T>().Where(condition);

    /// <summary>
    ///     Searches for the first descendant of type <typeparamref name="T" /> in the visual tree.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the desired descendant.
    /// </typeparam>
    /// <param name="dependencyObject">
    ///     The element that starts the search.
    /// </param>
    /// <returns>
    ///     Returns the found descendant or <see langword="null" /> if the descendant is not found.
    /// </returns>
    public static T? FindVisualChild<T>(this DependencyObject dependencyObject)
        where T : DependencyObject
        => TraverseService.TraverseVisualChildren(dependencyObject, null, TraverseChildrenOptions.None).OfType<T>().FirstOrDefault();

    /// <summary>
    ///     Searches for the first descendant of the type <typeparamref name="T" /> in the visual tree
    ///     that satisfies the condition <paramref name="condition" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the desired descendant.
    /// </typeparam>
    /// <param name="dependencyObject">
    ///     The element that starts the search.
    /// </param>
    /// <param name="condition">
    ///     A condition that must be satisfied by the desired descendant.
    /// </param>
    /// <returns>
    ///     Returns the found descendant or <see langword="null" /> if the descendant is not found.
    /// </returns>
    public static T? FindVisualChild<T>(this DependencyObject dependencyObject, Func<T, bool> condition)
        where T : DependencyObject
        => TraverseService.TraverseVisualChildren(dependencyObject, null, TraverseChildrenOptions.None).OfType<T>().FirstOrDefault(condition);

    /// <summary>
    ///     Searches for all descendants of type <typeparamref name="T" /> in the visual tree.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of descendants you are looking for.
    /// </typeparam>
    /// <param name="dependencyObject">
    ///     The element that starts the search.
    /// </param>
    /// <returns>
    ///     Returns the sequence of found descendants.
    /// </returns>
    public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject dependencyObject)
        where T : DependencyObject
        => TraverseService.TraverseVisualChildren(dependencyObject, null, TraverseChildrenOptions.None).OfType<T>();

    /// <summary>
    ///     Searches for all descendants of the type <typeparamref name="T" /> in the visual tree that
    ///     meet the condition <paramref name="condition" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of descendants you are looking for.
    /// </typeparam>
    /// <param name="dependencyObject">
    ///     The element that starts the search.
    /// </param>
    /// <param name="condition">
    ///     A condition that must be satisfied by the desired descendants.
    /// </param>
    /// <returns>
    ///     Returns the sequence of found descendants.
    /// </returns>
    public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject dependencyObject, Func<T, bool> condition)
        where T : DependencyObject
        => TraverseService.TraverseVisualChildren(dependencyObject, null, TraverseChildrenOptions.None).OfType<T>().Where(condition);

    /// <summary>
    ///     Traverses the descendants of an element in the visual tree.
    /// </summary>
    /// <param name="dependencyObject">
    ///     The element that the crawl starts with.
    /// </param>
    /// <param name="traverseCondition">
    ///     Condition for continuing the crawl. If <see langword="false" /> is returned for an element, its subtree is not bypassed.
    /// </param>
    /// <returns>
    ///     Returns a sequence of descendants of an element.
    /// </returns>
    public static IEnumerable<DependencyObject> TraverseVisualChildren(
       this DependencyObject dependencyObject,
       Func<DependencyObject, bool>? traverseCondition = null)
        => TraverseService.TraverseVisualChildren(dependencyObject, traverseCondition, TraverseChildrenOptions.None);

    /// <summary>
    ///     Bypasses the parents of the element in the visual tree.
    /// </summary>
    /// <param name="dependencyObject">
    ///     The element that the crawl starts with.
    /// </param>
    /// <param name="traverseCondition">
    ///     Condition for continuing the crawl. If <see langword="false" /> is returned for the element, the crawl is stopped.
    /// </param>
    /// <returns>
    ///     Returns the sequence of the element's parents.
    /// </returns>
    public static IEnumerable<DependencyObject> TraverseVisualParents(
        this DependencyObject dependencyObject,
        Func<DependencyObject, bool>? traverseCondition = null)
        => TraverseService.TraverseParents(dependencyObject, traverseCondition, TraverseParentOptions.VisualTraverse);

    /// <summary>
    ///     Bypasses the parents of the element in the logical tree.
    /// </summary>
    /// <param name="dependencyObject">
    ///     The element that the crawl starts with.
    /// </param>
    /// <param name="traverseCondition">
    ///     Condition for continuing the crawl. If <see langword="false" /> is returned for the element, the crawl is stopped.
    /// </param>
    /// <returns>
    ///     Returns the sequence of the element's parents.
    /// </returns>
    public static IEnumerable<DependencyObject> TraverseLogicalParents(
        this DependencyObject dependencyObject,
        Func<DependencyObject, bool>? traverseCondition = null)
        => TraverseService.TraverseParents(dependencyObject, traverseCondition, TraverseParentOptions.LogicalTraverse);

    /// <summary>
    ///     Returns the context of the element data.
    /// </summary>
    /// <param name="dependencyObject">
    ///     The element whose data context is being requested.
    /// </param>
    /// <returns>
    ///     Returns the data context or <see langword="null" /> if it is not specified.
    /// </returns>
    public static object? GetDataContext(this DependencyObject dependencyObject)
    {
        Guard.ArgumentIsNotNull(dependencyObject);

        return new FrameworkObject(dependencyObject).DataContext;
    }

    /// <summary>
    ///     Returns the window where the element is located.
    /// </summary>
    /// <param name="dependencyObject">
    ///     The element for which the window is being searched.
    /// </param>
    /// <returns>
    ///     Returns the found window.
    /// </returns>
    public static Window GetWindow(this DependencyObject dependencyObject)
    {
        Guard.ArgumentIsNotNull(dependencyObject);

        return Window.GetWindow(dependencyObject);
    }

    /// <summary>
    ///     Returns the value of the dependency property, converted to the type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the property value.
    /// </typeparam>
    /// <param name="dependencyObject">
    ///     The element whose property value is being requested.
    /// </param>
    /// <param name="dependencyProperty">
    ///     Dependency property.
    /// </param>
    /// <returns>
    ///     Returns the property value or the default value for the type <typeparamref name="T" /> if the
    ///     value could not be converted to this type.
    /// </returns>
    public static T? GetValue<T>(this DependencyObject dependencyObject, DependencyProperty dependencyProperty)
    {
        Guard.ArgumentIsNotNull(dependencyObject);
        Guard.ArgumentIsNotNull(dependencyProperty);

        var value = dependencyObject.GetValue(dependencyProperty);
        if (value is T valueAsT)
        {
            return valueAsT;
        }

        return default;
    }

    /// <summary>
    ///     Sets the localized resource as the value of the dependency property.
    /// </summary>
    /// <param name="dependencyObject">
    ///     The element to which the value is set.
    /// </param>
    /// <param name="dependencyProperty">
    ///     Dependency property.
    /// </param>
    /// <param name="resource">
    ///     An extension of the localization markup that provides a resource.
    /// </param>
    public static void SetResource(this DependencyObject dependencyObject, DependencyProperty dependencyProperty, BaseLocalizationMarkupExtension resource)
    {
        Guard.ArgumentIsNotNull(dependencyObject);
        Guard.ArgumentIsNotNull(dependencyProperty);
        Guard.ArgumentIsNotNull(resource);

        BindingOperations.SetBinding(dependencyObject, dependencyProperty, resource.ProvideBinding());
    }

    /// <summary>
    ///     Performs the action once after the item is loaded, or immediately if the item is already loaded.
    /// </summary>
    /// <param name="dependencyObject">
    ///     The item being tracked.
    /// </param>
    /// <param name="action">
    ///     The action being performed.
    /// </param>
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

    /// <summary>
    ///     Performs the action once after initializing the element, or immediately if the element has already been initialized.
    /// </summary>
    /// <param name="dependencyObject">
    ///     The item being tracked.
    /// </param>
    /// <param name="action">
    ///     The action being performed.
    /// </param>
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

    /// <summary>
    ///     Performs the action once after unloading the item.
    /// </summary>
    /// <param name="dependencyObject">
    ///     The item being tracked.
    /// </param>
    /// <param name="action">
    ///     The action being performed.
    /// </param>
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
