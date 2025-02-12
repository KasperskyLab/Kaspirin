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

namespace Kaspirin.UI.Framework.Guards
{
    /// <summary>
    ///     An exception that is thrown when the guard conditions are violated in the <see cref="Guard" /> class.
    /// </summary>
    [Serializable]
    public sealed class GuardException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="GuardException" /> class.
        /// </summary>
        /// <param name="message">
        ///     The error message.
        /// </param>
        public GuardException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GuardException" /> class.
        /// </summary>
        /// <param name="message">
        ///     The error message.
        /// </param>
        /// <param name="inner">
        ///     Nested exception.
        /// </param>
        public GuardException(string message, Exception inner)
            : base(message, inner)
        {
        }

        private GuardException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
