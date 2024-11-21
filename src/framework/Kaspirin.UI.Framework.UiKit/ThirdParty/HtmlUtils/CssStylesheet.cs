// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 1/22/2024.
// Scope of modification:
//   - Code adaptation to project requirements.

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Kaspirin.UI.Framework.UiKit.ThirdParty.HtmlUtils
{
    internal sealed class CssStylesheet
    {
        private List<StyleDefinition> _styleDefinitions;

        public CssStylesheet(XmlElement htmlElement, string basePath)
        {
            if (htmlElement != null)
            {
                DiscoverStyleDefinitions(htmlElement, basePath);
            }
        }

        // Recursively traverses an html tree, discovers STYLE elements and creates a style definition table
        // for further cascading style application
        public void DiscoverStyleDefinitions(XmlElement htmlElement, string basePath)
        {
            var stylesheetBuffer = new StringBuilder();

            if (htmlElement.LocalName.ToLowerInvariant() == "link")
            {
                //  Add LINK elements processing for included stylesheets
                // <LINK href="css/ptnr/orange.css" type=text/css rel=stylesheet>
                if (HtmlToXamlConverter.GetAttribute(htmlElement, "rel") != "stylesheet")
                {
                    return;
                }

                var href = HtmlToXamlConverter.GetAttribute(htmlElement, "href")?.Trim();
                if (href == null)
                {
                    return;
                }

                var fullPath = HtmlToXamlConverter.GetFullPath(href, basePath);
                if (!File.Exists(fullPath))
                {
                    return;
                }

                stylesheetBuffer.Append(RemoveComments(File.ReadAllText(fullPath)));
                DiscoverStyleDefinitions(stylesheetBuffer);
                return;
            }

            if (htmlElement.LocalName.ToLowerInvariant() != "style")
            {
                // This is not a STYLE element. Recurse into it
                for (var htmlChildNode = htmlElement.FirstChild;
                    htmlChildNode != null;
                    htmlChildNode = htmlChildNode.NextSibling)
                {
                    if (htmlChildNode is XmlElement)
                    {
                        DiscoverStyleDefinitions((XmlElement)htmlChildNode, basePath);
                    }
                }

                return;
            }

            // Add style definitions from this style.

            // Collect all text from this style definition
            for (var htmlChildNode = htmlElement.FirstChild;
                htmlChildNode != null;
                htmlChildNode = htmlChildNode.NextSibling)
            {
                if (htmlChildNode is XmlText || htmlChildNode is XmlComment)
                {
                    stylesheetBuffer.Append(RemoveComments(htmlChildNode.Value));
                }
            }

            DiscoverStyleDefinitions(stylesheetBuffer);
        }

        private void DiscoverStyleDefinitions(StringBuilder stylesheetBuffer)
        {
            // CssStylesheet has the following syntactical structure:
            //     @import declaration;
            //     selector { definition }
            // where "selector" is one of: ".classname", "tagname"
            // It can contain comments in the following form: /*...*/

            var nextCharacterIndex = 0;
            while (nextCharacterIndex < stylesheetBuffer.Length)
            {
                // Extract selector
                var selectorStart = nextCharacterIndex;
                while (nextCharacterIndex < stylesheetBuffer.Length && stylesheetBuffer[nextCharacterIndex] != '{')
                {
                    // Skip declaration directive starting from @
                    if (stylesheetBuffer[nextCharacterIndex] == '@')
                    {
                        while (nextCharacterIndex < stylesheetBuffer.Length &&
                               stylesheetBuffer[nextCharacterIndex] != ';')
                        {
                            nextCharacterIndex++;
                        }

                        selectorStart = nextCharacterIndex + 1;
                    }

                    nextCharacterIndex++;
                }

                if (nextCharacterIndex < stylesheetBuffer.Length)
                {
                    // Extract definition
                    var definitionStart = nextCharacterIndex;
                    while (nextCharacterIndex < stylesheetBuffer.Length && stylesheetBuffer[nextCharacterIndex] != '}')
                    {
                        nextCharacterIndex++;
                    }

                    // Define a style
                    if (nextCharacterIndex - definitionStart > 2)
                    {
                        AddStyleDefinition(
                            stylesheetBuffer.ToString(selectorStart, definitionStart - selectorStart),
                            stylesheetBuffer.ToString(definitionStart + 1, nextCharacterIndex - definitionStart - 2));
                    }

                    // Skip closing brace
                    if (nextCharacterIndex < stylesheetBuffer.Length)
                    {
                        Guard.Assert(stylesheetBuffer[nextCharacterIndex] == '}');
                        nextCharacterIndex++;
                    }
                }
            }
        }

        // Returns a string with all c-style comments replaced by spaces
        private string RemoveComments(string text)
        {
            var commentStart = text.IndexOf("/*", StringComparison.Ordinal);
            if (commentStart < 0)
            {
                return text;
            }

            var commentEnd = text.IndexOf("*/", commentStart + 2, StringComparison.Ordinal);
            if (commentEnd < 0)
            {
                return text.Substring(0, commentStart);
            }

            return text.Substring(0, commentStart) + " " + RemoveComments(text.Substring(commentEnd + 2));
        }

        public void AddStyleDefinition(string selector, string definition)
        {
            // Notrmalize parameter values
            selector = selector.Trim().ToLowerInvariant();
            definition = definition.Trim().ToLowerInvariant();
            if (selector.Length == 0 || definition.Length == 0)
            {
                return;
            }

            if (_styleDefinitions == null)
            {
                _styleDefinitions = new List<StyleDefinition>();
            }

            var simpleSelectors = selector.Split(',');

            foreach (var t in simpleSelectors)
            {
                var simpleSelector = t.Trim();
                if (simpleSelector.Length > 0)
                {
                    _styleDefinitions.Add(new StyleDefinition(simpleSelector, definition));
                }
            }
        }

        public string GetStyle(string elementName, List<XmlElement> sourceContext)
        {
            Guard.Assert(sourceContext.Count > 0);

            var xmlElement = sourceContext[sourceContext.Count - 1];
            Guard.Assert(elementName == xmlElement.LocalName);

            string styleDefinition = null;
            //  Add id processing for style selectors
            if (_styleDefinitions != null)
            {
                var styleDefsOrder = _styleDefinitions
                    .OrderBy(s => s.Selector.Contains('.'))
                    .ThenBy(s => s.Selector.Contains('#'));

                foreach (var styleDef in styleDefsOrder)
                {
                    var selector = styleDef.Selector;

                    var selectorLevels = selector.Split(' ');

                    var indexInSelector = selectorLevels.Length - 1;
                    var indexInContext = sourceContext.Count - 1;
                    var selectorLevel = selectorLevels[indexInSelector].Trim();

                    var elementId = HtmlToXamlConverter.GetAttribute(xmlElement, "id");
                    var elementClasses = HtmlToXamlConverter.GetAttribute(xmlElement, "class")?.Split() ?? new string[] { null };
                    foreach (var elementClass in elementClasses)
                    {
                        if (MatchSelectorLevel(selectorLevel, elementName, elementId, elementClass))
                        {
                            styleDefinition += styleDef.Definition;
                        }
                    }
                }
            }

            return styleDefinition;
        }

        private bool MatchSelectorLevel(string selectorLevel, string elementName, string elementId, string elementClass)
        {
            if (selectorLevel.Length == 0)
            {
                return false;
            }

            var indexOfDot = selectorLevel.IndexOf('.');
            var indexOfPound = selectorLevel.IndexOf('#');

            string selectorClass = null;
            string selectorId = null;
            string selectorTag = null;
            if (indexOfDot >= 0)
            {
                if (indexOfDot > 0)
                {
                    selectorTag = selectorLevel.Substring(0, indexOfDot);
                }

                selectorClass = selectorLevel.Substring(indexOfDot + 1);
            }
            else if (indexOfPound >= 0)
            {
                if (indexOfPound > 0)
                {
                    selectorTag = selectorLevel.Substring(0, indexOfPound);
                }

                selectorId = selectorLevel.Substring(indexOfPound + 1);
            }
            else
            {
                selectorTag = selectorLevel;
            }

            if (selectorTag != null && selectorTag != elementName)
            {
                return false;
            }

            if (selectorId != null && elementId != selectorId)
            {
                return false;
            }

            if (selectorClass != null && elementClass != selectorClass)
            {
                return false;
            }

            return true;
        }

        private class StyleDefinition
        {
            public readonly string Definition;
            public readonly string Selector;

            public StyleDefinition(string selector, string definition)
            {
                Selector = selector;
                Definition = definition;
            }
        }
    }
}