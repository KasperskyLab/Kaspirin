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
using Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Icons;
using Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Illustrations;
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
    public sealed class CleanupTask : ConfigurableTask
    {
        public override bool Execute()
        {
            try
            {
                if (!CheckArguments() || !ReadConfiguration())
                {
                    return false;
                }

                CleanupStyles();
                CleanupCustomizationDictionaries();
                CleanupPalettes();
                CleanupFonts();
                CleanupSvg();
                CleanupIcons();
                CleanupIllustrations();

                Log.LogMessage(MessageImportance.High, "UI Kit cleanup successfully completed.");
            }
            catch (Exception ex)
            {
                Log.LogErrorFromException(ex, showStackTrace: true);
            }

            return !Log.HasLoggedErrors;
        }

        private void CleanupStyles()
        {
            CleanupFile(
                _configuration.GeneratedStylesPath,
                nameof(_configuration.GeneratedStylesPath),
                "XAML styles resource dictionary");
        }

        private void CleanupCustomizationDictionaries()
        {
            CleanupFiles(
                _configuration.GeneratedResourcesDirectory,
                nameof(_configuration.GeneratedResourcesDirectory),
                GetFilenamePattern(Const.XamlExtension),
                "XAML customization resource dictionaries");
        }

        private void CleanupPalettes()
        {
            CleanupFiles(
                Path.Combine(_configuration.PaletteDirectory, "..\\"),
                nameof(_configuration.PaletteDirectory),
                Const.PaletteFilename + Const.XamlExtension,
                "XAML palettes",
                includeSubdirectories: true);

            CleanupFile(
                _configuration.PaletteEnumPath,
                nameof(_configuration.PaletteEnumPath),
                "XAML palette enums file");
        }

        private void CleanupFonts()
        {
            CleanupFile(
                _configuration.FontEnumPath,
                nameof(_configuration.FontEnumPath),
                "Font enums file");
        }

        private void CleanupSvg()
        {
            CleanupFiles(
                _configuration.SvgDirectory,
                nameof(_configuration.SvgDirectory),
                GetFilenamePattern(Const.SvgExtension),
                "SVG files");
        }

        private void CleanupIcons()
        {
            CleanupFiles(
                _configuration.IconSvgDirectory,
                nameof(_configuration.IconSvgDirectory),
                GetFilenamePattern(IconSvgGenerator.IconSvgFilenamePrefix, Const.SvgExtension),
                "Icon SVG files",
                includeSubdirectories: true);

            CleanupFile(
                _configuration.IconEnumPath,
                nameof(_configuration.IconEnumPath),
                "Icon enums file");
        }

        private void CleanupIllustrations()
        {
            if (_configuration?.ProductMediaProjectInfoMapping.Any() != true)
            {
                Log.LogMessage(
                    MessageImportance.High,
                    $"Configuration parameter '{nameof(_configuration.ProductMediaProjectInfoMapping)}' is not set. Skip illustrations cleanup.");
                return;
            }

            foreach (var kvp in _configuration.ProductMediaProjectInfoMapping)
            {
                var product = kvp.Key;
                var productMediaProjectInfo = kvp.Value;

                CleanupFiles(
                    productMediaProjectInfo.IllustrationSvgDirectory,
                    nameof(productMediaProjectInfo.IllustrationSvgDirectory),
                    GetFilenamePattern(IllustrationSvgGenerator.IllustrationSvgFilenamePrefix, Const.SvgExtension),
                    $"Illustration SVG files (product '{product}')",
                    includeSubdirectories: true);

                CleanupFile(
                    productMediaProjectInfo.IllustrationEnumPath,
                    nameof(productMediaProjectInfo.IllustrationEnumPath),
                    $"Illustration enums file (product '{product}')");

                CleanupFile(
                    productMediaProjectInfo.IllustrationMarkupExtensionPath,
                    nameof(productMediaProjectInfo.IllustrationMarkupExtensionPath),
                    $"Illustration markup extension file (product '{product}')");
            }
        }

        private void CleanupFile(string configurationParameterValue, string configurationParameterName, string fileDescription)
        {
            if (string.IsNullOrWhiteSpace(configurationParameterValue))
            {
                Log.LogMessage(
                    MessageImportance.High,
                    $"Configuration parameter '{configurationParameterName}' is not set. Skip {fileDescription} cleanup.");
                return;
            }

            var path = Path.GetFullPath(configurationParameterValue);
            if (!File.Exists(path))
            {
                Log.LogMessage(
                    MessageImportance.High,
                    $"File '{path}' doesn't exist. Skip {fileDescription} cleanup.");
                return;
            }

            try
            {
                File.Delete(path);

                Log.LogMessage(MessageImportance.High, $"{fileDescription} cleanup completed.");
            }
            catch (Exception ex)
            {
                Log.LogWarning(null, null, null, path, 0, 0, 0, 0, $"Unable to remove file.");
                Log.LogWarningFromException(ex, true);
            }
        }

        private void CleanupFiles(
            string configurationParameterValue,
            string configurationParameterName,
            string filenamePattern,
            string filesDescription,
            bool includeSubdirectories = false)
        {
            if (string.IsNullOrWhiteSpace(configurationParameterValue))
            {
                Log.LogMessage(
                    MessageImportance.High,
                    $"Configuration parameter '{configurationParameterName}' is not set. Skip {filesDescription} cleanup.");
                return;
            }

            var directory = Path.GetFullPath(configurationParameterValue);
            if (!Directory.Exists(directory))
            {
                Log.LogMessage(
                    MessageImportance.High,
                    $"Directory '{directory}' doesn't exist. Skip {filesDescription} cleanup.");
                return;
            }

            var searchOptions = includeSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            var counter = 0;
            foreach (var path in Directory.EnumerateFiles(directory, filenamePattern, searchOptions))
            {
                Log.LogMessage(MessageImportance.Normal, $"Removing '{path}'.");

                try
                {
                    File.Delete(path);
                    counter++;
                }
                catch (Exception ex)
                {
                    Log.LogWarning(null, null, null, path, 0, 0, 0, 0, $"Unable to remove file.");
                    Log.LogWarningFromException(ex, true);
                }
            }

            Log.LogMessage(
                MessageImportance.High,
                counter > 0
                    ? $"{filesDescription} cleanup completed: {counter} file(s) removed."
                    : $"Directory '{directory}' contains no matching files. Skip {filesDescription} cleanup.");
        }

        private static string GetFilenamePattern(string extension)
            => $"{Const.UIKitPrefix}*{extension}";

        private static string GetFilenamePattern(string prefix, string extension)
            => $"{prefix}*{extension}";
    }
}
