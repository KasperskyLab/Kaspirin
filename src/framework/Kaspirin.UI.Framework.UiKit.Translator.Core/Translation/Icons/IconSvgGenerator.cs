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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Icons;

internal sealed class IconSvgGenerator : GeneratorBase
{
    public const string IconSvgFilenamePrefix = "UIKitIcon_";

    public IconSvgGenerator(
        string outputDirectory,
        string[] excludedControls,
        LineEndingMode lineEndingMode,
        EmbeddedResourceXmlUrlResolver xsltUrlResolver,
        XmlWriterSettings xmlWriterSettings,
        TaskLoggingHelper log)
        : base(excludedControls, lineEndingMode, xsltUrlResolver, xmlWriterSettings, log)
    {
        _outputDirectory = outputDirectory;
    }

    public override bool Generate(string uiKitContent, out string[] generatedFilePaths)
    {
        var svgStorageName = $"Icons_Common";
        var svgStorage = new SvgStorage(svgStorageName, _outputDirectory, _lineEndingMode, _log);

        try
        {
            var icons = new XmlIconSource(msg => _log.LogWarning(msg))
                .GetIcons(uiKitContent)
                .ToArray();

            foreach (var icon in icons)
            {
                svgStorage.Add(icon.Svg, icon.Hash, GetSvgPaths(icon.Name, icon.Size, isRTL: false));

                var hasRtl = !string.IsNullOrWhiteSpace(icon.SvgRTL);
                if (hasRtl)
                {
                    svgStorage.Add(icon.SvgRTL, icon.HashRTL, GetSvgPaths(icon.Name, icon.Size, isRTL: true));
                }
            }

            svgStorage.Store();

            _log.LogMessage(
                MessageImportance.High,
                $"Icon SVG files generation completed: {svgStorage.Files.Count()} file(s) created.");
        }
        catch (Exception ex)
        {
            _log.LogError("Icon SVG files generation failed.");
            _log.LogErrorFromException(ex, showStackTrace: true);
            generatedFilePaths = new string[0];
            return false;
        }

        generatedFilePaths = svgStorage.Files;
        return true;
    }

    private IEnumerable<string> GetSvgPaths(string name, int size, bool isRTL)
    {
        var filename = $"{IconSvgFilenamePrefix}{name}_{size}.svg";

        var locales = isRTL
            ? Const.RtlLocales
            : Const.NeutralLocales;

        foreach (var locale in locales)
        {
            var directory = Path.Combine(_outputDirectory, locale, @"images\svg");
            Directory.CreateDirectory(directory);

            yield return Path.Combine(directory, filename);
        }
    }



    private readonly string _outputDirectory;
}
