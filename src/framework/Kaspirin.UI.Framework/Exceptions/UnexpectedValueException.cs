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

using System;
using System.Runtime.Serialization;

#if NET6_0_OR_GREATER
using System.Runtime.CompilerServices;
#else
using Kaspirin.UI.Framework.BackwardCompatibility;
#endif

namespace Kaspirin.UI.Framework.Exceptions;

/// <summary>
///     An exception that is thrown when an unexpected value is detected.
/// </summary>
[Serializable]
public sealed class UnexpectedValueException : Exception
{
    /// <summary>
    ///     Initializes a new instance of the class <see cref="Unexpected ValueException" />.
    /// </summary>
    /// <param name="value">
    ///     Unexpected value.
    /// </param>
    /// <param name="message">
    ///     The error message.
    /// </param>
    /// <param name="valueExpression">
    ///     An expression containing an unexpected value.
    /// </param>
    public UnexpectedValueException(object? value, string? message = null, [CallerArgumentExpression("value")] string? valueExpression = null)
        : base(FormatMessage(value, message, valueExpression))
    {
    }

    private UnexpectedValueException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    private static string FormatMessage(object? value, string? message, string? valueExpression)
    {
        var valueMessage = value == null
            ? $"Unexpected null value"
            : $"Unexpected value '{value}' of type '{value.GetType().Name}'";

        var valueExpressionMessage = string.IsNullOrEmpty(valueExpression)
            ? $""
            : $" in value expression '{valueExpression}'";

        return $"{valueMessage}{valueExpressionMessage}. {message}";
    }
}