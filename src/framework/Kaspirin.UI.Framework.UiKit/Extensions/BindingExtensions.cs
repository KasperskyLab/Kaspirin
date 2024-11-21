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
using System.Linq;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Extensions
{
    public static class BindingExtensions
    {
        public static TBinding Clone<TBinding>(this TBinding binding) where TBinding : BindingBase
            => (TBinding)CloneWithInternal(binding, modificator: null);

        public static TBinding CloneWith<TBinding>(this TBinding binding, Action<BindingBase> modificator) where TBinding : BindingBase
            => (TBinding)CloneWithInternal(binding, modificator);

        private static BindingBase CloneWithInternal(BindingBase bindingBase, Action<BindingBase>? modificator)
        {
            Guard.ArgumentIsNotNull(bindingBase);

            if (bindingBase is Binding binding)
            {
                return CloneBinding(binding, modificator);
            }
            else if (bindingBase is MultiBinding multiBinding)
            {
                return CloneMultiBinding(multiBinding, modificator);
            }

            throw new NotSupportedException($"Binding of type '{bindingBase.GetType()}' not supported");
        }

        private static Binding CloneBinding(Binding source, Action<BindingBase>? modificator)
        {
            var clone = new Binding
            {
                Path = source.Path,
                Mode = source.Mode,
                XPath = source.XPath,
                IsAsync = source.IsAsync,
                AsyncState = source.AsyncState,
                BindsDirectlyToSource = source.BindsDirectlyToSource,
                ConverterCulture = source.ConverterCulture,
                Converter = source.Converter,
                ConverterParameter = source.ConverterParameter,
                NotifyOnSourceUpdated = source.NotifyOnSourceUpdated,
                NotifyOnTargetUpdated = source.NotifyOnTargetUpdated,
                NotifyOnValidationError = source.NotifyOnValidationError,
                UpdateSourceTrigger = source.UpdateSourceTrigger,
                ValidatesOnDataErrors = source.ValidatesOnDataErrors,
            };

            if (source.ValidationRules?.Any() == true)
            {
                clone.ValidationRules.AddRange(source.ValidationRules);
            }

            if (source.RelativeSource != null)
            {
                clone.RelativeSource = source.RelativeSource;
            }
            else if (source.Source != null)
            {
                clone.Source = source.Source;
            }
            else if (source.ElementName != null)
            {
                clone.ElementName = source.ElementName;
            }

            FillBindingBaseProperties(source, clone);

            modificator?.Invoke(clone);

            return clone;
        }

        private static MultiBinding CloneMultiBinding(MultiBinding source, Action<BindingBase>? modificator)
        {
            var clone = new MultiBinding
            {
                ConverterCulture = source.ConverterCulture,
                Converter = source.Converter,
                ConverterParameter = source.ConverterParameter,
                NotifyOnSourceUpdated = source.NotifyOnSourceUpdated,
                NotifyOnTargetUpdated = source.NotifyOnTargetUpdated,
                NotifyOnValidationError = source.NotifyOnValidationError,
                UpdateSourceExceptionFilter = source.UpdateSourceExceptionFilter
            };

            foreach (var binding in source.Bindings)
            {
                clone.Bindings.Add(CloneWithInternal(binding, modificator));
            }

            FillBindingBaseProperties(source, clone);

            modificator?.Invoke(clone);

            return clone;
        }

        private static void FillBindingBaseProperties(BindingBase source, BindingBase target)
        {
            target.FallbackValue = source.FallbackValue;
            target.StringFormat = source.StringFormat;
            target.TargetNullValue = source.TargetNullValue;
            target.BindingGroupName = source.BindingGroupName;
        }
    }
}
