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
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Illustrations
{
    internal sealed class IllustrationMarkupExtensionGenerator
    {
        public IllustrationMarkupExtensionGenerator(
            Func<string, Configuration.ProductMediaProjectInfo> productMediaProjectInfoProvider,
            string comment,
            TaskLoggingHelper log)
        {
            _productMediaProjectInfoProvider = productMediaProjectInfoProvider;
            _comment = comment;
            _log = log;
        }

        public bool Generate(string uiKitContent)
        {
            var illustrations = GetIllustrations(uiKitContent);
            if (illustrations.Any())
            {
                Translate(illustrations);
            }

            return true;
        }

        private IDictionary<string, Illustration[]> GetIllustrations(string uiKitContent) =>
            new XmlIllustrationSource()
            .GetIllustrations(uiKitContent)
            .GroupBy(i => i.Product)
            .ToDictionary(g => g.Key, g => g.ToArray());

        private void Translate(IDictionary<string, Illustration[]> illustrations)
        {
            var markupExtensionTemplate = ReadMarkupExtensionTemplate();

            foreach (var kvp in illustrations)
            {
                var product = kvp.Key;

                var productScopes = kvp.Value
                    .Select(i => i.Scope)
                    .Distinct()
                    .OrderBy(scope => scope)
                    .ToArray();

                var productMarkupExtensionFile = markupExtensionTemplate;

                productMarkupExtensionFile = FillNamespace(product, productMarkupExtensionFile);
                productMarkupExtensionFile = FillEnumProperties(productScopes, productMarkupExtensionFile);
                productMarkupExtensionFile = FillEnumFields(productScopes, productMarkupExtensionFile);
                productMarkupExtensionFile = CommentGenerator.AddFileComment(productMarkupExtensionFile, _comment);

                SaveMarkupExtension(product, productMarkupExtensionFile);
            }
        }

        private string ReadMarkupExtensionTemplate()
        {
            using var stream = EmbeddedResourceHelper.GetEmbeddedResource(
                Const.UIKitIllustrationTemplateDirectory,
                Const.UIKitIllustrationMarkupExtensionTemplateFilename);
            using var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        private string FillNamespace(string product, string markupExtensionFile)
        {
            var productNamespacePart = _productMediaProjectInfoProvider(product).ProductNamespacePart;
            if (string.IsNullOrWhiteSpace(productNamespacePart))
            {
                throw new InvalidOperationException($"Media project namespace for product '{product}' is not configured");
            }

            return markupExtensionFile.Replace("@ProductNamespacePart", productNamespacePart);
        }

        private string FillEnumProperties(string[] scopes, string markupExtensionFile)
        {
            var illustrationEnumPropertiesStringBuilder = new StringBuilder();

            foreach (var scope in scopes)
            {
                if (illustrationEnumPropertiesStringBuilder.Length > 0)
                {
                    illustrationEnumPropertiesStringBuilder.AppendLine();
                    illustrationEnumPropertiesStringBuilder.AppendLine();
                }

                var enumTypeName = GetEnumTypeName(scope);
                var enumPropertyName = GetEnumPropertyName(scope);
                var enumFieldName = GetEnumFieldName(scope);

                // Template is:
                // public <EnumTypeName> <PropertyName> { get => <_fieldName>; set => EnsureCanSet(ref <_fieldName>, value); }

                illustrationEnumPropertiesStringBuilder.Append(
                    $"{_indent}{_indent}public {enumTypeName} {enumPropertyName} {{ get => {enumFieldName}; set => EnsureCanSet(ref {enumFieldName}, value); }}");
            }

            return markupExtensionFile.Replace("@IllustrationEnumPropertyDeclarations", illustrationEnumPropertiesStringBuilder.ToString());
        }

        private string FillEnumFields(string[] scopes, string markupExtensionFile)
        {
            var illustrationEnumFieldsStringBuilder = new StringBuilder();

            foreach (var scope in scopes)
            {
                if (illustrationEnumFieldsStringBuilder.Length > 0)
                {
                    illustrationEnumFieldsStringBuilder.AppendLine();
                }

                var enumTypeName = GetEnumTypeName(scope);
                var enumFieldName = GetEnumFieldName(scope);

                // Template is:
                // private <EnumTypeName> <_fieldName>;

                illustrationEnumFieldsStringBuilder.Append($"{_indent}{_indent}private {enumTypeName} {enumFieldName};");
            }

            return markupExtensionFile.Replace("@IllustrationEnumFieldsDeclarations", illustrationEnumFieldsStringBuilder.ToString());
        }

        private void SaveMarkupExtension(string product, string markupExtensionFile)
        {
            var productMarkupExtensionFilePath = _productMediaProjectInfoProvider(product).IllustrationMarkupExtensionPath;
            if (string.IsNullOrWhiteSpace(productMarkupExtensionFilePath))
            {
                throw new InvalidOperationException($"Illustration markup extension file path for product '{product}' is not configured");
            }

            Directory.CreateDirectory(Path.GetDirectoryName(productMarkupExtensionFilePath));

            File.WriteAllText(productMarkupExtensionFilePath, markupExtensionFile);

            _log.LogMessage(
                MessageImportance.High,
                $"Illustration markup extension file generation completed for product '{product}'.");
        }

        private static string GetEnumTypeName(string scope)
            => $"{Const.UIKitIllustrationPrefix}{scope}";

        private static string GetEnumPropertyName(string scope)
            => scope;

        private static string GetEnumFieldName(string scope)
            => $"_{char.ToLowerInvariant(scope[0])}{scope.Substring(1)}";

        private readonly string _indent = Const.DefaultOutputIndentChars;
        private readonly string _comment;
        private readonly Func<string, Configuration.ProductMediaProjectInfo> _productMediaProjectInfoProvider;
        private readonly TaskLoggingHelper _log;
    }
}
