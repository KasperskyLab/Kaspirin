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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

[ContentProperty(nameof(PopupContent))]
public sealed class TextHint : TextInlineBase
{
    public TextHint()
    {
        _hintPopup = new Popup();
        _hintPopup.SetBinding(Popup.PopupHeaderProperty, new Binding { Source = this, Path = PopupHeaderProperty.AsPath() });
        _hintPopup.SetBinding(Popup.PopupContentProperty, new Binding { Source = this, Path = PopupContentProperty.AsPath() });
        _hintPopup.SetBinding(Popup.PopupContentTemplateProperty, new Binding { Source = this, Path = PopupContentTemplateProperty.AsPath() });
        _hintPopup.SetBinding(Popup.PopupPositionProperty, new Binding { Source = this, Path = PopupPositionProperty.AsPath() });
        _hintPopup.IsPopupStaysOpen = false;
        _hintPopup.Opened += OnPopupOpened;
        _hintPopup.Closed += OnPopupClosed;

        _hintPanel = new Grid { Background = Brushes.Transparent }; // Transparent background is needed to handle mouse events.
        _hintPanel.MouseDown += OnHintPanelMouseDown;
        _hintPanel.Children.Add(_hintPopup);
    }

    public event EventHandler PopupOpened = (_, _) => { };

    public event EventHandler PopupClosed = (_, _) => { };

