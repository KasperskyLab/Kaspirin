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

namespace Kaspirin.UI.Framework.UiKit.@ProductNamespacePart.Illustrations
{
@IllustrationEnumDeclarations

    internal static class UIKitIllustrationMetadataRegistrar
    {
#if NETFRAMEWORK
        public static void RegisterMetadata()
#else
        [ModuleInitializer]
        internal static void RegisterMetadata()
#endif
        {
            if (Interlocked.CompareExchange(ref _isRegistered, 1, 0) == 0)
            {
@IllustrationsMetadataRegistration
            }
        }

        private static int _isRegistered = 0;
    }
}