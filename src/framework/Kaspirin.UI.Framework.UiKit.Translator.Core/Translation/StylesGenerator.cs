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
    internal sealed class StylesGenerator : GeneratorBase
    {
        public StylesGenerator(
            string outputPath,
            string comment,
            string[] excludedControls,
            string[] baseStylesResourceDictionaries,
            LineEndingMode lineEndingMode,
            EmbeddedResourceXmlUrlResolver xsltUrlResolver,
            XmlWriterSettings xmlWriterSettings,
            TaskLoggingHelper log)
            : base(excludedControls, lineEndingMode, xsltUrlResolver, xmlWriterSettings, log)
        {
            _outputPath = outputPath;
            _comment = comment;
            _baseStylesResourceDictionaries = baseStylesResourceDictionaries;
        }

        public override bool Generate(string uiKitContent, out string[] generatedFilePaths)
        {
            try
            {
                var xslTransform = new XslCompiledTransform();

                var transformationStream = EmbeddedResourceHelper.GetEmbeddedResource(
                    Path.Combine(Const.TransformationResourcesDirectory, Const.TransformationCoreDirectory),
                    Const.StylesTransformationFilename);

                using var transformationReader = XmlReader.Create(transformationStream);
                xslTransform.Load(transformationReader, XsltSettings.TrustedXslt, _xsltUrlResolver);

                var arguments = new XsltArgumentList();
                arguments.AddParam(Const.ExcludedControlsFilterParameterName, string.Empty, GetExcludedControlsFilter());
                arguments.AddParam(Const.ExcludedControlsFilterDelimiterParameterName, string.Empty, Const.ExcludedControlsFilterDelimiter);
                arguments.AddParam(Const.ExternalResourceDictionariesParameterName, string.Empty, GetExternalResourceDictionariesString());
                arguments.AddParam(Const.ExternalResourceDictionariesDelimiterParameterName, string.Empty, Const.ExternalResourceDictionariesDelimiter);
                arguments.AddParam(Const.ConditionalNamespacePrefixParameterName, string.Empty, ConditionalNamespacePrefix);
                arguments.AddParam(Const.CommentTextParameterName, string.Empty, _comment);

                var xmlParserContext = new XmlParserContext(null, null, null, XmlSpace.Default);

                using var xmlReader = new XmlTextReader(uiKitContent, XmlNodeType.Document, xmlParserContext);
                using var stringWriter = new StringWriter();
                using var xmlWriter = XmlWriter.Create(stringWriter, _xmlWriterSettings);
                xslTransform.Transform(xmlReader, arguments, xmlWriter);

                var xaml = ProcessConditionalNamespaces(stringWriter.ToString());
                var content = LineEndingHelper.NormalizeLineEndings(xaml, _lineEndingMode);

                Directory.CreateDirectory(Path.GetDirectoryName(_outputPath));

                _log.LogMessage(MessageImportance.Normal, $"Creating '{_outputPath}'.");
                File.WriteAllText(_outputPath, content, Encoding.UTF8);

                _log.LogMessage(MessageImportance.High, $"XAML styles resource dictionary generation completed.");
            }
            catch (Exception ex)
            {
                _log.LogError("XAML styles resource dictionary generation failed.");
                _log.LogErrorFromException(ex, showStackTrace: true);
                generatedFilePaths = new string[0];
                return false;
            }

            generatedFilePaths = new[] { _outputPath };
            return true;
        }

        private static string ProcessConditionalNamespaces(string xml)
        {
            var xmlDocument = XDocument.Parse(xml);

            var conditionalNamespaceAttributes = xmlDocument.Root?.Attributes()
                .Where(attribute => attribute.Name.LocalName.StartsWith(ConditionalNamespacePrefix))
                .ToArray();
            if (conditionalNamespaceAttributes?.Any() != true)
            {
                return xml;
            }

            foreach (var conditionalNamespaceAttribute in conditionalNamespaceAttributes)
            {
                xmlDocument.Root.SetAttributeValue(conditionalNamespaceAttribute.Name, null);

                var namespaceName = conditionalNamespaceAttribute.Name.LocalName.Replace(ConditionalNamespacePrefix, string.Empty);
                xmlDocument.Root.SetAttributeValue(XNamespace.Xmlns + namespaceName, conditionalNamespaceAttribute.Value);
            }

            return xmlDocument.ToString(SaveOptions.None);
        }

        private string GetExternalResourceDictionariesString()
            => string.Join(Const.ExternalResourceDictionariesDelimiter, _baseStylesResourceDictionaries);

        private readonly string _outputPath;
        private readonly string _comment;
        private readonly string[] _baseStylesResourceDictionaries;

        private const string ConditionalNamespacePrefix = "xmlns_";
    }
}
