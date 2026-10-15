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
using Kaspirin.UI.Framework.UiKit.Navigation;
using Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Navigation.Infrastructure;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Navigation;

[TestClass]
public sealed class RegionsLifetimeTests : BaseTests
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
    public void SetRegionName_CreatesRegionAndRaisesRegionCreated()
    {
        // Arrange
        using var window = new TestWindow();
        var createdRegions = new List<Region>();
        RegionsService.RegionCreated += (_, args) => createdRegions.Add(args.Region);

        // Act
        Regions.SetRegionName(window.Host, "LifetimeRegionCreate");

        // Assert
        var region = RegionsService.Single();
        Assert.AreEqual("LifetimeRegionCreate", region.Name);
        CollectionAssert.AreEqual(new[] { region }, createdRegions);
    }

    [STATestMethod]
    public void SetRegionName_DuplicateNameOnLiveHost_Throws()
    {
        // Arrange
        using var firstWindow = new TestWindow();
        using var secondWindow = new TestWindow();
        Regions.SetRegionName(firstWindow.Host, "LifetimeRegionDuplicate");

        // Act & Assert
        Assert.ThrowsException<GuardException>(
            () => Regions.SetRegionName(secondWindow.Host, "LifetimeRegionDuplicate"));
    }

    [STATestMethod]
    public void HostUnloaded_RemovesRegionAndRaisesRegionRemoved()
    {
        // Arrange
        using var window = new TestWindow();
        Regions.SetRegionName(window.Host, "LifetimeRegionUnload");
        var region = RegionsService.Single();
        var removedRegions = new List<Region>();
        RegionsService.RegionRemoved += (_, args) => removedRegions.Add(args.Region);

        // Act
        window.DetachHost();

        // Assert
        Assert.AreEqual(0, RegionsService.Count());
        CollectionAssert.AreEqual(new[] { region }, removedRegions);
    }

    [STATestMethod]
    public void SetRegionName_DuplicateNameIsCaseInsensitive_Throws()
    {
        // Arrange
        using var firstWindow = new TestWindow();
        using var secondWindow = new TestWindow();
        Regions.SetRegionName(firstWindow.Host, "LifetimeRegionCase");

        // Act & Assert
        Assert.ThrowsException<GuardException>(
            () => Regions.SetRegionName(secondWindow.Host, "LIFETIMEREGIONCASE"));
    }

    private Regions RegionsService => Guard.EnsureIsNotNull(_regionsService);

    private Regions? _regionsService;
    private IUnityContainer? _originContainer;
}
