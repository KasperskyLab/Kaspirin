// Copyright © 2025 AO Kaspersky Lab.
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

using System.Threading;

namespace Kaspirin.UI.Framework.Guards;

/// <summary>
///     A static class that provides various methods for checking conditions (guards).
/// </summary>
public static partial class Guard
{
    /// <summary>
    ///     Checks that the current thread is a user interface thread.
    /// </summary>
    /// <exception cref="GuardException">
    ///     It is thrown if the method is not executed in the user interface thread.
    /// </exception>
    public static void AssertIsUiThread()
    {
        IsNotNull(_uiThreadId, "UI Thread ID is not set");

        if (_uiThreadId != Thread.CurrentThread.ManagedThreadId)
        {
            OnGuardFailed("Method should be executed in UI thread");
        }
    }

    /// <summary>
    ///     Checks that the current thread is not a user interface thread.
    /// </summary>
    /// <exception cref="GuardException">
    ///     It is thrown if the method is executed in the user interface thread.
    /// </exception>
    public static void AssertIsNotUiThread()
    {
        IsNotNull(_uiThreadId, "UI Thread ID is not set");

        if (_uiThreadId == Thread.CurrentThread.ManagedThreadId)
        {
            OnGuardFailed("Method should be executed not in UI thread");
        }
    }

    /// <summary>
    ///     Sets the UI thread ID used in the <see cref="AssertIsUiThread" /> and <see cref="AssertIsNotUiThread" /> methods.
    /// </summary>
    /// <param name="uiThreadId">
    ///     The identifier of the UI stream.
    /// </param>
    public static void SetUiThreadId(int uiThreadId)
    {
        _uiThreadId = uiThreadId;
    }

    private static int? _uiThreadId;
}
