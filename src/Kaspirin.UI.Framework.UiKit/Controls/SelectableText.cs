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
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

[TemplatePart(Name = PART_TextHyperlink, Type = typeof(SelectableTextHyperlink))]
[TemplatePart(Name = PART_TextContainer, Type = typeof(TextBlock))]
public sealed class SelectableText : Control
{
    public const string PART_TextHyperlink = "PART_TextHyperlink";
    public const string PART_TextContainer = "PART_TextContainer";

    public SelectableText()
    {
        _resetStateAction = DeferredActionFactory.CreateDebouncerOnUi(ResetState, TimeSpan.FromMilliseconds(1200));
    }

    #region FontStyle

    public new UIKitFontStyleSettings FontStyle
    {
        get => (UIKitFontStyleSettings)GetValue(FontStyleProperty);
        set => SetValue(FontStyleProperty, value);
    }

    public static new readonly DependencyProperty FontStyleProperty = DependencyProperty.Register(
        nameof(FontStyle),
        typeof(UIKitFontStyleSettings),
        typeof(SelectableText),
        new PropertyMetadata(default(UIKitFontStyleSettings)));

    #endregion

    #region FontBrush

    public UIKitFontBrushSettings FontBrush
    {
        get => (UIKitFontBrushSettings)GetValue(FontBrushProperty);
        set => SetValue(FontBrushProperty, value);
    }

    public static readonly DependencyProperty FontBrushProperty = DependencyProperty.Register(
        nameof(FontBrush),
        typeof(UIKitFontBrushSettings),
        typeof(SelectableText),
        new PropertyMetadata(default(UIKitFontBrushSettings), OnFontBrushChanged));

    private static void OnFontBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((SelectableText)d).OnFontBrushChanged();

    #endregion

    #region HasFontBrush

    public bool HasFontBrush
    {
        get => (bool)GetValue(HasFontBrushProperty);
        private set => SetValue(_hasFontBrushPropertyKey, value);
    }

    private static readonly DependencyPropertyKey _hasFontBrushPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(HasFontBrush),
        typeof(bool),
        typeof(SelectableText),
        new PropertyMetadata(default(bool)));

    public static readonly DependencyProperty HasFontBrushProperty = _hasFontBrushPropertyKey.DependencyProperty;

    #endregion

    #region State

    public SelectableTextState State
    {
        get => (SelectableTextState)GetValue(StateProperty);
        private set => SetValue(_statePropertyKey, value);
    }

    private static readonly DependencyPropertyKey _statePropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(State),
        typeof(SelectableTextState),
        typeof(SelectableText),
        new PropertyMetadata(SelectableTextState.Rest));

    public static readonly DependencyProperty StateProperty = _statePropertyKey.DependencyProperty;

    #endregion

    #region Text

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text),
        typeof(string),
        typeof(SelectableText),
        new PropertyMetadata(default(string)));

    #endregion

    #region TextTrimming

    public TextTrimming TextTrimming
    {
        get => (TextTrimming)GetValue(TextTrimmingProperty);
        set => SetValue(TextTrimmingProperty, value);
    }

    public static readonly DependencyProperty TextTrimmingProperty = DependencyProperty.Register(
        nameof(TextTrimming),
        typeof(TextTrimming),
        typeof(SelectableText),
        new PropertyMetadata(TextTrimming.None));

    #endregion

    #region TextWrapping

    public TextWrapping TextWrapping
    {
        get => (TextWrapping)GetValue(TextWrappingProperty);
        set => SetValue(TextWrappingProperty, value);
    }

    public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register(
        nameof(TextWrapping),
        typeof(TextWrapping),
        typeof(SelectableText),
        new PropertyMetadata(TextWrapping.NoWrap));

    #endregion

    public override void OnApplyTemplate()
    {
        _textContainer = Guard.EnsureIsInstanceOfType<TextBlock>(GetTemplateChild(PART_TextContainer));
        _textContainer.SetBinding(AccessibilityProperties.IsPronounceableProperty, new Binding { Source = this, Path = AccessibilityProperties.IsPronounceableProperty.AsPath() });

        _textButton = Guard.EnsureIsInstanceOfType<SelectableTextHyperlink>(GetTemplateChild(PART_TextHyperlink));
        _textButton.Command = new DelegateCommand(CopyText);
        _textButton.SetBinding(AccessibilityProperties.LabelProperty, new Binding { Source = this, Path = AccessibilityProperties.LabelProperty.AsPath() });
        _textButton.SetBinding(AutomationProperties.NameProperty, new Binding { Source = this, Path = AutomationProperties.NameProperty.AsPath() });
        _textButton.SetBinding(AutomationProperties.LabeledByProperty, new Binding { Source = this, Path = AutomationProperties.LabeledByProperty.AsPath() });
        _textButton.SetBinding(AutomationProperties.HelpTextProperty, new Binding { Source = this, Path = AutomationProperties.HelpTextProperty.AsPath() });
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        base.OnPropertyChanged(e);

        if (e.Property != IsMouseOverProperty || State == SelectableTextState.Done)
        {
            return;
        }

        var isMouseOver = (bool)e.NewValue;

        State = isMouseOver
            ? SelectableTextState.Ready
            : SelectableTextState.Rest;
    }

    private void OnFontBrushChanged()
    {
        HasFontBrush = FontBrush != null;
    }

    private void CopyText()
    {
        Clipboard.SetText(Text);

        State = SelectableTextState.Done;

        _resetStateAction.Execute();
    }

    private void ResetState()
    {
        State = SelectableTextState.Rest;
    }

    private TextBlock? _textContainer;
    private SelectableTextHyperlink? _textButton;

    private readonly IDeferredAction _resetStateAction;
}