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
using System.Xml.Linq;
using System.Xml.Xsl;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Palettes
{
    internal sealed class PalettesGenerator : GeneratorBase
    {
        public PalettesGenerator(
           string defaultPaletteId,
           string defaultPaletteOutputDirectory,
           string comment,
           string[] excludedControls,
           LineEndingMode lineEndingMode,
           EmbeddedResourceXmlUrlResolver xsltUrlResolver,
           XmlWriterSettings xmlWriterSettings,
           TaskLoggingHelper log)
           : base(excludedControls, lineEndingMode, xsltUrlResolver, xmlWriterSettings, log)
        {
            _defaultPaletteId = defaultPaletteId;
            _defaultPaletteOutputDirectory = defaultPaletteOutputDirectory;
            _comment = comment;
            _nonDefaultPalettesOutputDirectory = Path.Combine(defaultPaletteOutputDirectory, "..\\");
        }

        public override bool Generate(string uiKitContent, out string[] generatedFilePaths)
        {
            var generatedFilePathList = new List<string>();

            try
            {
                var xslTransform = new XslCompiledTransform();

                var transformationStream = EmbeddedResourceHelper.GetEmbeddedResource(
                    Path.Combine(Const.TransformationResourcesDirectory, Const.TransformationCoreDirectory),
                    Const.PalettesTransformationFilename);

                using var transformationReader = XmlReader.Create(transformationStream);
                xslTransform.Load(transformationReader, XsltSettings.TrustedXslt, _xsltUrlResolver);

                var arguments = new XsltArgumentList();
                arguments.AddParam(Const.CommentTextParameterName, string.Empty, _comment);

                var xmlParserContext = new XmlParserContext(null, null, null, XmlSpace.Default);

                using var xmlReader = new XmlTextReader(uiKitContent, XmlNodeType.Document, xmlParserContext);
                using var palettesStringWriter = new StringWriter();
                using var palettesXmlWriter = XmlWriter.Create(palettesStringWriter, _xmlWriterSettings);
                xslTransform.Transform(xmlReader, arguments, palettesXmlWriter);

                var xml = palettesStringWriter.ToString();
                var xmlDocument = XDocument.Parse(xml);

                var palettesElement = xmlDocument.Elements(Const.PalettesElementName).SingleOrDefault();
                if (palettesElement == null)
                {
                    throw new InvalidOperationException(
                        $"Unable to find '{Const.PalettesElementName}' element inside palettes XSLT-transformation result: {xml}");
                }

                var paletteElements = palettesElement.Elements(Const.PaletteElementName).ToArray();
                if (!paletteElements.Any())
                {
                    throw new InvalidOperationException(
                        $"Unable to find '{Const.PaletteElementName}' elements inside palettes XSLT-transformation result: {xml}");
                }

                var defaultPaletteElement = paletteElements
                    .SingleOrDefault(paletteElement =>
                    {
                        var paletteId = paletteElement.Attribute(Const.PaletteIdAttributeName)?.Value;
                        return string.Equals(paletteId, _defaultPaletteId, StringComparison.InvariantCultureIgnoreCase);
                    });
                if (defaultPaletteElement == null)
                {
                    throw new InvalidOperationException(
                        $"Unable to find default palette with id '{_defaultPaletteId}' inside palettes XSLT-transformation result: {xml}");
                }

                Directory.CreateDirectory(_defaultPaletteOutputDirectory);

                var path = Path.Combine(_defaultPaletteOutputDirectory, Path.ChangeExtension(Const.PaletteFilename, Const.XamlExtension));
                SavePalette(defaultPaletteElement, path, _xmlWriterSettings);

                generatedFilePathList.Add(path);

                _log.LogMessage(MessageImportance.High, "XAML default palette generation completed.");

                var counter = 0;

                foreach (var nonDefaultPaletteElement in paletteElements.Except(new[] { defaultPaletteElement }))
                {
                    var paletteId = nonDefaultPaletteElement.Attribute(Const.PaletteIdAttributeName)?.Value;
                    if (string.IsNullOrWhiteSpace(paletteId))
                    {
                        throw new InvalidOperationException($"Unable to determine id of palette: {nonDefaultPaletteElement}");
                    }

                    var directory = Path.Combine(_nonDefaultPalettesOutputDirectory, $@"{paletteId.ToLowerInvariant()}\dictionaries");
                    Directory.CreateDirectory(directory);

                    path = Path.Combine(directory, Path.ChangeExtension(Const.PaletteFilename, Const.XamlExtension));
                    SavePalette(nonDefaultPaletteElement, path, _xmlWriterSettings);

                    generatedFilePathList.Add(path);

                    counter++;
                }

                _log.LogMessage(
                    MessageImportance.High,
                    counter > 0
                        ? $"XAML non-default palettes generation completed: {counter} file(s) created."
                        : $"There are no non-default palletes inside palettes XSLT-transformation result. Skip XAML non-default palettes generation.");
            }
            catch (Exception ex)
            {
                _log.LogError("XAML palettes generation failed.");
                _log.LogErrorFromException(ex, showStackTrace: true);
                generatedFilePaths = new string[0];
                return false;
            }

            generatedFilePaths = generatedFilePathList.ToArray();
            return true;
        }

        private void SavePalette(XElement paletteElement, string path, XmlWriterSettings settings)
        {
            using var paletteStringWriter = new StringWriter();
            using (var paletteXmlWriter = XmlWriter.Create(paletteStringWriter, settings))
            {
                foreach (var node in paletteElement.Nodes())
                {
                    node.WriteTo(paletteXmlWriter);
                }
            }

            var content = LineEndingHelper.NormalizeLineEndings(paletteStringWriter.ToString(), _lineEndingMode);

            _log.LogMessage(MessageImportance.Normal, $"Creating '{path}'.");
            File.WriteAllText(path, content, Encoding.UTF8);
        }

        private readonly string _comment;
        private readonly string _defaultPaletteId;
        private readonly string _defaultPaletteOutputDirectory;
        private readonly string _nonDefaultPalettesOutputDirectory;
    }
}
