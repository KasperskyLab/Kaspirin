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
using System.Text;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Kaspirin.UI.Framework.UiKit.Controls.Automation;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

[TemplatePart(Name = PART_CloseButton, Type = typeof(Button))]
[TemplatePart(Name = PART_Overlay, Type = typeof(InteractivityOverlay))]
[TemplatePart(Name = PART_Dialog, Type = typeof(FrameworkElement))]
public class InteractivityDialog : ContentControl, INotificationAnimatable, IAccessibilityAware
{
    public const string PART_CloseButton = "PART_CloseButton";
    public const string PART_Overlay = "PART_Overlay";
    public const string PART_Dialog = "PART_Dialog";

    public const string DefaultScopeName = "RootScope";

    public InteractivityDialog()
    {
        this.WhenLoaded(OnLoaded);
    }

    #region OverlayBehavior

    public InteractivityOverlayBehavior OverlayBehavior
    {
        get => (InteractivityOverlayBehavior)GetValue(OverlayBehaviorProperty);
        set => SetValue(OverlayBehaviorProperty, value);
    }

    public static readonly DependencyProperty OverlayBehaviorProperty = DependencyProperty.Register(
        nameof(OverlayBehavior),
        typeof(InteractivityOverlayBehavior),
        typeof(InteractivityDialog),
        new PropertyMetadata(InteractivityOverlayBehavior.DragWindow));

    #endregion

    #region OverlayCommand

    public ICommand OverlayCommand
    {
        get => (ICommand)GetValue(OverlayCommandProperty);
        set => SetValue(OverlayCommandProperty, value);
    }

    public static readonly DependencyProperty OverlayCommandProperty = DependencyProperty.Register(
        nameof(OverlayCommand),
        typeof(ICommand),
        typeof(InteractivityDialog),
        new PropertyMetadata(default(ICommand)));

    #endregion

    #region DialogActualHeight

    public double DialogActualHeight
    {
        get => (double)GetValue(DialogActualHeightProperty);
        private set => SetValue(_dialogActualHeightPropertyKey, value);
    }

