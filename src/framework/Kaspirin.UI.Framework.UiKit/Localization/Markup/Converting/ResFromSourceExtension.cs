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
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.Localization.Markup.Converting
{
    public abstract class ResFromSourceExtension : MarkupExtension, IResourceProvider, IBindingProvider
    {
        public Binding? Source { get; set; }

        public abstract object? GetResource(object? key);

        public override object? ProvideValue(IServiceProvider serviceProvider)
        {
            Guard.ArgumentIsNotNull(serviceProvider);

            return ProvideBindingExpression(serviceProvider);
        }

        public BindingBase ProvideBinding()
        {
            return ProvideBinding(null);
        }

        public BindingBase ProvideBinding(object? targetObject)
        {
            Guard.IsNotNull(Source, "Source != null");

            var resourceDelivery = new ResourceDelivery(this, Source.Clone());
            var binding = resourceDelivery.CreateBinding(targetObject);
            return binding;
        }

        private object? ProvideBindingExpression(IServiceProvider serviceProvider)
        {
            try
            {
                var provideValueTarget = (IProvideValueTarget?)serviceProvider.GetService(typeof(IProvideValueTarget));
                var targetObject = provideValueTarget?.TargetObject;

                if (targetObject is DependencyObject or Setter or MarkupExtension)
                {
                    var binding = ProvideBinding(targetObject);

                    return targetObject switch
                    {
                        Setter => binding,
                        _ => binding.ProvideValue(serviceProvider)
                    };
                }

                // When a markup extension is evaluated inside a template, TargetObject is
                // an instance of System.Windows.SharedDp, an internal WPF class.
                // For the markup extension to be able to access its target, it has to be evaluated
                // when the template is applied. So we need to defer its evaluation until this time.
                return this;

            }
            catch (Exception e)
            {
                e.ProcessAsMarkupException("Failed to provide value.");
                return null;
            }
        }
    }
}
