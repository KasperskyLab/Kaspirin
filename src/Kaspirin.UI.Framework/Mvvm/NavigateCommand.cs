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
using System.Windows.Input;

namespace Kaspirin.UI.Framework.Mvvm;

/// <summary>
///     Provides an implementation of <see cref="ICommand" /> for commands that perform navigation.
/// </summary>
public class NavigateCommand : DelegateCommand
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="NavigateCommand" /> class with the specified delegates
    ///     to perform and define the external navigation feature.
    /// </summary>
    /// <param name="executeMethod">
    ///     The delegate being executed.
    /// </param>
    /// <param name="isExternalMethod">
    ///     A delegate for determining whether navigation leads to a location external to the application.
    /// </param>
    public NavigateCommand(Action executeMethod, Func<bool> isExternalMethod)
        : this(executeMethod, () => true, isExternalMethod)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="NavigateCommand" /> class with the specified delegates
    ///     to execute, determine whether the command can be executed, and determine whether external navigation is available.
    /// </summary>
    /// <param name="executeMethod">
    ///     The delegate being executed.
    /// </param>
    /// <param name="canExecuteMethod">
    ///     A delegate to determine whether a command can be executed.
    /// </param>
    /// <param name="isExternalMethod">
    ///     A delegate for determining whether navigation leads to a location external to the application.
    /// </param>
    public NavigateCommand(Action executeMethod, Func<bool> canExecuteMethod, Func<bool> isExternalMethod)
        : this(parameter => executeMethod(), parameter => canExecuteMethod(), isExternalMethod)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="NavigateCommand" /> class with the specified delegates
    ///     to execute, determine whether the command can be executed, and determine whether external navigation is available.
    /// </summary>
    /// <param name="executeMethod">
    ///     The delegate being executed.
    /// </param>
    /// <param name="canExecuteMethod">
    ///     A delegate to determine whether a command can be executed.
    /// </param>
    /// <param name="isExternalMethod">
    ///     A delegate for determining whether navigation leads to a location external to the application.
    /// </param>
    protected NavigateCommand(Action<object?> executeMethod, Func<object?, bool> canExecuteMethod, Func<bool> isExternalMethod)
        : base(executeMethod, canExecuteMethod)
    {
        _isExternal = Guard.EnsureArgumentIsNotNull(isExternalMethod);
    }

    /// <summary>
    ///     An event that occurs when the <see cref="IsExternal()" /> attribute changes.
    /// </summary>
    public event EventHandler? IsExternalChanged;

    /// <summary>
    ///     Determines whether navigation leads to a location external to the application (for example, opening a browser).
    /// </summary>
    /// <returns>
    ///     Returns the value <see langword="true" /> if the navigation is external, otherwise <see langword="false" />.
    /// </returns>
    public bool IsExternal()
        => _isExternal.Invoke();

    /// <summary>
    ///     Triggers the <see cref="IsExternalChanged" /> event.
    /// </summary>
    public void RaiseIsExternalChanged()
        => RaiseEventInUiThread(IsExternalChanged);

    private readonly Func<bool> _isExternal;
}
