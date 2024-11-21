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
using System.Linq;

namespace Kaspirin.UI.Framework.Extensions.Enums
{
    /// <summary>
    ///     Methods for c operations <see cref="Enum" />.
    /// </summary>
    public static class EnumOperations
    {
        /// <summary>
        ///     Checks that the value <paramref name="value" /> is in the enumeration <typeparamref name="TEnum" />.
        /// </summary>
        /// <typeparam name="TSource">
        ///     The type of the value being checked.
        /// </typeparam>
        /// <typeparam name="TEnum">
        ///     Type <see cref="Enum" />.
        /// </typeparam>
        /// <param name="value">
        ///     The value being checked.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the value <paramref name="value" /> is in the enumeration <typeparamref name="TEnum" />,
        ///     otherwise —see langword="false" />.
        /// </returns>
        public static bool IsValidValue<TSource, TEnum>(TSource value) where TEnum : struct, Enum
            => Enum.GetValues(typeof(TEnum)).Cast<TSource>().Contains(value);

        /// <inheritdoc cref="IsValidValue{TSource, TEnum}(TSource)"/>
        public static bool IsValidValue<TEnum>(int value) where TEnum : struct, Enum
            => IsValidValue<int, TEnum>(value);

        /// <inheritdoc cref="IsValidValue{TSource, TEnum}(TSource)"/>
        public static bool IsValidValue<TEnum>(uint value) where TEnum : struct, Enum
            => IsValidValue<uint, TEnum>(value);

        /// <inheritdoc cref="IsValidValue{TSource, TEnum}(TSource)"/>
        public static bool IsValidValue<TEnum>(long value) where TEnum : struct, Enum
            => IsValidValue<long, TEnum>(value);

        /// <inheritdoc cref="IsValidValue{TSource, TEnum}(TSource)"/>
        public static bool IsValidValue<TEnum>(ulong value) where TEnum : struct, Enum
            => IsValidValue<ulong, TEnum>(value);

        /// <inheritdoc cref="IsValidValue{TSource, TEnum}(TSource)"/>
        public static bool IsValidValue<TEnum>(TEnum value) where TEnum : struct, Enum
            => IsValidValue<TEnum, TEnum>(value);

        /// <summary>
        ///     Gets all the values of <typeparamref name="TEnum" /> as an enumeration.
        /// </summary>
        /// <typeparam name="TEnum">
        ///     Type <see cref="Enum" />.
        /// </typeparam>
        /// <returns>
        ///     An enumeration with all values for the type <typeparamref name="TEnum" />.
        /// </returns>
        public static IEnumerable<TEnum> GetMembers<TEnum>() where TEnum : struct, Enum
            => Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

        /// <summary>
        ///     Checks whether the <paramref name="flag" /> flag is set in the mask <paramref name="mask" />.
        /// </summary>
        /// <typeparam name="TMask">
        ///     The type of mask. Supported types: <see cref="int" />, <see cref="uint" />, <see cref="long" />, <see cref="ulong" />, <see cref="Enum" />.
        /// </typeparam>
        /// <typeparam name="TFlag">
        ///     The type of flag.
        /// </typeparam>
        /// <param name="mask">
        ///     The target mask.
        /// </param>
        /// <param name="flag">
        ///     The flag being checked.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if <paramref name="flag" /> is set in a mask <paramref name="mask" />,
        ///     otherwise <see langword="false" />.
        /// </returns>
        public static bool HasFlag<TMask, TFlag>(TMask mask, TFlag flag)
            where TMask : notnull
            where TFlag : struct, Enum
        {
            if (mask is int || mask is long)
            {
                var bitFlag = Convert.ToInt64(flag);
                var bitMask = Convert.ToInt64(mask);

                return (bitMask & bitFlag) == bitFlag;
            }

            if (mask is uint || mask is ulong || mask is Enum)
            {
                var bitFlag = Convert.ToUInt64(flag);
                var bitMask = Convert.ToUInt64(mask);

                return (bitMask & bitFlag) == bitFlag;
            }

            throw new InvalidOperationException($"Unexpected {mask} type: {mask.GetType()}");
        }

