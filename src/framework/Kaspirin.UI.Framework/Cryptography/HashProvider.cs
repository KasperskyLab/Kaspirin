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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace Kaspirin.UI.Framework.Cryptography
{
    /// <summary>
    ///     Calculates the hash sum for the specified data.
    /// </summary>
    public static class HashProvider
    {
        /// <summary>
        ///     Calculates SHA256 for the specified file.
        /// </summary>
        /// <param name="filePath">
        ///     The path to the file.
        /// </param>
        /// <returns>
        ///     The hash amount for the specified file.
        /// </returns>
        public static string CalculateSha256FromFile(string filePath)
        {
            using var sha256Hash = SHA256.Create();
            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            fileStream.Position = 0;
            var hashValue = sha256Hash.ComputeHash(fileStream);

            return BitConverter.ToString(hashValue).Replace("-", string.Empty);
        }

        /// <summary>
        ///     Calculates SHA256 for the specified string.
        /// </summary>
        /// <param name="data">
        ///     Line.
        /// </param>
        /// <returns>
        ///     The hash amount for the specified file.
        /// </returns>
        public static string CalculateSha256FromString(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return string.Empty;
            }

            using var sha256Hash = SHA256.Create();

            return BitConverter
                .ToString(sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data)))
                .Replace("-", string.Empty);
        }

        /// <summary>
        ///     Calculates MD5 for the specified secure string.
        /// </summary>
        /// <param name="secureString">
        ///     A secure string.
        /// </param>
        /// <returns>
        ///     The hash amount for the specified secure string.
        /// </returns>
        public static string CalculateMd5(SecureString secureString)
        {
            var length = secureString.Length;

            var buffer = IntPtr.Zero;
            var charArray = new char[length];
            var byteArray = default(byte[]);
            var hash = default(byte[]);

            try
            {
                buffer = Marshal.SecureStringToBSTR(secureString);
                Marshal.Copy(buffer, charArray, 0, length);
                byteArray = Encoding.Unicode.GetBytes(charArray);
                using var md5 = MD5.Create();
                hash = md5.ComputeHash(byteArray);
            }
            finally
            {
                if (buffer != IntPtr.Zero)
                {
                    Marshal.ZeroFreeBSTR(buffer);
                }

                Array.Clear(charArray, 0, charArray.Length);

                if (byteArray is not null)
                {
                    Array.Clear(byteArray, 0, byteArray.Length);
                }
            }

            return string.Join(string.Empty, hash.Select(x => x.ToString("X2")));
        }
    }
}
