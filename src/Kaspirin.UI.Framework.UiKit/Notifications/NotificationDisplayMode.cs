// Copyright © 2025 AO Kaspersky Lab.
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

namespace Kaspirin.UI.Framework.UiKit.Notifications;

/// <summary>
///     Notification display mode <see cref="NotificationView" />.
/// </summary>
public enum NotificationDisplayMode
{
    /// <summary>
    ///     Modal mode.
    /// </summary>
    /// <remarks>
    ///     While the notification is being displayed, interaction with the child elements of the <see cref="NotificationLayer" />
    ///     component used to display the notification is blocked.
    ///     <br /> It is allowed to display only one modal notification on one component <see cref="NotificationLayer" />.
    ///     The rest of the modal notifications will be shown in turn, after the current notification is closed.
    /// </remarks>
    Modal,

    /// <summary>
    ///     Modal mode with notification display on top of all others.
    /// </summary>
    /// <remarks>
    ///     While the notification is being displayed, interaction with the child elements of the <see cref="NotificationLayer" />
    ///     component used to display the notification is blocked, as well as with the currently active
    ///     notification <see cref="NotificationView" />.
    ///     <br /> It is acceptable to display multiple modal notifications on the same component <see cref="NotificationLayer" />,
    ///     which will sequentially overlap the previous notification.
    /// </remarks>
    ModalTopmost,

    /// <summary>
    ///     Non-modal mode.
    /// </summary>
    /// <remarks>
    ///     It is allowed to display only one non-modal notification on one component <see cref="NotificationLayer" />.
    ///     <br /> The active notification will be closed if a request is received to display the next notification.
    /// </remarks>
    NonModal
}
