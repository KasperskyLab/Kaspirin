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
using System.Runtime.Serialization;

namespace Kaspirin.UI.Framework.IoC
{
    /// <summary>
    ///     An exception that occurs when an unsuccessful attempt is made to get a dependency from an IoC container.
    /// </summary>
    [Serializable]
    public sealed class ResolutionFailedException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ResolutionFailedException" /> class.
        /// </summary>
        /// <param name="message">
        ///     The error message.
        /// </param>
        /// <param name="innerException">
        ///     An internal exception.
        /// </param>
        public ResolutionFailedException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResolutionFailedException" /> class.
        /// </summary>
        /// <param name="message">
        ///     The error message.
        /// </param>
        public ResolutionFailedException(string? message)
            : base(message)
        {
        }

        private ResolutionFailedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
