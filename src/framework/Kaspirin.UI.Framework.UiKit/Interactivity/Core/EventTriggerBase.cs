// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 1/10/2024.
// Scope of modification:
//   - Code adaptation to project requirements.

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Reflection;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Interactivity.Core
{
    public abstract class EventTriggerBase : TriggerBase
    {
        internal EventTriggerBase(Type sourceTypeConstraint) : base(typeof(DependencyObject))
        {
            SourceTypeConstraint = sourceTypeConstraint;
        }

        #region SourceObject

        public object SourceObject
        {
            get { return GetValue(SourceObjectProperty); }
            set { SetValue(SourceObjectProperty, value); }
        }

        public static readonly DependencyProperty SourceObjectProperty =
            DependencyProperty.Register("SourceObject", typeof(object), typeof(EventTriggerBase), new PropertyMetadata(OnSourceObjectChanged));

        private static void OnSourceObjectChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            => ((EventTriggerBase)obj).OnSourceChanged(args.OldValue, args.NewValue);

        #endregion

        public object? Source => SourceObject ?? AssociatedObject;

        protected sealed override Type AssociatedObjectTypeConstraint => typeof(DependencyObject);

        protected Type SourceTypeConstraint { get; }

        protected abstract string GetEventName();

        protected virtual void OnEvent(EventArgs eventArgs)
        {
            InvokeActions(eventArgs);

            _trace.TraceInformation($"{GetType().Name} invoked actions for '{GetEventName()}' event on '{AssociatedObject}' object.");
        }

        protected void OnEventNameChanged(string oldEventName, string newEventName)
        {
            if (AssociatedObject != null)
            {
                Guard.IsNotNull(Source);

                if (!string.IsNullOrEmpty(oldEventName))
                {
                    UnregisterEvent(Source, oldEventName);
                }

                if (!string.IsNullOrEmpty(newEventName))
                {
                    RegisterEvent(Source, newEventName);
                }
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            var newHostElement = AssociatedObject as FrameworkElement;

            if (newHostElement == null || SourceObject != null)
            {
                OnSourceChanged(null, Source);
            }
            else if (newHostElement != null)
            {
                newHostElement.WhenLoaded(() =>
                {
                    if (SourceObject == null)
                    {
                        OnSourceChanged(null, Source);
                    }
                });
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            try
            {
                OnSourceChanged(Source, null);
            }
            catch (InvalidOperationException e)
            {
                // We fire off the source changed manually when we detach. However, if we've been attached to 
                // something that doesn't meet the target type constraint, accessing Source will throw.

                e.TraceExceptionSuppressed();
            }
        }

        private void OnSourceChanged(object? oldSource, object? newSource)
        {
            if (AssociatedObject != null)
            {
                if (string.IsNullOrEmpty(GetEventName()))
                {
                    return;
                }

                if (oldSource != null && SourceTypeConstraint.IsAssignableFrom(oldSource.GetType()))
                {
                    UnregisterEvent(oldSource, GetEventName());
                }

                if (newSource != null && SourceTypeConstraint.IsAssignableFrom(newSource.GetType()))
                {
                    RegisterEvent(newSource, GetEventName());
                }
            }
        }

        private void RegisterEvent(object obj, string eventName)
        {
            Guard.Assert(_eventHandlerMethodInfo == null);

            var targetType = obj.GetType();
            var eventInfo = targetType.GetEvent(eventName);
            if (eventInfo == null)
            {
                if (SourceObject != null)
                {
                    throw new ArgumentException($"Cannot find an event named \"{eventName}\" on type \"{obj.GetType().Name}\".");
                }
                else
                {
                    return;
                }
            }

            if (!IsValidEvent(eventInfo))
            {
                if (SourceObject != null)
                {
                    throw new ArgumentException($"The event \"{eventName}\" on type \"{obj.GetType().Name}\" has an incompatible signature. Make sure the event is public and satisfies the EventHandler delegate.");
                }
                else
                {
                    return;
                }
            }

            _eventHandlerMethodInfo = typeof(EventTriggerBase).GetMethod(nameof(OnEventImpl), BindingFlags.NonPublic | BindingFlags.Instance);
            eventInfo.AddEventHandler(obj, Delegate.CreateDelegate(eventInfo.EventHandlerType!, this, _eventHandlerMethodInfo!));
        }

        private void UnregisterEvent(object obj, string eventName)
        {
            if (_eventHandlerMethodInfo == null)
            {
                return;
            }

            var targetType = obj.GetType();
            var eventInfo = targetType.GetEvent(eventName);

            Guard.IsNotNull(eventInfo);

            eventInfo.RemoveEventHandler(obj, Delegate.CreateDelegate(eventInfo.EventHandlerType!, this, _eventHandlerMethodInfo));
            _eventHandlerMethodInfo = null;
        }

        private void OnEventImpl(object sender, EventArgs eventArgs)
        {
            OnEvent(eventArgs);
        }

        private static bool IsValidEvent(EventInfo eventInfo)
        {
            var eventHandlerType = eventInfo.EventHandlerType;
            if (typeof(Delegate).IsAssignableFrom(eventInfo.EventHandlerType))
            {
                var invokeMethod = eventHandlerType?.GetMethod("Invoke");
                var parameters = invokeMethod?.GetParameters();

                return parameters != null && parameters.Length == 2 && typeof(object).IsAssignableFrom(parameters[0].ParameterType) && typeof(EventArgs).IsAssignableFrom(parameters[1].ParameterType);
            }

            return false;
        }

        private static readonly ComponentTracer _trace = ComponentTracer.Get(UIKitComponentTracers.Interactivity);

        private MethodInfo? _eventHandlerMethodInfo;
    }
}
