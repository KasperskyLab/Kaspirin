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
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Interactivity;

public sealed class InteractionRequestTrigger : EventTriggerBase<InteractionRequestBase>
{
    public override string ToString()
    {
        return $"{nameof(InteractionRequestTrigger)} [" +
            $"SourceObject:{SourceObject?.GetType().Name ?? "<null>"}; " +
            $"SourceObjectPath:{SourceObjectPath?.Path ?? "<null>"}; " +
            $"AssociatedObject:{AssociatedObject?.GetType().Name ?? "<null>"}; " +
            $"Hash:{GetHashCode()}]";
    }

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
            _parent = parent;
            _parent.WhenUnloaded(OnParentUnloaded);
        }

        _tracer.TraceInformation($"{this} attached.");
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        _tracer.TraceInformation($"{this} detached.");
    }

    private void OnParentUnloaded()
    {
        Guard.IsNotNull(_parent);

        _tracer.TraceInformation($"{this} parent unloaded.");

        _parent.WhenLoaded(OnParentLoaded);

        Detach();
    }

    private void OnParentLoaded()
    {
        Guard.IsNotNull(_parent);

        _tracer.TraceInformation($"{this} parent loaded.");

        Attach(_parent);
    }

    private PropertyPath? SourceObjectPath => (BindingOperations.GetBindingExpression(this, SourceObjectProperty)?.ParentBindingBase as Binding)?.Path;

    private FrameworkElement? _parent;

    private static readonly ComponentTracer _tracer = ComponentTracer.Get(UIKitComponentTracers.Interactivity);
}
