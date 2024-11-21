// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 1/22/2024.
// Scope of modification:
//   - Code adaptation to project requirements.

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#nullable disable

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Media;
using System.Xml;

namespace Kaspirin.UI.Framework.UiKit.ThirdParty.HtmlUtils
{
    // DependencyProperty

    // TextElement

    internal static class HtmlCssParser
    {
        private static readonly string[] Colors =
        {
            "aliceblue", "antiquewhite", "aqua", "aquamarine", "azure", "beige", "bisque", "black", "blanchedalmond",
            "blue", "blueviolet", "brown", "burlywood", "cadetblue", "chartreuse", "chocolate", "coral",
            "cornflowerblue", "cornsilk", "crimson", "cyan", "darkblue", "darkcyan", "darkgoldenrod", "darkgray",
            "darkgreen", "darkkhaki", "darkmagenta", "darkolivegreen", "darkorange", "darkorchid", "darkred",
            "darksalmon", "darkseagreen", "darkslateblue", "darkslategray", "darkturquoise", "darkviolet", "deeppink",
            "deepskyblue", "dimgray", "dodgerblue", "firebrick", "floralwhite", "forestgreen", "fuchsia", "gainsboro",
            "ghostwhite", "gold", "goldenrod", "gray", "green", "greenyellow", "honeydew", "hotpink", "indianred",
            "indigo", "ivory", "khaki", "lavender", "lavenderblush", "lawngreen", "lemonchiffon", "lightblue",
            "lightcoral",
            "lightcyan", "lightgoldenrodyellow", "lightgreen", "lightgrey", "lightpink", "lightsalmon", "lightseagreen",
            "lightskyblue", "lightslategray", "lightsteelblue", "lightyellow", "lime", "limegreen", "linen", "magenta",
            "maroon", "mediumaquamarine", "mediumblue", "mediumorchid", "mediumpurple", "mediumseagreen",
            "mediumslateblue",
            "mediumspringgreen", "mediumturquoise", "mediumvioletred", "midnightblue", "mintcream", "mistyrose",
            "moccasin",
            "navajowhite", "navy", "oldlace", "olive", "olivedrab", "orange", "orangered", "orchid", "palegoldenrod",
            "palegreen", "paleturquoise", "palevioletred", "papayawhip", "peachpuff", "peru", "pink", "plum",
            "powderblue",
            "purple", "red", "rosybrown", "royalblue", "saddlebrown", "salmon", "sandybrown", "seagreen", "seashell",
            "sienna", "silver", "skyblue", "slateblue", "slategray", "snow", "springgreen", "steelblue", "tan", "teal",
            "thistle", "tomato", "turquoise", "violet", "wheat", "white", "whitesmoke", "yellow", "yellowgreen"
        };

        private static readonly string[] SystemColors =
        {
            "activeborder", "activecaption", "appworkspace", "background", "buttonface", "buttonhighlight",
            "buttonshadow",
            "buttontext", "captiontext", "graytext", "highlight", "highlighttext", "inactiveborder", "inactivecaption",
            "inactivecaptiontext", "infobackground", "infotext", "menu", "menutext", "scrollbar", "threeddarkshadow",
            "threedface", "threedhighlight", "threedlightshadow", "threedshadow", "window", "windowframe", "windowtext"
        };

        // .................................................................
        //
        // Pasring CSS font Property
        //
        // .................................................................

        // CSS has five font properties: font-family, font-style, font-variant, font-weight, font-size.
        // An aggregated "font" property lets you specify in one action all the five in combination
        // with additional line-height property.
        // 
        // font-family: [<family-name>,]* [<family-name> | <generic-family>]
        //    generic-family: serif | sans-serif | monospace | cursive | fantasy
        //       The list of families sets priorities to choose fonts;
        //       Quotes not allowed around generic-family names
        // font-style: normal | italic | oblique
        // font-variant: normal | small-caps
        // font-weight: normal | bold | bolder | lighter | 100 ... 900 |
        //    Default is "normal", normal==400
        // font-size: <absolute-size> | <relative-size> | <length> | <percentage>
        //    absolute-size: xx-small | x-small | small | medium | large | x-large | xx-large
        //    relative-size: larger | smaller
        //    length: <point> | <pica> | <ex> | <em> | <points> | <millimeters> | <centimeters> | <inches>
        //    Default: medium
        // font: [ <font-style> || <font-variant> || <font-weight ]? <font-size> [ / <line-height> ]? <font-family>

