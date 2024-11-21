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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Markup;
using TriggerBase = System.Windows.TriggerBase;

namespace Kaspirin.UI.Framework.UiKit.MarkupExtensions
{
    public abstract class ExtendedMarkupExtension : MarkupExtension
    {
        public sealed override object? ProvideValue(IServiceProvider? serviceProvider)
        {
            var targetType = GetTargetType(serviceProvider, out var targetObject, out var targetProperty);

            var result = targetType switch
            {
                TargetType.Unknown => ProvideSelf(serviceProvider),
                TargetType.Freezable => ProvideForFreezable(serviceProvider!, (Freezable)targetObject!, targetProperty!),
                TargetType.Setter => ProvideForSetter(serviceProvider!, (SetterBase)targetObject!, targetProperty!),
                TargetType.Trigger => ProvideForTrigger(serviceProvider!, (TriggerBase)targetObject!, targetProperty!),
                TargetType.Control => ProvideForControl(serviceProvider!, (DependencyObject)targetObject!, (DependencyProperty)targetProperty!),
                TargetType.ControlTemplate => ProvideForControlTemplate(serviceProvider!, targetObject!, (DependencyProperty)targetProperty!),
                TargetType.MarkupExtension => ProvideForMarkupExtension(serviceProvider!, (MarkupExtension)targetObject!, targetProperty!),
                _ => null
            };

            return result ?? ProvideValue(serviceProvider, targetType);
        }

        protected virtual object? ProvideForFreezable(IServiceProvider serviceProvider, Freezable targetObject, object targetProperty)
            => null;

        protected virtual object? ProvideForControl(IServiceProvider serviceProvider, DependencyObject targetObject, DependencyProperty targetProperty)
            => null;

        protected virtual object? ProvideForControlTemplate(IServiceProvider serviceProvider, object targetObject, DependencyProperty targetProperty)
            => null;

        protected virtual object? ProvideForSetter(IServiceProvider serviceProvider, SetterBase targetObject, object targetProperty)
            => null;

        protected virtual object? ProvideForTrigger(IServiceProvider serviceProvider, TriggerBase targetObject, object targetProperty)
            => null;

        protected virtual object? ProvideForMarkupExtension(IServiceProvider serviceProvider, MarkupExtension markupExtension, object targetProperty)
            => null;

        protected virtual object? ProvideSelf(IServiceProvider? serviceProvider)
            => this;

        protected virtual object? ProvideValue(IServiceProvider? serviceProvider, TargetType valueType)
            => DependencyProperty.UnsetValue;

        private static bool TryGetTargetInfo(IServiceProvider serviceProvider, [NotNullWhen(true)] out object? targetObject, [NotNullWhen(true)] out object? targetProperty)
        {
            targetObject = null;
            targetProperty = null;

            var targetInfo = (IProvideValueTarget?)serviceProvider.GetService(typeof(IProvideValueTarget));
            if (targetInfo != null)
            {
                targetObject = targetInfo.TargetObject;
                targetProperty = targetInfo.TargetProperty;
                return true;
            }

            return false;
        }

        private static TargetType GetTargetType(IServiceProvider? serviceProvider, out object? targetObject, out object? targetProperty)
        {
            targetObject = null;
            targetProperty = null;

            if (serviceProvider == null)
            {
                return TargetType.Unknown;
            }

            if (TryGetTargetInfo(serviceProvider, out targetObject, out targetProperty))
            {
                if (targetObject is Freezable)
                {
                    return TargetType.Freezable;
                }

                if (targetObject is DependencyObject &&
                    targetProperty is DependencyProperty)
                {
                    return TargetType.Control;
                }

                if (targetObject is not DependencyObject &&
                    targetProperty is DependencyProperty)
                {
                    return TargetType.ControlTemplate;
                }

                if (targetObject is SetterBase)
                {
                    return TargetType.Setter;
                }

                if (targetObject is TriggerBase)
                {
                    return TargetType.Trigger;
                }

                if (targetObject is MarkupExtension)
                {
                    return TargetType.MarkupExtension;
                }
            }

            return TargetType.Unknown;
        }

        protected enum TargetType
        {
            Unknown,
            Freezable,
            Setter,
            Trigger,
            ControlTemplate,
            Control,
            MarkupExtension,
        }
    }
}
