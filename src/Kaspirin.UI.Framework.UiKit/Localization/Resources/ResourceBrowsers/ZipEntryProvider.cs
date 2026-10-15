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

#if NETCOREAPP
using System.IO;
using System.IO.Compression;

namespace Kaspirin.UI.Framework.UiKit.Localization.Resources.ResourceBrowsers;

internal sealed class ZipEntryProvider : IZipEntryProvider
{
    public ZipEntryProvider(ZipArchiveEntry entry)
        => _entry = entry;

    public string FullName => _entry.FullName;

    public bool IsFile => _entry.Length > 0;

    public Stream Open()
        => _entry.Open();

    private readonly ZipArchiveEntry _entry;
}
#endif