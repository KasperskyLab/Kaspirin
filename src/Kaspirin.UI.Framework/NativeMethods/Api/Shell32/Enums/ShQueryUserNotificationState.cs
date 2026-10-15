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
///     <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/shellapi/ne-shellapi-query_user_notification_state">Learn more</seealso>.
/// </summary>
public enum ShQueryUserNotificationState
{
    /// <summary>
    ///     The QUNS_NOT_PRESENT constant.
    /// </summary>
    NotPresent = 1,

    /// <summary>
    ///     The QUNS_BUSY constant.
    /// </summary>
    Busy = 2,

    /// <summary>
    ///     The QUNS_RUNNING_D3D_FULL_SCREEN constant.
    /// </summary>
    RunningD3DFullScreen = 3,

    /// <summary>
    ///     The QUNS_PRESENTATION_MODE constant.
    /// </summary>
    PresentationMode = 4,

    /// <summary>
    ///     The QUNS_ACCEPTS_NOTIFICATIONS constant.
    /// </summary>
    AcceptsNotifications = 5,

    /// <summary>
    ///     The QUNS_QUIET_TIME constant.
    /// </summary>
    QuietTime = 6,

    /// <summary>
    ///     The QUNS_APP constant.
    /// </summary>
    App = 7,
}