        private static readonly string[] FontGenericFamilies =
        {
            "serif", "sans-serif", "monospace", "cursive", "fantasy"
        };

        private static readonly string[] FontStyles = { "normal", "italic", "oblique" };
        private static readonly string[] FontVariants = { "normal", "small-caps" };

        private static readonly string[] FontWeights =
        {
            "normal", "bold", "bolder", "lighter", "100", "200", "300",
            "400", "500", "600", "700", "800", "900"
        };

        private static readonly string[] FontAbsoluteSizes =
        {
            "xx-small", "x-small", "small", "medium", "large",
            "x-large", "xx-large"
        };

        private static readonly string[] FontRelativeSizes = { "larger", "smaller" };

        private static readonly string[] FontSizeUnits = { "px", "mm", "cm", "in", "pt", "pc", "em", "ex", "%" };

        // .................................................................
        //
        // Pasring CSS list-style Property
        //
        // .................................................................

        // list-style: [ <list-style-type> || <list-style-position> || <list-style-image> ]

        private static readonly string[] ListStyleTypes =
        {
            "disc", "circle", "square", "decimal", "lower-roman",
            "upper-roman", "lower-alpha", "upper-alpha", "none"
        };

        private static readonly string[] ListStylePositions = { "inside", "outside" };
        private static readonly string[] ListStyleImage = { "none", "inherit" };
        // .................................................................
        //
        // Pasring CSS text-decorations Property
        //
        // .................................................................

        private static readonly string[] TextDecorations = { "none", "underline", "overline", "line-through", "blink" };

        // .................................................................
        //
        // Pasring CSS text-transform Property
        //
        // .................................................................

        private static readonly string[] TextTransforms = { "none", "capitalize", "uppercase", "lowercase" };
        // .................................................................
        //
        // Pasring CSS text-align Property
        //
        // .................................................................

        private static readonly string[] TextAligns = { "left", "right", "center", "justify" };
        // .................................................................
        //
        // Pasring CSS vertical-align Property
        //
        // .................................................................

        private static readonly string[] VerticalAligns =
        {
            "baseline", "sub", "super", "top", "text-top", "middle",
            "bottom", "text-bottom"
        };

        // .................................................................
        //
        // Pasring CSS float Property
        //
        // .................................................................

        private static readonly string[] Floats = { "left", "right", "none" };
        // .................................................................
        //
        // Pasring CSS clear Property
        //
        // .................................................................

        private static readonly string[] Clears = { "none", "left", "right", "both" };
        // .................................................................
        //
        // Pasring CSS border-style Propertie
        //
        // .................................................................

        private static readonly string[] BorderStyles =
        {
            "none", "dotted", "dashed", "solid", "double", "groove",
            "ridge", "inset", "outset"
        };

        // .................................................................
        //
        //  What are these definitions doing here:
        //
        // .................................................................

        private static string[] _blocks = { "block", "inline", "list-item", "none" };

        private static readonly string[] ObjectFits = { "none", "contain", "cover", "fill", "scale-down" };

        // .................................................................
        //
        // Processing CSS Attributes
        //
        // .................................................................

