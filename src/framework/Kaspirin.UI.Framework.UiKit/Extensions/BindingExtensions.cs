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

namespace Kaspirin.UI.Framework.UiKit.Extensions;

/// <summary>
///     Extension methods for binding <see cref="BindingBase" />.
/// </summary>
public static class BindingExtensions
{
    /// <summary>
    ///     Creates a copy of the specified binding object <typeparamref name="TBinding" />.
    /// </summary>
    /// <typeparam name="TBinding">
    ///     The type of the binding object to clone.
    /// </typeparam>
    /// <param name="binding">
    ///     Binding for cloning.
    /// </param>
    /// <returns>
    ///     A copy of the specified binding.
    /// </returns>
    public static TBinding Clone<TBinding>(this TBinding binding) where TBinding : BindingBase
        => (TBinding)CloneWithInternal(binding, modifier: null);

    /// <summary>
    ///     Creates a copy of the specified binding object <typeparamref name="TBinding" />, applies the
    ///     modifier <paramref name="modifier" /> and returns the result.
    /// </summary>
    /// <typeparam name="TBinding">
    ///     The type of the binding object to clone.
    /// </typeparam>
    /// <param name="binding">
    ///     Binding for cloning.
    /// </param>
    /// <param name="modifier">
    ///     The modifier that will be applied to the cloned object.
    /// </param>
    /// <returns>
    ///     A copy of the specified binding.
    /// </returns>
    public static TBinding CloneWith<TBinding>(this TBinding binding, Action<BindingBase> modifier) where TBinding : BindingBase
        => (TBinding)CloneWithInternal(binding, modifier);

    private static BindingBase CloneWithInternal(BindingBase bindingBase, Action<BindingBase>? modifier)
    {
        Guard.ArgumentIsNotNull(bindingBase);

        return bindingBase switch
        {
            Binding binding => CloneBinding(binding, modifier),
            MultiBinding multiBinding => CloneMultiBinding(multiBinding, modifier),
            _ => throw new UnexpectedValueException(bindingBase),
        };
    }

    private static Binding CloneBinding(Binding source, Action<BindingBase>? modifier)
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

        modifier?.Invoke(clone);

        return clone;
    }

    private static MultiBinding CloneMultiBinding(MultiBinding source, Action<BindingBase>? modifier)
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
            clone.Bindings.Add(CloneWithInternal(binding, modifier));
        }

        FillBindingBaseProperties(source, clone);

        modifier?.Invoke(clone);

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