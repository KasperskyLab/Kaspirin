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

namespace Kaspirin.UI.Framework.UiKit.Translator.Core.Translation
{
    internal sealed class SvgGenerator : GeneratorBase
    {
        public SvgGenerator(
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
            var generatedFilePathList = new List<string>();

            try
            {
                var xslTransform = new XslCompiledTransform();

                var transformationStream = EmbeddedResourceHelper.GetEmbeddedResource(
                    Const.TransformationResourcesDirectory,
                    Const.SvgTransformationFilename);

                var transformationReader = XmlReader.Create(transformationStream);
                xslTransform.Load(transformationReader, XsltSettings.TrustedXslt, _xsltUrlResolver);

                var arguments = new XsltArgumentList();
                // TODO [UI_KIT]: to mitigate compatibility issues control excludes are disabled for SVG.
                // Replace empty string with GetExcludedControlsFilter() call when it get possible.
                arguments.AddParam(Const.ExcludedControlsFilterParameterName, string.Empty, string.Empty);
                arguments.AddParam(Const.ExcludedControlsFilterDelimiterParameterName, string.Empty, Const.ExcludedControlsFilterDelimiter);

                var xmlParserContext = new XmlParserContext(null, null, null, XmlSpace.Default);

                using var xmlReader = new XmlTextReader(uiKitContent, XmlNodeType.Document, xmlParserContext);
                using var svgFilesStringWriter = new StringWriter();
                using var svgFilesXmlWriter = XmlWriter.Create(svgFilesStringWriter, _xmlWriterSettings);
                xslTransform.Transform(xmlReader, arguments, svgFilesXmlWriter);

                var xml = svgFilesStringWriter.ToString();
                var xmlDocument = XDocument.Parse(xml);

                var svgFilesElement = xmlDocument.Elements(Const.SvgFilesElementName).SingleOrDefault();
                if (svgFilesElement == null)
                {
                    throw new InvalidOperationException(
                        $"Unable to find '{Const.SvgFilesElementName}' element inside SVG XSLT-transformation result: {xml}");
                }

                var svgFileElements = svgFilesElement.Elements(Const.SvgFileElementName).ToArray();
                if (!svgFileElements.Any())
                {
                    _log.LogWarning($"Unable to find '{Const.SvgFileElementName}' elements inside SVG XSLT-transformation result: {xml}");
                    _log.LogWarning("Skip SVG processing since there is no SVG payload inside the file with UI Kit.");
                    generatedFilePaths = new string[0];
                    return true;
                }

                Directory.CreateDirectory(_outputDirectory);

                foreach (var svgFileElement in svgFileElements)
                {
                    var filename = svgFileElement.Attribute(Const.SvgFileFilenameAttributeName)?.Value;
                    if (string.IsNullOrWhiteSpace(filename))
                    {
                        throw new InvalidOperationException($"Unable to determine filename of SVG file: {svgFileElement}");
                    }

                    using var svgStringWriter = new StringWriter();
                    using (var svgXmlWriter = XmlWriter.Create(svgStringWriter, _xmlWriterSettings))
                    {
                        foreach (var node in svgFileElement.Nodes())
                        {
                            node.WriteTo(svgXmlWriter);
                        }
                    }

                    var path = Path.Combine(_outputDirectory, filename);
                    var content = LineEndingHelper.NormalizeLineEndings(svgStringWriter.ToString(), _lineEndingMode);

                    _log.LogMessage(MessageImportance.Normal, $"Creating '{path}'.");
                    File.WriteAllText(path, content, Encoding.UTF8);

                    generatedFilePathList.Add(path);
                }

                _log.LogMessage(
                    MessageImportance.High,
                    $"SVG files generation completed: {generatedFilePathList.Count} file(s) created.");
            }
            catch (Exception ex)
            {
                _log.LogError("SVG files generation failed.");
                _log.LogErrorFromException(ex, showStackTrace: true);
                generatedFilePaths = new string[0];
                return false;
            }

            generatedFilePaths = generatedFilePathList.ToArray();
            return true;
        }

        private readonly string _outputDirectory;
    }
}
