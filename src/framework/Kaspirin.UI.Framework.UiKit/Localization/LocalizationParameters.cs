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
using System.Globalization;
using System.IO;

namespace Kaspirin.UI.Framework.UiKit.Localization;

public class LocalizationParameters
{
    public LocalizationParameters(
        string displayCulture,
        string? theme = null,
        string? prefix = null,
        string? resourcePath = null,
        IEnumerable<string>? assemblyNameOrder = null)
    {
        DisplayCulture = Guard.EnsureArgumentIsNotNull(displayCulture);
        Theme = theme;
        Prefix = prefix;
        ResourceBrowsers = CreateDefaultResourceBrowsers(resourcePath, assemblyNameOrder);
    }

    public string DisplayCulture { get; }

    public string FormatCulture { get; set; } = CultureInfo.CurrentCulture.Name;

    public string OsCulture { get; set; } = CultureInfo.CurrentUICulture.Name;

    public LocalizerFactoryDelegate LocalizerFactory { get; set; } = (parameters) => new LocalizerFactory(parameters);

    public string? Theme { get; set; } = null;

    public string? Prefix { get; set; } = null;

    public LocExceptionPolicySettings ExceptionPolicySettings { get; set; } = new();

    public MetadataStorageSettings MetadataStorageSettings { get; set; } = new();

    public LocalizerSettings LocalizerSettings { get; set; } = new();

    public IResourceBrowser[] ResourceBrowsers { get; set; }

    private static IResourceBrowser[] CreateDefaultResourceBrowsers(string? resourcePath, IEnumerable<string>? assemblyNameOrder)
    {
        var internalRoot = DefaultRootFolderName + "/";
        var externalRoot = ValidateResourcePath(resourcePath) ?? CreateDefaultResourcePath();

        assemblyNameOrder ??= new List<string>();

        var resourceBrowsers = new IResourceBrowser[]
        {
            new FileSystemResourceBrowser(root:externalRoot, handleChanges:false),
#if NETCOREAPP
            new ZipArchiveResourceBrowser(root:externalRoot, handleChanges:false, fileName:DefaultRootFolderName + ".zip"),
#endif
            new AssemblyResourceBrowser(root:internalRoot, handleChanges:true, assemblyNameOrder)
        };

        return resourceBrowsers;
    }

    private static string? ValidateResourcePath(string? resourcePath)
    {
        if (string.IsNullOrWhiteSpace(resourcePath))
        {
            return null;
        }

        resourcePath = resourcePath!.TrimEnd('/', '\\');

        if (!resourcePath.EndsWith(DefaultRootFolderName, StringComparison.CurrentCultureIgnoreCase))
        {
            resourcePath = Path.Combine(resourcePath, DefaultRootFolderName);
        }

        if (!Directory.Exists(resourcePath))
        {
            return null;
        }

        return resourcePath;
    }

    private static string CreateDefaultResourcePath()
    {
        var currentFolder = Guard.EnsureIsNotNull(Path.GetDirectoryName(typeof(LocalizationParameters).Assembly.Location));

        return Path.Combine(currentFolder, DefaultRootFolderName);
    }

    private const string DefaultRootFolderName = "resources";
}