    #region Command

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
        nameof(Command),
        typeof(ICommand),
        typeof(TextHint),
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
        typeof(TextHint),
        new PropertyMetadata(default(object)));

    #endregion

    #region IconBrush

    public Brush IconBrush
    {
        get => (Brush)GetValue(IconBrushProperty);
        set => SetValue(IconBrushProperty, value);
    }

    public static readonly DependencyProperty IconBrushProperty = DependencyProperty.Register(
        nameof(IconBrush),
        typeof(Brush),
        typeof(TextHint),
        new PropertyMetadata(default(Brush), OnIconBrushChanged));

    private static void OnIconBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((TextHint)d).UpdateHintIconBrush();

    #endregion

    #region IsTabStop

    public bool IsTabStop
    {
        get => (bool)GetValue(IsTabStopProperty);
        set => SetValue(IsTabStopProperty, value);
    }

    public static readonly DependencyProperty IsTabStopProperty = DependencyProperty.Register(
        nameof(IsTabStop),
        typeof(bool),
        typeof(TextHint),
        new PropertyMetadata(true));

    #endregion

    #region PopupHeader

    public object PopupHeader
    {
        get => GetValue(PopupHeaderProperty);
        set => SetValue(PopupHeaderProperty, value);
    }

    public static readonly DependencyProperty PopupHeaderProperty = DependencyProperty.Register(
        nameof(PopupHeader),
        typeof(object),
        typeof(TextHint),
        new PropertyMetadata(default(object)));

    #endregion

    #region PopupContent

    public object PopupContent
    {
        get => GetValue(PopupContentProperty);
        set => SetValue(PopupContentProperty, value);
    }

    public static readonly DependencyProperty PopupContentProperty = DependencyProperty.Register(
        nameof(PopupContent),
        typeof(object),
        typeof(TextHint),
        new PropertyMetadata(default(object)));

    #endregion

    #region PopupContentTemplate

    public DataTemplate PopupContentTemplate
    {
        get => (DataTemplate)GetValue(PopupContentTemplateProperty);
        set => SetValue(PopupContentTemplateProperty, value);
    }

    public static readonly DependencyProperty PopupContentTemplateProperty = DependencyProperty.Register(
        nameof(PopupContentTemplate),
        typeof(DataTemplate),
        typeof(TextHint),
        new PropertyMetadata(default(DataTemplate)));

    #endregion

    #region PopupPosition

    public PopupPosition PopupPosition
    {
        get => (PopupPosition)GetValue(PopupPositionProperty);
        set => SetValue(PopupPositionProperty, value);
    }

    public static readonly DependencyProperty PopupPositionProperty = DependencyProperty.Register(
        nameof(PopupPosition),
        typeof(PopupPosition),
        typeof(TextHint),
        new PropertyMetadata(PopupPosition.Right));

    #endregion

    public void Show()
    {
        if (_hintPopup != null &&
            _hintPopup.PopupContent != null)
        {
            _hintPopup.IsPopupOpen = true;
        }
    }

    public void Close()
    {
        if (_hintPopup != null)
        {
            _hintPopup.IsPopupOpen = false;
        }
    }

    protected override UIElement PrepareChildElement(TextBlock parent)
    {
        _textBlockLineHeightChangeNotifier = new PropertyChangeNotifier<TextBlock, double>(parent, TextBlock.LineHeightProperty.AsPath());
        _textBlockLineHeightChangeNotifier.ValueChanged += OnTextBlockLineHeightChanged;

        CreateHintButton(parent);

        return _hintPanel;
    }

    private void CreateHintButton(TextBlock textBlock)
    {
        IconButtonBase hintButton;

        if (double.IsNaN(textBlock.LineHeight) is false)
        {
            hintButton = textBlock.LineHeight <= 24
                ? new IconButton() { Icon = UIKitIcon_16.StatusQuestion }
                : new BigIconButton() { Icon = UIKitIcon_24.StatusQuestion };
        }
        else
        {
            hintButton = textBlock.FontSize <= 18
                ? new IconButton() { Icon = UIKitIcon_16.StatusQuestion }
                : new BigIconButton() { Icon = UIKitIcon_24.StatusQuestion };
        }

        hintButton.Click += OnButtonClick;
        hintButton.SetBinding(IconButtonBase.CommandProperty, new Binding { Source = this, Path = CommandProperty.AsPath() });
        hintButton.SetBinding(IconButtonBase.CommandParameterProperty, new Binding { Source = this, Path = CommandParameterProperty.AsPath() });
        hintButton.SetBinding(Control.IsTabStopProperty, new Binding { Source = this, Path = IsTabStopProperty.AsPath() });
        hintButton.WhenLoaded(UpdateHintIconBrush);

        _hintPopup.SetValue(Popup.PopupTargetProperty, hintButton);

        if (_hintButton != null)
        {
            _hintButton.Click -= OnButtonClick;
            _hintPanel.Children.Remove(_hintButton);
        }

        _hintPanel.Children.Add(hintButton);

        _hintButton = hintButton;
    }

    private void OnHintPanelMouseDown(object sender, MouseButtonEventArgs e)
        // When popup is open we disable IsHitTestVisible on the icon button to let popup close on the next button click (instead of opening again).
        // But due to disabled hit testing MouseDown event bubbles upward and can be handled by some parent control.
        // If TextHint is contained inside any interactive control, it can cause unwanted clicks.
        // So mark event as handled if icon button is enabled to suppress bubbling. If icon button is disabled, it's okay to let click be handled by parents.
        => e.Handled = _hintButton?.GetValue<bool>(IsEnabledProperty) ?? false;

    private void OnTextBlockLineHeightChanged(TextBlock textBlock, double oldValue, double newValue)
        => CreateHintButton(textBlock);

    private void OnButtonClick(object sender, RoutedEventArgs e)
    {
        Guard.IsNotNull(_hintPopup);

        if (_hintPopup.IsPopupOpen)
        {
            Close();
        }
        else
        {
            Show();
        }
    }

    private void OnPopupOpened(object? sender, EventArgs e)
    {
        PopupOpened(this, EventArgs.Empty);
        Guard.EnsureIsNotNull(_hintButton).IsHitTestVisible = false;
    }

    private void OnPopupClosed(object? sender, EventArgs e)
    {
        PopupClosed(this, EventArgs.Empty);
        Guard.EnsureIsNotNull(_hintButton).IsHitTestVisible = true;
    }

    private void UpdateHintIconBrush()
    {
        if (_hintButton == null)
        {
            return;
        }

        if (IconBrush == null)
        {
            _hintButton.ClearValue(IconButtonBase.IconBrushProperty);
        }
        else
        {
            _hintButton.SetValue(IconButtonBase.IconBrushProperty, IconBrush);
        }
    }

    private readonly Popup _hintPopup;
    private readonly Grid _hintPanel;

    private PropertyChangeNotifier<TextBlock, double>? _textBlockLineHeightChangeNotifier;
    private IconButtonBase? _hintButton;
}
