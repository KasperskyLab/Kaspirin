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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using Kaspirin.UI.Framework.UiKit.Controls.Automation;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

[TemplatePart(Name = PART_ExpandToggle, Type = typeof(ToggleButton))]
public sealed class ExpandButton : ContentControl
{
    public const string PART_ExpandToggle = "PART_ExpandToggle";

    public ExpandButton()
    {
        this.WhenLoaded(() =>
        {
            ApplyToggleStyle();
            SwitchPanelExpand();
        });
    }

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
        typeof(ExpandButton));

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
        typeof(ExpandButton));

    #endregion

    #region Command

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
        nameof(Command),
        typeof(ICommand),
        typeof(ExpandButton),
        new PropertyMetadata(default(ICommand)));

    #endregion

    #region CommandParameter

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
        nameof(CommandParameter),
        typeof(object),
        typeof(ExpandButton),
        new PropertyMetadata(default(object)));

    #endregion

    #region ToggleStyle

    public Style ToggleStyle
    {
        get => (Style)GetValue(ToggleStyleProperty);
        set => SetValue(ToggleStyleProperty, value);
    }

    public static readonly DependencyProperty ToggleStyleProperty = DependencyProperty.Register(
        nameof(ToggleStyle),
        typeof(Style),
        typeof(ExpandButton),
        new PropertyMetadata(default(Style)));

    #endregion

    #region IsExpanded

    public bool IsExpanded
    {
        get => (bool)GetValue(IsExpandedProperty);
        set => SetValue(IsExpandedProperty, value);
    }

    public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(
        nameof(IsExpanded),
        typeof(bool),
        typeof(ExpandButton),
        new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIsExpandedChanged));

    private static void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((ExpandButton)d).OnIsExpandedChanged();

    #endregion

    public override void OnApplyTemplate()
    {
        ExpanderToggleButton = Guard.EnsureIsInstanceOfType<ToggleButton>(GetTemplateChild(PART_ExpandToggle));
        ExpanderToggleButton.SetBinding(ToggleButton.IsCheckedProperty, new Binding()
        {
            Source = this,
            Path = IsExpandedProperty.AsPath(),
            Mode = BindingMode.TwoWay
        });

        ApplyToggleStyle();
    }

    public ToggleButton? ExpanderToggleButton { get; private set; }

    protected override AutomationPeer OnCreateAutomationPeer()
    {
        return new ExpandButtonAutomationPeer(this);
    }

    private void ApplyToggleStyle()
    {
        if (ExpanderToggleButton == null)
        {
            return;
        }

        ExpanderToggleButton.WhenLoaded(() =>
        {
            _defaultToggleStyle ??= ExpanderToggleButton.Style;

            ExpanderToggleButton.SetBinding(ToggleButton.StyleProperty, new Binding()
            {
                Source = this,
                Path = ToggleStyleProperty.AsPath(),
                Converter = new DelegateConverter<Style>(style => style ?? _defaultToggleStyle)
            });
        });
    }

    private void OnIsExpandedChanged()
    {
        if (IsExpanded)
        {
            RaiseEvent(new RoutedEventArgs(ExpandedEvent));
        }
        else
        {
            RaiseEvent(new RoutedEventArgs(CollapsedEvent));
        }

        SwitchPanelExpand();
    }

    private void SwitchPanelExpand()
    {
        if (Parent is Panel panel)
        {
            panel.Children
                .OfType<ExpandPanel>()
                .Where(p => BindingOperations.GetBinding(p, ExpandPanel.IsExpandedProperty) == null)
                .ForEach(p => p.IsExpanded = IsExpanded);
        }
    }

    private Style? _defaultToggleStyle;
}
