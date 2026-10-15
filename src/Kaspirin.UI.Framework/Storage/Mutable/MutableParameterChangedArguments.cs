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

namespace Kaspirin.UI.Framework.Storage.Mutable;

/// <summary>
///     Defines the arguments for the parameter value change event.
/// </summary>
/// <typeparam name="TValue">
///     The type of the parameter value.
/// </typeparam>
/// <remarks>
///     This class is used to pass the old and new parameter values when it is changed.
/// </remarks>
public sealed class MutableParameterChangedArguments<TValue>
{
    internal MutableParameterChangedArguments(TValue? oldValue, TValue? newValue)
    {
        OldValue = oldValue;
        NewValue = newValue;
    }

    /// <summary>
    ///     The old parameter value.
    /// </summary>
    public TValue? OldValue { get; }

    /// <summary>
    ///     The new parameter value.
    /// </summary>
    public TValue? NewValue { get; }
}
