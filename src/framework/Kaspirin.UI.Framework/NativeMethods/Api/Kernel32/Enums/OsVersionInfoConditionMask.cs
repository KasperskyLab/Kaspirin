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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Kernel32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winnt/nf-winnt-versetconditionmask">Learn more</seealso>.
/// </summary>
public enum OsVersionInfoConditionMask : uint
{
    /// <summary>
    ///     The VER_EQUAL constant.
    /// </summary>
    Equal = 1,

    /// <summary>
    ///     The VER_GREATER constant.
    /// </summary>
    Greater = 2,

    /// <summary>
    ///     The VER_GREATER_EQUAL constant.
    /// </summary>
    GreaterEqual = 3,

    /// <summary>
    ///     The VER_LESS constant.
    /// </summary>
    Less = 4,

    /// <summary>
    ///     The VER_LESS_EQUAL constant.
    /// </summary>
    LessEqual = 5,

    /// <summary>
    ///     The VER_AND constant.
    /// </summary>
    And = 6,

    /// <summary>
    ///     The VER_OR constant.
    /// </summary>
    Or = 7,
}
