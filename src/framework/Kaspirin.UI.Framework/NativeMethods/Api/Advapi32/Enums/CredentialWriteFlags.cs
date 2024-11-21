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
#pragma warning disable KCAIDE0002 // Enum has incorrect suffix

using System;

namespace Kaspirin.UI.Framework.NativeMethods.Api.Advapi32.Enums
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wincred/nf-wincred-credwritew">Learn more</seealso>.
    /// </summary>
    [Flags]
    public enum CredentialWriteFlags : uint
    {
        None = 0,
        CRED_PRESERVE_CREDENTIAL_BLOB = 0x1
    }
}