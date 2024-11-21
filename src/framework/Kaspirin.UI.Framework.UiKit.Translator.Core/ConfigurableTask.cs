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
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core
{
    public abstract class ConfigurableTask
#if !NET6_0_OR_GREATER
        : AppDomainIsolatedTask
#else
        : Task
#endif
    {
        [Required]
        public string ConfigPath { get; set; }

        protected virtual bool CheckArguments()
        {
            if (!File.Exists(ConfigPath))
            {
                Log.LogError($"Config file '{Path.GetFullPath(ConfigPath)}' doesn't exist.");
                return false;
            }

            return true;
        }

        protected bool ReadConfiguration()
        {
            try
            {
                var configContent = File.ReadAllText(ConfigPath);
                _configuration = JsonHelper.Deserialize<Configuration>(configContent);
            }
            catch (Exception ex)
            {
                Log.LogError("Config deserialization failed.");
                Log.LogErrorFromException(ex, showStackTrace: true);
                return false;
            }

            return true;
        }

        private protected Configuration _configuration;
    }
}
