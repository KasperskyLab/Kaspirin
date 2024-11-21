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

using System.Linq;
using System.Xml;
using Microsoft.Build.Utilities;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core.Translation
{
    internal abstract class GeneratorBase
    {
        public GeneratorBase(
            string[] excludedControls,
            LineEndingMode lineEndingMode,
            EmbeddedResourceXmlUrlResolver xsltUrlResolver,
            XmlWriterSettings xmlWriterSettings,
            TaskLoggingHelper log)
        {
            _excludedControls = excludedControls;
            _lineEndingMode = lineEndingMode;
            _xsltUrlResolver = xsltUrlResolver;
            _xmlWriterSettings = xmlWriterSettings;
            _log = log;
        }

        public abstract bool Generate(string uiKitContent, out string[] generatedFilePaths);

        protected string GetExcludedControlsFilter()
        {
            if (_excludedControls?.Any() != true)
            {
                return string.Empty;
            }

            return string.Join(Const.ExcludedControlsFilterDelimiter, _excludedControls);
        }

        protected readonly LineEndingMode _lineEndingMode;
        protected readonly EmbeddedResourceXmlUrlResolver _xsltUrlResolver;
        protected readonly XmlWriterSettings _xmlWriterSettings;
        protected readonly TaskLoggingHelper _log;

        private readonly string[] _excludedControls;
    }
}
