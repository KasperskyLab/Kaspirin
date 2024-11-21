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

using System.Linq;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Interactivity.Core
{
    public abstract class Behavior<TAssociatedObject, TBehavior> : Behavior<TAssociatedObject>
        where TAssociatedObject : DependencyObject
        where TBehavior : Behavior<TAssociatedObject>, new()
    {
        #region IsEnabled

        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
            "IsEnabled",
            typeof(bool),
            typeof(Behavior<TAssociatedObject, TBehavior>),
            new PropertyMetadata(false, OnIsEnabledChanged));

        public static bool GetIsEnabled(DependencyObject obj)
            => (bool)obj.GetValue(IsEnabledProperty);

        public static void SetIsEnabled(DependencyObject obj, bool value)
            => obj.SetValue(IsEnabledProperty, value);

        private static void OnIsEnabledChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (dependencyObject is not TAssociatedObject)
            {
                return;
            }

            var behaviors = Interaction.GetBehaviors(dependencyObject);
            if ((bool)args.NewValue)
            {
                var alreadyExists = behaviors.OfType<TBehavior>().GuardedSingleOrDefault() is not null;
                if (!alreadyExists)
                {
                    behaviors.Add(new TBehavior());
                }
            }
            else
            {
                var itemToRemove = behaviors.Where(x => x.GetType() == typeof(TBehavior)).GuardedSingleOrDefault();
                if (itemToRemove is not null)
                {
                    behaviors.Remove(itemToRemove);
                }
            }
        }

        #endregion

        protected static TBehavior GetBehavior(DependencyObject obj)
            => Interaction.GetBehaviors(obj).OfType<TBehavior>().GuardedSingle();
    }
}