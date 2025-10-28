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

namespace Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Palettes;

internal sealed class PaletteEnumGenerator
{
    public PaletteEnumGenerator(
        string enumFileTargetPath,
        string paletteNamespacePart,
        string comment,
        TaskLoggingHelper log)
    {
        _enumFileTargetPath = enumFileTargetPath;
        _paletteNamespacePart = paletteNamespacePart;
        _comment = comment;
        _log = log;
    }

    public bool Generate(string uiKitContent)
    {
        var paletteItems = GetPaletteItems(uiKitContent);
        if (paletteItems.Any())
        {
            Translate(paletteItems);
        }

        return true;
    }

    private IList<PaletteItem> GetPaletteItems(string uiKitContent) =>
        new XmlPaletteSource()
        .GetPalettes(uiKitContent)
        .OrderBy(r => r.Id)
        .ToList();

    private void Translate(IList<PaletteItem> paletteItems)
    {
        var enumFile = ReadEnumTemplate();

        enumFile = CommentGenerator.AddFileComment(enumFile, _comment);
        enumFile = FillNamespace(enumFile);
        enumFile = FillEnums(paletteItems, enumFile);
        enumFile = FillMetadata(paletteItems, enumFile);

        SaveEnum(enumFile);
    }

    private string ReadEnumTemplate()
    {
        using var stream = EmbeddedResourceHelper.GetEmbeddedResource(
            Const.UIKitPaletteTemplateDirectory,
            Const.UIKitPaletteTemplateFilename);
        using var reader = new StreamReader(stream);

        return reader.ReadToEnd();
    }

    private string FillNamespace(string enumFile)
    {
        return enumFile.Replace("@PaletteNamespacePart", _paletteNamespacePart);
    }

    private string FillEnums(IList<PaletteItem> paletteItems, string enumFile)
    {
        var paletteItemsString = string.Join(",\r\n", paletteItems
            .Where(i => i.Name != "Transparent")
            .Select(i => $"{_indent}{i.Scope}{i.Name}"));

        var paletteItemScopesString = string.Join(",\r\n", paletteItems
            .Where(i => i.Scope != null)
            .GroupBy(i => i.Scope)
            .OrderBy(i => i.Key)
            .Select(i => $"{_indent}{i.Key}"));

        enumFile = enumFile.Replace($"@PaletteItems", paletteItemsString);
        enumFile = enumFile.Replace($"@PaletteItemScopes", paletteItemScopesString);

        return enumFile;
    }

    private string FillMetadata(IList<PaletteItem> paletteItems, string enumsFile)
    {
        var paletteMetadata = string.Join("\r\n", paletteItems
            .Select(i =>
                $"{_indent}{_indent}{_indent}" +
                $"UIKitPaletteMetadataStorage.Register(" +
                    $"color: UIKitPalette.{i.Scope}{i.Name}, " +
                    $"scope: UIKitPaletteScope.{i.Scope ?? "UIKitUnset"}, " +
                    $"resourceId: \"{i.Id}\");"));

        return enumsFile.Replace("@PaletteMetadataRegistration", paletteMetadata);
    }

    private void SaveEnum(string enumFile)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(_enumFileTargetPath));

        File.WriteAllText(_enumFileTargetPath, enumFile);

        _log.LogMessage(
            MessageImportance.High,
            $"Palette enums file generation completed.");
    }

    private readonly string _indent = Const.DefaultOutputIndentChars;
    private readonly string _enumFileTargetPath;
    private readonly string _paletteNamespacePart;
    private readonly string _comment;
    private readonly TaskLoggingHelper _log;
}
