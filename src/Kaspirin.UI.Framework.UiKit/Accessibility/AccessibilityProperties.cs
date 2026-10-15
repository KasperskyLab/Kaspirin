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

using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Accessibility;

public static class AccessibilityProperties
{
    #region Label

    public static object GetLabel(DependencyObject obj)
        => (object)obj.GetValue(LabelProperty);

    public static void SetLabel(DependencyObject obj, object value)
        => obj.SetValue(LabelProperty, value);

    public static readonly DependencyProperty LabelProperty = DependencyProperty.RegisterAttached(
        "Label",
        typeof(object),
        typeof(AccessibilityProperties),
        new PropertyMetadata(default(object)));

    #endregion

    #region FontPriority

    public static int GetFontPriority(DependencyObject obj)
        => (int)obj.GetValue(FontPriorityProperty);

    public static void SetFontPriority(DependencyObject obj, int value)
        => obj.SetValue(FontPriorityProperty, value);

    public static readonly DependencyProperty FontPriorityProperty = DependencyProperty.RegisterAttached(
        "FontPriority",
        typeof(int),
        typeof(AccessibilityProperties),
        new PropertyMetadata(default(int)));

    #endregion

    #region IsIgnored

    public static bool GetIsIgnored(DependencyObject obj)
    => (bool)obj.GetValue(IsIgnoredProperty);

    public static void SetIsIgnored(DependencyObject obj, bool value)
        => obj.SetValue(IsIgnoredProperty, value);

    public static readonly DependencyProperty IsIgnoredProperty = DependencyProperty.RegisterAttached(
        "IsIgnored",
        typeof(bool),
        typeof(AccessibilityProperties),
        new PropertyMetadata(default(bool)));

    #endregion

    #region IsPronounceable

    public static bool? GetIsPronounceable(DependencyObject obj)
    => (bool?)obj.GetValue(IsPronounceableProperty);

    public static void SetIsPronounceable(DependencyObject obj, bool? value)
        => obj.SetValue(IsPronounceableProperty, value);

    public static readonly DependencyProperty IsPronounceableProperty = DependencyProperty.RegisterAttached(
        "IsPronounceable",
        typeof(bool?),
        typeof(AccessibilityProperties),
        new PropertyMetadata(default(bool?)));

    #endregion

    #region IsHeader

    internal static bool GetIsHeader(DependencyObject obj)
    => (bool)obj.GetValue(_isHeaderProperty);

    internal static void SetIsHeader(DependencyObject obj, bool value)
        => obj.SetValue(_isHeaderProperty, value);

    internal static readonly DependencyProperty _isHeaderProperty = DependencyProperty.RegisterAttached(
        "IsHeader",
        typeof(bool),
        typeof(AccessibilityProperties),
        new PropertyMetadata(default(bool)));

    #endregion
}