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
    internal sealed class CustomizationDictionariesGenerator : GeneratorBase
    {
        public CustomizationDictionariesGenerator(
            string outputDirectory,
            string comment,
            string[] excludedControls,
            LineEndingMode lineEndingMode,
            EmbeddedResourceXmlUrlResolver xsltUrlResolver,
            XmlWriterSettings xmlWriterSettings,
            TaskLoggingHelper log)
            : base(excludedControls, lineEndingMode, xsltUrlResolver, xmlWriterSettings, log)
        {
            _outputDirectory = outputDirectory;
            _comment = comment;
        }

        public override bool Generate(string uiKitContent, out string[] generatedFilePaths)
        {
            var generatedFilePathList = new List<string>();

            try
            {
                var xslTransform = new XslCompiledTransform();

                var transformationStream = EmbeddedResourceHelper.GetEmbeddedResource(
                    Path.Combine(Const.TransformationResourcesDirectory, Const.TransformationCoreDirectory),
                    Const.CustomizationDictionariesTransformationFilename);

                using var transformationReader = XmlReader.Create(transformationStream);
                xslTransform.Load(transformationReader, XsltSettings.TrustedXslt, _xsltUrlResolver);

                var arguments = new XsltArgumentList();
                arguments.AddParam(Const.ExcludedControlsFilterParameterName, string.Empty, GetExcludedControlsFilter());
                arguments.AddParam(Const.ExcludedControlsFilterDelimiterParameterName, string.Empty, Const.ExcludedControlsFilterDelimiter);
                arguments.AddParam(Const.CommentTextParameterName, string.Empty, _comment);

                var xmlParserContext = new XmlParserContext(null, null, null, XmlSpace.Default);

                using var xmlReader = new XmlTextReader(uiKitContent, XmlNodeType.Document, xmlParserContext);
                using var dictionariesStringWriter = new StringWriter();
                using var dictionariesXmlWriter = XmlWriter.Create(dictionariesStringWriter, _xmlWriterSettings);
                xslTransform.Transform(xmlReader, arguments, dictionariesXmlWriter);

                var xml = dictionariesStringWriter.ToString();
                var xmlDocument = XDocument.Parse(xml);

                var customizationDictionariesElement = xmlDocument
                    .Elements(Const.CustomizationDictionariesElementName)
                    .SingleOrDefault();
                if (customizationDictionariesElement == null)
                {
                    throw new InvalidOperationException(
                        $"Unable to find '{Const.CustomizationDictionariesElementName}' element " +
                        $"inside customization dictionary XSLT-transformation result: {xml}");
                }

                var customizationDictionaryElements = customizationDictionariesElement
                    .Elements(Const.CustomizationDictionaryElementName)
                    .ToArray();
                if (!customizationDictionaryElements.Any())
                {
                    throw new InvalidOperationException(
                        $"Unable to find '{Const.CustomizationDictionaryElementName}' elements " +
                        $"inside customization dictionary XSLT-transformation result: {xml}");
                }

                Directory.CreateDirectory(_outputDirectory);

                foreach (var customizationDictionaryElement in customizationDictionaryElements)
                {
                    var id = customizationDictionaryElement.Attribute(Const.CustomizationDictionaryIdAttributeName)?.Value;
                    if (string.IsNullOrWhiteSpace(id))
                    {
                        throw new InvalidOperationException($"Unable to determine id of customization dictionary: {customizationDictionaryElement}");
                    }

                    using var dictionaryStringWriter = new StringWriter();
                    using (var dictionaryXmlWriter = XmlWriter.Create(dictionaryStringWriter, _xmlWriterSettings))
                    {
                        foreach (var node in customizationDictionaryElement.Nodes())
                        {
                            node.WriteTo(dictionaryXmlWriter);
                        }
                    }

                    var path = Path.Combine(_outputDirectory, Path.ChangeExtension(id, Const.XamlExtension));
                    var content = LineEndingHelper.NormalizeLineEndings(dictionaryStringWriter.ToString(), _lineEndingMode);

                    _log.LogMessage(MessageImportance.Normal, $"Creating '{path}'.");
                    File.WriteAllText(path, content, Encoding.UTF8);

                    generatedFilePathList.Add(path);
                }

                _log.LogMessage(
                    MessageImportance.High,
                    $"XAML customization resource dictionaries generation completed: {generatedFilePathList.Count} file(s) created.");
            }
            catch (Exception ex)
            {
                _log.LogError("XAML customization resource dictionaries generation failed.");
                _log.LogErrorFromException(ex, showStackTrace: true);
                generatedFilePaths = new string[0];
                return false;
            }

            generatedFilePaths = generatedFilePathList.ToArray();
            return true;
        }

        private readonly string _outputDirectory;
        private readonly string _comment;
    }
}
