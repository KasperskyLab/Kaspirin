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
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Services;

public sealed class TraverseService
{
    public static IEnumerable<DependencyObject> TraverseParents(
        DependencyObject dependencyObject,
        Func<DependencyObject, bool>? traverseCondition,
        TraverseParentOptions options)
    {
        var parent = Guard.EnsureArgumentIsNotNull(dependencyObject);
        var isLogicalTraverse = options.HasFlag(TraverseParentOptions.LogicalTraverse);
        var isVisualTraverse = options.HasFlag(TraverseParentOptions.VisualTraverse);

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

    public static IEnumerable<DependencyObject> TraverseVisualChildren(
        DependencyObject dependencyObject,
        Func<DependencyObject, bool>? traverseCondition,
        TraverseChildrenOptions options)
    {
        Guard.ArgumentIsNotNull(dependencyObject);

        if (dependencyObject is TextBlock textBlock)
        {
            var hasInlineElements = DependencyPropertyHelper.GetValueSource(textBlock, TextBlock.TextProperty).BaseValueSource is BaseValueSource.Default;
            if (hasInlineElements)
            {
                foreach (var textBlockInlineElement in textBlock.Inlines)
                {
                    yield return textBlockInlineElement;
                }
            }
        }

        if (dependencyObject is FlowDocument flowDocument)
        {
            foreach (var element in TraverseFlowDocumentElements(flowDocument, traverseCondition, options))
            {
                yield return element;
            }
        }
        else
        {
            if (dependencyObject is not Visual)
            {
                yield break;
            }

            for (int i = 0, count = VisualTreeHelper.GetChildrenCount(dependencyObject); i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(dependencyObject, i);

                var continueTraverse = traverseCondition?.Invoke(child);
                if (continueTraverse == null || continueTraverse.Value == true)
                {
                    yield return child;

                    foreach (var childOfChild in TraverseVisualChildren(child, traverseCondition, options))
                    {
                        yield return childOfChild;
                    }
                }
                else // conditionResult.Value == false
                {
                    if (options.HasFlag(TraverseChildrenOptions.IncludeConditionLeaf))
                    {
                        yield return child;
                    }
                }
            }
        }
    }

    private static IEnumerable<DependencyObject> TraverseFlowDocumentElements(
        DependencyObject dependencyObject,
        Func<DependencyObject, bool>? traverseCondition,
        TraverseChildrenOptions options)
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
            foreach (var element in TraverseVisualChildren(uiContainer.Child, traverseCondition, options))
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
                foreach (var element in TraverseFlowDocumentElements(block, traverseCondition, options))
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
}
