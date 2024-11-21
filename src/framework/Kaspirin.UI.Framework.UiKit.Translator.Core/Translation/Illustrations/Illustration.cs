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

namespace Kaspirin.UI.Framework.UiKit.Translator.Core.Translation.Illustrations
{
    public sealed class Illustration
    {
        public double Height { get; set; }

        public bool IsAutoRTL { get; set; }

        public string Name { get; set; }

        public string Product { get; set; }

        public string Scope { get; set; }

        public IllustrationVector[] Vectors { get; set; }

        public double Width { get; set; }

        public sealed class IllustrationVector
        {
            public string Theme { get; set; }

            public bool IsRTL { get; set; }

            public string Vector { get; set; }
        }
    }
}
