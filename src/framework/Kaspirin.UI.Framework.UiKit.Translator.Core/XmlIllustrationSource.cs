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
using System.Linq;
using System.Xml.Linq;
using Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Illustrations;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core
{
    internal sealed class XmlIllustrationSource
    {
        public IEnumerable<Illustration> GetIllustrations(string uiKitContent)
        {
            var xmlDocument = XDocument.Parse(uiKitContent);

            var illustrationsElement = xmlDocument
                .Elements(Const.RootElementName)
                .Elements(Const.IllustrationsElementName)
                .SingleOrDefault();

            if (illustrationsElement == null)
            {
                throw new InvalidOperationException(
                    $"Unable to find '{Const.IllustrationsElementName}' element inside the file with UI Kit: {Environment.NewLine}{uiKitContent}");
            }

            var illustrationElements = illustrationsElement.Elements(Const.IllustrationElementName).ToArray();
            if (!illustrationElements.Any())
            {
                throw new InvalidOperationException(
                    $"Unable to find '{Const.IllustrationElementName}' elements inside the file with UI Kit: {Environment.NewLine}{uiKitContent}");
            }

            return illustrationElements
                .Select(NodeToDto)
                .Where(i => i != null);
        }

        private Illustration NodeToDto(XElement illustrationElement)
        {
            var illustrationId = illustrationElement.Attribute(Const.IllustrationIdAttributeName)?.Value;
            if (string.IsNullOrWhiteSpace(illustrationId))
            {
                throw new InvalidOperationException($"Unable to get illustration id: {Environment.NewLine}{illustrationElement}.");
            }

            if (!double.TryParse(illustrationElement.Attribute(Const.IllustrationHeightAttributeName)?.Value, out var height))
            {
                throw new InvalidOperationException(
                    $"Unable to parse double value of attribute '{Const.IllustrationHeightAttributeName}': {Environment.NewLine}{illustrationElement}");
            }

            if (!bool.TryParse(illustrationElement.Attribute(Const.IllustrationIsAutoRTLAttributeName)?.Value, out var isAutoRTL))
            {
                throw new InvalidOperationException(
                    $"Unable to parse boolean value of attribute '{Const.IllustrationIsAutoRTLAttributeName}': {Environment.NewLine}{illustrationElement}");
            }

            var illustrationName = illustrationElement.Attribute(Const.IllustrationNameAttributeName)?.Value;
            if (string.IsNullOrWhiteSpace(illustrationName))
            {
                throw new InvalidOperationException($"Unable to get illustration name: {Environment.NewLine}{illustrationElement}.");
            }

            var product = illustrationElement.Attribute(Const.IllustrationProductAttributeName)?.Value;
            if (string.IsNullOrWhiteSpace(product))
            {
                throw new InvalidOperationException($"Unable to get illustration product: {Environment.NewLine}{illustrationElement}.");
            }

            var scope = illustrationElement.Attribute(Const.IllustrationScopeAttributeName)?.Value;
            if (string.IsNullOrWhiteSpace(scope))
            {
                throw new InvalidOperationException($"Unable to get illustration scope: {Environment.NewLine}{illustrationElement}.");
            }

            if (!double.TryParse(illustrationElement.Attribute(Const.IllustrationWidthAttributeName)?.Value, out var width))
            {
                throw new InvalidOperationException(
                    $"Unable to parse double value of attribute '{Const.IllustrationWidthAttributeName}': {Environment.NewLine}{illustrationElement}");
            }

            var vectors = illustrationElement
                .Elements(Const.VectorsElementName)
                ?.Elements(Const.VectorElementName)
                ?.ToArray();

            if (vectors?.Any() != true)
            {
                throw new InvalidOperationException($"Unable to get illustration vectors: {Environment.NewLine}{illustrationElement}.");
            }

            if (vectors.Length > 4)
            {
                throw new InvalidOperationException($"Unsupported number of illustration vectors: {Environment.NewLine}{illustrationElement}.");
            }

            var illustrationVectors = new List<Illustration.IllustrationVector>();

            foreach (var vector in vectors)
            {
                if (!bool.TryParse(vector.Attribute(Const.VectorIsRTLAttributeName)?.Value, out var isRTL))
                {
                    throw new InvalidOperationException(
                        $"Unable to parse boolean value of attribute '{Const.VectorIsRTLAttributeName}' of vector inside illustration with id '{illustrationId}': {Environment.NewLine}{vector}");
                }

                var theme = vector.Attribute(Const.VectorThemeAttributeName)?.Value;

                var svgData = vector
                    .Element(Const.VectorDataElementName)
                    ?.Element(Const.SvgElementName)
                    ?.Value;

                if (string.IsNullOrWhiteSpace(svgData))
                {
                    throw new InvalidOperationException($"Unable to get vector's SVG inside illustration with id '{illustrationId}': {Environment.NewLine}{vector}");
                }

                illustrationVectors.Add(new Illustration.IllustrationVector() { IsRTL = isRTL, Theme = theme, Vector = svgData });
            }

            return new Illustration()
            {
                Height = height,
                IsAutoRTL = isAutoRTL,
                Name = illustrationName,
                Product = product,
                Scope = scope,
                Vectors = illustrationVectors.ToArray(),
                Width = width
            };
        }
    }
}
