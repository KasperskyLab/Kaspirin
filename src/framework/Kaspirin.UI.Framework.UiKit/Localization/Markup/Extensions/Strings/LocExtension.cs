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

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Localization.Markup.Extensions.Strings
{
    public class LocExtension : LocalizationMarkupBase
    {
        public LocExtension() : this(string.Empty) { }

        public LocExtension(string key) : base(key) { }

        public LocExtension(string key, string scope) : base(key, scope) { }

        public LocParameter? Param { get; set; }

        public LocParameterCollection? Params { get; set; }

        public LocOption? Option { get; set; }

        public string? ProvideConstantStringValue()
        {
            return ProvideValue()?.ToString();
        }

        protected override bool HasParameters => Param != null || Params?.Any() == true;

        protected override object ProvideFallback()
        {
            return $"[Unresolved Key='{Key}' Scope='{Scope}']";
        }

        protected override object? ProvideValue(object?[] parameterValues)
        {
            if (Params?.IgnoreUnsetParameters == false)
            {
                if (parameterValues.Any(p => p == DependencyProperty.UnsetValue) || ResourceDelivery.HasUnsetParameters(parameterValues))
                {
                    return DependencyProperty.UnsetValue;
                }
            }
            else if (parameterValues.All(p => p == DependencyProperty.UnsetValue || ResourceDelivery.IsUnsetParameter(p)))
            {
                return DependencyProperty.UnsetValue;
            }

            var paramKeys = GetAllParameters().Select(p => p.ParamName).ToList();

            var parameters = paramKeys
                .Zip(parameterValues, (key, value) => new { key, value })
                .ToDictionary(kvp => kvp.key, kvp => kvp.value);

            return ProvideLocalizer<IStringLocalizer>().GetString(Key, parameters, Option?.LocalizerOption);
        }

        protected override object? ProvideValue()
        {
            var allParams = GetAllParameters();

            var badParams = allParams.Where(p => p.ParamSource?.Source is not string).ToList();
            if (badParams.Any())
            {
                throw new LocalizationMarkupException($"Can't get parameters values of non constants parameters: {string.Join(", ", badParams.Select(p => p.ParamName))}");
            }

            return ProvideLocalizer<IStringLocalizer>()
                    .GetString(Key, allParams.ToDictionary(p => p.ParamName, p => p.ParamSource?.Source), Option?.LocalizerOption);
        }

        protected override IList<Binding> PrepareParameterBindings()
        {
            var allParams = GetAllParameters();

            var paramsBindings = new List<Binding>(allParams.Count);

            foreach (var loc2Parameter in allParams)
            {
                var binding = loc2Parameter.ParamSource;

                if (binding == null)
                {
                    continue;
                }

                if (binding.Source is ResourceDelivery resourceDelivery)
                {
                    var newResourceDelivery = resourceDelivery.CreateCopyResourceDelivery();
                    binding = newResourceDelivery.CreateParameterBinding();
                }
                else
                {
                    binding = binding.Clone();
                    if (binding.Converter == null && !string.IsNullOrEmpty(binding.StringFormat))
                    {
                        binding.Converter = new DelegateConverter(sourceValue =>
                        {
                            return string.Format(binding.StringFormat, sourceValue);
                        });
                    }
                }

                paramsBindings.Add(binding);
            }

            return paramsBindings;
        }

        protected ICollection<LocParameter> GetAllParameters()
        {
            if (Param != null)
            {
                return new List<LocParameter>(1) { Param };
            }

            if (Params != null)
            {
                return Params;
            }

            return new List<LocParameter>(0);
        }

        protected override ILocalizer PrepareLocalizer()
        {
            return LocalizationManager.Current.LocalizerFactory.Resolve<IStringLocalizer>(Scope);
        }
    }
}
