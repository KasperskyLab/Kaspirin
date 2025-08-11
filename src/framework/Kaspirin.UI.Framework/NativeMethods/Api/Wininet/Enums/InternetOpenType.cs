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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Wininet.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/wininet/nf-wininet-internetopenw">Learn more</seealso>.
/// </summary>
public enum InternetOpenType : uint
{
    /// <summary>
    ///     The INTERNET_OPEN_TYPE_PRECONFIG constant.
    /// </summary>
    PreConfig = 0,

    /// <summary>
    ///     The INTERNET_OPEN_TYPE_DIRECT constant.
    /// </summary>
    Direct = 1,

    /// <summary>
    ///     The INTERNET_OPEN_TYPE_PROXY constant.
    /// </summary>
    Proxy = 3,

    /// <summary>
    ///     The INTERNET_OPEN_TYPE_PRECONFIG_WITH_NO_AUTOPROXY constant.
    /// </summary>
    PreConfigWithNoAutoProxy = 4,
}
