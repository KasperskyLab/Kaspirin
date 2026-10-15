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
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Localization.Resources.ResourceBrowsers;

internal sealed class ZipArchiveProvider : IZipArchiveProvider
{
    public static IZipArchiveProvider Create(string filePath)
        => new ZipArchiveProvider(filePath);

    public ZipArchiveProvider(string filePath)
        => _zipArchive = CreateZipArchive(filePath);

    public IEnumerable<IZipEntryProvider> Entries
        => _zipArchive.Entries.Select(e => new ZipEntryProvider(e)).ToArray();

    public void Dispose()
        => _zipArchive.Dispose();

    private static ZipArchive CreateZipArchive(string filePath)
    {
        var fInfo = new FileInfo(filePath);
        if (fInfo.Length < _preloadFileSizeLimit)
        {
            return ReadZipAsMemoryStream(filePath);
        }
        else
        {
            return ReadZipAsFileStream(filePath);
        }
    }

    private static ZipArchive ReadZipAsMemoryStream(string filePath)
    {
        var fStream = GetFileStream(filePath);
        var mStream = new MemoryStream();

        fStream.CopyTo(mStream);
        fStream.Dispose();

        return new ZipArchive(mStream, ZipArchiveMode.Read);
    }

    private static ZipArchive ReadZipAsFileStream(string filePath)
    {
        var fStream = GetFileStream(filePath);

        return new ZipArchive(fStream, ZipArchiveMode.Read);
    }

    private static Stream GetFileStream(string filePath)
        => new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 4096, FileOptions.SequentialScan);

    private readonly ZipArchive _zipArchive;

    private static readonly int _preloadFileSizeLimit = 4 * 1024 * 1024;
}
#endif