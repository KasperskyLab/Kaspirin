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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Advapi32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wincred/ns-wincred-credentiala">Learn more</seealso>.
/// </summary>
public enum CredentialType : uint
{
    /// <summary>
    ///     The CRED_TYPE_GENERIC constant.
    /// </summary>
    Generic = 1,

    /// <summary>
    ///     The CRED_TYPE_DOMAIN_PASSWORD constant.
    /// </summary>
    DomainPassword = 2,

    /// <summary>
    ///     The CRED_TYPE_DOMAIN_CERTIFICATE constant.
    /// </summary>
    DomainCertificate = 3,

    /// <summary>
    ///     The CRED_TYPE_DOMAIN_VISIBLE_PASSWORD constant.
    /// </summary>
    DomainVisiblePassword = 4,

    /// <summary>
    ///     The CRED_TYPE_GENERIC_CERTIFICATE constant.
    /// </summary>
    GenericCertificate = 5,

    /// <summary>
    ///     The CRED_TYPE_DOMAIN_EXTENDED constant.
    /// </summary>
    DomainExtended = 6,

    /// <summary>
    ///     The CRED_TYPE_MAXIMUM constant.
    /// </summary>
    Maximum = 7,

    /// <summary>
    ///     The CRED_TYPE_MAXIMUM_EX constant.
    /// </summary>
    MaximumEx = Maximum + 1000,
}