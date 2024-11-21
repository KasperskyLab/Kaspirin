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
using System.Text;
using System.Xml;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Illustrations
{
    internal sealed class IllustrationSvgGenerator : GeneratorBase
    {
        public const string IllustrationSvgFilenamePrefix = "UIKitIllustration_";

        public IllustrationSvgGenerator(
            Func<string, Configuration.ProductMediaProjectInfo> productMediaProjectInfoProvider,
            string[] excludedControls,
            LineEndingMode lineEndingMode,
            EmbeddedResourceXmlUrlResolver xsltUrlResolver,
            XmlWriterSettings xmlWriterSettings,
            TaskLoggingHelper log)
            : base(excludedControls, lineEndingMode, xsltUrlResolver, xmlWriterSettings, log)
        {
            _productMediaProjectInfoProvider = productMediaProjectInfoProvider;
        }

        public override bool Generate(string uiKitContent, out string[] generatedFilePaths)
        {
            var generatedFilePathList = new List<string>();

            try
            {
                var illustrations = GetIllustrations(uiKitContent);
                foreach (var kvp in illustrations)
                {
                    var product = kvp.Key;

                    var productIllustrations = kvp.Value
                        .OrderBy(i => i.Scope)
                        .ThenBy(i => i.Name)
                        .ToArray();

                    var productIllustrationSvgDirectory = _productMediaProjectInfoProvider(product).IllustrationSvgDirectory;
                    if (string.IsNullOrWhiteSpace(productIllustrationSvgDirectory))
                    {
                        throw new InvalidOperationException($"Illustration SVG directory for product '{product}' is not configured");
                    }

                    Directory.CreateDirectory(productIllustrationSvgDirectory);

                    foreach (var illustration in productIllustrations)
                    {
                        foreach (var vector in illustration.Vectors)
                        {
                            var svgPaths = GetSvgPaths(
                                productIllustrationSvgDirectory,
                                illustration.Scope,
                                illustration.Name,
                                vector.IsRTL,
                                vector.Theme);

                            generatedFilePathList.AddRange(SaveSvg(vector.Vector, svgPaths));
                        }
                    }

                    _log.LogMessage(
                        MessageImportance.High,
                        $"Illustration SVG files generation for product '{product}' completed: {generatedFilePathList.Count} file(s) created.");
                }
            }
            catch (Exception ex)
            {
                _log.LogError("Illustration SVG files generation failed.");
                _log.LogErrorFromException(ex, showStackTrace: true);
                generatedFilePaths = new string[0];
                return false;
            }

            generatedFilePaths = generatedFilePathList.ToArray();
            return true;
        }

        private IDictionary<string, Illustration[]> GetIllustrations(string uiKitContent) =>
            new XmlIllustrationSource()
            .GetIllustrations(uiKitContent)
            .GroupBy(i => i.Product)
            .ToDictionary(g => g.Key, g => g.ToArray());

        private IEnumerable<string> GetSvgPaths(string outputDirectory, string scope, string name, bool isRTL, string theme)
        {
            var filename = $"{IllustrationSvgFilenamePrefix}{name}.svg";

            var locales = isRTL
                ? Const.RtlLocales
                : Const.NeutralLocales;

            foreach (var locale in locales)
            {
                var themeSubdirectory = string.IsNullOrWhiteSpace(theme) || theme.Equals(Const.DefaultPaletteId, StringComparison.InvariantCultureIgnoreCase)
                    ? string.Empty
                    : $@"{theme}\";

                var directory = Path.Combine(outputDirectory, Path.Combine(locale, $@"{themeSubdirectory}images\{scope}").ToLower());

                Directory.CreateDirectory(directory);

                yield return Path.Combine(directory, filename);
            }
        }

        private IEnumerable<string> SaveSvg(string svg, IEnumerable<string> paths)
        {
            if (string.IsNullOrWhiteSpace(svg))
            {
                yield break;
            }

            var svgFileContent = LineEndingHelper.NormalizeLineEndings(svg, _lineEndingMode);

            foreach (var path in paths)
            {
                _log.LogMessage(MessageImportance.Normal, $"Creating illustration SVG: '{path}'.");

                File.WriteAllText(path, svgFileContent, Encoding.UTF8);

                yield return path;
            }
        }

        private readonly Func<string, Configuration.ProductMediaProjectInfo> _productMediaProjectInfoProvider;
    }
}
