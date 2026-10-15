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

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Kaspirin.UI.Framework.UiKit.Navigation;
using Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Navigation.Infrastructure;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Navigation;

[TestClass]
public sealed class RegionCacheTests : BaseTests
{
    [STATestMethod]
    public void Add_View_RaisesAddedAndBecomesFindable()
    {
        // Arrange
        var cache = new RegionCache();
        var addedViews = new List<RegionView>();
        cache.Added += addedViews.Add;
        var view = CreateRegionView<TestViewA>();

        // Act
        cache.Add(view);

        // Assert
        Assert.AreEqual(1, cache.Count);
        Assert.AreSame(view, cache.TryFind(view.Name));
        Assert.IsTrue(cache.Contains<TestViewA>());
        CollectionAssert.AreEqual(new[] { view }, addedViews);
    }

    [STATestMethod]
    public void Remove_ExistingView_RaisesRemovedAndReturnsTrue()
    {
        // Arrange
        var cache = new RegionCache();
        var removedViews = new List<RegionView>();
        cache.Removed += removedViews.Add;
        var view = CreateRegionView<TestViewA>();
        cache.Add(view);

        // Act
        var removed = cache.Remove<TestViewA>();

        // Assert
        Assert.IsTrue(removed);
        Assert.AreEqual(0, cache.Count);
        Assert.IsFalse(cache.Contains<TestViewA>());
        CollectionAssert.AreEqual(new[] { view }, removedViews);
    }

    [STATestMethod]
    public void Remove_MissingView_ReturnsFalseAndDoesNotRaiseRemoved()
    {
        // Arrange
        var cache = new RegionCache();
        var removedViews = new List<RegionView>();
        cache.Removed += removedViews.Add;

        // Act
        var removed = cache.Remove<TestViewA>();

        // Assert
        Assert.IsFalse(removed);
        Assert.AreEqual(0, removedViews.Count);
    }

    [STATestMethod]
    public void TryFind_EmptyName_ReturnsNull()
    {
        // Arrange
        var cache = new RegionCache { CreateRegionView<TestViewA>() };

        // Act & Assert
        Assert.IsNull(cache.TryFind(string.Empty));
        Assert.IsNull(cache.TryFind(viewName: null));
    }

    [STATestMethod]
    public void GetEnumerator_ReturnsAllAddedViews()
    {
        // Arrange
        var first = CreateRegionView<TestViewA>();
        var second = CreateRegionView<TestViewB>();
        var cache = new RegionCache { first, second };

        // Act
        var views = cache.ToArray();

        // Assert
        CollectionAssert.AreEqual(new[] { first, second }, views);
    }

    private static RegionView CreateRegionView<TView>()
        where TView : FrameworkElement, new()
    {
        var factory = new RegionViewFactoryMock().Register<TView>();
        var region = new Region("CacheTestRegion", new ContentControl(), factory, new RegionBehaviorsRegistry());
        var viewName = typeof(TView).FullName!;
        var viewFactory = () => new TView();

        return new RegionView(viewName, viewFactory, region);
    }
}
