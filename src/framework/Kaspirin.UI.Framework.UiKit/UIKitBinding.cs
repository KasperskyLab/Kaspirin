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
using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using Kaspirin.UI.Framework.UiKit.MarkupExtensions;

namespace Kaspirin.UI.Framework.UiKit
{
    internal class UIKitBinding : ExtendedMarkupExtension
    {
        public UIKitBinding(string id)
        {
            _id = Guard.EnsureArgumentIsNotNullOrEmpty(id);
            Mode = RelativeSourceMode.TemplatedParent;
        }

        public Type? Type { get; set; }

        public object? DefaultValue { get; set; }

        public RelativeSourceMode Mode { get; set; }

        public Type? AncestorType { get; set; }

        public IValueConverter? Converter { get; set; }

        public object? ConverterParameter { get; set; }

        protected override object? ProvideValue(IServiceProvider? serviceProvider, TargetType valueType)
            => CreateBinding().ProvideValue(serviceProvider);

        protected override object? ProvideForSetter(IServiceProvider serviceProvider, SetterBase targetObject, object targetProperty)
            => CreateBinding();

        protected override object? ProvideForControlTemplate(IServiceProvider serviceProvider, object targetObject, DependencyProperty targetProperty)
            => CreateBinding(targetProperty).ProvideValue(serviceProvider);

        protected override object? ProvideForControl(IServiceProvider serviceProvider, DependencyObject targetObject, DependencyProperty targetProperty)
            => CreateBinding(targetProperty).ProvideValue(serviceProvider);

        protected virtual MarkupExtension CreateBinding(DependencyProperty? targetProperty = null)
        {
            var targetPropertyType = targetProperty != null
                ? Type ?? targetProperty.PropertyType
                : Guard.EnsureIsNotNull(Type);

            var path = UIKitPropertyStorage.GetOrCreate(_id, targetPropertyType, DefaultValue).AsPath();

            return new Binding()
            {
                Mode = BindingMode.OneWay,
                Converter = Converter,
                ConverterParameter = ConverterParameter,
                Path = path,
                RelativeSource = new RelativeSource(Mode)
                {
                    AncestorType = Mode == RelativeSourceMode.FindAncestor ? AncestorType : null
                }
            };
        }

        private readonly string _id;
    }
}
