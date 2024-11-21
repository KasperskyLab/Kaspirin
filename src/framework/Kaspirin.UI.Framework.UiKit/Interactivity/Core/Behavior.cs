// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 1/10/2024.
// Scope of modification:
//   - Code adaptation to project requirements.

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using System;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Interactivity.Core
{
    public abstract class Behavior : Freezable
    {
        internal Behavior(Type associatedType)
        {
            AssociatedType = associatedType;
        }

        public Type AssociatedType { get; }

        public DependencyObject? AssociatedObject { get; private set; }

        public void Attach(DependencyObject? dependencyObject)
        {
            if (AssociatedObject == dependencyObject)
            {
                return;
            }

            if (AssociatedObject != null)
            {
                throw new InvalidOperationException("An instance of a Behavior cannot be attached to more than one object at a time.");
            }

            if (dependencyObject != null && !AssociatedType.IsAssignableFrom(dependencyObject.GetType()))
            {
                throw new InvalidOperationException($"Cannot attach type \"{GetType().Name}\" to type \"{dependencyObject.GetType().Name}\". Instances of type \"{GetType().Name}\" can only be attached to objects of type \"{AssociatedType.Name}\".");
            }

            AssociatedObject = dependencyObject;
            OnAssociatedObjectChanged();

            AssociatedObject?.WhenInitialized(() =>
            {
                if (AssociatedObject != null)
                {
                    OnAttached();

                    _foObject = new FrameworkObject(AssociatedObject);
                    _foObject.Loaded += OnAssociatedObjectLoaded;
                    _foObject.Unloaded += OnAssociatedObjectUnloaded;
                }
            });
        }

        public void Detach()
        {
            OnDetaching();
            AssociatedObject = null;

            if (_foObject != null)
            {
                _foObject.Loaded -= OnAssociatedObjectLoaded;
                _foObject.Unloaded -= OnAssociatedObjectUnloaded;
                _foObject = null;
            }

            OnAssociatedObjectChanged();
        }

        internal event EventHandler AssociatedObjectChanged = delegate { };

        protected virtual void OnAssociatedObjectLoaded()
        {
        }

        protected virtual void OnAssociatedObjectUnloaded()
        {
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

        private void OnAssociatedObjectChanged()
        {
            AssociatedObjectChanged.Invoke(this, new EventArgs());
        }

        private void OnAssociatedObjectUnloaded(object sender, RoutedEventArgs e)
        {
            if (AssociatedObject != null)
            {
                OnAssociatedObjectUnloaded();
            }
        }

        private void OnAssociatedObjectLoaded(object sender, RoutedEventArgs e)
        {
            if (AssociatedObject != null)
            {
                OnAssociatedObjectLoaded();
            }
        }

        private FrameworkObject? _foObject;
    }
}
