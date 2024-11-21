// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 1/10/2024.
// Scope of modification:
//   - Code adaptation to project requirements.

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Interactivity.Core
{
    public abstract class TriggerAction : Freezable
    {
        internal TriggerAction(Type associatedObjectTypeConstraint)
        {
            _associatedObjectTypeConstraint = associatedObjectTypeConstraint;
        }

        #region IsEnabled

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register("IsEnabled", typeof(bool), typeof(TriggerAction), new PropertyMetadata(true));

        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        #endregion

        public DependencyObject? AssociatedObject { get; private set; }

        public void Attach(DependencyObject? dependencyObject)
        {
            if (AssociatedObject == dependencyObject)
            {
                return;
            }

            if (AssociatedObject != null)
            {
                throw new InvalidOperationException("Cannot host an instance of a TriggerAction in multiple TriggerCollections simultaneously. Remove it from one TriggerCollection before adding it to another.");
            }

            if (dependencyObject != null && !AssociatedObjectTypeConstraint.IsAssignableFrom(dependencyObject.GetType()))
            {
                throw new InvalidOperationException($"Cannot attach type \"{GetType().Name}\" to type \"{dependencyObject.GetType().Name}\". " +
                                                    $"Instances of type \"{GetType().Name}\" can only be attached to objects of type \"{AssociatedObjectTypeConstraint.Name}\".");
            }

            AssociatedObject = dependencyObject;

            OnAttached();
        }

        public void Detach()
        {
            OnDetaching();
            AssociatedObject = null;
        }

        internal bool IsHosted { get; set; }

        internal void CallInvoke(object parameter)
        {
            if (IsEnabled)
            {
                Invoke(parameter);
            }
        }

        protected virtual Type AssociatedObjectTypeConstraint
        {
            get
            {
                return _associatedObjectTypeConstraint;
            }
        }

        protected abstract void Invoke(object parameter);

        protected virtual void OnAttached()
        {
        }

        protected virtual void OnDetaching()
        {
        }

        protected override Freezable? CreateInstanceCore()
        {
            throw new NotSupportedException();
        }

        private readonly Type _associatedObjectTypeConstraint;
    }
}
