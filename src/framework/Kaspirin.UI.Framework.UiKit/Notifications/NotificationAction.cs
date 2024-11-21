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
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.Notifications
{
    [StyleTypedProperty(Property = "ContentPanelStyle", StyleTargetType = typeof(Control))]
    [DefaultProperty("Content")]
    [ContentProperty("Content")]
    public sealed class NotificationAction : TriggerAction<FrameworkElement>
    {
        #region Content
        [Bindable(true)]
        public object Content
        {
            get { return GetValue(ContentControl.ContentProperty); }
            set { SetValue(ContentControl.ContentProperty, value); }
        }

        #endregion

        #region ContentTemplate
        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }

        public static readonly DependencyProperty ContentTemplateProperty =
            DependencyProperty.Register("ContentTemplate", typeof(DataTemplate), typeof(NotificationAction));
        #endregion

        #region AutoCloseTimeout
        public TimeSpan AutoCloseTimeout
        {
            get { return (TimeSpan)GetValue(AutoCloseTimeoutProperty); }
            set { SetValue(AutoCloseTimeoutProperty, value); }
        }

        public static readonly DependencyProperty AutoCloseTimeoutProperty =
            DependencyProperty.Register("AutoCloseTimeout", typeof(TimeSpan), typeof(NotificationAction), new PropertyMetadata(TimeSpan.FromSeconds(4)));
        #endregion

        #region IsModal
        public bool IsModal
        {
            get { return (bool)GetValue(IsModalProperty); }
            set { SetValue(IsModalProperty, value); }
        }

        public static readonly DependencyProperty IsModalProperty =
            DependencyProperty.Register("IsModal", typeof(bool), typeof(NotificationAction), new PropertyMetadata(true));
        #endregion

        #region NotificationLayerName

        public string? NotificationLayerName
        {
            get { return (string?)GetValue(NotificationLayerNameProperty); }
            set { SetValue(NotificationLayerNameProperty, value); }
        }

        public static readonly DependencyProperty NotificationLayerNameProperty = DependencyProperty.Register(
            "NotificationLayerName", typeof(string), typeof(NotificationAction));

        #endregion

        #region MaxNotificationCount

        public int MaxNotificationCount
        {
            get { return (int)GetValue(MaxNotificationCountProperty); }
            set { SetValue(MaxNotificationCountProperty, value); }
        }

        public static readonly DependencyProperty MaxNotificationCountProperty =
            DependencyProperty.Register("MaxNotificationCount", typeof(int), typeof(NotificationAction), new PropertyMetadata(10));

        #endregion

        #region NotificationLocation

        public NotificationLocationSettings LocationSettings
        {
            get { return (NotificationLocationSettings)GetValue(LocationSettingsProperty); }
            set { SetValue(LocationSettingsProperty, value); }
        }

        public static readonly DependencyProperty LocationSettingsProperty =
            DependencyProperty.Register("LocationSettings", typeof(NotificationLocationSettings), typeof(NotificationAction));

        #endregion

        protected override void Invoke(object parameter)
        {
            _trace.TraceDebug($"NotificationAction.Invoke called, parameter={parameter?.GetType().Name}");

            var args = parameter as InteractionRequestedEventArgs;

            if (args == null || args.InteractionObject == null)
            {
                _trace.TraceDebug($"Skip NotificationAction.Invoke, args={args}, args.InteractionObject={args?.InteractionObject?.GetType().Name}");
                return;
            }

            var view = _view;
            if (view != null)
            {
                if (view.IsClosing)
                {
                    view.CloseForced();
                }
                else if (view.IsModal)
                {
                    _trace.TraceWarning("Skipping notification view, because another modal view is active");
                    return;
                }
            }

            var interactionObject = args.InteractionObject;

            _trace.TraceDebug($"Start creating NotificationView with interactionObject={interactionObject.GetType().Name}");

            Guard.IsNotNull(AssociatedObject);

            view = NotificationLauncher.Create(
                associatedObject: AssociatedObject,
                content: Content ?? interactionObject.GetDataContext(),
                contentTemplate: ContentTemplate,
                locationSettings: LocationSettings,
                notificationLayerName: NotificationLayerName,
                autoCloseTimeout: AutoCloseTimeout,
                maxNotificationCount: MaxNotificationCount,
                isModal: IsModal);

            _trace.TraceDebug($"NotificationView with interactionObject={interactionObject.GetType().Name} created");

            interactionObject.PreviewDecided += () => view.CloseSmooth();

            view.Closed += (s, e) => OnClosed(interactionObject);

            _trace.TraceDebug($"Show NotificationView with interactionObject={interactionObject.GetType().Name}");

            view.SetInteractionObject(interactionObject);
            view.Show();

            _view = view;
        }

        private void OnClosed(InteractionObject interactionObject)
        {
            if (interactionObject.IsDecided is false)
            {
                interactionObject.Handle();
            }

            _view = null;
        }

        private NotificationView? _view;

        private static readonly ComponentTracer _trace = ComponentTracer.Get(UIKitComponentTracers.Notification);
    }
}
