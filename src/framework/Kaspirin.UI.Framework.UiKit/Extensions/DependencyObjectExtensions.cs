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
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Extensions
{
    public static class DependencyObjectExtensions
    {
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
            => dependencyObject.TraverseVisualChildren().OfType<T>().FirstOrDefault();

        public static T? FindVisualChild<T>(this DependencyObject dependencyObject, Func<T, bool> condition)
            where T : DependencyObject
            => dependencyObject.TraverseVisualChildren().OfType<T>().FirstOrDefault(condition);

        public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject dependencyObject)
            where T : DependencyObject
            => dependencyObject.TraverseVisualChildren().OfType<T>();

        public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject dependencyObject, Func<T, bool> condition)
            where T : DependencyObject
            => dependencyObject.TraverseVisualChildren().OfType<T>().Where(condition);

        public static IEnumerable<DependencyObject> TraverseVisualChildren(
            this DependencyObject dependencyObject,
            Func<DependencyObject, bool>? traverseCondition = null)
        {
            Guard.ArgumentIsNotNull(dependencyObject);

            if (dependencyObject is TextBlock textBlockElement && ContainsInlines(textBlockElement))
            {
                foreach (var textBlockInlineElement in textBlockElement.Inlines)
                {
                    yield return textBlockInlineElement;
                }
            }

            if (dependencyObject is FlowDocument flowDocument)
            {
                foreach (var element in TraverseFlowDocumentElements(flowDocument, traverseCondition))
                {
                    yield return element;
                }
            }
            else
            {
                for (int i = 0, count = VisualTreeHelper.GetChildrenCount(dependencyObject); i < count; i++)
                {
                    var child = VisualTreeHelper.GetChild(dependencyObject, i);

                    var continueTraverse = traverseCondition == null || traverseCondition?.Invoke(child) == true;
                    if (continueTraverse)
                    {
                        yield return child;

                        foreach (var childOfChild in TraverseVisualChildren(child, traverseCondition))
                        {
                            yield return childOfChild;
                        }
                    }
                }
            }

            bool ContainsInlines(TextBlock textBlock) =>
                DependencyPropertyHelper.GetValueSource(textBlock, TextBlock.TextProperty).BaseValueSource is BaseValueSource.Default;
        }

        public static IEnumerable<DependencyObject> TraverseFlowDocumentElements(
            this DependencyObject dependencyObject,
            Func<DependencyObject, bool>? traverseCondition = null)
        {
            Guard.ArgumentIsNotNull(dependencyObject);

            yield return dependencyObject;

            if (dependencyObject is FlowDocument flowDocument && IsNotEmptyBlockCollection(flowDocument.Blocks))
            {
                foreach (var element in TraverseBlockCollection(flowDocument.Blocks))
                {
                    yield return element;
                }
            }

            if (dependencyObject is Section section && IsNotEmptyBlockCollection(section.Blocks))
            {
                foreach (var element in TraverseBlockCollection(section.Blocks))
                {
                    yield return element;
                }
            }

            if (dependencyObject is List list)
            {
                yield return list;

                foreach (var listItem in list.ListItems)
                {
                    yield return listItem;

                    if (IsNotEmptyBlockCollection(listItem.Blocks))
                    {
                        foreach (var element in TraverseBlockCollection(listItem.Blocks))
                        {
                            yield return element;
                        }
                    }
                }
            }

            if (dependencyObject is Paragraph paragraph)
            {
                yield return paragraph;

                if (IsNotEmptyInlineCollection(paragraph.Inlines))
                {
                    foreach (var element in TraverseInlineCollection(paragraph.Inlines))
                    {
                        yield return element;
                    }
                }
            }

            if (dependencyObject is BlockUIContainer uiContainer)
            {
                foreach (var element in TraverseVisualChildren(uiContainer.Child, traverseCondition))
                {
                    yield return element;
                }
            }

            bool IsNotEmptyBlockCollection(BlockCollection blockCollection) => blockCollection.Any();
            IEnumerable<DependencyObject> TraverseBlockCollection(BlockCollection blockCollection)
            {
                Guard.ArgumentIsNotNull(blockCollection);

                foreach (var block in blockCollection)
                {
                    foreach (var element in TraverseFlowDocumentElements(block, traverseCondition))
                    {
                        yield return element;
                    }
                }
            }

            bool IsNotEmptyInlineCollection(InlineCollection inlineCollection) => inlineCollection.Any();
            IEnumerable<DependencyObject> TraverseInlineCollection(InlineCollection inlineCollection)
            {
                Guard.ArgumentIsNotNull(inlineCollection);

                foreach (var inlineElement in inlineCollection)
                {
                    yield return inlineElement;

                    if (inlineElement is Span span && IsNotEmptyInlineCollection(span.Inlines))
                    {
                        foreach (var element in TraverseInlineCollection(span.Inlines))
                        {
                            yield return element;
                        }
                    }
                }
            }
        }

        public static IEnumerable<DependencyObject> TraverseVisualParents(
            this DependencyObject dependencyObject,
            Func<DependencyObject, bool>? traverseCondition = null)
            => dependencyObject.TraverseParents(isVisualTraverse: true, traverseCondition: traverseCondition);

        public static IEnumerable<DependencyObject> TraverseLogicalParents(
            this DependencyObject dependencyObject,
            Func<DependencyObject, bool>? traverseCondition = null)
            => dependencyObject.TraverseParents(isLogicalTraverse: true, traverseCondition: traverseCondition);

        public static IEnumerable<DependencyObject> TraverseParents(
            this DependencyObject dependencyObject,
            bool isLogicalTraverse = false,
            bool isVisualTraverse = false,
            Func<DependencyObject, bool>? traverseCondition = null)
        {
            Guard.ArgumentIsNotNull(dependencyObject);
            Guard.Argument(isLogicalTraverse || isVisualTraverse);

            var parent = dependencyObject;

            while (parent != null)
            {
                DependencyObject? nextParent = null;

                if (isLogicalTraverse)
                {
                    nextParent = parent switch
                    {
                        Popup popup => popup.PlacementTarget,
                        ContextMenu contextMenu => contextMenu.PlacementTarget,
                        ListBoxItem listBoxItem => ItemsControl.ItemsControlFromItemContainer(listBoxItem),
                        NotificationView notificationView => notificationView.AssociatedObject,
                        _ => null
                    };
                }
                else if (isVisualTraverse)
                {
                    nextParent = parent switch
                    {
                        FrameworkContentElement frameworkContentElement => frameworkContentElement.Parent,
                        _ => null
                    };
                }

                if (nextParent == null && isLogicalTraverse)
                {
                    nextParent = LogicalTreeHelper.GetParent(parent);
                }

                if (nextParent == null && isVisualTraverse)
                {
                    nextParent = VisualTreeHelper.GetParent(parent);
                }

                parent = nextParent;

                var continueTraverse = parent != null && (traverseCondition == null || traverseCondition?.Invoke(parent) == true);
                if (continueTraverse)
                {
                    yield return parent!;
                }
                else
                {
                    yield break;
                }
            }
        }

        public static object? GetDataContext(this DependencyObject dependencyObject)
        {
            Guard.ArgumentIsNotNull(dependencyObject);

            if (dependencyObject is FrameworkElement fe)
            {
                return fe.DataContext;
            }
            else if (dependencyObject is FrameworkContentElement fce)
            {
                return fce.DataContext;
            }

            return null;
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

        public static void WhenLoaded(this DependencyObject dependencyObject, Action action)
        {
            new FrameworkObject(dependencyObject).WhenLoaded(action);
        }

        public static void WhenInitialized(this DependencyObject dependencyObject, Action action)
        {
            new FrameworkObject(dependencyObject).WhenInitialized(action);
        }

        public static void WhenUnloaded(this DependencyObject dependencyObject, Action action)
        {
            new FrameworkObject(dependencyObject).WhenUnloaded(action);
        }
    }
}
