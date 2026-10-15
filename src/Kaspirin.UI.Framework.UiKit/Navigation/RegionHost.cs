// Copyright © 2026 AO Kaspersky Lab.
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
using System.Windows.Controls;

namespace Kaspirin.UI.Framework.UiKit.Navigation;

public sealed class RegionHost
{
    internal RegionHost()
    {
    }

    internal void SetHost(ContentControl hostControl)
    {
        _host = Guard.EnsureArgumentIsNotNull(hostControl);
    }

    internal void SetHostView(RegionView? regionView)
    {
        Guard.IsNotNull(_host, "RegionHost is not initialized");

        _host.Content = regionView?.View;
    }

    public bool IsInitialized => _host != null;

    public bool IsLoaded => _host != null && _host.IsLoaded;

    public void WhenLoaded(Action action)
    {
        Guard.IsNotNull(_host, "RegionHost is not initialized");

        _host.WhenLoaded(action);
    }

    public void WhenUnloaded(Action action)
    {
        Guard.IsNotNull(_host, "RegionHost is not initialized");

        _host.WhenUnloaded(action);
    }

    private ContentControl? _host;
}
