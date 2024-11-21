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
    public static class Interaction
    {
        #region Triggers

        private static readonly DependencyProperty _triggersProperty =
            DependencyProperty.RegisterAttached("TriggersInternal", typeof(TriggerCollection), typeof(Interaction),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnTriggersChanged)));

        public static TriggerCollection GetTriggers(DependencyObject obj)
        {
            var triggerCollection = (TriggerCollection)obj.GetValue(_triggersProperty);
            if (triggerCollection == null)
            {
                triggerCollection = new TriggerCollection();
                obj.SetValue(_triggersProperty, triggerCollection);
            }

            return triggerCollection;
        }

        private static void OnTriggersChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var oldCollection = args.OldValue as TriggerCollection;
            var newCollection = args.NewValue as TriggerCollection;

            if (oldCollection != newCollection)
            {
                if (oldCollection != null && oldCollection.AssociatedObject != null)
                {
                    oldCollection.Detach();
                }

                if (newCollection != null && obj != null)
                {
                    if (newCollection.AssociatedObject != null)
                    {
                        throw new InvalidOperationException("Cannot set the same TriggerCollection on multiple objects.");
                    }

                    newCollection.Attach(obj);
                }
            }
        }

        #endregion

        #region Behaviors

        private static readonly DependencyProperty _behaviorsProperty =
            DependencyProperty.RegisterAttached("BehaviorsInternal", typeof(BehaviorCollection), typeof(Interaction),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnBehaviorsChanged)));

        public static BehaviorCollection GetBehaviors(DependencyObject obj)
        {
            var behaviorCollection = (BehaviorCollection)obj.GetValue(_behaviorsProperty);
            if (behaviorCollection == null)
            {
                behaviorCollection = new BehaviorCollection();
                obj.SetValue(_behaviorsProperty, behaviorCollection);
            }

            return behaviorCollection;
        }

        private static void OnBehaviorsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var oldCollection = (BehaviorCollection)args.OldValue;
            var newCollection = (BehaviorCollection)args.NewValue;

            if (oldCollection != newCollection)
            {
                if (oldCollection != null && oldCollection.AssociatedObject != null)
                {
                    oldCollection.Detach();
                }

                if (newCollection != null && obj != null)
                {
                    if (newCollection.AssociatedObject != null)
                    {
                        throw new InvalidOperationException("Cannot set the same BehaviorCollection on multiple objects.");
                    }

                    newCollection.Attach(obj);
                }
            }
        }

        #endregion
    }
}
