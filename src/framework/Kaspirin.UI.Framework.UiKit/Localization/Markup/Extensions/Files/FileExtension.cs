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
using System.Text;

namespace Kaspirin.UI.Framework.UiKit.Localization.Markup.Extensions.Files
{
    public class FileExtension : LocalizationMarkupBase
    {
        public FileExtension() : this(string.Empty) { }

        public FileExtension(string key) : base(key) { }

        public FileExtension(string key, string scope) : base(key, scope) { }

        public FileExtensionMode Mode { get; set; } = FileExtensionMode.Uri;

        protected override object? ProvideValue()
        {
            var localizer = ProvideLocalizer<IFileLocalizer>();

            return Mode switch
            {
                FileExtensionMode.Uri => localizer.GetFileUri(Key),
                FileExtensionMode.Path => localizer.GetFilePath(Key),
                FileExtensionMode.Content => localizer.GetFileContent(Key),
                FileExtensionMode.Text => localizer.GetFileText(Key, Encoding.UTF8),
                FileExtensionMode.Stream => localizer.GetFileStream(Key),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected override ILocalizer PrepareLocalizer()
        {
            return LocalizationManager.Current.LocalizerFactory.Resolve<IFileLocalizer>(Scope);
        }
    }
}