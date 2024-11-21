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
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.MarkupExtensions.Common
{
    public sealed class UIKitTemplateBindingExtension : ExtendedMarkupExtension
    {
        public UIKitTemplateBindingExtension(DependencyProperty dependencyProperty)
        {
            _dependencyProperty = dependencyProperty;
        }

        public IValueConverter? Converter { get; set; }

        public object? ConverterParameter { get; set; }

        protected override object? ProvideForSetter(IServiceProvider serviceProvider, SetterBase targetObject, object targetProperty)
            => CreateBinding();

        protected override object? ProvideValue(IServiceProvider? serviceProvider, TargetType valueType)
            => CreateBinding().ProvideValue(serviceProvider);

        private Binding CreateBinding()
            => new()
            {
                Mode = BindingMode.OneWay,
                Converter = Converter,
                ConverterParameter = ConverterParameter,
                Path = _dependencyProperty.AsPath(),
                RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent)
            };

        private readonly DependencyProperty _dependencyProperty;
    }
}
