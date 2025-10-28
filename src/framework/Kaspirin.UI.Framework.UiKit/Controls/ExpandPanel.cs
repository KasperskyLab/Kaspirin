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
using System.Windows.Controls;

namespace Kaspirin.UI.Framework.UiKit.Controls;

[TemplatePart(Name = PART_ExpandHost, Type = typeof(FrameworkElement))]
public sealed class ExpandPanel : ContentControl
{
    public const string PART_ExpandHost = "PART_ExpandHost";

    public ExpandPanel()
    {
        this.WhenLoaded(UpdateExpandHost);
    }

    #region IsExpanded

    public bool IsExpanded
    {
        get => (bool)GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(
        nameof(IsExpanded),
        typeof(bool),
        typeof(ExpandPanel),
        new PropertyMetadata(true, OnIsExpandedChanged));

    private static void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((ExpandPanel)d).UpdateExpandHost();

    #endregion

    #region ExpandDirection

    public ExpandDirection ExpandDirection
    {
        get => (ExpandDirection)GetValue(ExpandDirectionProperty);
        set => SetValue(ExpandDirectionProperty, value);
    }

    public static readonly DependencyProperty ExpandDirectionProperty = DependencyProperty.Register(
        nameof(ExpandDirection),
        typeof(ExpandDirection),
        typeof(ExpandPanel),
        new PropertyMetadata(ExpandDirection.Height));

    #endregion

    #region Expanded Event

    public event RoutedEventHandler Expanded
    {
        add => AddHandler(ExpandedEvent, value);
        remove => RemoveHandler(ExpandedEvent, value);
    }

    public static readonly RoutedEvent ExpandedEvent = EventManager.RegisterRoutedEvent(
        nameof(Expanded),
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(ExpandPanel));

    #endregion

    #region Collapsed Event

    public event RoutedEventHandler Collapsed
    {
        add => AddHandler(CollapsedEvent, value);
        remove => RemoveHandler(CollapsedEvent, value);
    }

    public static readonly RoutedEvent CollapsedEvent = EventManager.RegisterRoutedEvent(
        nameof(Collapsed),
        RoutingStrategy.Bubble,
        typeof(RoutedEventHandler),
        typeof(ExpandPanel));

    #endregion

    public override void OnApplyTemplate()
    {
        _host = Guard.EnsureIsInstanceOfType<FrameworkElement>(GetTemplateChild(PART_ExpandHost));
        _mediaProvider = new(_host);
        _mediaProvider.Expanded += () => RaiseEvent(new RoutedEventArgs(ExpandedEvent));
        _mediaProvider.Collapsed += () => RaiseEvent(new RoutedEventArgs(CollapsedEvent));

        UpdateExpandHost();
    }

    private void UpdateExpandHost()
    {
        if (_host == null)
        {
            return;
        }

        if (IsExpanded)
        {
            _mediaProvider?.LaunchExpandAnimation(ExpandDirection);
        }
        else
        {
            _mediaProvider?.LaunchCollapseAnimation(ExpandDirection);
        }
    }

    private ExpandPanelMediaProvider? _mediaProvider;
    private FrameworkElement? _host;
}
