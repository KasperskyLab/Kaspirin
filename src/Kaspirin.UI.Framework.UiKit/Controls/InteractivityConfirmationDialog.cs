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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

[TemplatePart(Name = PART_Dialog, Type = typeof(InteractivityDialog))]
[TemplatePart(Name = PART_DialogFooter, Type = typeof(InteractivityDialogFooter))]
public sealed class InteractivityConfirmationDialog : ContentControl, INotificationAnimatable
{
    public const string PART_Dialog = "PART_Dialog";
    public const string PART_DialogFooter = "PART_DialogFooter";

    public InteractivityConfirmationDialog()
    {
        this.WhenLoaded(ApplyFooterButtonStyles);
    }

    #region Icon

    public ImageSource Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(ImageSource),
        typeof(InteractivityConfirmationDialog),
        new PropertyMetadata(default(ImageSource)));

    #endregion

    #region Type

    public InteractivityDialogType Type
    {
        get => (InteractivityDialogType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type),
        typeof(InteractivityDialogType),
        typeof(InteractivityConfirmationDialog),
        new PropertyMetadata(InteractivityDialogType.Neutral));

    #endregion

    #region Header

    public object Header
    {
        get => GetValue(HeaderProperty);
        set => SetValue(HeaderProperty, value);
    }

    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        nameof(Header),
        typeof(object),
        typeof(InteractivityConfirmationDialog),
        new PropertyMetadata(default(object)));

    #endregion

    #region SubHeader

    public object SubHeader
    {
        get => GetValue(SubHeaderProperty);
        set => SetValue(SubHeaderProperty, value);
    }

    public static readonly DependencyProperty SubHeaderProperty = DependencyProperty.Register(
        nameof(SubHeader),
        typeof(object),
        typeof(InteractivityConfirmationDialog),
        new PropertyMetadata(default(object)));

    #endregion

    #region Description

    public object Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
        nameof(Description),
        typeof(object),
        typeof(InteractivityConfirmationDialog),
        new PropertyMetadata(default(object)));

    #endregion

    #region HasHelpButton

    public bool HasHelpButton
    {
        get => (bool)GetValue(HasHelpButtonProperty);
        set => SetValue(HasHelpButtonProperty, value);
    }

    public static readonly DependencyProperty HasHelpButtonProperty = DependencyProperty.Register(
        nameof(HasHelpButton),
        typeof(bool),
        typeof(InteractivityConfirmationDialog),
        new PropertyMetadata(default(bool)));

    #endregion

    #region HelpButtonCommand

    public ICommand HelpButtonCommand
    {
        get => (ICommand)GetValue(HelpButtonCommandProperty);
        set => SetValue(HelpButtonCommandProperty, value);
    }

    public static readonly DependencyProperty HelpButtonCommandProperty = DependencyProperty.Register(
        nameof(HelpButtonCommand),
        typeof(ICommand),
        typeof(InteractivityConfirmationDialog),
        new PropertyMetadata(default(ICommand)));

    #endregion

    #region HasCloseButton

    public bool HasCloseButton
    {
        get => (bool)GetValue(HasCloseButtonProperty);
        set => SetValue(HasCloseButtonProperty, value);
    }

    public static readonly DependencyProperty HasCloseButtonProperty = DependencyProperty.Register(
        nameof(HasCloseButton),
        typeof(bool),
        typeof(InteractivityConfirmationDialog),
        new PropertyMetadata(default(bool)));

    #endregion

    #region CloseButtonCommand

    public ICommand CloseButtonCommand
    {
        get => (ICommand)GetValue(CloseButtonCommandProperty);
        set => SetValue(CloseButtonCommandProperty, value);
    }

    public static readonly DependencyProperty CloseButtonCommandProperty = DependencyProperty.Register(
        nameof(CloseButtonCommand),
        typeof(ICommand),
        typeof(InteractivityConfirmationDialog),
        new PropertyMetadata(default(ICommand)));

    #endregion

    #region ConfirmButtonStyle

    public Style ConfirmButtonStyle
    {
        get => (Style)GetValue(ConfirmButtonStyleProperty);
        set => SetValue(ConfirmButtonStyleProperty, value);
    }

    public static readonly DependencyProperty ConfirmButtonStyleProperty = DependencyProperty.Register(
        nameof(ConfirmButtonStyle),
        typeof(Style),
        typeof(InteractivityConfirmationDialog),
        new PropertyMetadata(default(Style)));

    #endregion

    #region ConfirmButtonCaption

    public string ConfirmButtonCaption
    {
        get => (string)GetValue(ConfirmButtonCaptionProperty);
        set => SetValue(ConfirmButtonCaptionProperty, value);
    }

    public static readonly DependencyProperty ConfirmButtonCaptionProperty = DependencyProperty.Register(
        nameof(ConfirmButtonCaption),
        typeof(string),
        typeof(InteractivityConfirmationDialog),
        new PropertyMetadata(default(string)));

    #endregion

    #region ConfirmButtonCommand

    public ICommand ConfirmButtonCommand
    {
        get => (ICommand)GetValue(ConfirmButtonCommandProperty);
        set => SetValue(ConfirmButtonCommandProperty, value);
    }

    public static readonly DependencyProperty ConfirmButtonCommandProperty = DependencyProperty.Register(
        nameof(ConfirmButtonCommand),
        typeof(ICommand),
        typeof(InteractivityConfirmationDialog),
        new PropertyMetadata(default(ICommand)));

    #endregion

    #region CancelButtonCaption

    public string CancelButtonCaption
    {
        get => (string)GetValue(CancelButtonCaptionProperty);
        set => SetValue(CancelButtonCaptionProperty, value);
    }

    public static readonly DependencyProperty CancelButtonCaptionProperty = DependencyProperty.Register(
        nameof(CancelButtonCaption),
        typeof(string),
        typeof(InteractivityConfirmationDialog),
        new PropertyMetadata(default(string)));

    #endregion

    #region CancelButtonCommand

    public ICommand CancelButtonCommand
    {
        get => (ICommand)GetValue(CancelButtonCommandProperty);
        set => SetValue(CancelButtonCommandProperty, value);
    }

    public static readonly DependencyProperty CancelButtonCommandProperty = DependencyProperty.Register(
        nameof(CancelButtonCommand),
        typeof(ICommand),
        typeof(InteractivityConfirmationDialog),
        new PropertyMetadata(default(ICommand)));

    #endregion

    #region CancelButtonStyle

    public Style CancelButtonStyle
    {
        get => (Style)GetValue(CancelButtonStyleProperty);
        set => SetValue(CancelButtonStyleProperty, value);
    }

    public static readonly DependencyProperty CancelButtonStyleProperty = DependencyProperty.Register(
        nameof(CancelButtonStyle),
        typeof(Style),
        typeof(InteractivityConfirmationDialog),
        new PropertyMetadata(default(Style)));

    #endregion

    #region INotificationAnimatable

    void INotificationAnimatable.OnOpening(Action? completedCallback)
    {
        _dialog?.OnOpening(completedCallback);
    }

    void INotificationAnimatable.OnClosing(Action? completedCallback)
    {
        _dialog?.OnClosing(completedCallback);
    }

    #endregion

    public override void OnApplyTemplate()
    {
        _dialog = Guard.EnsureIsInstanceOfType<INotificationAnimatable>(GetTemplateChild(PART_Dialog));
        _dialogFooter = Guard.EnsureIsInstanceOfType<InteractivityDialogFooter>(GetTemplateChild(PART_DialogFooter));

        ApplyFooterButtonStyles();
    }

    private void ApplyFooterButtonStyles()
    {
        if (_dialogFooter == null || !_dialogFooter.IsLoaded)
        {
            return;
        }

        _defaultConfirmButtonStyle ??= _dialogFooter.PrimaryButtonStyle;
        _defaultCancelButtonStyle ??= _dialogFooter.SecondaryButtonStyle;

        _dialogFooter.SetBinding(InteractivityDialogFooter.PrimaryButtonStyleProperty, new Binding()
        {
            Source = this,
            Path = ConfirmButtonStyleProperty.AsPath(),
            Converter = new DelegateConverter<Style>(style => style ?? _defaultConfirmButtonStyle)
        });

        _dialogFooter.SetBinding(InteractivityDialogFooter.SecondaryButtonStyleProperty, new Binding()
        {
            Source = this,
            Path = CancelButtonStyleProperty.AsPath(),
            Converter = new DelegateConverter<Style>(style => style ?? _defaultCancelButtonStyle)
        });
    }

    private INotificationAnimatable? _dialog;
    private InteractivityDialogFooter? _dialogFooter;
    private Style? _defaultConfirmButtonStyle;
    private Style? _defaultCancelButtonStyle;
}