    private static readonly DependencyPropertyKey _dialogActualHeightPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(DialogActualHeight),
        typeof(double),
        typeof(InteractivityDialog),
        new PropertyMetadata(default(double)));

    public static readonly DependencyProperty DialogActualHeightProperty = _dialogActualHeightPropertyKey.DependencyProperty;

    #endregion

    #region DialogActualWidth

    public double DialogActualWidth
    {
        get => (double)GetValue(DialogActualWidthProperty);
        private set => SetValue(_dialogActualWidthPropertyKey, value);
    }

    private static readonly DependencyPropertyKey _dialogActualWidthPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(DialogActualWidth),
        typeof(double),
        typeof(InteractivityDialog),
        new PropertyMetadata(default(double)));

    public static readonly DependencyProperty DialogActualWidthProperty = _dialogActualWidthPropertyKey.DependencyProperty;

    #endregion

    #region DialogWidth

    public InteractivityDialogWidth DialogWidth
    {
        get => (InteractivityDialogWidth)GetValue(DialogWidthProperty);
        set => SetValue(DialogWidthProperty, value);
    }

    public static readonly DependencyProperty DialogWidthProperty = DependencyProperty.Register(
        nameof(DialogWidth),
        typeof(InteractivityDialogWidth),
        typeof(InteractivityDialog),
        new PropertyMetadata(InteractivityDialogWidth.Standard));

    #endregion

    #region DialogMargin

    public Thickness DialogMargin
    {
        get => (Thickness)GetValue(DialogMarginProperty);
        set => SetValue(DialogMarginProperty, value);
    }

    public static readonly DependencyProperty DialogMarginProperty = DependencyProperty.Register(
        nameof(DialogMargin),
        typeof(Thickness),
        typeof(InteractivityDialog),
        new PropertyMetadata(default(Thickness)));

    #endregion

    #region DialogHeight

    public double DialogHeight
    {
        get => (double)GetValue(DialogHeightProperty);
        set => SetValue(DialogHeightProperty, value);
    }

    public static readonly DependencyProperty DialogHeightProperty = DependencyProperty.Register(
        nameof(DialogHeight),
        typeof(double),
        typeof(InteractivityDialog),
        new PropertyMetadata(double.NaN));

    #endregion

    #region DialogMaxHeight

    public double DialogMaxHeight
    {
        get => (double)GetValue(DialogMaxHeightProperty);
        set => SetValue(DialogMaxHeightProperty, value);
    }

    public static readonly DependencyProperty DialogMaxHeightProperty = DependencyProperty.Register(
        nameof(DialogMaxHeightProperty),
        typeof(double),
        typeof(InteractivityDialog),
        new PropertyMetadata(double.MaxValue, OnDialogMaxHeightChanged));

    private static void OnDialogMaxHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((InteractivityDialog)d).OnDialogMaxHeightChanged();

    #endregion

    #region DialogMinHeight

    public double DialogMinHeight
    {
        get => (double)GetValue(DialogMinHeightProperty);
        set => SetValue(DialogMinHeightProperty, value);
    }

    public static readonly DependencyProperty DialogMinHeightProperty = DependencyProperty.Register(
        nameof(DialogMinHeightProperty),
        typeof(double),
        typeof(InteractivityDialog),
        new PropertyMetadata(default(double), OnDialogMinHeightChanged));

    private static void OnDialogMinHeightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((InteractivityDialog)d).OnDialogMinHeightChanged();

    #endregion

    #region Icon

    public ImageSource Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(ImageSource),
        typeof(InteractivityDialog),
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
        typeof(InteractivityDialog),
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
        typeof(InteractivityDialog),
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
        typeof(InteractivityDialog),
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
        typeof(InteractivityDialog),
        new PropertyMetadata(default(object)));

    #endregion

    #region UseContentLeftPadding

    public bool UseContentLeftPadding
    {
        get => (bool)GetValue(UseContentLeftPaddingProperty);
        set => SetValue(UseContentLeftPaddingProperty, value);
    }

    public static readonly DependencyProperty UseContentLeftPaddingProperty = DependencyProperty.Register(
        nameof(UseContentLeftPadding),
        typeof(bool),
        typeof(InteractivityDialog),
        new PropertyMetadata(default(bool)));

    #endregion

    #region Footer

    public object Footer
    {
        get => GetValue(FooterProperty);
        set => SetValue(FooterProperty, value);
    }

    public static readonly DependencyProperty FooterProperty = DependencyProperty.Register(
        nameof(Footer),
        typeof(object),
        typeof(InteractivityDialog),
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
        typeof(InteractivityDialog),
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
        typeof(InteractivityDialog),
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
        typeof(InteractivityDialog),
        new PropertyMetadata(default(bool), OnHasCloseButtonChanged));

    private static void OnHasCloseButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((InteractivityDialog)d).OnHasCloseButtonChanged();

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
        typeof(InteractivityDialog),
        new PropertyMetadata(default(ICommand)));

    #endregion

    #region ScopeName

    public string ScopeName
    {
        get => (string)GetValue(ScopeNameProperty);
        set => SetValue(ScopeNameProperty, value);
    }

    public static readonly DependencyProperty ScopeNameProperty = DependencyProperty.Register(
        nameof(ScopeName),
        typeof(string),
        typeof(InteractivityDialog),
        new PropertyMetadata(DefaultScopeName));

    #endregion

    #region INotificationAnimatable

    void INotificationAnimatable.OnOpening(Action? completedCallback)
    {
        InteractivityDialogScope.Register(this, completedCallback);
    }

    void INotificationAnimatable.OnClosing(Action? completedCallback)
    {
        InteractivityDialogScope.Unregister(this, completedCallback);
    }

    #endregion

    public override void OnApplyTemplate()
    {
        _closeButton = Guard.EnsureIsInstanceOfType<Button>(GetTemplateChild(PART_CloseButton));
        _overlay = Guard.EnsureIsInstanceOfType<InteractivityOverlay>(GetTemplateChild(PART_Overlay));
        _dialog = Guard.EnsureIsInstanceOfType<FrameworkElement>(GetTemplateChild(PART_Dialog));
        _dialog.SizeChanged += OnDialogSizeChanged;
        _dialog.SetBinding(ContentControl.MaxWidthProperty, new Binding() { Source = this, Path = ActualWidthProperty.AsPath() });
        _dialog.SetBinding(ContentControl.MaxHeightProperty, new MultiBinding
        {
            Bindings =
            {
                new Binding(){ Source = this, Path = DialogMaxHeightProperty.AsPath() },
                new Binding(){ Source = this, Path = ActualHeightProperty.AsPath() },
            },
            Converter = new DelegateMultiConverter(values => Math.Min((double)values[0]!, (double)values[1]!))
        });
        _dialog.SetBinding(ContentControl.MinHeightProperty, new MultiBinding
        {
            Bindings =
            {
                new Binding(){ Source = this, Path = DialogMinHeightProperty.AsPath() },
                new Binding(){ Source = this, Path = ActualHeightProperty.AsPath() },
            },
            Converter = new DelegateMultiConverter(values => Math.Min((double)values[0]!, (double)values[1]!))
        });

        SetCloseAction();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append($"{nameof(InteractivityDialog)} [");

        if (CheckAccess())
        {
            sb.Append($"DataContext:{DataContext?.GetType().Name ?? "<null>"}");
        }
        else
        {
            sb.Append(base.ToString());
        }

        sb.Append(']');

        return sb.ToString();
    }

    internal void ShowContent(Action? continueCallback)
    {
        if (_overlay != null)
        {
            _overlay.ShowContent(completedCallback: () =>
            {
                if (UIElementAutomationPeer.FromElement(this) is InteractivityDialogAutomationPeer peer)
                {
                    peer.RaiseShown();
                }

                continueCallback?.Invoke();
            });
        }
        else
        {
            continueCallback?.Invoke();
        }
    }

    internal void HideContent(Action? continueCallback)
    {
        if (_overlay != null)
        {
            _overlay.HideContent(continueCallback);
        }
        else
        {
            continueCallback?.Invoke();
        }
    }

    internal void ShowOverlay()
        => _overlay?.ShowOverlay();

    internal void HideOverlay()
        => _overlay?.HideOverlay();

    protected override AutomationPeer OnCreateAutomationPeer()
    {
        return new InteractivityDialogAutomationPeer(this);
    }

    private void OnLoaded()
    {
        SetCloseAction();
    }

    private void OnHasCloseButtonChanged()
    {
        SetCloseAction();
    }

    private void SetCloseAction()
    {
        if (_closeButton == null)
        {
            return;
        }

        if (HasCloseButton)
        {
            _closeButton.IsCancel = true;

            SuppressCancelButtons();
        }
        else
        {
            _closeButton.IsCancel = false;

            RestoreCancelButtons();
        }
    }

    private void SuppressCancelButtons()
    {
        RestoreCancelButtons();

        _cancelButtons.Clear();
        _cancelButtons.AddRange(GetCancelButtons());
        _cancelButtons.ForEach(b => AccessKeyManager.Unregister(EscKey, b));
    }

    private void RestoreCancelButtons()
    {
        _cancelButtons.ForEach(b => AccessKeyManager.Register(EscKey, b));
    }

    private IEnumerable<Button> GetCancelButtons()
        => this.FindVisualChildren<Button>(b => b.IsCancel).Except(new[] { _closeButton! });

    private void OnDialogMaxHeightChanged()
    {
        SetCurrentValue(DialogMaxHeightProperty, Math.Max(DialogMinHeight, DialogMaxHeight));
    }

    private void OnDialogMinHeightChanged()
    {
        if (DialogMinHeight < 0)
        {
            DialogMinHeight = 0;
        }
        else
        {
            SetCurrentValue(DialogMinHeightProperty, Math.Min(DialogMinHeight, DialogMaxHeight));
        }
    }

    private void OnDialogSizeChanged(object sender, SizeChangedEventArgs e)
    {
        var dialog = Guard.EnsureIsInstanceOfType<FrameworkElement>(sender);

        DialogActualHeight = dialog.ActualHeight;
        DialogActualWidth = dialog.ActualWidth;
    }

    bool IAccessibilityAware.Validate()
    {
        return this.Validate();
    }

    private Button? _closeButton;
    private InteractivityOverlay? _overlay;
    private FrameworkElement? _dialog;

    private readonly List<Button> _cancelButtons = new();

    private const string EscKey = "\u001b";
}