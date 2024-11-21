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
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Icons;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core
{
    internal sealed class XmlIconSource
    {
        public XmlIconSource(Action<string> logWarning)
        {
            _warn = logWarning ?? (_ => { });
        }

        public IEnumerable<Icon> GetIcons(string uiKitContent)
        {
            var xmlDocument = XDocument.Parse(uiKitContent);

            var iconsElement = xmlDocument
                .Elements(Const.RootElementName)
                .Elements(Const.SvgIconsElementName)
                .SingleOrDefault();

            if (iconsElement == null)
            {
                throw new InvalidOperationException(
                    $"Unable to find '{Const.SvgIconsElementName}' element inside the file with UI Kit: {Environment.NewLine}{uiKitContent}");
            }

            var iconElements = iconsElement.Elements(Const.SvgIconElementName).ToArray();
            if (!iconElements.Any())
            {
                throw new InvalidOperationException(
                    $"Unable to find '{Const.SvgIconElementName}' elements inside the file with UI Kit: {Environment.NewLine}{uiKitContent}");
            }

            return iconElements
                .Select(NodeToDto)
                .Where(i => i != null);
        }

        private Icon NodeToDto(XElement iconElement)
        {
            var iconId = iconElement.Attribute(Const.SvgIconIdAttributeName)?.Value;
            if (string.IsNullOrWhiteSpace(iconId))
            {
                throw new InvalidOperationException($"Unable to get icon id: {Environment.NewLine}{iconElement}.");
            }

            var match = _iconIdRegex.Match(iconId);
            if (!match.Success)
            {
                _warn($"Unexpected icon id: '{iconId}'.");
                return null;
            }

            if (!bool.TryParse(iconElement.Attribute(Const.SvgIconIsAutoRTLAttributeName)?.Value, out var isAutoRTL))
            {
                throw new InvalidOperationException(
                    $"Unable to parse boolean value of attribute '{Const.SvgIconIsAutoRTLAttributeName}': {Environment.NewLine}{iconElement}");
            }

            if (!bool.TryParse(iconElement.Attribute(Const.SvgIconIsColorfullAttributeName)?.Value, out var isColorfull))
            {
                throw new InvalidOperationException(
                    $"Unable to parse boolean value of attribute '{Const.SvgIconIsColorfullAttributeName}': {Environment.NewLine}{iconElement}");
            }

            if (!int.TryParse(match.Groups[1].Value, out var size))
            {
                throw new InvalidOperationException($"Unable to get icon size from icon id: '{iconId}'.");
            }

            var name = match.Groups[2].Value.Replace(" ", "");

            var vectors = iconElement
                .Elements(Const.VectorsElementName)
                ?.Elements(Const.VectorElementName)
                ?.ToArray();

            if (vectors?.Any() != true)
            {
                throw new InvalidOperationException($"Unable to get icon vectors: {Environment.NewLine}{iconElement}.");
            }

            if (vectors.Length > 2)
            {
                throw new InvalidOperationException($"Unsupported number of icon vectors: {Environment.NewLine}{iconElement}.");
            }

            var svg = default(string);
            var svgRTL = default(string);

            foreach (var vector in vectors)
            {
                if (!bool.TryParse(vector.Attribute(Const.VectorIsRTLAttributeName)?.Value, out var isRTL))
                {
                    throw new InvalidOperationException(
                        $"Unable to parse boolean value of attribute '{Const.VectorIsRTLAttributeName}' of icon with id '{iconId}': {Environment.NewLine}{vector}");
                }

                var svgData = vector
                    .Element(Const.VectorDataElementName)
                    ?.Element(Const.SvgElementName)
                    ?.Value;

                if (string.IsNullOrWhiteSpace(svgData))
                {
                    throw new InvalidOperationException($"Unable to get vector's SVG of icon with id '{iconId}': {Environment.NewLine}{vector}");
                }

                if (isRTL)
                {
                    svgRTL = svgData;
                }
                else
                {
                    svg = svgData;
                }
            }

            return new Icon()
            {
                IsColorfull = isColorfull,
                IsAutoRTL = isAutoRTL,
                Size = size,
                Name = name,
                Svg = svg,
                SvgRTL = svgRTL
            };
        }

        private readonly Regex _iconIdRegex = new("^(\\d+) \\/.*? ([\\w ]+)$");
        private readonly Action<string> _warn;
    }
}
