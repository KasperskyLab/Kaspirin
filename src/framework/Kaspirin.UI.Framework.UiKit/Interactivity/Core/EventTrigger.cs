// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 1/10/2024.
// Scope of modification:
//   - Code adaptation to project requirements.

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Interactivity.Core
{
    public class EventTrigger : EventTriggerBase<object>
    {
        public EventTrigger()
        {
        }

        public EventTrigger(string eventName)
        {
            EventName = eventName;
        }

        #region EventName

        public string EventName
        {
            get => (string)GetValue(EventNameProperty);
            set => SetValue(EventNameProperty, value);
        }

        public static readonly DependencyProperty EventNameProperty = DependencyProperty.Register(
            nameof(EventName),
            typeof(string),
            typeof(EventTrigger),
            new PropertyMetadata(default(string), OnEventNameChanged));

        private static void OnEventNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((EventTrigger)d).OnEventNameChanged((string)e.OldValue, (string)e.NewValue);

        #endregion

        protected override string GetEventName()
        {
            return EventName;
        }
    }
}
