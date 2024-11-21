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
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;

namespace Kaspirin.UI.Framework.Extensions.SecureStrings
{
    /// <summary>
    ///     Extension methods for <see cref="SecureString" />.
    /// </summary>
    public static class SecureStringExtensions
    {
        /// <summary>
        ///     An object <see cref="SecureString" /> with an empty string inside.
        /// </summary>
        public static SecureString Empty { get; } = CreateEmpty();

        /// <summary>
        ///     Converts <see cref="SecureString" /> to a string.
        /// </summary>
        /// <param name="secureString">
        ///     A secure string.
        /// </param>
        /// <returns>
        ///     The <see cref="string" /> object with the result of the conversion.
        /// </returns>
        public static string ToSimpleString(this SecureString? secureString)
        {
            if (secureString == null)
            {
                return string.Empty;
            }

            var buffer = IntPtr.Zero;

            try
            {
                buffer = Marshal.SecureStringToBSTR(secureString);
                return Marshal.PtrToStringBSTR(buffer);
            }
            finally
            {
                if (buffer != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(buffer);
                }
            }
        }

        /// <summary>
        ///     Converts <see cref="SecureString" /> to an array of characters.
        /// </summary>
        /// <param name="secureString">
        ///     A secure string.
        /// </param>
        /// <returns>
        ///     An array <see cref="char" /> with the result of the conversion.
        /// </returns>
        public static char[] ToCharArray(this SecureString secureString)
        {
            Guard.ArgumentIsNotNull(secureString);

            var buffer = IntPtr.Zero;
            var charArray = new char[secureString.Length];

            try
            {
                buffer = Marshal.SecureStringToBSTR(secureString);
                Marshal.Copy(buffer, charArray, 0, charArray.Length);
            }
            finally
            {
                if (buffer != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(buffer);
                }
            }

            return charArray;
        }

        /// <summary>
        ///     Converts <see cref="SecureString" /> to an array of bytes.
        /// </summary>
        /// <param name="secureString">
        ///     A secure string.
        /// </param>
        /// <returns>
        ///     An array <see cref="byte" /> with the result of the conversion.
        /// </returns>
        public static byte[] ToByteArray(this SecureString secureString)
        {
            Guard.ArgumentIsNotNull(secureString);

            var buffer = IntPtr.Zero;
            var byteArray = new byte[secureString.Length * sizeof(char)];

            try
            {
                buffer = Marshal.SecureStringToBSTR(secureString);
                Marshal.Copy(buffer, byteArray, 0, byteArray.Length);
            }
            finally
            {
                if (buffer != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(buffer);
                }
            }

            return byteArray;
        }

        /// <summary>
        ///     Checks if <paramref name="SecureString" /> is an empty string.
        /// </summary>
        /// <param name="secureString">
        ///     A secure string.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the string is empty, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsNullOrEmpty(this SecureString? secureString)
        {
            if (secureString == null || secureString.Length == 0)
            {
                return true;
            }

            var charArray = default(char[]);

            try
            {
                charArray = ToCharArray(secureString);
                return charArray.Length == 0;
            }
            finally
            {
                if (charArray is not null)
                {
                    Array.Clear(charArray, 0, charArray.Length);
                }
            }
        }

        /// <summary>
        ///     Checks whether <paramref name="SecureString" /> is an empty string or contains only space characters.
        /// </summary>
        /// <param name="secureString">
        ///     A secure string.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the string is empty or contains only space characters, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsNullOrWhiteSpace(this SecureString? secureString)
        {
            if (secureString == null || secureString.Length == 0)
            {
                return true;
            }

            var charArray = default(char[]);

            try
            {
                charArray = ToCharArray(secureString);
                return charArray.All(char.IsWhiteSpace);
            }
            finally
            {
                if (charArray is not null)
                {
                    Array.Clear(charArray, 0, charArray.Length);
                }
            }
        }

        /// <summary>
        ///     Checks whether <paramref name="x" /> and <paramref name="y" /> are equal in value.
        /// </summary>
        /// <param name="x">
        ///     The first safe line.
        /// </param>
        /// <param name="y">
        ///     The second safe line.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the strings are equal in value, otherwise <see langword="false" />.
        /// </returns>
        public static bool IsEqualTo(this SecureString? x, SecureString? y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x is null || y is null || x.Length != y.Length)
            {
                return false;
            }

            var xCharArray = default(char[]);
            var yCharArray = default(char[]);

            try
            {
                xCharArray = ToCharArray(x);
                yCharArray = ToCharArray(y);

                return xCharArray.SequenceEqual(yCharArray);
            }
            finally
            {
                if (xCharArray is not null)
                {
                    Array.Clear(xCharArray, 0, xCharArray.Length);
                }

                if (yCharArray is not null)
                {
                    Array.Clear(yCharArray, 0, yCharArray.Length);
                }
            }
        }

        /// <summary>
        ///     Calculates the MD5 hash sum for a secure string.
        /// </summary>
        /// <param name="secureString">
        ///     A secure string.
        /// </param>
        /// <returns>
        ///     The hash amount for the specified secure string.
        /// </returns>
        public static string CalculateMd5Hash(this SecureString secureString)
            => HashProvider.CalculateMd5(secureString);

        /// <summary>
        ///     Returns <paramref name="SecureString" />, or <see cref="Empty" /> if <paramref name="SecureString" /> is <see langword="null" />.
        /// </summary>
        /// <param name="secureString">
        ///     A secure string.
        /// </param>
        /// <returns>
        ///     The original safe string, or <see cref="Empty" />.
        /// </returns>
        public static SecureString OrEmpty(this SecureString? secureString)
            => secureString ?? Empty;

        private static SecureString CreateEmpty()
        {
            var empty = new SecureString();
            empty.MakeReadOnly();
            return empty;
        }
    }
}