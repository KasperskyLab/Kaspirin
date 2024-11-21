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

namespace Kaspirin.UI.Framework.Guards
{
    /// <summary>
    ///     Arguments for the event <see cref="Guard.GuardFailed" />.
    /// </summary>
    public sealed class GuardFailedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GuardFailedEventArgs" /> class.
        /// </summary>
        /// <param name="message">
        ///     The error message.
        /// </param>
        /// <param name="originalException">
        ///     The original exception.
        /// </param>
        public GuardFailedEventArgs(string message, Exception originalException)
        {
            Message = message;
            OriginalException = originalException;
        }

        /// <summary>
        ///     The error message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        ///     The original exception.
        /// </summary>
        public Exception OriginalException { get; }
    }
}
