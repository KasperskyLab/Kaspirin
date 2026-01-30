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
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;
using Kaspirin.UI.Framework.UiKit.Controls.Automation;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

public sealed class InteractivityNotification : ContentControl, INotificationAnimatable
{
    public const string DefaultScopeName = "RootScope";

    public InteractivityNotification()
    {
        _automationPeer = new Lazy<InteractivityNotificationAutomationPeer>(() => new(this));

        this.WhenLoaded(OnLoaded);
    }

    #region Type

    public InteractivityNotificationType Type
    {
        get => (InteractivityNotificationType)GetValue(TypeProperty);
        set => SetValue(TypeProperty, value);
    }

    public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
        nameof(Type),
        typeof(InteractivityNotificationType),
        typeof(InteractivityNotification),
        new PropertyMetadata(InteractivityNotificationType.Neutral));

    #endregion

    #region Icon

    public UIKitIcon_24 Icon
    {
        get => (UIKitIcon_24)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
        nameof(Icon),
        typeof(UIKitIcon_24),
        typeof(InteractivityNotification),
        new PropertyMetadata(default(UIKitIcon_24)));

    #endregion

    #region ActionCaption

    public string? ActionCaption
    {
        get => (string?)GetValue(ActionCaptionProperty);
        set => SetValue(ActionCaptionProperty, value);
    }

    public static readonly DependencyProperty ActionCaptionProperty = DependencyProperty.Register(
        nameof(ActionCaption),
        typeof(string),
        typeof(InteractivityNotification),
        new PropertyMetadata(default(string)));

    #endregion

    #region ActionCommand

    public ICommand ActionCommand
    {
        get => (ICommand)GetValue(ActionCommandProperty);
        set => SetValue(ActionCommandProperty, value);
    }

    public static readonly DependencyProperty ActionCommandProperty = DependencyProperty.Register(
        nameof(ActionCommand),
        typeof(ICommand),
        typeof(InteractivityNotification),
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
        typeof(InteractivityNotification),
        new PropertyMetadata(true));

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
        typeof(InteractivityNotification),
        new PropertyMetadata(default(ICommand)));

    #endregion

    #region AutoCloseTimeout

    public TimeSpan AutoCloseTimeout
    {
        get => (TimeSpan)GetValue(AutoCloseTimeoutProperty);
        set => SetValue(AutoCloseTimeoutProperty, value);
    }

    public static readonly DependencyProperty AutoCloseTimeoutProperty = DependencyProperty.Register(
        nameof(AutoCloseTimeout),
        typeof(TimeSpan),
        typeof(InteractivityNotification),
        new PropertyMetadata(default(TimeSpan)));

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
        typeof(InteractivityNotification),
        new PropertyMetadata(DefaultScopeName));

    #endregion

    #region ScopeMaxNotificationCount

    public int ScopeMaxNotificationCount
    {
        get => (int)GetValue(ScopeMaxNotificationCountProperty);
        set => SetValue(ScopeMaxNotificationCountProperty, value);
    }

    public static readonly DependencyProperty ScopeMaxNotificationCountProperty = DependencyProperty.Register(
        nameof(ScopeMaxNotificationCount),
        typeof(int),
        typeof(InteractivityNotification),
        new PropertyMetadata(1));

    #endregion

    #region INotificationAnimatable

    void INotificationAnimatable.OnOpening(Action? completedCallback)
    {
        InteractivityNotificationScope.Register(this, completedCallback);
    }

    void INotificationAnimatable.OnClosing(Action? completedCallback)
    {
        InteractivityNotificationScope.Unregister(this, completedCallback);
    }

    #endregion

    internal void ShowContent(Action? continueCallback)
    {
        _mediaProvider.LaunchShowAnimation(this, onCompletedAction: () =>
        {
            if (_automationPeer.IsValueCreated)
            {
                _automationPeer.Value.RaiseShown();
            }

            continueCallback?.Invoke();
        });
    }

    internal void HideContent(Action? continueCallback)
    {
        _mediaProvider.LaunchHideAnimation(this, continueCallback);
    }

    internal void Close(bool forced = false)
    {
        if (forced)
        {
            _notificationView?.CloseForced();
        }
        else
        {
            _notificationView?.Close();
        }
    }

    protected override AutomationPeer OnCreateAutomationPeer()
    {
        return _automationPeer.Value;
    }

    private void OnLoaded()
    {
        var hasAutoClose = AutoCloseTimeout != TimeSpan.Zero;

        var notificationView = this.FindVisualParent<NotificationView>();
        if (notificationView != null && hasAutoClose)
        {
            _notificationView = notificationView;
            _notificationView.WhenOpened(() =>
            {
                if (!IsMouseOver)
                {
                    StartAutoClose();
                }
            });

            MouseLeave += (o, eventArgs) => StartAutoClose();
            MouseEnter += (o, eventArgs) => StopAutoClose();
        }
    }

    private void StartAutoClose()
    {
        _deferredClose?.Cancel();
        _deferredClose = DeferredActionFactory.CreateDebouncerOnUi(() => Close(forced: false), AutoCloseTimeout);
        _deferredClose.Execute();
    }

    private void StopAutoClose()
    {
        _deferredClose?.Cancel();
    }

    private readonly InteractivityMediaProvider _mediaProvider = new();
    private readonly Lazy<InteractivityNotificationAutomationPeer> _automationPeer;

    private IDeferredAction? _deferredClose;
    private NotificationView? _notificationView;
}
