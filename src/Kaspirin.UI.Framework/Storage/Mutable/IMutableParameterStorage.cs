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

namespace Kaspirin.UI.Framework.Storage.Mutable;

/// <summary>
///     Provides access to the storage of changeable parameters.
/// </summary>
public interface IMutableParameterStorage
{
    /// <summary>
    ///     Gets the parameter by the specified ID.
    /// </summary>
    /// <param name="parameterId">
    ///     ID of the parameter.
    /// </param>
    /// <returns>
    ///     The value of the parameter.
    /// </returns>
    object GetParameter(Enum parameterId);

    /// <summary>
    ///     An event that occurs when the parameter is changed.
    /// </summary>
    event Action<Enum> ParameterChanged;
}
