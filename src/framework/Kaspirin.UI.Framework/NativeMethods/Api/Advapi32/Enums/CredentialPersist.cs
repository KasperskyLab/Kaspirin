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

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Kaspirin.UI.Framework.NativeMethods.Api.Advapi32.Enums
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wincred/ns-wincred-credentiala">Learn more</seealso>.
    /// </summary>
    public enum CredentialPersist
    {
        CRED_PERSIST_SESSION = 1,
        CRED_PERSIST_LOCAL_MACHINE = 2,
        CRED_PERSIST_ENTERPRISE = 3,
    }
}