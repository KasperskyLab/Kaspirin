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
using System.Text.RegularExpressions;

namespace Kaspirin.UI.Framework.UiKit.Localization.LocResources
{
    public interface IResourceBrowser
    {
        IEnumerable<Uri> Search(string searchRequest);

        IEnumerable<Match> Query(string queryRequest);

        bool Contains(Uri resourceUri);

        byte[] Read(Uri resourceUri);

        IEnumerable<Uri> GetResources();

        event EventHandler<ResourcesLoadedEventArgs>? ResourcesLoaded;
    }
}
