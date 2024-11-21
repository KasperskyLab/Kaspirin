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

@FileComment
using System.Runtime.CompilerServices;
using System.Threading;

namespace Kaspirin.UI.Framework.UiKit.Icons
{
    /// <summary>UIKit icons with 12x12 size.</summary>
    public enum UIKitIcon_12
    {
        UIKitUnset = 0,
@Icons12
    }

    /// <summary>UIKit icons with 16x16 size.</summary>
    public enum UIKitIcon_16
    {
        UIKitUnset = 0,
@Icons16
    }

    /// <summary>UIKit icons with 24x24 size.</summary>
    public enum UIKitIcon_24
    {
        UIKitUnset = 0,
@Icons24
    }

    /// <summary>UIKit icons with 32x32 size.</summary>
    public enum UIKitIcon_32
    {
        UIKitUnset = 0,
@Icons32
    }

    /// <summary>UIKit icons with 48x48 size.</summary>
    public enum UIKitIcon_48
    {
        UIKitUnset = 0,
@Icons48
    }

    internal static class UIKitIconMetadataRegistrar
    {
#if !NETFRAMEWORK
        [ModuleInitializer]
#endif
        internal static void RegisterMetadata()
        {
            if (Interlocked.CompareExchange(ref _isRegistered, 1, 0) == 0)
            {
@IconsMetadataRegistration
            }
        }

        private static int _isRegistered = 0;
    }
}