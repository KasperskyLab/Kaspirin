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

using Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Palettes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core
{
    internal sealed class XmlPaletteSource
    {
        public IEnumerable<PaletteItem> GetPalettes(string uiKitContent)
        {
            var xmlDocument = XDocument.Parse(uiKitContent);

            var paletteItemsElement = xmlDocument
                .Elements(Const.RootElementName)
                .Elements(Const.PalettesElementName)
                .SingleOrDefault();

            if (paletteItemsElement == null)
            {
                throw new InvalidOperationException($"Unable to find '{Const.PalettesElementName}' element inside XSLT-transformation result: {uiKitContent}");
            }

            var paletteElements = paletteItemsElement.Elements(Const.PaletteElementName).ToArray();
            if (!paletteElements.Any())
            {
                throw new InvalidOperationException($"Unable to find '{Const.PaletteElementName}' elements inside XSLT-transformation result: {uiKitContent}");
            }

            var brushesElement = paletteElements.First().Element(Const.BrushesElementName)
                ?? throw new InvalidOperationException($"Unable to find '{Const.BrushesElementName}' element inside XSLT-transformation result: {uiKitContent}");

            var brushesElements = brushesElement.Elements(Const.BrushElementName);
            if (!brushesElements.Any())
            {
                throw new InvalidOperationException($"Unable to find '{Const.BrushElementName}' elements inside XSLT-transformation result: {uiKitContent}");
            }

            return brushesElements.Select(NodeToDto);
        }

        private PaletteItem NodeToDto(XElement paletteBrushElement)
        {
            var brushId = paletteBrushElement.Attribute(Const.BrushIdAttributeName)?.Value;
            if (string.IsNullOrWhiteSpace(brushId))
            {
                throw new InvalidOperationException($"Unable to determine id in paletteBrush element: {paletteBrushElement}");
            }

            return new PaletteItem()
            {
                Id = brushId,
                Name = brushId.Replace("-", "").Replace("_", ""),
            };
        }
    }
}
