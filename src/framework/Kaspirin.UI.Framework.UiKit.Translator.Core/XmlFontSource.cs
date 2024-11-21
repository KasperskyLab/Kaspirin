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
using Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Fonts;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core
{
    internal class XmlFontSource
    {
        public XmlFontSource(Action<string> logWarning)
        {
            _warn = logWarning ?? (_ => { });
        }

        public IEnumerable<Font> GetFonts(string uiKitContent)
        {
            var xmlDocument = XDocument.Parse(uiKitContent);

            var controlsElement = xmlDocument
                .Elements(Const.RootElementName)
                .Elements(Const.ControlsElementName)
                .SingleOrDefault();

            if (controlsElement == null)
            {
                throw new InvalidOperationException($"Unable to find '{Const.ControlsElementName}' element inside XSLT-transformation result: {uiKitContent}");
            }

            var textStylesElement = controlsElement.Elements(Const.TextStylesElementName).ToArray();
            if (!textStylesElement.Any())
            {
                throw new InvalidOperationException($"Unable to find '{Const.TextStylesElementName}' elements inside XSLT-transformation result: {uiKitContent}");
            }

            var textStyleElements = textStylesElement.Elements(Const.TextStyleElementName).ToArray();
            if (!textStyleElements.Any())
            {
                throw new InvalidOperationException($"Unable to find '{Const.TextStyleElementName}' elements inside XSLT-transformation result: {uiKitContent}");
            }

            return textStyleElements.Select(NodeToDto);
        }

        private Font NodeToDto(XElement textStyleElement)
        {
            var textStyleId = textStyleElement.Attribute(Const.TextStyleIdAttributeName)?.Value;
            if (string.IsNullOrWhiteSpace(textStyleId))
            {
                throw new InvalidOperationException($"Unable to get text style id: {Environment.NewLine}{textStyleElement}.");
            }

            if (!textStyleId.StartsWith(Const.TextStyleIdPrefix))
            {
                _warn($"Unexpected text style id: '{textStyleId}'.");
                return null;
            }

            return new Font()
            {
                Id = textStyleId,
                Name = textStyleId.Replace(Const.TextStyleIdPrefix, ""),
            };
        }

        private readonly Action<string> _warn;
    }
}
