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

using System.Linq;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
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
            .TraverseVisualChildren(dependencyObject, ContinueTraverse, TraverseChildrenOptions.IncludeConditionLeaf)
            .Where(IsVisible)
            .Where(t => AccessibilityProperties.GetIsHeader(t) &&
                        AccessibilityProperties.GetIsPronounceable(t) != false)
            .OrderBy(AccessibilityProperties.GetFontPriority)
            .Select(FrameworkObject.CreatePeerForElement)
            .FirstOrDefault();
    }

    private static AutomationPeer[] GetPronounceableElements(this DependencyObject dependencyObject)
    {
        return TraverseService
            .TraverseVisualChildren(dependencyObject, ContinueTraverse, TraverseChildrenOptions.IncludeConditionLeaf)
            .Where(IsVisible)
            .Where(t => AccessibilityProperties.GetIsPronounceable(t) == true)
            .Select(FrameworkObject.CreatePeerForElement)
            .ToArray();
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

    private static bool IsVisible(DependencyObject dependencyObject)
    {
        if (dependencyObject is FrameworkElement fe && fe.IsVisible == false)
        {
            return false;
        }

        return true;
    }
}
