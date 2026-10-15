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
using Kaspirin.UI.Framework.UiKit.Navigation;
using Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Navigation.Infrastructure;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Navigation;

[TestClass]
public sealed class RegionsLookupTests : BaseTests
{
    protected override void TestInitialize()
    {
        _regionsService = new Regions(
            new RegionViewFactoryMock().Register<TestViewA>(),
            new RegionBehaviorsRegistry());

        var container = new RapidUnityContainer();
        container.RegisterInstance(_regionsService);

        _originContainer = ServiceLocator.ExchangeContainer(container);
    }

    protected override void TestCleanup()
    {
        ServiceLocator.ExchangeContainer(_originContainer);
    }

    [STATestMethod]
    public void TryGetRegion_ExistingRegion_ReturnsRegion()
    {
        // Arrange
        using var window = new TestWindow();
        Regions.SetRegionName(window.Host, "LookupRegionExisting");

        // Act
        var region = RegionsService.TryGetRegion("LookupRegionExisting");

        // Assert
        Assert.IsNotNull(region);
        Assert.AreEqual("LookupRegionExisting", region!.Name);
    }

    [STATestMethod]
    public void TryGetRegion_NameIsCaseInsensitive_ReturnsRegion()
    {
        // Arrange
        using var window = new TestWindow();
        Regions.SetRegionName(window.Host, "LookupRegionCase");

        // Act
        var region = RegionsService.TryGetRegion("LOOKUPREGIONCASE");

        // Assert
        Assert.IsNotNull(region);
        Assert.AreEqual("LookupRegionCase", region!.Name);
    }

    [STATestMethod]
    public void TryGetRegion_UnknownName_ReturnsNull()
    {
        // Act
        var region = RegionsService.TryGetRegion("LookupRegionUnknown");

        // Assert
        Assert.IsNull(region);
    }

    [STATestMethod]
    public void Indexer_ExistingRegion_ReturnsRegion()
    {
        // Arrange
        using var window = new TestWindow();
        Regions.SetRegionName(window.Host, "LookupRegionIndexer");

        // Act
        var region = RegionsService["LookupRegionIndexer"];

        // Assert
        Assert.AreEqual("LookupRegionIndexer", region.Name);
    }

    [STATestMethod]
    public void Indexer_UnknownName_ThrowsException()
    {
        // Act & Assert
        Assert.ThrowsException<InvalidOperationException>(() => RegionsService["LookupRegionMissing"]);
    }

    [STATestMethod]
    public void WhenRegionCreated_RegionAlreadyExists_InvokesImmediately()
    {
        // Arrange
        using var window = new TestWindow();
        Regions.SetRegionName(window.Host, "LookupRegionImmediate");
        var receivedRegions = new List<Region>();

        // Act
        RegionsService.WhenRegionCreated("LookupRegionImmediate", receivedRegions.Add);

        // Assert
        Assert.AreEqual(1, receivedRegions.Count);
        Assert.AreEqual("LookupRegionImmediate", receivedRegions[0].Name);
    }

    [STATestMethod]
    public void WhenRegionCreated_RegionCreatedLater_InvokesOnceWithRegion()
    {
        // Arrange
        using var window = new TestWindow();
        var receivedRegions = new List<Region>();
        RegionsService.WhenRegionCreated("LookupRegionDeferred", receivedRegions.Add);

        // Assert
        Assert.AreEqual(0, receivedRegions.Count);

        // Act
        Regions.SetRegionName(window.Host, "LookupRegionDeferred");

        // Assert
        Assert.AreEqual(1, receivedRegions.Count);
        Assert.AreEqual("LookupRegionDeferred", receivedRegions[0].Name);
    }

    [STATestMethod]
    public void WhenRegionCreated_RegionRemovedAndRecreated_DoesNotInvokeTwice()
    {
        // Arrange
        using var firstWindow = new TestWindow();
        using var secondWindow = new TestWindow();
        var receivedRegions = new List<Region>();
        RegionsService.WhenRegionCreated("LookupRegionRecreate", receivedRegions.Add);
        Regions.SetRegionName(firstWindow.Host, "LookupRegionRecreate");

        // Act
        firstWindow.DetachHost();
        Regions.SetRegionName(secondWindow.Host, "LookupRegionRecreate");

        // Assert
        Assert.AreEqual(1, receivedRegions.Count);
    }

    [STATestMethod]
    public void WhenRegionCreated_TwoCallbacksSameName_BothInvokedInOrder()
    {
        // Arrange
        using var window = new TestWindow();
        var invocationOrder = new List<string>();
        RegionsService.WhenRegionCreated("LookupRegionMulti", _ => invocationOrder.Add("First"));
        RegionsService.WhenRegionCreated("LookupRegionMulti", _ => invocationOrder.Add("Second"));

        // Act
        Regions.SetRegionName(window.Host, "LookupRegionMulti");

        // Assert
        CollectionAssert.AreEqual(new[] { "First", "Second" }, invocationOrder);
    }

    private Regions RegionsService => Guard.EnsureIsNotNull(_regionsService);

    private Regions? _regionsService;
    private IUnityContainer? _originContainer;
}
