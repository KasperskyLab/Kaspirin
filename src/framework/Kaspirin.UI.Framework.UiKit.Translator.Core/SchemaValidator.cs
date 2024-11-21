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
using System.Xml;
using System.Xml.Schema;
using Microsoft.Build.Utilities;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core
{
    internal sealed class SchemaValidator
    {
        public SchemaValidator(bool failOnValidationErrors, TaskLoggingHelper log)
        {
            _failOnValidationErrors = failOnValidationErrors;
            _log = log;
        }

        public bool Validate(string uiKitPath)
        {
            var schemaStream = EmbeddedResourceHelper.GetEmbeddedResource(Const.SchemaResourcesDirectory, Const.UiKitSchemaFilename);

            var schemas = new XmlSchemaSet();

            var schemaReadingErrors = 0;
            schemas.ValidationEventHandler += (s, args) =>
            {
                Trace(args);

                if (args.Severity == XmlSeverityType.Error)
                {
                    schemaReadingErrors++;
                }
            };

            schemas.XmlResolver = new EmbeddedResourceXmlUrlResolver(Const.SchemaResourcesDirectory, _log);

            var schema = XmlSchema.Read(schemaStream, null);
            schemas.Add(schema);

            schemas.Compile();

            if (schemaReadingErrors > 0)
            {
                Trace("UI Kit XSD-schema reading failed.");
                if (_failOnValidationErrors)
                {
                    return false;
                }
            }

            var settings = new XmlReaderSettings();
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationType = ValidationType.Schema;

            settings.Schemas.Add(schema);

            var schemaValidationErrors = 0;
            settings.ValidationEventHandler += (s, args) =>
            {
                Trace(args);

                if (args.Severity == XmlSeverityType.Error)
                {
                    schemaValidationErrors++;
                }
            };

            using var xmlReader = XmlReader.Create(uiKitPath, settings);
            var xmlDocument = new XmlDocument { PreserveWhitespace = true };
            xmlDocument.Load(xmlReader);

            if (schemaValidationErrors > 0)
            {
                Trace("UI Kit XSD-schema validation failed.");
                if (_failOnValidationErrors)
                {
                    return false;
                }
            }

            return true;
        }

        private void Trace(string message)
        {
            if (!_failOnValidationErrors)
            {
                _log.LogWarning(message);
            }
            else
            {
                _log.LogError(message);
            }
        }

        private void Trace(ValidationEventArgs args)
        {
            var message = args.Message;
            var lineNumber = args.Exception.LineNumber;
            var linePosition = args.Exception.LinePosition;
            var sourceUri = args.Exception.SourceUri;

            var shouldLogAsError = _failOnValidationErrors && args.Severity == XmlSeverityType.Error;

            if (TryResolvePath(sourceUri, out var path))
            {
                // If we have a reference to an existing file, we can provide logging routines with this information.
                // In this case warnings and errors will be interactive, so one can click on it and get redirected
                // to the particular location inside the file with the problem. Very convenient for XML and XSD troubleshooting.

                if (shouldLogAsError)
                {
                    _log.LogError(null, null, null, path, lineNumber, linePosition, lineNumber, linePosition, message);
                }
                else
                {
                    _log.LogWarning(null, null, null, path, lineNumber, linePosition, lineNumber, linePosition, message);
                }

                return;
            }

            message = $"{message} ({sourceUri}, line: {lineNumber}, position: {linePosition})";

            if (shouldLogAsError)
            {
                _log.LogError(message);
            }
            else
            {
                _log.LogWarning(message);
            }
        }

        private static bool TryResolvePath(string sourceUri, out string path)
        {
            if (Uri.TryCreate(sourceUri, UriKind.Absolute, out var uri))
            {
                path = Path.GetFullPath(Uri.UnescapeDataString(uri.AbsolutePath));
                if (File.Exists(path))
                {
                    return true;
                }

                var embeddedConfigurationProvider = new EmbeddedConfigurationProvider();
                if (embeddedConfigurationProvider.TryGetValue(Const.SchemasDirectoryEmbeddedConfigurationKey, out var schemasDirectory))
                {
                    // Schema files are packed into the assembly as embedded resources.
                    // Try to locate file from embedded resource in the original directory with schemas.
                    path = Path.GetFullPath(Path.Combine(schemasDirectory, Path.GetFileName(path)));
                    if (File.Exists(path))
                    {
                        return true;
                    }
                }
            }

            path = sourceUri;
            return false;
        }

        private readonly bool _failOnValidationErrors;
        private readonly TaskLoggingHelper _log;
    }
}
