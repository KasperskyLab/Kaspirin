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
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Kaspirin.UI.Framework.Storage
{
    /// <summary>
    ///     Interface for tracking changes in the registry.
    /// </summary>
    public interface IRegistryTracker
    {
        /// <summary>
        ///     Tracks changes in the specified registry directory.
        /// </summary>
        /// <param name="hive">
        ///     The registry to open.
        /// </param>
        /// <param name="view">
        ///     The root folder of the registry.
        /// </param>
        /// <param name="regPath">
        ///     The path to the registry directory.
        /// </param>
        /// <param name="cancellationToken">
        ///     A token to stop tracking registry changes.
        /// </param>
        /// <param name="changed">
        ///     The action to be performed when a change is detected.
        /// </param>
        /// <returns>
        ///     A task representing an asynchronous change tracking operation.
        /// </returns>
        Task TrackChangesAsync(RegistryHive hive, RegistryView view, string regPath, CancellationToken cancellationToken, Action changed);
    }
}
