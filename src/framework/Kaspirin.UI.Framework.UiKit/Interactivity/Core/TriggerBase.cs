// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 1/10/2024.
// Scope of modification:
//   - Code adaptation to project requirements.

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Windows;
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.Interactivity.Core
{
    [ContentProperty("Actions")]
    public abstract class TriggerBase : Freezable
    {
        internal TriggerBase(Type associatedObjectTypeConstraint)
        {
            _associatedObjectTypeConstraint = associatedObjectTypeConstraint;

            SetValue(_actionsPropertyKey, new TriggerActionCollection());
        }

        #region Actions

        private static readonly DependencyPropertyKey _actionsPropertyKey =
            DependencyProperty.RegisterReadOnly("Actions", typeof(TriggerActionCollection), typeof(TriggerBase), new FrameworkPropertyMetadata());

        public static readonly DependencyProperty ActionsProperty = _actionsPropertyKey.DependencyProperty;

        public TriggerActionCollection Actions
        {
            get
            {
                return (TriggerActionCollection)GetValue(ActionsProperty);
            }
        }

        #endregion

        public void Attach(DependencyObject? dependencyObject)
        {
            if (AssociatedObject == dependencyObject)
            {
                return;
            }

            if (AssociatedObject != null)
            {
                throw new InvalidOperationException("An instance of a trigger cannot be attached to more than one object at a time.");
            }

            if (dependencyObject != null && !AssociatedObjectTypeConstraint.IsAssignableFrom(dependencyObject.GetType()))
            {
                throw new InvalidOperationException($"Cannot attach type \"{GetType().Name}\" to type \"{dependencyObject.GetType().Name}\". " +
                                                    $"Instances of type \"{GetType().Name}\" can only be attached to objects of type \"{AssociatedObjectTypeConstraint.Name}\".");
            }

            AssociatedObject = dependencyObject;

            Actions.Attach(dependencyObject);
            OnAttached();
        }

        public void Detach()
        {
            OnDetaching();
            AssociatedObject = null;
            Actions.Detach();
        }

        internal DependencyObject? AssociatedObject { get; private set; }

        protected virtual Type AssociatedObjectTypeConstraint
        {
            get
            {
                return _associatedObjectTypeConstraint;
            }
        }

        protected void InvokeActions(object parameter)
        {
            foreach (var action in Actions)
            {
                action.CallInvoke(parameter);
            }
        }

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
