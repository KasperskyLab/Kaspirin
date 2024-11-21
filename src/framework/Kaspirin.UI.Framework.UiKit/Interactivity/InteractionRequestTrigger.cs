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

using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Interactivity
{
    public sealed class InteractionRequestTrigger : EventTriggerBase<InteractionRequestBase>
    {
        protected override string GetEventName()
        {
            return nameof(InteractionRequestBase<InteractionObject>.TriggerActionRaised);
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            var parent = (FrameworkElement?)AssociatedObject;
            if (parent != null)
            {
                Tracer.TraceDebug($"Trigger[{GetHashCode()}] attached to {parent.GetType().Name}");

                _parent = parent;
                _parent.Unloaded += ParentUnloaded;
            }
        }

        private void ParentUnloaded(object sender, RoutedEventArgs e)
        {
            Guard.IsNotNull(_parent);

            Tracer.TraceDebug($"Trigger[{GetHashCode()}] parent {_parent.GetType().Name} unloaded");

            _parent.Unloaded -= ParentUnloaded;
            _parent.Loaded += ParentReloaded;
            Detach();
        }

        private void ParentReloaded(object sender, RoutedEventArgs e)
        {
            Guard.IsNotNull(_parent);

            Tracer.TraceDebug($"Trigger[{GetHashCode()}] parent {_parent.GetType().Name} reloaded");

            _parent.Loaded -= ParentReloaded;
            Attach(_parent);
        }

        private FrameworkElement? _parent;

        private static readonly ComponentTracer Tracer = ComponentTracer.Get(UIKitComponentTracers.Interactivity);
    }
}
