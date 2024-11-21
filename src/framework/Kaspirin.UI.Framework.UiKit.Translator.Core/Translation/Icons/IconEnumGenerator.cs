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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Icons
{
    public sealed class IconEnumGenerator
    {
        public IconEnumGenerator(
            string enumFileTargetPath, 
            string comment,
            TaskLoggingHelper log)
        {
            _enumFileTargetPath = enumFileTargetPath;
            _comment = comment;
            _log = log;
        }

        public bool Generate(string uiKitContent)
        {
            var icons = GetIcons(uiKitContent);
            if (icons.Any())
            {
                Translate(icons);
            }

            return true;
        }

        private IList<Icon> GetIcons(string uiKitContent) =>
            new XmlIconSource(msg => _log.LogWarning(msg))
            .GetIcons(uiKitContent)
            .OrderBy(r => r.Size)
            .ThenBy(r => r.Name)
            .ToList();

        private void Translate(IList<Icon> icons)
        {
            var enumsTemplate = ReadEnumsTemplate();

            foreach (var iconSize in _supportedSizes)
            {
                enumsTemplate = FillEnum(iconSize, icons, enumsTemplate);
            }

            enumsTemplate = FillMetadata(icons, enumsTemplate);
            enumsTemplate = CommentGenerator.AddFileComment(enumsTemplate, _comment);

            SaveEnums(enumsTemplate);
        }

        private string ReadEnumsTemplate()
        {
            using var stream = EmbeddedResourceHelper.GetEmbeddedResource(
                Const.UIKitIconTemplateDirectory,
                Const.UIKitIconEnumsTemplateFilename);
            using var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        private string FillEnum(int iconSize, IList<Icon> icons, string enumsFile)
        {
            var iconsString = string.Join(",\r\n", icons
                .Where(i => i.Size == iconSize)
                .Select(i => $"{_indent}{_indent}{i.Name}"));

            return enumsFile.Replace($"@Icons{iconSize}", iconsString);
        }

        private string FillMetadata(IList<Icon> icons, string enumsFile)
        {
            var iconsMetadata = string.Join("\r\n", icons
                .Where(i => i.IsColorfull || i.IsAutoRTL)
                .Select(i =>
                    $"{_indent}{_indent}{_indent}{_indent}" +
                    $"UIKitIconMetadataStorage.Register(" +
                        $"{GetEnumTypeName(i.Size)}.{i.Name}, " +
                        $"isAutoRTL: {i.IsAutoRTL.ToString().ToLower()}, " +
                        $"isColorfull: {i.IsColorfull.ToString().ToLower()});"));

            return enumsFile.Replace("@IconsMetadataRegistration", iconsMetadata);
        }

        private void SaveEnums(string enumFile)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_enumFileTargetPath));

            File.WriteAllText(_enumFileTargetPath, enumFile);

            _log.LogMessage(MessageImportance.High, "Icon enums file generation completed.");
        }

        private static string GetEnumTypeName(int size)
            => $"UIKitIcon_{size}";

        private readonly int[] _supportedSizes = new[] { 12, 16, 24, 32, 48 };

        private readonly string _indent = Const.DefaultOutputIndentChars;
        private readonly string _enumFileTargetPath;
        private readonly string _comment;
        private readonly TaskLoggingHelper _log;
    }
}
