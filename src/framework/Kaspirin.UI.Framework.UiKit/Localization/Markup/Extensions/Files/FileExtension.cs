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

using System.Text;

namespace Kaspirin.UI.Framework.UiKit.Localization.Markup.Extensions.Files;

public class FileExtension : BaseLocalizationMarkupExtension
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
            FileExtensionMode.Uri => localizer.GetUri(Key),
            FileExtensionMode.Content => localizer.GetContent(Key),
            FileExtensionMode.Text => localizer.GetText(Key, Encoding.UTF8),
            FileExtensionMode.Stream => localizer.GetStream(Key),
            _ => throw new UnexpectedValueException(Mode)
        };
    }

    protected override ILocalizer PrepareLocalizer()
    {
        return LocalizationManager.GetLocalizer<IFileLocalizer>(Scope);
    }
}