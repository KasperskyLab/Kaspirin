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

#pragma warning disable  CA1416 // This call site is reachable on all platforms

namespace Kaspirin.UI.Framework.Threading;

/// <summary>
///     An interface for monitoring UI flow lifecycle events in WPF applications.
/// </summary>
public interface IWpfThreadObserver
{
    /// <summary>
    ///     Notifies the observer that a UI thread has been created.
    /// </summary>
    /// <remarks>
    ///     This method is called in the pool thread.
    /// </remarks>
#if NETCOREAPP
    void Created() { }
#else
    void Created();
#endif

    /// <summary>
    ///     Notifies the observer that the UI thread has started execution.
    /// </summary>
    /// <remarks>
    ///     This method is called in the UI thread.
    /// </remarks>
#if NETCOREAPP
    void Started() { }
#else
    void Started();
#endif
}
