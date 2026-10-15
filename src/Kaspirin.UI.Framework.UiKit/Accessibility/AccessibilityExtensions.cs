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
using System.Linq;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Documents;
using Kaspirin.UI.Framework.UiKit.Controls.Automation.Internals;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Accessibility;

public static class AccessibilityExtensions
{
    public static bool Validate(this DependencyObject uiElement)
        => FrameworkObject.CreatePeerForElement(uiElement) is not IAccessibilityAware peer || peer.Validate();

    public static string? GetAccessibilityText(
        this DependencyObject dependencyObject,
        DependencyProperty? headerProperty = null,
        DependencyProperty? contentProperty = null,
        AccessibilityTextOptions options = AccessibilityTextOptions.None)
    {
        var headerFromHeaderProp = dependencyObject.GetHeaderText(headerProperty);
        var headerFromContentProp = dependencyObject.GetHeaderText(contentProperty);
        var headerFromElement = dependencyObject.GetHeaderText();
        var pronounceableFromHeaderProp = dependencyObject.GetPronounceableText(headerProperty);
        var pronounceableFromContentProp = dependencyObject.GetPronounceableText(contentProperty);
        var pronounceableFromElement = dependencyObject.GetPronounceableText();

        var headerText = string.Empty;
        var pronounceableText = string.Empty;

        if (headerFromHeaderProp.IsNotNullOrEmpty())
        {
            headerText = headerFromHeaderProp;
        }
        else if (headerFromContentProp.IsNotNullOrEmpty())
        {
            headerText = headerFromContentProp;
        }
        else if (headerFromElement.IsNotNullOrEmpty() && !options.HasFlag(AccessibilityTextOptions.IgnoreHeaderFallback))
        {
            headerText = headerFromElement;
        }

        if (pronounceableFromHeaderProp.IsNotNullOrEmpty())
        {
            pronounceableText = pronounceableFromHeaderProp;
        }

        if (pronounceableFromContentProp.IsNotNullOrEmpty())
        {
            pronounceableText = pronounceableText.IsNotEmpty()
                ? $"{pronounceableFromHeaderProp}, {pronounceableFromContentProp}"
                : $"{pronounceableFromContentProp}";
        }

        if (pronounceableText.IsNullOrEmpty() && !options.HasFlag(AccessibilityTextOptions.IgnoreContentFallback))
        {
            pronounceableText = pronounceableFromElement;
        }

        if (headerText.IsNotNullOrEmpty() && pronounceableText.IsNotNullOrEmpty())
        {
            if (headerText.Equals(pronounceableText, StringComparison.OrdinalIgnoreCase))
            {
                return headerText;
            }

            return $"{headerText}, {pronounceableText}";
        }
        else if (headerText.IsNotNullOrEmpty())
        {
            return headerText;
        }
        else if (pronounceableText.IsNotNullOrEmpty())
        {
            return pronounceableText;
        }

        return null;
    }

    private static string? GetHeaderText(this DependencyObject dependencyObject, DependencyProperty? property)
    {
        if (property == null)
        {
            return null;
        }

        var propertyObj = dependencyObject.GetValue(property);
        if (propertyObj is string propertyString)
        {
            return propertyString;
        }

        if (propertyObj is TextBlock textBlock)
        {
            return FrameworkObject.CreatePeerForElement(textBlock).GetName();
        }

        if (propertyObj is DependencyObject propertyDepObj)
        {
            return propertyDepObj.GetHeaderText();
        }

        return null;
    }

    private static string? GetHeaderText(this DependencyObject dependencyObject)
    {
        return dependencyObject.GetHeaderElement()?.GetName();
    }

    private static string? GetPronounceableText(this DependencyObject dependencyObject, DependencyProperty? property)
    {
        if (property == null)
        {
            return null;
        }

        var propertyObj = dependencyObject.GetValue(property);
        if (propertyObj is DependencyObject propertyDepObj)
        {
            return propertyDepObj.GetPronounceableText();
        }

        return null;
    }

