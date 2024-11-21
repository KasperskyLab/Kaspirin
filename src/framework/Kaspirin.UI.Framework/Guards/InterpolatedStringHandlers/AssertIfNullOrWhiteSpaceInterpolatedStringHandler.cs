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

#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
#else
using Kaspirin.UI.Framework.BackwardCompatibility;
#endif

namespace Kaspirin.UI.Framework.Guards.InterpolatedStringHandlers
{
    /// <summary>
    ///     An interpolated string handler to check that the string contains only space characters or is
    ///     equal to <see langword="null" />.
    /// </summary>
    [InterpolatedStringHandler]
    public ref struct AssertIfNullOrWhiteSpaceInterpolatedStringHandler
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="AssertIfNullOrWhiteSpaceInterpolatedStringHandler" /> class.
        /// </summary>
        /// <param name="literalLength">
        ///     The length of the literal part of the interpolated string.
        /// </param>
        /// <param name="formattedCount">
        ///     The number of formatted values.
        /// </param>
        /// <param name="str">
        ///     The string to check.
        /// </param>
        /// <param name="isEnabled">
        ///     Indicates whether the handler is enabled.
        /// </param>
        public AssertIfNullOrWhiteSpaceInterpolatedStringHandler(
            int literalLength,
            int formattedCount,
            string? str,
            out bool isEnabled)
        {
            isEnabled = string.IsNullOrWhiteSpace(str);
            _defaultHandler = isEnabled ? new(literalLength, formattedCount) : default;
        }

        /// <summary>
        ///     Adds a literal string to the interpolated string handler.
        /// </summary>
        /// <param name="value">
        ///     A literal string to add.
        /// </param>
        public void AppendLiteral(string value)
            => _defaultHandler.AppendLiteral(value);

        /// <summary>
        ///     Adds the formatted value to the interpolated string handler.
        /// </summary>
        /// <typeparam name="TArgument">
        ///     The type of formatted value.
        /// </typeparam>
        /// <param name="value">
        ///     Formatted value to add.
        /// </param>
        /// <param name="alignment">
        ///     Alignment of the formatted value.
        /// </param>
        /// <param name="format">
        ///     The format of the formatted value.
        /// </param>
        public void AppendFormatted<TArgument>(TArgument value, int alignment = 0, string? format = null)
            => _defaultHandler.AppendFormatted(value, alignment, format);

        /// <summary>
        ///     Converts the interpolated string handler to a string and clears the handler.
        /// </summary>
        /// <returns>
        ///     The resulting string.
        /// </returns>
        public string ToStringAndClear()
            => _defaultHandler.ToStringAndClear();

#pragma warning disable IDE0044 // With readonly modifier struct is copied every time it is accessed.
        private DefaultInterpolatedStringHandler _defaultHandler;
#pragma warning restore IDE0044
    }
}