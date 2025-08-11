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
using System.Text;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Files;

public class FileLocalizer : BaseLocalizer<ResourceItem>, IFileLocalizer
{
    public FileLocalizer(LocalizerParameters parameters) : base(parameters) { }

    public virtual byte[]? GetContent(string key)
    {
        Guard.ArgumentIsNotNull(key);

        try
        {
            var resource = GetValue(key);

            return resource == null ? null : ResourceProvider.ReadResourceBytes(resource);
        }
        catch (Exception e)
        {
            e.ProcessAsLocalizerException($"Failed to provide file bytes for key '{key}' in scope '{ScopeInfo.Scope}'.");
            return null;
        }
    }

    public virtual Stream? GetStream(string key)
    {
        Guard.ArgumentIsNotNull(key);

        try
        {
            var resource = GetValue(key);

            return resource == null ? null : ResourceProvider.ReadResourceStream(resource);
        }
        catch (Exception e)
        {
            e.ProcessAsLocalizerException($"Failed to provide file stream for key '{key}' in scope '{ScopeInfo.Scope}'.");
            return null;
        }
    }

    public virtual string? GetText(string key, Encoding encoding)
    {
        Guard.ArgumentIsNotNull(key);
        Guard.ArgumentIsNotNull(encoding);

        try
        {
            var resource = GetValue(key);

            return resource == null ? null : ResourceProvider.ReadResourceString(resource, encoding);
        }
        catch (Exception e)
        {
            e.ProcessAsLocalizerException($"Failed to provide file text for key '{key}' in scope '{ScopeInfo.Scope}'.");
            return null;
        }
    }

    public virtual Uri? GetUri(string key)
    {
        Guard.ArgumentIsNotNull(key);

        try
        {
            return GetValue(key).Uri.AbsoluteUri;
        }
        catch (Exception e)
        {
            e.ProcessAsLocalizerException($"Failed to provide file URI for key '{key}' in scope '{ScopeInfo.Scope}'.");
            return null;
        }
    }

    protected override IScope<ResourceItem> CreateDirectoryScopeObject(IEnumerable<ResourceItem> resources)
    {
        return new FileScope(resources);
    }
}