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

namespace Kaspirin.UI.Framework.Extensions.Doubles
{
    /// <summary>
    ///     Extension methods for <see cref="double" />.
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        ///     Checks that the first number is greater than or equal to the second, taking into account the error.
        /// </summary>
        /// <param name="a">
        ///     A number to compare.
        /// </param>
        /// <param name="b">
        ///     A number to compare.
        /// </param>
        /// <param name="epsilon">
        ///     The magnitude of the error.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the number <paramref name="a" /> is greater than or equal
        ///     to <paramref name="b" /> within the margin of error, otherwise <see langword="false" />.
        /// </returns>
        public static bool LargerOrNearlyEqual(this double a, double b, double epsilon = double.Epsilon)
            => a > b || a.NearlyEqual(b, epsilon);

        /// <summary>
        ///     Checks that the first number is less than or equal to the second, taking into account the error.
        /// </summary>
        /// <param name="a">
        ///     A number to compare.
        /// </param>
        /// <param name="b">
        ///     A number to compare.
        /// </param>
        /// <param name="epsilon">
        ///     The magnitude of the error.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the number <paramref name="a" /> is less than or equal to <paramref name="b" />
        ///     within the margin of error, otherwise <see langword="false" />.
        /// </returns>
        public static bool LesserOrNearlyEqual(this double a, double b, double epsilon = double.Epsilon)
            => a < b || a.NearlyEqual(b, epsilon);

        /// <summary>
        ///     Checks the equality of two numbers, taking into account the error.
        /// </summary>
        /// <param name="a">
        ///     A number to compare.
        /// </param>
        /// <param name="b">
        ///     A number to compare.
        /// </param>
        /// <param name="epsilon">
        ///     The magnitude of the error.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the number <paramref name="a" /> is <paramref name="b" />
        ///     within the margin of error, otherwise <see langword="false" />.
        /// </returns>
        public static bool NearlyEqual(this double a, double b, double epsilon = double.Epsilon)
        {
            // Shortcut for infinites equations.
            if (a.Equals(b))
            {
                return true;
            }

            var absA = Math.Abs(a);
            var absB = Math.Abs(b);
            var absDiff = Math.Abs(a - b);

            // If either a or b (or both) is either equals or close to 0 - use simple difference.
            // Otherwise - use relative error.
            return a.Equals(0.0) || b.Equals(0.0) || absA + absB < epsilon
                ? absDiff < epsilon
                : absDiff / (absA + absB) < epsilon;
        }

        /// <summary>
        ///     Checks the inequality of two numbers, taking into account the error.
        /// </summary>
        /// <param name="a">
        ///     A number to compare.
        /// </param>
        /// <param name="b">
        ///     A number to compare.
        /// </param>
        /// <param name="epsilon">
        ///     The magnitude of the error.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the number <paramref name="a" /> is not equal to <paramref name="b" />
        ///     within the margin of error, otherwise <see langword="false" />.
        /// </returns>
        public static bool NotNearlyEqual(this double a, double b, double epsilon = double.Epsilon)
            => !NearlyEqual(a, b, epsilon);

        /// <summary>
        ///     Checks that the number is zero, taking into account the error.
        /// </summary>
        /// <param name="a">
        ///     A number to compare.
        /// </param>
        /// <param name="epsilon">
        ///     The magnitude of the error.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the number <paramref name="a" /> is zero within the margin
        ///     of error, otherwise <see langword="false" />.
        /// </returns>
        public static bool NearlyZero(this double a, double epsilon = double.Epsilon)
            => NearlyEqual(a, 0.0, epsilon);

        /// <summary>
        ///     Checks that the number is not zero, taking into account the error.
        /// </summary>
        /// <param name="a">
        ///     A number to compare.
        /// </param>
        /// <param name="epsilon">
        ///     The magnitude of the error.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the number <paramref name="a" /> is not zero within the
        ///     margin of error, otherwise <see langword="false" />.
        /// </returns>
        public static bool NotNearlyZero(this double a, double epsilon = double.Epsilon)
            => !NearlyZero(a, epsilon);

        /// <inheritdoc cref="NearlyEqual(double, double, double)"/>
        public static bool NearlyEqual(this double? a, double b, double epsilon = double.Epsilon)
            => a is not null && a.Value.NearlyEqual(b, epsilon);

        /// <inheritdoc cref="NearlyEqual(double, double, double)"/>
        public static bool NearlyEqual(this double a, double? b, double epsilon = double.Epsilon)
            => b is not null && b.Value.NearlyEqual(a, epsilon);

        /// <inheritdoc cref="NearlyEqual(double, double, double)"/>
        public static bool NearlyEqual(this double? a, double? b, double epsilon = double.Epsilon)
            => a is null
                ? b is null
                : a.Value.NearlyEqual(b, epsilon);
    }
}
