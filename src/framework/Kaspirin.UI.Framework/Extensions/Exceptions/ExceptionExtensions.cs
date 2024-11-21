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
using System.Collections.Generic;
using System.Reflection;

namespace Kaspirin.UI.Framework.Extensions.Exceptions
{
    /// <summary>
    ///     Extension methods for <see cref="Exception" />.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        ///     Writes an exception message to the trace.
        /// </summary>
        /// <param name="exception">
        ///     Exception.
        /// </param>
        /// <param name="message">
        ///     Additional exception message.
        /// </param>
        public static void TraceException(this Exception exception, string? message = null)
        {
            Guard.ArgumentIsNotNull(exception);

            var exceptionText = exception.ToString();

            message = string.IsNullOrEmpty(message)
                ? exceptionText
                : message + "\n" + exceptionText;

            _tracer.TraceError($"Exception occurred. \n {message}");
        }

        /// <summary>
        ///     Writes a message about an ignored exception to the trace.
        /// </summary>
        /// <param name="exception">
        ///     Exception.
        /// </param>
        /// <param name="message">
        ///     Additional exception message.
        /// </param>
        public static void TraceExceptionSuppressed(this Exception exception, string? message = null)
        {
            Guard.ArgumentIsNotNull(exception);

            var exceptionText = exception.ToString();

            message = string.IsNullOrEmpty(message)
                ? exceptionText
                : message + "\n" + exceptionText;

            _tracer.TraceWarning($"Suppressed exception occurred. \n {message}");
        }

        /// <summary>
        ///     Retrieves a nested exception of the type <see cref="TargetInvocationException" /> from <see cref="Exception.InnerException" />, if specified.
        /// </summary>
        /// <param name="exception">
        ///     Exception.
        /// </param>
        /// <returns>
        ///     Returns a nested exception of type <see cref="TargetInvocationException" />, if specified, or the original exception.
        /// </returns>
        public static Exception UnwrapExceptionIfNecessary(this Exception exception)
        {
            return exception.UnwrapExceptionIfNecessary<TargetInvocationException>();
        }

        /// <summary>
        ///     Retrieves a nested exception of type <typeparamref name="TException" /> from <see cref="Exception.InnerException" />, if specified.
        /// </summary>
        /// <param name="exception">
        ///     Exception.
        /// </param>
        /// <returns>
        ///     Returns a nested exception of type <typeparamref name="TException" />, if specified, or the original exception.
        /// </returns>
        public static Exception UnwrapExceptionIfNecessary<TException>(this Exception exception)
            where TException : Exception
        {
            while (exception is TException && exception.InnerException != null)
            {
                exception = exception.InnerException;
            }

            return exception;
        }

        /// <summary>
        ///     Retrieves a list of nested exceptions.
        /// </summary>
        /// <param name="exception">
        ///     The original exception.
        /// </param>
        /// <returns>
        ///     A list of nested exceptions for <paramref name="exception" />.
        /// </returns>
        public static IList<Exception> GetFlattenExceptionList(this Exception? exception)
        {
            var exceptions = new List<Exception>();

            while (!ReferenceEquals(exception, null))
            {
                exceptions.Add(exception);
                exception = exception.InnerException;
            }

            return exceptions;
        }

        private static readonly ComponentTracer _tracer = ComponentTracer.Get(ComponentTracers.Exception);
    }
}
