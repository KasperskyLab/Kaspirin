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

namespace Kaspirin.UI.Framework.NativeMethods.Api.Shcore.Enums;

/// <summary>
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shellscalingapi/ne-shellscalingapi-monitor_dpi_type">Learn more</seealso>.
/// </summary>
public enum MonitorDpiType : uint
{
    /// <summary>
    ///     The MDT_EFFECTIVE_DPI constant.
    /// </summary>
    EffectiveDpi = 0,

    /// <summary>
    ///     The MDT_ANGULAR_DPI constant.
    /// </summary>
    AngularDpi = 1,

    /// <summary>
    ///     The MDT_RAW_DPI constant.
    /// </summary>
    RawDpi = 2,

    /// <summary>
    ///     The MDT_DEFAULT constant.
    /// </summary>
    Default = EffectiveDpi
}
