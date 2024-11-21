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
    internal sealed class IllustrationEnumGenerator
    {
        public IllustrationEnumGenerator(
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
            var enumsTemplate = ReadEnumsTemplate();

            foreach (var kvp in illustrations)
            {
                var product = kvp.Key;

                var productIllustrations = kvp.Value
                    .OrderBy(i => i.Scope)
                    .ThenBy(i => i.Name)
                    .ToArray();

                var productEnumsFile = enumsTemplate;

                productEnumsFile = FillNamespace(product, productEnumsFile);
                productEnumsFile = FillEnums(productIllustrations, productEnumsFile);
                productEnumsFile = FillMetadata(productIllustrations, productEnumsFile);
                productEnumsFile = CommentGenerator.AddFileComment(productEnumsFile, _comment);

                SaveEnums(product, productEnumsFile);
            }
        }

        private string ReadEnumsTemplate()
        {
            using var stream = EmbeddedResourceHelper.GetEmbeddedResource(
                Const.UIKitIllustrationTemplateDirectory,
                Const.UIKitIllustrationEnumsTemplateFilename);
            using var reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }

        private string FillNamespace(string product, string enumsFile)
        {
            var productNamespacePart = _productMediaProjectInfoProvider(product).ProductNamespacePart;
            if (string.IsNullOrWhiteSpace(productNamespacePart))
            {
                throw new InvalidOperationException($"Media project namespace for product '{product}' is not configured");
            }

            return enumsFile.Replace("@ProductNamespacePart", productNamespacePart);
        }

        private string FillEnums(Illustration[] illustrations, string enumsFile)
        {
            var illustrationEnumsStringBuilder = new StringBuilder();

            foreach (var g in illustrations.GroupBy(i => i.Scope))
            {
                var scope = g.Key;

                var enumTypeName = GetEnumTypeName(scope);

                if (illustrationEnumsStringBuilder.Length > 0)
                {
                    illustrationEnumsStringBuilder.AppendLine();
                    illustrationEnumsStringBuilder.AppendLine();
                }

                // Template is:
                // /// </summary>UIKit illustrations with <Scope> scope.</summary>
                // public enum <EnumTypeName>
                // {
                //     UIKitUnset = 0,
                //     <EnumItem 1>,
                //     ...
                //     <EnumItem N>,
                // }

                illustrationEnumsStringBuilder.AppendLine($"{_indent}/// </summary>UIKit illustrations with {scope} scope.</summary>");
                illustrationEnumsStringBuilder.AppendLine($"{_indent}public enum {enumTypeName}");
                illustrationEnumsStringBuilder.AppendLine($"{_indent}{{");

                illustrationEnumsStringBuilder.AppendLine($"{_indent}{_indent}UIKitUnset = 0,");

                foreach (var illustration in g)
                {
                    illustrationEnumsStringBuilder.AppendLine($"{_indent}{_indent}{illustration.Name},");
                }

                illustrationEnumsStringBuilder.Append($"{_indent}}}");
            }

            return enumsFile.Replace("@IllustrationEnumDeclarations", illustrationEnumsStringBuilder.ToString());
        }

        private string FillMetadata(Illustration[] illustrations, string enumsFile)
        {
            // Template is:
            // UIKitIllustrationMetadataStorage.Register(<EnumTypeName>.<EnumItem>, isAutoRTL: <IsAutoRTL>, height: <Height>, width: <Width>);

            var illustrationsMetadata = string.Join("\r\n", illustrations
                .Select(i =>
                    $"{_indent}{_indent}{_indent}{_indent}" +
                    $"UIKitIllustrationMetadataStorage.Register(" +
                        $"{GetEnumTypeName(i.Scope)}.{i.Name}, " +
                        $"isAutoRTL: {i.IsAutoRTL.ToString().ToLower()}, " +
                        $"height: {i.Height}, " +
                        $"width: {i.Width});"));

            return enumsFile.Replace("@IllustrationsMetadataRegistration", illustrationsMetadata);
        }

        private void SaveEnums(string product, string enumsFile)
        {
            var productEnumFilePath = _productMediaProjectInfoProvider(product).IllustrationEnumPath;
            if (string.IsNullOrWhiteSpace(productEnumFilePath))
            {
                throw new InvalidOperationException($"Illustration enums file path for product '{product}' is not configured");
            }

            Directory.CreateDirectory(Path.GetDirectoryName(productEnumFilePath));

            File.WriteAllText(productEnumFilePath, enumsFile);

            _log.LogMessage(
                MessageImportance.High,
                $"Illustration enums file generation completed for product '{product}'.");
        }

        private static string GetEnumTypeName(string scope)
            => $"{Const.UIKitIllustrationPrefix}{scope}";

        private readonly string _indent = Const.DefaultOutputIndentChars;
        private readonly string _comment;
        private readonly Func<string, Configuration.ProductMediaProjectInfo> _productMediaProjectInfoProvider;
        private readonly TaskLoggingHelper _log;
    }
}
