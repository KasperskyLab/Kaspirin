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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-osversioninfoexw">Learn more</seealso>.
/// </summary>
public enum OsProductType : byte
{
    /// <summary>
    ///     The VER_NT_DOMAIN_CONTROLLER constant.
    /// </summary>
    DomainController = 2,

    /// <summary>
    ///     The VER_NT_SERVER constant.
    /// </summary>
    Server = 3,

    /// <summary>
    ///     The VER_NT_WORKSTATION constant.
    /// </summary>
    Workstation = 1,
}
