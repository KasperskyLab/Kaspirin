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
using System.Xaml;

namespace Kaspirin.UI.Framework.UiKit.MarkupExtensions
{
    [MarkupExtensionReturnType(typeof(Style))]
    public sealed class MultiStyleExtension : MarkupExtension
    {
        public MultiStyleExtension(string resourceKeys)
        {
            Guard.ArgumentIsNotNullOrWhiteSpace(resourceKeys);

            _resourceKeys = resourceKeys.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            Guard.Assert(_resourceKeys.Length > 0, "No resource keys specified");
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_resourceKeys.Length == 1)
            {
                return TryGetStyle(serviceProvider, _resourceKeys[0], out var style)
                    ? style
                    : this;
            }

            var mergedStyle = new Style();

            foreach (var resourceKey in _resourceKeys)
            {
                if (!TryGetStyle(serviceProvider, resourceKey, out var style))
                {
                    return this;
                }

                mergedStyle.Merge(style);
            }

            return mergedStyle;
        }

        private static bool TryGetStyle(IServiceProvider serviceProvider, string resourceKey, [NotNullWhen(true)] out Style? style)
        {
            style = default;

            object key = resourceKey;

            var isImplicitStyle = resourceKey == ".";
            if (isImplicitStyle)
            {
                var targetProvider = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
                if (targetProvider == null || targetProvider.TargetObject is not DependencyObject dependencyObject)
                {
                    // When a markup extension is evaluated inside a template, TargetObject is
                    // an instance of System.Windows.SharedDp, an internal WPF class.
                    // For the markup extension to be able to access its target, it has to be evaluated
                    // when the template is applied. So we need to defer its evaluation until this time.
                    // Source: https://thomaslevesque.com/2009/08/23/wpf-markup-extensions-and-templates

                    return false;
                }

                key = dependencyObject.GetType();
            }

            var canProvideStaticResource = serviceProvider.GetService(typeof(IXamlSchemaContextProvider)) is not null;
            if (canProvideStaticResource)
            {
                style = new StaticResourceExtension(key).ProvideValue(serviceProvider) as Style;
            }
            else
            {
                style = Application.Current.TryFindResource(key) as Style;
            }

            Guard.IsNotNull(style, message: $"Could not find style with resource key {resourceKey}");

            return true;
        }

        private readonly string[] _resourceKeys;
    }
}