        /// <summary>
        ///     Toggles the <paramref name="flag" /> flag in the mask <paramref name="mask" />.
        /// </summary>
        /// <typeparam name="TMask">
        ///     The type of mask. Supported types: <see cref="int" />, <see cref="uint" />, <see cref="long" />, <see cref="ulong" />, <see cref="Enum" />.
        /// </typeparam>
        /// <typeparam name="TFlag">
        ///     The type of flag.
        /// </typeparam>
        /// <param name="mask">
        ///     The target mask.
        /// </param>
        /// <param name="flag">
        ///     Switchable flag.
        /// </param>
        /// <returns>
        ///     Returns a mask with the flag switched.
        /// </returns>
        public static TMask ToggleFlag<TMask, TFlag>(TMask mask, TFlag flag)
            where TMask : notnull
            where TFlag : struct, Enum
        {
            if (mask is int || mask is long)
            {
                var bitFlag = Convert.ToInt64(flag);
                var bitMask = Convert.ToInt64(mask);

                bitMask = bitMask ^ bitFlag;

                return (TMask)((IConvertible)bitMask).ToType(typeof(TMask), null);
            }

            if (mask is uint || mask is ulong || mask is Enum)
            {
                var bitFlag = Convert.ToUInt64(flag);
                var bitMask = Convert.ToUInt64(mask);

                bitMask = bitMask ^ bitFlag;

                return (TMask)((IConvertible)bitMask).ToType(typeof(TMask), null);
            }

            throw new InvalidOperationException($"Unexpected {mask} type: {mask.GetType()}");
        }

        /// <summary>
        ///     Updates the <paramref name="flag" /> flag in the mask <paramref name="mask" />.
        /// </summary>
        /// <typeparam name="TMask">
        ///     The type of mask. Supported types: <see cref="int" />, <see cref="uint" />, <see cref="long" />, <see cref="ulong" />, <see cref="Enum" />.
        /// </typeparam>
        /// <typeparam name="TFlag">
        ///     The type of flag.
        /// </typeparam>
        /// <param name="mask">
        ///     The target mask.
        /// </param>
        /// <param name="flag">
        ///     A changeable flag.
        /// </param>
        /// <param name="flagIsOn">
        ///     Indicates that the <paramref name="flag" /> flag should be set or unchecked.
        /// </param>
        /// <returns>
        ///     Returns a mask with the updated flag value.
        /// </returns>
        public static TMask UpdateFlag<TMask, TFlag>(TMask mask, TFlag flag, bool flagIsOn)
            where TMask : notnull
            where TFlag : struct, Enum
        {
            if (mask is int || mask is long)
            {
                var bitFlag = Convert.ToInt64(flag);
                var bitMask = Convert.ToInt64(mask);

                bitMask = flagIsOn ? bitMask | bitFlag : bitMask & ~bitFlag;

                return (TMask)((IConvertible)bitMask).ToType(typeof(TMask), null);
            }

            if (mask is uint || mask is ulong || mask is Enum)
            {
                var bitFlag = Convert.ToUInt64(flag);
                var bitMask = Convert.ToUInt64(mask);

                bitMask = flagIsOn ? bitMask | bitFlag : bitMask & ~bitFlag;

                return (TMask)((IConvertible)bitMask).ToType(typeof(TMask), null);
            }

            throw new InvalidOperationException($"Unexpected {mask} type: {mask.GetType()}");
        }

        /// <summary>
        ///     Sets the <paramref name="flag" /> flag in the mask <paramref name="mask" />.
        /// </summary>
        /// <typeparam name="TMask">
        ///     The type of mask. Supported types: <see cref="int" />, <see cref="uint" />, <see cref="long" />, <see cref="ulong" />, <see cref="Enum" />.
        /// </typeparam>
        /// <typeparam name="TFlag">
        ///     The type of flag.
        /// </typeparam>
        /// <param name="mask">
        ///     The target mask.
        /// </param>
        /// <param name="flag">
        ///     A changeable flag.
        /// </param>
        /// <returns>
        ///     Returns a mask with the updated flag value.
        /// </returns>
        public static TMask SetFlag<TMask, TFlag>(TMask mask, TFlag flag)
            where TMask : IConvertible
            where TFlag : struct, Enum
            => UpdateFlag(mask, flag, true);

        /// <summary>
        ///     Removes the <paramref name="flag" /> flag in the mask <paramref name="mask" />.
        /// </summary>
        /// <typeparam name="TMask">
        ///     The type of mask. Supported types: <see cref="int" />, <see cref="uint" />, <see cref="long" />, <see cref="ulong" />, <see cref="Enum" />.
        /// </typeparam>
        /// <typeparam name="TFlag">
        ///     The type of flag.
        /// </typeparam>
        /// <param name="mask">
        ///     The target mask.
        /// </param>
        /// <param name="flag">
        ///     A changeable flag.
        /// </param>
        /// <returns>
        ///     Returns a mask with the updated flag value.
        /// </returns>
        public static TMask ClearFlag<TMask, TFlag>(TMask mask, TFlag flag)
            where TMask : IConvertible
            where TFlag : struct, Enum
            => UpdateFlag(mask, flag, false);

