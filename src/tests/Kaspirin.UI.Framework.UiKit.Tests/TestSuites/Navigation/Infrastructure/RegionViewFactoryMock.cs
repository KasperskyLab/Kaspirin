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
using System.Collections.Generic;
using System.Windows;
using Kaspirin.UI.Framework.UiKit.Navigation;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Navigation.Infrastructure;

public sealed class RegionViewFactoryMock : IRegionViewFactory
{
    public FrameworkElement CreateView<TParameters>(string viewName, TParameters? parameters)
    {
        Guard.Assert(_factories.ContainsKey(viewName), $"View {viewName} is not registered in the factory.");

        return _factories[viewName].Invoke();
    }

    public RegionViewFactoryMock Register<TView>()
        where TView : FrameworkElement, new()
    {
        return Register<TView>(() => new TView());
    }

    public RegionViewFactoryMock Register<TView>(Func<FrameworkElement> viewFactory)
        where TView : FrameworkElement
    {
        Guard.ArgumentIsNotNull(viewFactory);

        _factories[typeof(TView).FullName!] = viewFactory;

        return this;
    }

    private readonly Dictionary<string, Func<FrameworkElement>> _factories = new();
}
