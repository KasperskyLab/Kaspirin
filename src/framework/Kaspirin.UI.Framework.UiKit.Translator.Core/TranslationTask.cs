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
using System.Xml;
using Kaspirin.UI.Framework.UiKit.Translator.Core.Translation;
using Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Fonts;
using Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Icons;
using Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Illustrations;
using Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Palettes;
using Microsoft.Build.Framework;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core
{
#if !NET6_0_OR_GREATER
    /// <remarks>
    /// In case you need to modify this assembly and test it afterwards, it's better to define MSBUILDDISABLENODEREUSE environment variable with value 1.
    /// Otherwise MSBuild will reuse its nodes (i.e. MSBuild.exe processes) that would load this assembly blocking it from any modifications.
    /// </remarks>
    [LoadInSeparateAppDomain]
#endif
    public sealed class TranslationTask : ConfigurableTask
    {
        public TranslationTask()
        {
            _xsltUrlResolver = GetXsltUrlResolver();
            _xmlWriterSettings = GetXmlWriterSettings();
            _lineEndingMode = GetLineEndingMode();
            _excludedControls = new Lazy<string[]>(GetExcludedControls);
            _baseStylesResourceDictionaries = GetBaseStylesResourceDictionaries();
        }

        public override bool Execute()
        {
            try
            {
                if (!CheckArguments() || !ReadConfiguration() || !ValidateConfiguration() || !ValidateSchema())
                {
                    return false;
                }

                var uiKitContent = File.ReadAllText(_configuration.UiKitPath);

                if (!GenerateStyles(uiKitContent) ||
                    !GenerateCustomizationDictionaries(uiKitContent) ||
                    !GeneratePalettes(uiKitContent) ||
                    !GeneratePalettesEnums(uiKitContent) ||
                    !GenerateFontEnums(uiKitContent) ||
                    !GenerateSvg(uiKitContent) ||
                    !GenerateIconEnums(uiKitContent) ||
                    !GenerateIconSvg(uiKitContent) ||
                    !GenerateIllustrationEnums(uiKitContent) ||
                    !GenerateIllustrationMarkupExtensions(uiKitContent) ||
                    !GenerateIllustrationSvg(uiKitContent))
                {
                    return false;
                }

                Log.LogMessage(MessageImportance.High, "UI Kit translation successfully completed.");
            }
            catch (Exception ex)
            {
                Log.LogErrorFromException(ex, showStackTrace: true);
            }

            return !Log.HasLoggedErrors;
        }

        private bool ValidateConfiguration()
        {
            if (string.IsNullOrWhiteSpace(_configuration.GeneratedStylesPath))
            {
                Log.LogError($"Configuration required parameter '{nameof(_configuration.GeneratedStylesPath)}' is not set.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_configuration.GeneratedResourcesDirectory))
            {
                Log.LogError($"Configuration required parameter '{nameof(_configuration.GeneratedResourcesDirectory)}' is not set.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_configuration.PaletteDirectory))
            {
                Log.LogError($"Configuration required parameter '{nameof(_configuration.PaletteDirectory)}' is not set.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_configuration.UiKitPath))
            {
                Log.LogError($"Configuration required parameter '{nameof(_configuration.UiKitPath)}' is not set.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(_configuration.PaletteNamespacePart))
            {
                Log.LogError($"Configuration required parameter '{nameof(_configuration.PaletteNamespacePart)}' is not set.");
                return false;
            }

            return true;
        }

        private bool ValidateSchema()
        {
            var schemaValidator = new SchemaValidator(_configuration.FailOnValidationErrors, Log);
            return schemaValidator.Validate(_configuration.UiKitPath);
        }

        private bool GenerateStyles(string uiKitContent)
        {
            var stylesGenerator = new StylesGenerator(
                _configuration.GeneratedStylesPath,
                _configuration.FileComment,
                _excludedControls.Value,
                _baseStylesResourceDictionaries,
                _lineEndingMode,
                _xsltUrlResolver,
                _xmlWriterSettings,
                Log);

            var result = stylesGenerator.Generate(uiKitContent, out _);

            if (_configuration.FormatXamlFiles)
            {
                XamlStylerToolFacade.FormatXamlFiles(_configuration.GeneratedStylesPath, Log);
            }

            return result;
        }

        private bool GenerateCustomizationDictionaries(string uiKitContent)
        {
            var customizationDictionariesGenerator = new CustomizationDictionariesGenerator(
                _configuration.GeneratedResourcesDirectory,
                _configuration.FileComment,
                _excludedControls.Value,
                _lineEndingMode,
                _xsltUrlResolver,
                _xmlWriterSettings,
                Log);

            var result = customizationDictionariesGenerator.Generate(uiKitContent, out _);

            if (_configuration.FormatXamlFiles)
            {
                XamlStylerToolFacade.FormatXamlFiles(_configuration.GeneratedResourcesDirectory, Log);
            }

            return result;
        }

        private bool GeneratePalettes(string uiKitContent)
        {
            var palettesGenerator = new PalettesGenerator(
                GetDefaultPaletteId(),
                _configuration.PaletteDirectory,
                _configuration.FileComment,
                _excludedControls.Value,
                _lineEndingMode,
                _xsltUrlResolver,
                _xmlWriterSettings,
                Log);

            var result = palettesGenerator.Generate(uiKitContent, out var generatedFilePaths);

            if (_configuration.FormatXamlFiles)
            {
                foreach (var generatedFilePath in generatedFilePaths)
                {
                    XamlStylerToolFacade.FormatXamlFiles(generatedFilePath, Log);
                }
            }

            return result;
        }

        private bool GeneratePalettesEnums(string uiKitContent)
        {
            if (string.IsNullOrWhiteSpace(_configuration.PaletteEnumPath))
            {
                Log.LogWarning(
                    $"Configuration optional parameter '{nameof(_configuration.PaletteEnumPath)}' is not set." +
                    " Palette enums file generation will be skipped.");
                return true;
            }

            var paletteEnumGenerator = new PaletteEnumGenerator(
                _configuration.PaletteEnumPath,
                _configuration.PaletteNamespacePart,
                _configuration.FileComment,
                Log);

            return paletteEnumGenerator.Generate(uiKitContent);
        }

        private bool GenerateFontEnums(string uiKitContent)
        {
            if (string.IsNullOrWhiteSpace(_configuration.FontEnumPath))
            {
                Log.LogWarning(
                    $"Configuration optional parameter '{nameof(_configuration.FontEnumPath)}' is not set." +
                    " Font enums file generation will be skipped.");
                return true;
            }

            var paletteEnumGenerator = new FontEnumGenerator(
                _configuration.FontEnumPath,
                _configuration.FontNamespacePart,
                _configuration.PaletteNamespacePart,
                _configuration.FileComment,
                Log);

            return paletteEnumGenerator.Generate(uiKitContent);
        }

        private bool GenerateSvg(string uiKitContent)
        {
            if (string.IsNullOrWhiteSpace(_configuration.SvgDirectory))
            {
                Log.LogWarning(
                    $"Configuration optional parameter '{nameof(_configuration.SvgDirectory)}' is not set." +
                    " SVG files generation will be skipped.");
                return true;
            }

            var svgGenerator = new SvgGenerator(
                _configuration.SvgDirectory,
                _excludedControls.Value,
                _lineEndingMode,
                _xsltUrlResolver,
                _xmlWriterSettings,
                Log);

            return svgGenerator.Generate(uiKitContent, out _);
        }

        private bool GenerateIconEnums(string uiKitContent)
        {
            if (string.IsNullOrWhiteSpace(_configuration.IconEnumPath))
            {
                Log.LogWarning(
                    $"Configuration optional parameter '{nameof(_configuration.IconEnumPath)}' is not set." +
                    " Icon enums file generation will be skipped.");
                return true;
            }

            var iconEnumGenerator = new IconEnumGenerator(
                _configuration.IconEnumPath,
                _configuration.FileComment, 
                Log);

            return iconEnumGenerator.Generate(uiKitContent);
        }

        private bool GenerateIconSvg(string uiKitContent)
        {
            if (string.IsNullOrWhiteSpace(_configuration.IconSvgDirectory))
            {
                Log.LogWarning(
                    $"Configuration optional parameter '{nameof(_configuration.IconSvgDirectory)}' is not set." +
                    " Icon SVG files generation will be skipped.");
                return true;
            }

            var iconSvgGenerator = new IconSvgGenerator(
                _configuration.IconSvgDirectory,
                _excludedControls.Value,
                _lineEndingMode,
                _xsltUrlResolver,
                _xmlWriterSettings,
                Log);

            return iconSvgGenerator.Generate(uiKitContent, out _);
        }

        private bool GenerateIllustrationEnums(string uiKitContent)
        {
            if (_configuration.ProductMediaProjectInfoMapping is null)
            {
                Log.LogWarning(
                    $"Configuration optional parameter '{nameof(_configuration.ProductMediaProjectInfoMapping)}' is not set." +
                    " Illustration enums file generation will be skipped.");
                return true;
            }

            var illustrationEnumGenerator = new IllustrationEnumGenerator(
                _configuration.GetProductMediaProjectInfo,
                _configuration.FileComment,
                Log);

            return illustrationEnumGenerator.Generate(uiKitContent);
        }

        private bool GenerateIllustrationMarkupExtensions(string uiKitContent)
        {
            if (_configuration.ProductMediaProjectInfoMapping is null)
            {
                Log.LogWarning(
                    $"Configuration optional parameter '{nameof(_configuration.ProductMediaProjectInfoMapping)}' is not set." +
                    " Illustration markup extension files generation will be skipped.");
                return true;
            }

            var illustrationMarkupExtensionGenerator = new IllustrationMarkupExtensionGenerator(
                _configuration.GetProductMediaProjectInfo,
                _configuration.FileComment,
                Log);

            return illustrationMarkupExtensionGenerator.Generate(uiKitContent);
        }

        private bool GenerateIllustrationSvg(string uiKitContent)
        {
            if (string.IsNullOrWhiteSpace(_configuration.IconSvgDirectory))
            {
                Log.LogWarning(
                    $"Configuration optional parameter '{nameof(_configuration.ProductMediaProjectInfoMapping)}' is not set." +
                    " Illustration SVG files generation will be skipped.");
                return true;
            }

            var illustrationSvgGenerator = new IllustrationSvgGenerator(
                _configuration.GetProductMediaProjectInfo,
                _excludedControls.Value,
                _lineEndingMode,
                _xsltUrlResolver,
                _xmlWriterSettings,
                Log);

            return illustrationSvgGenerator.Generate(uiKitContent, out _);
        }

        private EmbeddedResourceXmlUrlResolver GetXsltUrlResolver()
            => new(Const.TransformationResourcesDirectory, Log);

        private XmlWriterSettings GetXmlWriterSettings()
            => new()
            {
                IndentChars = Const.DefaultOutputIndentChars,
                Indent = true,
                OmitXmlDeclaration = true
            };

        private string[] GetExcludedControls()
        {
            Log.LogMessage(
                MessageImportance.High,
                _configuration.ExcludedControls?.Any() == true
                    ? $"Configuration optional parameter '{nameof(_configuration.ExcludedControls)}' is set: {string.Join(", ", _configuration.ExcludedControls)}."
                    : $"Configuration optional parameter '{nameof(_configuration.ExcludedControls)}' is not set.");

            return _configuration.ExcludedControls;
        }

        private LineEndingMode GetLineEndingMode()
            => Const.DefaultLineEndingMode;

        private string GetDefaultPaletteId()
            => Const.DefaultPaletteId;

        private string[] GetBaseStylesResourceDictionaries()
            => new[] { Const.TemplateDictionaryName };

        private readonly EmbeddedResourceXmlUrlResolver _xsltUrlResolver;
        private readonly XmlWriterSettings _xmlWriterSettings;
        private readonly LineEndingMode _lineEndingMode;
        private readonly Lazy<string[]> _excludedControls;
        private readonly string[] _baseStylesResourceDictionaries;
    }
}