        /// <summary>
        ///     Converts the value of <paramref name="value" /> to the type <typeparamref name="TTarget" />.
        /// </summary>
        /// <typeparam name="TTarget">
        ///     The type of the target value.
        /// </typeparam>
        /// <typeparam name="TSource">
        ///     The type of the original value.
        /// </typeparam>
        /// <param name="value">
        ///     The value given.
        /// </param>
        /// <returns>
        ///     The reduced value is of type <typeparamref name="TTarget" />.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Called when <paramref name="value" /> does not match the type <typeparamref name="TTarget" />.
        /// </exception>
        public static TTarget Cast<TSource, TTarget>(TSource value)
            where TSource : struct
            where TTarget : struct, Enum
        {
            if (!TryCast(value, out TTarget result))
            {
                throw new ArgumentOutOfRangeException("value", value, string.Empty);
            }

            return result;
        }

        /// <inheritdoc cref="Cast{TSource, TEnum}(TSource)"/>
        public static TTarget Cast<TTarget>(int value) where TTarget : struct, Enum
            => Cast<int, TTarget>(value);

        /// <inheritdoc cref="Cast{TSource, TEnum}(TSource)"/>
        public static TTarget Cast<TTarget>(uint value) where TTarget : struct, Enum
            => Cast<uint, TTarget>(value);

        /// <inheritdoc cref="Cast{TSource, TEnum}(TSource)"/>
        public static TTarget Cast<TTarget>(long value) where TTarget : struct, Enum
            => Cast<long, TTarget>(value);

        /// <inheritdoc cref="Cast{TSource, TEnum}(TSource)"/>
        public static TTarget Cast<TTarget>(ulong value) where TTarget : struct, Enum
            => Cast<ulong, TTarget>(value);

        /// <summary>
        ///     Checks whether the value <paramref name="value" /> matches the type <typeparamref name="TTarget" />
        ///     and converts it to this type, if possible.
        /// </summary>
        /// <typeparam name="TTarget">
        ///     The type of the target value.
        /// </typeparam>
        /// <typeparam name="TSource">
        ///     The type of the original value.
        /// </typeparam>
        /// <param name="value">
        ///     The value given.
        /// </param>
        /// <param name="result">
        ///     The value <paramref name="value" />, cast to the type <typeparamref name="TTarget" /> if the
        ///     method returned <see langword="true" />.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if <paramref name="value" /> matches the type <typeparamref name="TTarget" />,
        ///     otherwise <see langword="false" />.
        /// </returns>
        public static bool TryCast<TSource, TTarget>(TSource value, out TTarget result)
            where TSource : struct
            where TTarget : struct, Enum
        {
            var targetType = typeof(TTarget);
            var sourceType = typeof(TSource);

            Guard.Argument(targetType.IsEnum);
            Guard.Argument(!targetType.IsDefined(typeof(FlagsAttribute), inherit: false));
            Guard.Argument(Enum.GetUnderlyingType(targetType) == sourceType, $"Enum.GetUnderlyingType({targetType} == {sourceType})");

            if (!Enum.IsDefined(targetType, value))
            {
                result = default;
                return false;
            }

            result = (TTarget)(object)value;
            return true;
        }

        /// <summary>
        ///     Converts the value <paramref name="value" /> to the type <typeparamref name="TTarget" />, or
        ///     returns <paramref name="fallback" /> if the cast failed.
        /// </summary>
        /// <typeparam name="TTarget">
        ///     Type <see cref="Enum" />.
        /// </typeparam>
        /// <typeparam name="TSource">
        ///     The type of the original value.
        /// </typeparam>
        /// <param name="value">
        ///     The value given.
        /// </param>
        /// <param name="fallback">
        ///     The return value if the cast failed.
        /// </param>
        /// <returns>
        ///     The reduced value is of the type <typeparamref name="TTarget" />, or <paramref name="fallback" /> if the cast failed.
        /// </returns>
        public static TTarget CastOrDefault<TSource, TTarget>(TSource value, TTarget fallback)
            where TSource : struct
            where TTarget : struct, Enum
            => TryCast(value, out TTarget result) ? result : fallback;

        /// <summary>
        ///     Converts the value of <paramref name="value" /> to the type <typeparamref name="TTarget" />.
        /// </summary>
        /// <typeparam name="TTarget">
        ///     Type <see cref="Enum" />.
        /// </typeparam>
        /// <typeparam name="TSource">
        ///     The type of the original value.
        /// </typeparam>
        /// <param name="value">
        ///     The value given.
        /// </param>
        /// <returns>
        ///     The reduced value is of type <typeparamref name="TTarget" />, or <see langword="null" /> if <paramref name="value" />
        ///     is equal to <see langword="null" />.
        /// </returns>
        public static TTarget? CastOptional<TSource, TTarget>(TSource? value)
            where TSource : struct
            where TTarget : struct, Enum
            => value == null ? null : Cast<TSource, TTarget>(value.Value);
    }
}
