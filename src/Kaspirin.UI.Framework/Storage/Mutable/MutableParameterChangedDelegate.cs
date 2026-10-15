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
///     The delegate of the subscriber of events about the parameter value change.
/// </summary>
/// <typeparam name="TValue">
///     The type of the parameter value.
/// </typeparam>
/// <param name="sender">
///     The object that triggered the event.
/// </param>
/// <param name="arguments">
///     Event arguments containing the old and new parameter values.
/// </param>
public delegate void MutableParameterChangedDelegate<TValue>(
    MutableParameter<TValue> sender,
    MutableParameterChangedArguments<TValue> arguments);
