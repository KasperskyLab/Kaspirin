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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Advapi32.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/winreg/nf-winreg-regnotifychangekeyvalue">Learn more</seealso>.
/// </summary>
[Flags]
public enum RegNotifyChangeKeyValueFlags : uint
{
    /// <summary>
    ///     The flag is not set.
    /// </summary>
    None = 0,

    /// <summary>
    ///     The REG_NOTIFY_CHANGE_NAME constant.
    /// </summary>
    ChangeName = 0x00000001,

    /// <summary>
    ///     The REG_NOTIFY_CHANGE_ATTRIBUTES constant.
    /// </summary>
    ChangeAttributes = 0x00000002,

    /// <summary>
    ///     The REG_NOTIFY_CHANGE_LAST_SET constant.
    /// </summary>
    ChangeLastSet = 0x00000004,

    /// <summary>
    ///     The REG_NOTIFY_CHANGE_SECURITY constant.
    /// </summary>
    ChangeSecurity = 0x00000008,

    /// <summary>
    ///     The REG_NOTIFY_THREAD_AGNOSTIC constant.
    /// </summary>
    ThreadAgnostic = 0x10000000,
}