    private static string? GetPronounceableText(this DependencyObject dependencyObject)
    {
        var elements = dependencyObject.GetPronounceableElements();
        if (elements.Length == 0)
        {
            return null;
        }

        return string.Join(", ", elements.Select(e => e.GetName()));
    }

    private static AutomationPeer? GetHeaderElement(this DependencyObject dependencyObject)
    {
        return TraverseService
            .TraverseVisualChildren(dependencyObject, ContinueHeaderTraverse, TraverseChildrenOptions.IncludeConditionLeaf)
            .Where(IsValidHeaderElement)
            .OrderBy(AccessibilityProperties.GetFontPriority)
            .Select(FrameworkObject.CreatePeerForElement)
            .FirstOrDefault();
    }

    private static AutomationPeer[] GetPronounceableElements(this DependencyObject dependencyObject)
    {
        return TraverseService
            .TraverseVisualChildren(dependencyObject, ContinueTraverse, TraverseChildrenOptions.IncludeConditionLeaf)
            .Where(IsValidPronounceableElement)
            .Select(FrameworkObject.CreatePeerForElement)
            .ToArray();
    }

    private static bool ContinueHeaderTraverse(DependencyObject dependencyObject)
    {
        var isInteractive = IsInteractive(dependencyObject);
        if (isInteractive)
        {
            return AccessibilityProperties.GetIsPronounceable(dependencyObject) == true;
        }

        return ContinueTraverse(dependencyObject);
    }

    private static bool ContinueTraverse(DependencyObject dependencyObject)
    {
        if (dependencyObject is FrameworkElement or FrameworkContentElement)
        {
            if (FrameworkObject.CreatePeerForElement(dependencyObject) is IAutomationPeer peer)
            {
                return !peer.CanProvideName;
            }
        }

        return true;
    }

    private static bool IsValidHeaderElement(DependencyObject dependencyObject)
    {
        var isVisible = IsVisible(dependencyObject);
        if (isVisible != true)
        {
            return false;
        }

        if (!AccessibilityProperties.GetIsHeader(dependencyObject))
        {
            return false;
        }

        var isPronounceable = AccessibilityProperties.GetIsPronounceable(dependencyObject);
        if (isPronounceable == false)
        {
            return false;
        }

        var hasPeer = FrameworkObject.CreatePeerForElement(dependencyObject) != null;

        var isInteractive = IsInteractive(dependencyObject);
        if (isInteractive)
        {
            return isPronounceable == true && hasPeer;
        }

        return hasPeer;
    }

    private static bool IsValidPronounceableElement(DependencyObject dependencyObject)
    {
        var isVisible = IsVisible(dependencyObject);
        if (isVisible != true)
        {
            return false;
        }

        var isPronounceable = AccessibilityProperties.GetIsPronounceable(dependencyObject);
        if (isPronounceable != true)
        {
            return false;
        }

        var hasPeer = FrameworkObject.CreatePeerForElement(dependencyObject) != null;

        return hasPeer;
    }

    private static bool IsVisible(DependencyObject dependencyObject)
    {
        if (dependencyObject is FrameworkElement fe && fe.IsVisible == false)
        {
            return false;
        }

        return true;
    }

    private static bool IsInteractive(DependencyObject dependencyObject)
    {
        if (dependencyObject is FrameworkElement or FrameworkContentElement)
        {
            var fo = new FrameworkObject(dependencyObject);

            var isInteractive = fo.Focusable && fo.FocusVisualStyle != null;
            if (isInteractive)
            {
                return true;
            }

            var isTextBlockWithHyperlink = dependencyObject is TextBlock textBlock && textBlock.FindVisualChild<Hyperlink>() != null;
            if (isTextBlockWithHyperlink)
            {
                return true;
            }
        }

        return false;
    }
}
