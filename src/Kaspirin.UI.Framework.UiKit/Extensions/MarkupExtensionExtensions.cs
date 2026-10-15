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

namespace Kaspirin.UI.Framework.UiKit.Extensions;

/// <summary>
///     Extension methods for <see cref="MarkupExtension" />.
/// </summary>
public static class MarkupExtensionExtensions
{
    /// <summary>
    ///     Returns the markup extension value for the specified target object and dependency properties.
    /// </summary>
    /// <param name="markupExtension">
    ///     Markup extension.
    /// </param>
    /// <param name="targetObject">
    ///     The target object.
    /// </param>
    /// <param name="targetProperty">
    ///     The target property of the dependency.
    /// </param>
    /// <returns>
    ///     Returns the value provided by the markup extension.
    /// </returns>
    public static object? ProvideValue(this MarkupExtension markupExtension, DependencyObject targetObject, DependencyProperty targetProperty)
        => markupExtension.ProvideValue(new DependencyTarget(targetObject, targetProperty));

    /// <summary>
    ///     Expands the markup extension to the type extension <typeparamref name="TType" />.
    /// </summary>
    /// <typeparam name="TType">
    ///     The type of target markup extension.
    /// </typeparam>
    /// <param name="markupExtension">
    ///     Deployable markup extension.
    /// </param>
    /// <param name="serviceProvider">
    ///     The service provider for getting the extension values.
    /// </param>
    /// <returns>
    ///     Returns a markup extension of the type <typeparamref name="TType" />.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    ///     The extension could not be expanded to the type <typeparamref name="TType" />.
    /// </exception>
    /// <remarks>
    ///     The expansion is performed recursively: the extension is called <see cref="MarkupExtension.ProvideValue" />,
    ///     and if the result is also a markup extension, the process repeats until an extension of the
    ///     type <typeparamref name="TType" /> or a value that is not a markup extension is obtained.
    /// </remarks>
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

    /// <summary>
    ///     Tries to expand the markup extension to a type extension <typeparamref name="TType" />.
    /// </summary>
    /// <typeparam name="TType">
    ///     The type of target markup extension.
    /// </typeparam>
    /// <param name="markupExtension">
    ///     Deployable markup extension.
    /// </param>
    /// <param name="serviceProvider">
    ///     The service provider for getting the extension values.
    /// </param>
    /// <param name="result">
    ///     The markup extension found or <see langword="null" />.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the deployment is successful, otherwise - <see langword="false" />.
    /// </returns>
    /// <remarks>
    ///     The expansion is performed recursively: the extension is called <see cref="MarkupExtension.ProvideValue" />,
    ///     and if the result is also a markup extension, the process repeats until an extension of the
    ///     type <typeparamref name="TType" /> or a value that is not a markup extension is obtained.
    /// </remarks>
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