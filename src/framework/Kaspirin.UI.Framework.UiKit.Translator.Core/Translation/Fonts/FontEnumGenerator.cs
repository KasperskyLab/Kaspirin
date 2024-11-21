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
using Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Palettes;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Fonts
{
    public sealed class FontEnumGenerator
    {
        public FontEnumGenerator(
            string enumFileTargetPath, 
            string fontNamespacePart,
            string paletteNamespacePart, 
            string comment, 
            TaskLoggingHelper log)
        {
            _enumFileTargetPath = enumFileTargetPath;
            _fontNamespacePart = fontNamespacePart;
            _paletteNamespacePart = paletteNamespacePart;
            _comment = comment;
            _log = log;
        }

        public bool Generate(string uiKitContent)
        {
            var paletteItems = GetPaletteItems(uiKitContent);

            var fontItems = GetFonts(uiKitContent);
            if (fontItems.Any())
            {
                Translate(fontItems, paletteItems);
            }

            return true;
        }

        private IList<Font> GetFonts(string uiKitContent) =>
            new XmlFontSource(msg => _log.LogWarning(msg))
            .GetFonts(uiKitContent)
            .OrderBy(r => r.Name)
            .ToList();

        private IList<PaletteItem> GetPaletteItems(string uiKitContent) =>
            new XmlPaletteSource()
            .GetPalettes(uiKitContent)
            .OrderBy(r => r.Name)
            .ToList();

        private void Translate(IList<Font> fontItems, IList<PaletteItem> paletteItems)
        {
            var enumsTemplate = ReadEnumsTemplate();

            enumsTemplate = FillEnum(fontItems, paletteItems, enumsTemplate);
            enumsTemplate = FillNamespace(enumsTemplate);
            enumsTemplate = CommentGenerator.AddFileComment(enumsTemplate, _comment);

            SaveEnums(enumsTemplate);
        }

        private string ReadEnumsTemplate()
        {
            using var stream = EmbeddedResourceHelper.GetEmbeddedResource(
                Const.UIKitFontTemplateDirectory,
                Const.UIKitFontEnumsTemplateFilename);
            using var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        private string FillNamespace(string enumsFile)
        {
            return enumsFile
                .Replace("@PaletteNamespacePart", _paletteNamespacePart)
                .Replace("@FontNamespacePart", _fontNamespacePart);
        }

        private string FillEnum(IList<Font> fontItems, IList<PaletteItem> paletteItems, string enumsFile)
        {
            var fontsString = string.Join(",\r\n", fontItems
                .Select(i => $"{_indent}{_indent}{_indent}{i.Name}"));

            var brushesString = string.Join(",\r\n", paletteItems
                .Where(i => i.Name.Contains(Const.UIKitPaletteTextIconsBrushPrefix))
                .Select(i => i.Name.Replace(Const.UIKitPaletteTextIconsBrushPrefix, ""))
                .Select(i => $"{_indent}{_indent}{_indent}{i}"));

            return enumsFile
                .Replace("@FontStyles", fontsString)
                .Replace("@FontStyles", fontsString)
                .Replace("@FontStyleIdPrefix", Const.TextStyleIdPrefix)
                .Replace("@FontBrushes", brushesString)
                .Replace("@FontBrushPrefix", Const.UIKitPaletteTextIconsBrushPrefix);
        }

        private void SaveEnums(string enumFile)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_enumFileTargetPath));

            File.WriteAllText(_enumFileTargetPath, enumFile);

            _log.LogMessage(MessageImportance.High, "Font enums file generation completed.");
        }

        private readonly string _indent = Const.DefaultOutputIndentChars;
        private readonly string _enumFileTargetPath;
        private readonly string _fontNamespacePart;
        private readonly string _paletteNamespacePart;
        private readonly string _comment;
        private readonly TaskLoggingHelper _log;
    }
}
