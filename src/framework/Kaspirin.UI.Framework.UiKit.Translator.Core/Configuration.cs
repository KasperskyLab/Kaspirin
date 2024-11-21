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
using System.Runtime.Serialization;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core
{
    [DataContract]
    internal sealed class Configuration
    {
        [DataMember]
        public string[] ExcludedControls { get; private set; }

        [DataMember]
        public IDictionary<string, ProductMediaProjectInfo> ProductMediaProjectInfoMapping { get; private set; }

        [DataMember]
        public string GeneratedStylesPath { get; private set; }

        [DataMember]
        public string GeneratedResourcesDirectory { get; private set; }

        [DataMember]
        public string FontEnumPath { get; private set; }

        [DataMember]
        public string FontNamespacePart { get; private set; }

        [DataMember]
        public string PaletteDirectory { get; private set; }

        [DataMember]
        public string PaletteEnumPath { get; private set; }

        [DataMember]
        public string PaletteNamespacePart { get; private set; }

        [DataMember]
        public string SvgDirectory { get; private set; }

        [DataMember]
        public string IconSvgDirectory { get; private set; }

        [DataMember]
        public string IconEnumPath { get; private set; }

        [DataMember]
        public bool FailOnValidationErrors { get; private set; }

        [DataMember]
        public bool FormatXamlFiles { get; private set; }

        [DataMember]
        public string UiKitPath { get; private set; }

        [DataMember]
        public string FileComment { get; internal set; }

        [DataContract]
        internal sealed class ProductMediaProjectInfo
        {
            [DataMember]
            public string IllustrationSvgDirectory { get; private set; }

            [DataMember]
            public string IllustrationEnumPath { get; private set; }

            [DataMember]
            public string IllustrationMarkupExtensionPath { get; private set; }

            [DataMember]
            public string ProductNamespacePart { get; private set; }
        }

        internal ProductMediaProjectInfo GetProductMediaProjectInfo(string product)
        {
            if (!ProductMediaProjectInfoMapping.TryGetValue(product, out var productMediaProjectInfo))
            {
                throw new InvalidOperationException($"Media project info for product '{product}' is not configured");
            }

            return productMediaProjectInfo;
        }
    }
}