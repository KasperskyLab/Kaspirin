// Copyright © 2026 AO Kaspersky Lab.
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

namespace Kaspirin.UI.Framework.Storage.Mutable;

/// <summary>
///     A variable parameter.
/// </summary>
/// <typeparam name="TValue">
///     The type of the parameter value.
/// </typeparam>
/// <remarks>
///     This class allows you to store and receive the parameter value, as well as notify subscribers of its change.
/// </remarks>
public sealed class MutableParameter<TValue>
{
    internal MutableParameter(IMutableParameterStorage parameterProvider, Enum parameterId)
    {
        Id = parameterId;
        _parameterProvider = parameterProvider;

        SubscribeOnParameterChanged(parameterProvider);

        _value = GetValue(parameterId);
    }

    /// <summary>
    ///     An event that occurs when the parameter value is changed.
    /// </summary>
    public event MutableParameterChangedDelegate<TValue>? ValueChanged;

    /// <summary>
    ///     ID of the parameter.
    /// </summary>
    public Enum Id { get; }

    /// <summary>
    ///     The value of the parameter.
    /// </summary>
    /// <remarks>
    ///     When the value changes, the <see cref="valueChanged" /> event is triggered.
    /// </remarks>
    public TValue Value
    {
        get => _value;
        private set
        {
            if (EqualityComparer<TValue>.Default.Equals(value, _value))
            {
                return;
            }

            var newValue = value;
            var oldValue = _value;

            _value = value;

            ValueChanged?.Invoke(this, new(oldValue, newValue));
        }
    }

    private TValue GetValue(Enum parameterId)
    {
        var parameterValue = _parameterProvider.GetParameter(parameterId);

        return Guard.EnsureIsInstanceOfType<TValue>(parameterValue);
    }

    private void UpdateValue(Enum parameterId)
    {
        if (!Equals(parameterId, Id))
        {
            return;
        }

        Value = GetValue(Id);
    }

    private void SubscribeOnParameterChanged(IMutableParameterStorage source)
    {
        source.ParameterChanged += WeakEventHandlerFactory.Wrap(
            handler: UpdateValue,
            cleanup: eh => source.ParameterChanged -= eh);
    }

    private readonly IMutableParameterStorage _parameterProvider;

    private TValue _value;
}
