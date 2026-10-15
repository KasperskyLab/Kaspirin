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

namespace Kaspirin.UI.Framework.Mvvm;

/// <summary>
///     Event handler arguments <see cref="CommandEventHandler" />.
/// </summary>
public sealed class CommandEventArgs : EventArgs
{
    internal CommandEventArgs(object? parameter)
        => Parameter = parameter;

    /// <summary>
    ///     A command parameter.
    /// </summary>
    public object? Parameter { get; }

    /// <summary>
    ///     Returns or sets a value indicating that the execution of the command was intercepted by the handler.
    /// </summary>
    public bool Handled { get; set; }
}
