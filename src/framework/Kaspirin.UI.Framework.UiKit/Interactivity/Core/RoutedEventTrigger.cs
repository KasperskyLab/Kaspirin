// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 1/10/2024.
// Scope of modification:
//   - Code adaptation to project requirements.

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Interactivity.Core
{
    public class RoutedEventTrigger : EventTriggerBase<UIElement>
    {
        public RoutedEventTrigger()
        {
        }

        public RoutedEventTrigger(RoutedEvent routedEvent)
        {
            Event = routedEvent;
        }

        #region Event

        public RoutedEvent Event
        {
            get => (RoutedEvent)GetValue(EventProperty);
            set => SetValue(EventProperty, value);
        }

        public static readonly DependencyProperty EventProperty = DependencyProperty.Register(
            nameof(Event),
            typeof(RoutedEvent),
            typeof(RoutedEventTrigger),
            new PropertyMetadata(default(RoutedEvent), OnEventChanged));

        private static void OnEventChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((RoutedEventTrigger)d).OnEventChanged(e.OldValue, e.NewValue);

        #endregion

        protected override string GetEventName()
        {
            return Event.Name;
        }

        protected override void OnAttached()
        {
            Guard.IsNotNull(Event);
            Guard.Assert(AssociatedObject is UIElement);

            AddHandler(Event);
        }

        protected override void OnDetaching()
        {
            Guard.IsNotNull(Event);
            Guard.Assert(AssociatedObject is UIElement);

            RemoveHandler(Event);
        }

        private void OnEventChanged(object? oldValue, object? newValue)
        {
            if (oldValue is RoutedEvent oldRoutedEvent)
            {
                RemoveHandler(oldRoutedEvent);
            }

            if (newValue is RoutedEvent newRoutedEvent)
            {
                AddHandler(newRoutedEvent);
            }
        }

        private void OnRoutedEvent(object sender, RoutedEventArgs args)
        {
            OnEvent(args);
        }

        private void AddHandler(RoutedEvent routedEvent)
        {
            if (AssociatedObject is UIElement uiElement)
            {
                uiElement.RemoveHandler(routedEvent, new RoutedEventHandler(OnRoutedEvent));
                uiElement.AddHandler(routedEvent, new RoutedEventHandler(OnRoutedEvent));
            }
        }

        private void RemoveHandler(RoutedEvent routedEvent)
        {
            if (AssociatedObject is UIElement uiElement)
            {
                uiElement.RemoveHandler(routedEvent, new RoutedEventHandler(OnRoutedEvent));
            }
        }
    }
}
