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

#pragma warning disable KCAIDE0002 // Enum has incorrect suffix

using System;

namespace Kaspirin.UI.Framework.NativeMethods.Api.Shell32.Enums
{
    /// <summary>
    ///     An undocumented API. Used as a parameter <see cref="Shell32Dll.SHGetUserPicturePath" />.
    /// </summary>
    [Flags]
    public enum ShGetUserPicturePathFlags : uint
    {
        /// <summary>
        ///     Create a default image catalog if it does not exist.
        /// </summary>
        SGUPP_CREATEPICTURESDIR = 0x80000000
    }
}