        internal static void GetElementPropertiesFromCssAttributes(XmlElement htmlElement, string elementName,
            CssStylesheet stylesheet, Hashtable localProperties, List<XmlElement> sourceContext)
        {
            var styleFromStylesheet = stylesheet.GetStyle(elementName, sourceContext);

            var styleInline = HtmlToXamlConverter.GetAttribute(htmlElement, "style");

            // Combine styles from stylesheet and from inline attribute.
            // The order is important - the latter styles will override the former.
            var style = styleFromStylesheet ?? null;
            if (styleInline != null)
            {
                style = style == null ? styleInline : (style + ";" + styleInline);
            }

            // Apply local style to current formatting properties
            if (style != null)
            {
                var styleValues = style.Split(';');
                foreach (var t in styleValues)
                {
                    string[] styleNameValue;

                    styleNameValue = t.Split(':');
                    if (styleNameValue.Length == 2)
                    {
                        var styleName = styleNameValue[0].Trim().ToLowerInvariant();
                        var styleValue = HtmlToXamlConverter.UnQuote(styleNameValue[1]).Trim().ToLowerInvariant();
                        var nextIndex = 0;

                        switch (styleName)
                        {
                            case "font":
                                ParseCssFont(styleValue, localProperties);
                                break;
                            case "font-family":
                                ParseCssFontFamily(styleValue, ref nextIndex, localProperties);
                                break;
                            case "font-size":
                                ParseCssSize(styleValue, ref nextIndex, localProperties, "font-size",
                                    /*mustBeNonNegative:*/true);
                                break;
                            case "font-style":
                                ParseCssFontStyle(styleValue, ref nextIndex, localProperties);
                                break;
                            case "font-weight":
                                ParseCssFontWeight(styleValue, ref nextIndex, localProperties);
                                break;
                            case "font-variant":
                                ParseCssFontVariant(styleValue, ref nextIndex, localProperties);
                                break;
                            case "line-height":
                                ParseCssSize(styleValue, ref nextIndex, localProperties, "line-height",
                                    /*mustBeNonNegative:*/true);
                                break;
                            case "word-spacing":
                                //  Implement word-spacing conversion
                                break;
                            case "letter-spacing":
                                //  Implement letter-spacing conversion
                                break;
                            case "color":
                                ParseCssColor(styleValue, ref nextIndex, localProperties, "color");
                                break;

                            case "text-decoration":
                                ParseCssTextDecoration(styleValue, ref nextIndex, localProperties);
                                break;

                            case "text-transform":
                                ParseCssTextTransform(styleValue, ref nextIndex, localProperties);
                                break;

                            case "background-color":
                                ParseCssColor(styleValue, ref nextIndex, localProperties, "background-color");
                                break;
                            case "background":
                                ParseCssBackground(styleValue, ref nextIndex, localProperties);
                                break;

                            case "text-align":
                                ParseCssTextAlign(styleValue, ref nextIndex, localProperties);
                                break;
                            case "vertical-align":
                                ParseCssVerticalAlign(styleValue, ref nextIndex, localProperties);
                                break;
                            case "text-indent":
                                ParseCssSize(styleValue, ref nextIndex, localProperties, "text-indent",
                                    /*mustBeNonNegative:*/false);
                                break;

                            case "width":
                            case "height":
                                ParseCssSize(styleValue, ref nextIndex, localProperties, styleName,
                                    /*mustBeNonNegative:*/true);
                                break;

                            case "margin": // top/right/bottom/left
                                ParseCssRectangleProperty(styleValue, ref nextIndex, localProperties, styleName);
                                break;
                            case "margin-top":
                            case "margin-right":
                            case "margin-bottom":
                            case "margin-left":
                                ParseCssSize(styleValue, ref nextIndex, localProperties, styleName,
                                    /*mustBeNonNegative:*/true);
                                break;

                            case "padding":
                                ParseCssRectangleProperty(styleValue, ref nextIndex, localProperties, styleName);
                                break;
                            case "padding-top":
                            case "padding-right":
                            case "padding-bottom":
                            case "padding-left":
                                ParseCssSize(styleValue, ref nextIndex, localProperties, styleName,
                                    /*mustBeNonNegative:*/true);
                                break;

                            case "border":
                                ParseCssBorder(styleValue, ref nextIndex, localProperties);
                                break;
                            case "border-style":
                            case "border-width":
                            case "border-color":
                                ParseCssRectangleProperty(styleValue, ref nextIndex, localProperties, styleName);
                                break;
                            case "border-top":
                            case "border-right":
                            case "border-left":
                            case "border-bottom":
                                //  Parse css border style
                                break;

                            // NOTE: css names for elementary border styles have side indications in the middle (top/bottom/left/right)
                            // In our internal notation we intentionally put them at the end - to unify processing in ParseCssRectangleProperty method
                            case "border-top-style":
                            case "border-right-style":
                            case "border-left-style":
                            case "border-bottom-style":
                            case "border-top-color":
                            case "border-right-color":
                            case "border-left-color":
                            case "border-bottom-color":
                            case "border-top-width":
                            case "border-right-width":
                            case "border-left-width":
                            case "border-bottom-width":
                                //  Parse css border style
                                break;

                            case "display":
                                //  Implement display style conversion
                                break;

                            case "float":
                                ParseCssFloat(styleValue, ref nextIndex, localProperties);
                                break;
                            case "clear":
                                ParseCssClear(styleValue, ref nextIndex, localProperties);
                                break;

                            case "object-fit":
                                ParseObjectFit(styleValue, ref nextIndex, localProperties);
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
        }

        // .................................................................
        //
        // Parsing CSS - Lexical Helpers
        //
        // .................................................................

        // Skips whitespaces in style values
        private static void ParseWhiteSpace(string styleValue, ref int nextIndex)
        {
            while (nextIndex < styleValue.Length && char.IsWhiteSpace(styleValue[nextIndex]))
            {
                nextIndex++;
            }
        }

        // Checks if the following character matches to a given word and advances nextIndex
        // by the word's length in case of success.
        // Otherwise leaves nextIndex in place (except for possible whitespaces).
        // Returns true or false depending on success or failure of matching.
        private static bool ParseWord(string word, string styleValue, ref int nextIndex)
        {
            ParseWhiteSpace(styleValue, ref nextIndex);

            for (var i = 0; i < word.Length; i++)
            {
                if (!(nextIndex + i < styleValue.Length && word[i] == styleValue[nextIndex + i]))
                {
                    return false;
                }
            }

            if (nextIndex + word.Length < styleValue.Length && char.IsLetterOrDigit(styleValue[nextIndex + word.Length]))
            {
                return false;
            }

            nextIndex += word.Length;
            return true;
        }

        // CHecks whether the following character sequence matches to one of the given words,
        // and advances the nextIndex to matched word length.
        // Returns null in case if there is no match or the word matched.
        private static string ParseWordEnumeration(string[] words, string styleValue, ref int nextIndex)
        {
            foreach (var t in words)
            {
                if (ParseWord(t, styleValue, ref nextIndex))
                {
                    return t;
                }
            }

            return null;
        }

        private static void ParseWordEnumeration(string[] words, string styleValue, ref int nextIndex,
            Hashtable localProperties, string attributeName)
        {
            var attributeValue = ParseWordEnumeration(words, styleValue, ref nextIndex);
            if (attributeValue != null)
            {
                localProperties[attributeName] = attributeValue;
            }
        }

        private static string ParseCssSize(string styleValue, ref int nextIndex, bool mustBeNonNegative)
        {
            ParseWhiteSpace(styleValue, ref nextIndex);
            var absoluteSize = ParseWordEnumeration(FontAbsoluteSizes, styleValue, ref nextIndex);
            {
                if (nextIndex == styleValue.Length)
                {
                    return absoluteSize;
                }
            }

            var relativeSize = ParseWordEnumeration(FontRelativeSizes, styleValue, ref nextIndex);
            if (nextIndex == styleValue.Length)
            {
                return relativeSize;
            }

            var startIndex = nextIndex;

            // Parse optional munis sign
            if (nextIndex < styleValue.Length && styleValue[nextIndex] == '-')
            {
                nextIndex++;
            }

            if (nextIndex < styleValue.Length && char.IsDigit(styleValue[nextIndex]))
            {
                while (nextIndex < styleValue.Length &&
                       (char.IsDigit(styleValue[nextIndex]) || styleValue[nextIndex] == '.'))
                {
                    nextIndex++;
                }

                var number = styleValue.Substring(startIndex, nextIndex - startIndex);

                var unit = ParseWordEnumeration(FontSizeUnits, styleValue, ref nextIndex) ?? "px";

                if (mustBeNonNegative && styleValue[startIndex] == '-')
                {
                    return "0";
                }

                if (unit == "pt")
                {
                    number = ConvertFromPointsToPixels(number).ToString(CultureInfo.InvariantCulture);
                    unit = "px";
                }

                return number + unit;
            }

            return null;
        }

        private static void ParseCssSize(string styleValue, ref int nextIndex, Hashtable localValues,
            string propertyName, bool mustBeNonNegative)
        {
            var length = ParseCssSize(styleValue, ref nextIndex, mustBeNonNegative);
            if (length != null)
            {
                localValues[propertyName] = length;
            }
        }

        private static double ConvertFromPointsToPixels(string stringValue)
        {
            if (double.TryParse(stringValue, out var value))
            {
                return ConvertFromPointsToPixels(value);
            }
            else
            {
                return double.NaN;
            }
        }

        private static double ConvertFromPointsToPixels(double value)
        {
            return value * 96.0 / 72.0;
        }

        private static string ParseCssColor(string styleValue, ref int nextIndex)
        {
            //  Implement color parsing
            // rgb(100%,53.5%,10%)
            // rgb(255,91,26)
            // #FF5B1A
            // black | silver | gray | ... | aqua
            // transparent - for background-color
            ParseWhiteSpace(styleValue, ref nextIndex);

            string color = null;

            if (nextIndex < styleValue.Length)
            {
                var startIndex = nextIndex;
                var character = styleValue[nextIndex];

                if (character == '#')
                {
                    nextIndex++;
                    while (nextIndex < styleValue.Length)
                    {
                        character = char.ToUpper(styleValue[nextIndex]);
                        if (!('0' <= character && character <= '9' || 'A' <= character && character <= 'F'))
                        {
                            break;
                        }

                        nextIndex++;
                    }

                    if (nextIndex > startIndex + 1)
                    {
                        color = styleValue.Substring(startIndex, nextIndex - startIndex);
                    }
                }
                else if (styleValue.Substring(nextIndex, 3).ToLowerInvariant() == "rgb")
                {
                    while (nextIndex < styleValue.Length && styleValue[nextIndex] != '(')
                    {
                        nextIndex++;
                    }

                    nextIndex++; // to skip '('

                    var colorSubstr = new string(styleValue.Skip(nextIndex).TakeWhile(x => x != ')').ToArray());
                    if (colorSubstr.Length > 0)
                    {
                        static void SetColorChannel(string value, Action<byte> setColorProperty, Action<float> setColorScProperty)
                        {
                            if (value.EndsWith("%"))
                            {
                                setColorScProperty(float.Parse(value.Trim('%'), CultureInfo.InvariantCulture) / 100);
                            }
                            else
                            {
                                setColorProperty(byte.Parse(value));
                            }
                        }

                        var colorStruct = new Color() { ScA = 1 };
                        var colorParts = colorSubstr.Split(',');
                        if (colorParts.Length >= 3)
                        {
                            SetColorChannel(colorParts[0].Trim(), (value) => colorStruct.R = value, (value) => colorStruct.ScR = value);
                            SetColorChannel(colorParts[1].Trim(), (value) => colorStruct.G = value, (value) => colorStruct.ScG = value);
                            SetColorChannel(colorParts[2].Trim(), (value) => colorStruct.B = value, (value) => colorStruct.ScB = value);
                        }

                        if (colorParts.Length == 4)
                        {
                            SetColorChannel(colorParts[3].Trim(), (value) => colorStruct.A = value, (value) => colorStruct.ScA = value);
                        }

                        color = colorStruct.ToString();
                        nextIndex += colorSubstr.Length;
                    }

                    if (nextIndex < styleValue.Length)
                    {
                        nextIndex++; // to skip ')'
                    }
                }
                else if (char.IsLetter(character))
                {
                    color = ParseWordEnumeration(Colors, styleValue, ref nextIndex);
                    if (color == null)
                    {
                        color = ParseWordEnumeration(SystemColors, styleValue, ref nextIndex);
                        if (color != null)
                        {
                            //  Implement smarter system color converions into real colors
                            color = "black";
                        }
                    }
                }
            }

            return color;
        }

        private static void ParseCssColor(string styleValue, ref int nextIndex, Hashtable localValues,
            string propertyName)
        {
            var color = ParseCssColor(styleValue, ref nextIndex);
            if (color != null)
            {
                localValues[propertyName] = color;
            }
        }

        // Parses CSS string fontStyle representing a value for css font attribute
        private static void ParseCssFont(string styleValue, Hashtable localProperties)
        {
            var nextIndex = 0;

            ParseCssFontStyle(styleValue, ref nextIndex, localProperties);
            ParseCssFontVariant(styleValue, ref nextIndex, localProperties);
            ParseCssFontWeight(styleValue, ref nextIndex, localProperties);

            ParseCssSize(styleValue, ref nextIndex, localProperties, "font-size", /*mustBeNonNegative:*/true);

            ParseWhiteSpace(styleValue, ref nextIndex);
            if (nextIndex < styleValue.Length && styleValue[nextIndex] == '/')
            {
                nextIndex++;
                ParseCssSize(styleValue, ref nextIndex, localProperties, "line-height", /*mustBeNonNegative:*/true);
            }

            ParseCssFontFamily(styleValue, ref nextIndex, localProperties);
        }

        private static void ParseCssFontStyle(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            ParseWordEnumeration(FontStyles, styleValue, ref nextIndex, localProperties, "font-style");
        }

        private static void ParseCssFontVariant(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            ParseWordEnumeration(FontVariants, styleValue, ref nextIndex, localProperties, "font-variant");
        }

        private static void ParseCssFontWeight(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            ParseWordEnumeration(FontWeights, styleValue, ref nextIndex, localProperties, "font-weight");
        }

        private static void ParseCssFontFamily(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            string fontFamilyList = null;

            while (nextIndex < styleValue.Length)
            {
                // Try generic-family
                var fontFamily = ParseWordEnumeration(FontGenericFamilies, styleValue, ref nextIndex);

                if (fontFamily == null)
                {
                    // Try quoted font family name
                    if (nextIndex < styleValue.Length && (styleValue[nextIndex] == '"' || styleValue[nextIndex] == '\''))
                    {
                        var quote = styleValue[nextIndex];

                        nextIndex++;

                        var startIndex = nextIndex;

                        while (nextIndex < styleValue.Length && styleValue[nextIndex] != quote)
                        {
                            nextIndex++;
                        }

                        fontFamily = '"' + styleValue.Substring(startIndex, nextIndex - startIndex) + '"';
                    }

                    if (fontFamily == null)
                    {
                        // Try unquoted font family name
                        var startIndex = nextIndex;
                        while (nextIndex < styleValue.Length && styleValue[nextIndex] != ',' &&
                               styleValue[nextIndex] != ';')
                        {
                            nextIndex++;
                        }

                        if (nextIndex > startIndex)
                        {
                            fontFamily = styleValue.Substring(startIndex, nextIndex - startIndex).Trim();
                            if (fontFamily.Length == 0)
                            {
                                fontFamily = null;
                            }
                        }
                    }
                }

                ParseWhiteSpace(styleValue, ref nextIndex);
                if (nextIndex < styleValue.Length && styleValue[nextIndex] == ',')
                {
                    nextIndex++;
                }

                if (fontFamily != null)
                {
                    //  css font-family can contein a list of names. We only consider the first name from the list. Need a decision what to do with remaining names
                    // fontFamilyList = (fontFamilyList == null) ? fontFamily : fontFamilyList + "," + fontFamily;
                    if (fontFamilyList == null && fontFamily.Length > 0)
                    {
                        fontFamily = HtmlToXamlConverter.UnQuote(fontFamily);
                        fontFamilyList = fontFamily;
                    }
                }
                else
                {
                    break;
                }
            }

            if (fontFamilyList != null)
            {
                localProperties["font-family"] = fontFamilyList;
            }
        }

        private static void ParseCssListStyle(string styleValue, Hashtable localProperties)
        {
            var nextIndex = 0;

            while (nextIndex < styleValue.Length)
            {
                var listStyleType = ParseCssListStyleType(styleValue, ref nextIndex);
                if (listStyleType != null)
                {
                    localProperties["list-style-type"] = listStyleType;
                }
                else
                {
                    var listStylePosition = ParseCssListStylePosition(styleValue, ref nextIndex);
                    if (listStylePosition != null)
                    {
                        localProperties["list-style-position"] = listStylePosition;
                    }
                    else
                    {
                        var listStyleImage = ParseCssListStyleImage(styleValue, ref nextIndex);
                        if (listStyleImage != null)
                        {
                            localProperties["list-style-image"] = listStyleImage;
                        }
                        else
                        {
                            // TODO: Process unrecognized list style value
                            break;
                        }
                    }
                }
            }
        }

        private static string ParseCssListStyleType(string styleValue, ref int nextIndex) => ParseWordEnumeration(ListStyleTypes, styleValue, ref nextIndex);

        private static string ParseCssListStylePosition(string styleValue, ref int nextIndex) => ParseWordEnumeration(ListStylePositions, styleValue, ref nextIndex);

        private static string ParseCssListStyleImage(string styleValue, ref int nextIndex) => ParseWordEnumeration(ListStyleImage, styleValue, ref nextIndex);

        private static void ParseCssTextDecoration(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            // Set default text-decorations:none;
            for (var i = 1; i < TextDecorations.Length; i++)
            {
                localProperties["text-decoration-" + TextDecorations[i]] = "false";
            }

            // Parse list of decorations values
            while (nextIndex < styleValue.Length)
            {
                var decoration = ParseWordEnumeration(TextDecorations, styleValue, ref nextIndex);
                if (decoration == null || decoration == "none")
                {
                    break;
                }

                localProperties["text-decoration-" + decoration] = "true";
            }
        }

        private static void ParseCssTextTransform(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            ParseWordEnumeration(TextTransforms, styleValue, ref nextIndex, localProperties, "text-transform");
        }

        private static void ParseCssTextAlign(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            ParseWordEnumeration(TextAligns, styleValue, ref nextIndex, localProperties, "text-align");
        }

        private static void ParseCssVerticalAlign(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            //  Parse percentage value for vertical-align style
            ParseWordEnumeration(VerticalAligns, styleValue, ref nextIndex, localProperties, "vertical-align");
        }

        private static void ParseCssFloat(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            ParseWordEnumeration(Floats, styleValue, ref nextIndex, localProperties, "float");
        }

        private static void ParseCssClear(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            ParseWordEnumeration(Clears, styleValue, ref nextIndex, localProperties, "clear");
        }

        private static void ParseObjectFit(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            ParseWordEnumeration(ObjectFits, styleValue, ref nextIndex, localProperties, "object-fit");
        }
        // .................................................................
        //
        // Pasring CSS margin and padding Properties
        //
        // .................................................................

        // Generic method for parsing any of four-values properties, such as margin, padding, border-width, border-style, border-color
        private static bool ParseCssRectangleProperty(string styleValue, ref int nextIndex, Hashtable localProperties,
            string propertyName)
        {
            // CSS Spec: 
            // If only one value is set, then the value applies to all four sides;
            // If two or three values are set, then missinng value(s) are taken fromm the opposite side(s).
            // The order they are applied is: top/right/bottom/left

            Guard.Assert(propertyName == "margin" || propertyName == "padding" || propertyName == "border-width" ||
                         propertyName == "border-style" || propertyName == "border-color");

            var value = propertyName == "border-color"
                ? ParseCssColor(styleValue, ref nextIndex)
                : propertyName == "border-style"
                    ? ParseCssBorderStyle(styleValue, ref nextIndex)
                    : ParseCssSize(styleValue, ref nextIndex, /*mustBeNonNegative:*/true);
            if (value != null)
            {
                localProperties[propertyName + "-top"] = value;
                localProperties[propertyName + "-bottom"] = value;
                localProperties[propertyName + "-right"] = value;
                localProperties[propertyName + "-left"] = value;
                value = propertyName == "border-color"
                    ? ParseCssColor(styleValue, ref nextIndex)
                    : propertyName == "border-style"
                        ? ParseCssBorderStyle(styleValue, ref nextIndex)
                        : ParseCssSize(styleValue, ref nextIndex, /*mustBeNonNegative:*/true);
                if (value != null)
                {
                    localProperties[propertyName + "-right"] = value;
                    localProperties[propertyName + "-left"] = value;
                    value = propertyName == "border-color"
                        ? ParseCssColor(styleValue, ref nextIndex)
                        : propertyName == "border-style"
                            ? ParseCssBorderStyle(styleValue, ref nextIndex)
                            : ParseCssSize(styleValue, ref nextIndex, /*mustBeNonNegative:*/true);
                    if (value != null)
                    {
                        localProperties[propertyName + "-bottom"] = value;
                        value = propertyName == "border-color"
                            ? ParseCssColor(styleValue, ref nextIndex)
                            : propertyName == "border-style"
                                ? ParseCssBorderStyle(styleValue, ref nextIndex)
                                : ParseCssSize(styleValue, ref nextIndex, /*mustBeNonNegative:*/true);
                        if (value != null)
                        {
                            localProperties[propertyName + "-left"] = value;
                        }
                    }
                }

                return true;
            }

            return false;
        }

        // .................................................................
        //
        // Pasring CSS border Properties
        //
        // .................................................................

        // border: [ <border-width> || <border-style> || <border-color> ]

        private static void ParseCssBorder(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            while (
                ParseCssRectangleProperty(styleValue, ref nextIndex, localProperties, "border-width") ||
                ParseCssRectangleProperty(styleValue, ref nextIndex, localProperties, "border-style") ||
                ParseCssRectangleProperty(styleValue, ref nextIndex, localProperties, "border-color"))
            {
            }
        }

        private static string ParseCssBorderStyle(string styleValue, ref int nextIndex) => ParseWordEnumeration(BorderStyles, styleValue, ref nextIndex);

        // .................................................................
        //
        // Pasring CSS Background Properties
        //
        // .................................................................

        private static void ParseCssBackground(string styleValue, ref int nextIndex, Hashtable localProperties)
        {
            //  Implement parsing background attribute
            // TODO: need to parse composite background property
            ParseCssColor(styleValue, ref nextIndex, localProperties, "background-color");
        }
    }
}