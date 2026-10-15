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

#if NETCOREAPP
using System;
#endif

namespace Kaspirin.UI.Framework.Services;

/// <summary>
///     Provides a method for returning empty arrays of any type.
/// </summary>
public static class EmptyArray
{
    /// <summary>
    ///     Returns an empty array of the specified type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of elements in the array.
    /// </typeparam>
    /// <returns>
    ///     An empty array of type <typeparamref name="T" />.
    /// </returns>
    public static T[] For<T>()
    {
#if NETCOREAPP
        return Array.Empty<T>();
#else
        return new T[0];
#endif
    }
}
