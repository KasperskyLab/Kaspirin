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
using System.Text;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Files
{
    public sealed class FileLocalizer : LocalizerBase, IFileLocalizer
    {
        public FileLocalizer(LocalizerParameters parameters) : base(parameters) { }

        #region IFileLocalizer

        public byte[]? GetFileContent(string key)
        {
            Guard.ArgumentIsNotNull(key);

            var uri = GetFileUri(key);
            return uri == null ? null : ResourceProvider.ReadResource(uri);
        }

        public Stream? GetFileStream(string key)
        {
            Guard.ArgumentIsNotNull(key);

            var content = GetFileContent(key);
            return content == null ? null : new MemoryStream(content);
        }

        public string? GetFileText(string key, Encoding encoding)
        {
            Guard.ArgumentIsNotNull(key);
            Guard.ArgumentIsNotNull(encoding);

            var content = GetFileContent(key);
            return content == null ? null : encoding.GetString(content);
        }

        public Uri? GetFileUri(string key)
        {
            Guard.ArgumentIsNotNull(key);

            try
            {
                return GetValue<Uri>(key);
            }
            catch (Exception e)
            {
                e.ProcessAsLocalizerException($"Failed to provide Uri for key='{key}'.");
                return null;
            }
        }

        public string? GetFilePath(string key)
        {
            Guard.ArgumentIsNotNull(key);

            try
            {
                var uri = GetValue<Uri>(key);
                return uri.LocalPath;
            }
            catch (Exception e)
            {
                e.ProcessAsLocalizerException($"Failed to provide file path for key='{key}'.");
                return null;
            }
        }

        #endregion

        protected override IScope CreateScopeObject(Uri scopeUri)
        {
            return new FileScope(scopeUri, ResourceProvider);
        }
    }
}