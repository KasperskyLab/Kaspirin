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
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Extensions
{
    public static class MarkupExtensionExtensions
    {
        public static object? ProvideValue(this MarkupExtension markupExtension, DependencyObject targetObject, DependencyProperty targetProperty)
            => markupExtension.ProvideValue(new DependencyTarget(targetObject, targetProperty));

        public static TType ExpandTo<TType>(this MarkupExtension markupExtension, IServiceProvider? serviceProvider)
            where TType : MarkupExtension
        {
            Guard.ArgumentIsNotNull(markupExtension);

            if (markupExtension.TryExpandTo<TType>(serviceProvider, out var result))
            {
                return result;
            }

            throw new InvalidOperationException($"Failed to expand {markupExtension} to requested type {typeof(TType).Name}");
        }

        public static bool TryExpandTo<TType>(this MarkupExtension markupExtension, IServiceProvider? serviceProvider, [NotNullWhen(true)] out TType? result)
            where TType : MarkupExtension
        {
            Guard.ArgumentIsNotNull(markupExtension);

            if (markupExtension is TType targetExtension)
            {
                result = targetExtension;
                return true;
            }

            var expandedValue = markupExtension.ProvideValue(serviceProvider);
            if (expandedValue is MarkupExtension expandedExtension)
            {
                if (expandedExtension.TryExpandTo<TType>(serviceProvider, out result))
                {
                    return true;
                }
            }

            result = null;
            return false;
        }
    }
}