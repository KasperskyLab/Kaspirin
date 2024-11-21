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

namespace Kaspirin.UI.Framework.UiKit.Translator.Core
{
    internal static class Const
    {
        public const LineEndingMode DefaultLineEndingMode = LineEndingMode.Lf;
        public const string DefaultOutputIndentChars = "    ";
        public const string SvgExtension = ".svg";
        public const string XamlExtension = ".xaml";
        public const string UIKitPrefix = "UIKit";
        public const string UIKitIllustrationPrefix = "UIKitIllustration_";

        public const string RootElementName = "UiKit";

        public const string UIKitFontEnumsTemplateFilename = "UIKitFonts.Enums.Template.cs";
        public const string UIKitFontTemplateDirectory = "Translation\\Fonts";

        public const string UIKitIconEnumsTemplateFilename = "UIKitIcons.Enums.Template.cs";
        public const string UIKitIconTemplateDirectory = "Translation\\Icons";

        public const string UIKitIllustrationEnumsTemplateFilename = "UIKitIllustrations.Enums.Template.cs";
        public const string UIKitIllustrationMarkupExtensionTemplateFilename = "UIKitIllustrations.MarkupExtension.Template.cs";
        public const string UIKitIllustrationTemplateDirectory = "Translation\\Illustrations";

        public const string UIKitPaletteTextIconsBrushPrefix = "TextIconsElements";
        public const string UIKitPaletteTemplateFilename = "UIKitPalettes.Template.cs";
        public const string UIKitPaletteTemplateDirectory = "Translation\\Palettes";

        public const string SchemaResourcesDirectory = "Schema";
        public const string UiKitSchemaFilename = "UiKit.xsd";
        public const string SchemasDirectoryEmbeddedConfigurationKey = "SchemasDirectory";
        public const string RootNamespaceEmbeddedConfigurationKey = "RootNamespace";

        public const string TransformationResourcesDirectory = "Transformations";
        public const string TransformationCoreDirectory = "Core";
        public const string StylesTransformationFilename = "Styles.xslt";
        public const string CustomizationDictionariesTransformationFilename = "CustomizationDictionaries.xslt";
        public const string PalettesTransformationFilename = "Palettes.xslt";
        public const string SvgTransformationFilename = "Svg.xslt";

        public const string CustomizationDictionariesElementName = "CustomizationDictionaries";
        public const string CustomizationDictionaryElementName = "CustomizationDictionary";
        public const string CustomizationDictionaryIdAttributeName = "Id";

        public const string PalettesElementName = "Palettes";
        public const string PaletteElementName = "Palette";
        public const string PaletteIdAttributeName = "Id";
        public const string BrushesElementName = "Brushes";
        public const string BrushElementName = "PaletteBrush";
        public const string BrushIdAttributeName = "Id";

        public const string DefaultPaletteId = "Light";
        public const string PaletteFilename = "Palette";
        public const string TemplateDictionaryName = "Templates.xaml";

        public const string TextStylesElementName = "TextStyles";
        public const string TextStyleElementName = "TextStyle";
        public const string TextStyleIdAttributeName = "Id";
        public const string TextStyleIdPrefix = "UiKitTextStyle";

        public const string SvgFilesElementName = "SvgFiles";
        public const string SvgFileElementName = "SvgFile";
        public const string SvgFileFilenameAttributeName = "Filename";

        public const string ControlsElementName = "Controls";

        public const string SvgIconsElementName = "SvgIcons";
        public const string SvgIconElementName = "SvgIcon";
        public const string SvgIconIdAttributeName = "Id";
        public const string SvgIconIsAutoRTLAttributeName = "IsAutoRTL";
        public const string SvgIconIsColorfullAttributeName = "IsColorfull";

        public const string IllustrationsElementName = "Illustrations";
        public const string IllustrationElementName = "Illustration";
        public const string IllustrationIdAttributeName = "Id";
        public const string IllustrationHeightAttributeName = "Height";
        public const string IllustrationIsAutoRTLAttributeName = "IsAutoRTL";
        public const string IllustrationNameAttributeName = "Name";
        public const string IllustrationProductAttributeName = "Product";
        public const string IllustrationScopeAttributeName = "Scope";
        public const string IllustrationWidthAttributeName = "Width";

        public const string VectorsElementName = "Vectors";
        public const string VectorElementName = "Vector";
        public const string VectorIsRTLAttributeName = "IsRTL";
        public const string VectorThemeAttributeName = "Theme";
        public const string VectorDataElementName = "Data";
        public const string SvgElementName = "Svg";

        public const string ExcludedControlsFilterParameterName = "excludedControlsFilter";
        public const string ExcludedControlsFilterDelimiterParameterName = "excludedControlsFilterDelimiter";
        public const string ExcludedControlsFilterDelimiter = ";";

        public const string ExternalResourceDictionariesParameterName = "externalResourceDictionaries";
        public const string ExternalResourceDictionariesDelimiterParameterName = "externalResourceDictionariesDelimiter";
        public const string ExternalResourceDictionariesDelimiter = "|";

        public const string ConditionalNamespacePrefixParameterName = "conditionalNamespacePrefix";

        public const string CommentTextParameterName = "commentText";

        public static readonly string[] NeutralLocales = new[] { "neutral" };
        public static readonly string[] RtlLocales = new[] { "ar" };
    }
}