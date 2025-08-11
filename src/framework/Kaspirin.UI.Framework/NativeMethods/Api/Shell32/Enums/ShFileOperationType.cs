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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Shell32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shellapi/ns-shellapi-shfileopstructa">Learn more</seealso>.
/// </summary>
public enum ShFileOperationType : uint
{
    /// <summary>
    ///     The FO_MOVE constant.
    /// </summary>
    Move = 1,

    /// <summary>
    ///     The FO_COPY constant.
    /// </summary>
    Copy = 2,

    /// <summary>
    ///     The FO_DELETE constant.
    /// </summary>
    Delete = 3,

    /// <summary>
    ///     The FO_RENAME constant.
    /// </summary>
    Rename = 4,
}
