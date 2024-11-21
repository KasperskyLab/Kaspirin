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
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.Localization.Markup
{
    public abstract class LocalizationMarkupBase : MarkupExtension, ILocalizationMarkup, IBindingProvider
    {
        protected LocalizationMarkupBase(string key) : this(key, string.Empty)
        {
        }

        protected LocalizationMarkupBase(string key, string scope)
        {
            Key = Guard.EnsureArgumentIsNotNull(key);
            Scope = Guard.EnsureArgumentIsNotNull(scope);

            _toString = new Lazy<string>(() =>
            {
                var scopeStr = string.IsNullOrWhiteSpace(Scope)
                    ? string.Empty
                    : $", {Scope}";
                return $"{GetType().Name}({Key}{scopeStr})";
            });
        }

        [ConstructorArgument("key")]
        public string Key { get; set; }

        public string Scope { get; set; }

        public sealed override object? ProvideValue(IServiceProvider? serviceProvider)
        {
            try
            {
                var valueType = GetValueType(serviceProvider);
                if (valueType == ValueType.MarkupExtension)
                {
                    return this;
                }

                switch (valueType)
                {
                    case ValueType.Static:
                        {
                            return ProvideValue();
                        }
                    case ValueType.Binding:
                        {
                            return ProvideBinding();
                        }
                    case ValueType.BindingExpression:
                        {
                            return ProvideBinding().ProvideValue(serviceProvider);
                        }
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
                e.ProcessAsMarkupException($"Failed to create localization resource. Key='{Key}' Scope='{Scope}'");
                return ProvideFallback();
            }
        }

        public BindingBase ProvideBinding()
        {
            var metadata = PrepareMetadataBinding();
            var parameters = HasParameters
                ? PrepareParameterBindings()
                : new List<Binding>();

            var complexParameters = parameters
                .Select(p => p.Source)
                .OfType<ResourceDelivery>()
                .ToList();

            var valueBinding = new MultiBinding
            {
                Mode = BindingMode.OneWay,
                Converter = new DelegateMultiConverter(OnValueRequested)
            };

            if (metadata != null)
            {
                valueBinding.Bindings.Add(metadata);
            }

            if (complexParameters.Any())
            {
                valueBinding.Bindings.Add(new Binding
                {
                    Path = new PropertyPath("DataContext"),
                    RelativeSource = new RelativeSource(RelativeSourceMode.Self),
                    Converter = new DelegateConverter(dataContext =>
                    {
                        UpdateParametersDataContext(complexParameters, dataContext);
                        return FakeObject;
                    })
                });
            }

            if (parameters.Any())
            {
                foreach (var parameter in parameters)
                {
                    valueBinding.Bindings.Add(parameter);
                }
            }

            return valueBinding;
        }

        public override string ToString() => _toString.Value;

        protected virtual object? ProvideFallback()
        {
            return null;
        }

        protected TLocalizer ProvideLocalizer<TLocalizer>() where TLocalizer : ILocalizer
        {
            return (TLocalizer)ProvideLocalizer();
        }

        protected virtual object? ProvideValue(object?[] parameterValues)
        {
            return ToString();
        }

        protected virtual object? ProvideValue()
        {
            return ToString();
        }

        protected virtual bool HasParameters => false;

        protected virtual Binding PrepareMetadataBinding()
        {
            var metadata = new MetadataItem(Key, Scope, ProvideLocalizer().GetType());

            var metadataBinding = new Binding
            {
                Source = metadata,
                Path = new PropertyPath(nameof(MetadataItem.Self)),
                Mode = BindingMode.OneWay
            };

            LocalizationManager.Current.MetadataStorage.Store(metadata, metadataBinding);

            return metadataBinding;
        }

        protected virtual IList<Binding> PrepareParameterBindings()
        {
            return new List<Binding>();
        }

        protected abstract ILocalizer PrepareLocalizer();

        private object? OnValueRequested(object?[] values)
        {
            try
            {
                var metadataValue = values.SingleOrDefault(v => v is MetadataItem);
                var parameterValues = values.Where(v => v != metadataValue && v != FakeObject).ToArray();

                return parameterValues.Any()
                    ? ProvideValue(parameterValues)
                    : ProvideValue();
            }
            catch (Exception e)
            {
                e.ProcessAsMarkupException($"Failed to provide localization resource. Key='{Key}' Scope='{Scope}'");
                return ProvideFallback();
            }
        }

        private ILocalizer ProvideLocalizer()
        {
            return _localizer?.IsValid switch
            {
                true => _localizer,
                _ => _localizer = PrepareLocalizer()
            };
        }

        private ValueType GetValueType(IServiceProvider? serviceProvider)
        {
            if (serviceProvider == null)
            {
                return ValueType.MarkupExtension;
            }

            var targetInfo = (IProvideValueTarget?)serviceProvider.GetService(typeof(IProvideValueTarget));
            if (targetInfo != null)
            {
                var targetObject = targetInfo.TargetObject;
                var targetProperty = targetInfo.TargetProperty;

                if (targetObject is Freezable)
                {
                    return ValueType.Static;
                }

                if (targetObject is DependencyObject &&
                    targetProperty is DependencyProperty)
                {
                    return ValueType.BindingExpression;
                }

                if (targetObject is Setter)
                {
                    return ValueType.Binding;
                }
            }

            return ValueType.MarkupExtension;
        }

        private static void UpdateParametersDataContext(List<ResourceDelivery> complexParameters, object? dataContext)
        {
            foreach (var parameter in complexParameters)
            {
                parameter.UpdateParameterDataContext(dataContext);
            }
        }

        private enum ValueType
        {
            MarkupExtension,
            Static,
            Binding,
            BindingExpression
        }

        private ILocalizer? _localizer;

        private Lazy<string> _toString;

        private static readonly object FakeObject = new();
    }
}