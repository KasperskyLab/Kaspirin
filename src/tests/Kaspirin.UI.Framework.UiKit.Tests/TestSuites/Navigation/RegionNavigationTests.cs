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
public sealed class RegionNavigationTests : BaseTests
{
    [STATestMethod]
    public void NavigateForward_CreatesViewAndActivatesIt()
    {
        // Arrange
        using var window = new TestWindow();
        var region = CreateRegion(window);

        // Act
        var view = region.Navigate<TestViewA>();

        // Assert
        Assert.IsNotNull(view);
        Assert.AreSame(view, region.ActiveView);
        Assert.IsInstanceOfType(window.Host.Content, typeof(TestViewA));
        Assert.AreEqual(typeof(TestViewA).FullName, region.History.Current);
        Assert.IsTrue(region.Cache.Contains<TestViewA>());
    }

    [STATestMethod]
    public void Navigate_TwoViews_TracksHistoryOrder()
    {
        // Arrange
        using var window = new TestWindow();
        var region = CreateRegion(window);

        // Act
        region.Navigate<TestViewA>();
        region.Navigate<TestViewB>();

        // Assert
        Assert.AreEqual(typeof(TestViewB).FullName, region.History.Current);
        Assert.AreEqual(typeof(TestViewA).FullName, region.History.Previous);
        Assert.AreEqual(typeof(TestViewB).FullName, region.ActiveView?.Name);
    }

    [STATestMethod]
    public void Navigate_TargetInHistory_NavigatesBack()
    {
        // Arrange
        using var window = new TestWindow();
        var region = CreateRegion(window);
        region.Navigate<TestViewA>();
        region.Navigate<TestViewB>();

        // Act
        region.Navigate<TestViewA>();

        // Assert
        Assert.AreEqual(typeof(TestViewA).FullName, region.ActiveView?.Name);
        Assert.IsFalse(region.History.Contains<TestViewB>());
        Assert.IsFalse(region.Cache.Contains<TestViewB>());
    }

    [STATestMethod]
    public void NavigateBack_ReturnsToPreviousViewAndReportsSuccess()
    {
        // Arrange
        using var window = new TestWindow();
        var region = CreateRegion(window);
        region.Navigate<TestViewA>();
        region.Navigate<TestViewB>();
        NavigationResult? result = null;

        // Act
        region.NavigateBack(r => result = r);

        // Assert
        Assert.AreEqual(typeof(TestViewA).FullName, region.ActiveView?.Name);
        Assert.IsNotNull(result);
        Assert.AreEqual(NavigationOutcome.Success, result!.Outcome);
        Assert.AreSame(region.ActiveView, result.View);
    }

    [STATestMethod]
    public void NavigateBack_EmptyHistory_ReportsNoOp()
    {
        // Arrange
        using var window = new TestWindow();
        var region = CreateRegion(window);
        NavigationResult? result = null;

        // Act
        region.NavigateBack(r => result = r);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(NavigationOutcome.NoOp, result!.Outcome);
    }

    [STATestMethod]
    public void Navigate_SameActiveView_ReportsNoOp()
    {
        // Arrange
        using var window = new TestWindow();
        var region = CreateRegion(window);
        region.Navigate<TestViewA>();
        NavigationResult? result = null;

        // Act
        region.Navigate<TestViewA>(r => result = r);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(NavigationOutcome.NoOp, result!.Outcome);
        Assert.AreEqual(1, region.Cache.Count);
    }

    [STATestMethod]
    public void Navigate_Callback_InvokedAfterOnNavigatedTo()
    {
        // Arrange
        using var window = new TestWindow();
        var invocationOrder = new List<string>();
        var viewModel = new TestViewModel
        {
            NavigatedToCallback = _ => invocationOrder.Add("OnNavigatedTo"),
        };
        var factory = new RegionViewFactoryMock()
            .Register<TestViewA>(() => new TestViewA { DataContext = viewModel });
        var region = CreateRegion(window, factory);

        // Act
        region.Navigate<TestViewA>(_ => invocationOrder.Add("Callback"));

        // Assert
        CollectionAssert.AreEqual(new[] { "OnNavigatedTo", "Callback" }, invocationOrder);
    }

    [STATestMethod]
    public void Navigate_ViewFactoryThrows_ReportsFailed()
    {
        // Arrange
        using var window = new TestWindow();
        var factory = new RegionViewFactoryMock()
            .Register<TestViewA>(() => throw new InvalidOperationException("Boom"));
        var region = CreateRegion(window, factory);
        NavigationResult? result = null;

        // Act
        region.Navigate<TestViewA>(r => result = r);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(NavigationOutcome.Failed, result!.Outcome);
        Assert.IsInstanceOfType(result.Error, typeof(InvalidOperationException));
    }

    [STATestMethod]
    public void Navigate_ActiveViewDeniesNavigation_ReportsCancelled()
    {
        // Arrange
        using var window = new TestWindow();
        var viewModel = new TestViewModel
        {
            BuildOptionsCallback = builder => builder.SetCanNavigateFrom(NavigateFromPermission.Deny),
        };
        var factory = DefaultFactory()
            .Register<TestViewA>(() => new TestViewA { DataContext = viewModel });
        var region = CreateRegion(window, factory);
        region.Navigate<TestViewA>();
        NavigationResult? result = null;

        // Act
        region.Navigate<TestViewB>(r => result = r);

        // Assert
        Assert.AreEqual(typeof(TestViewA).FullName, region.ActiveView?.Name);
        Assert.IsNotNull(result);
        Assert.AreEqual(NavigationOutcome.Cancelled, result!.Outcome);
    }

    [STATestMethod]
    public void Navigate_AskConfirmed_Navigates()
    {
        // Arrange
        using var window = new TestWindow();
        var region = CreateAskRegion(window, (_, confirm) => confirm(true));
        region.Navigate<TestViewA>();
        NavigationResult? result = null;

        // Act
        region.Navigate<TestViewB>(r => result = r);

        // Assert
        Assert.AreEqual(typeof(TestViewB).FullName, region.ActiveView?.Name);
        Assert.IsNotNull(result);
        Assert.AreEqual(NavigationOutcome.Success, result!.Outcome);
    }

    [STATestMethod]
    public void Navigate_AskRejected_ReportsCancelled()
    {
        // Arrange
        using var window = new TestWindow();
        var region = CreateAskRegion(window, (_, confirm) => confirm(false));
        region.Navigate<TestViewA>();
        NavigationResult? result = null;

        // Act
        region.Navigate<TestViewB>(r => result = r);

        // Assert
        Assert.AreEqual(typeof(TestViewA).FullName, region.ActiveView?.Name);
        Assert.IsNotNull(result);
        Assert.AreEqual(NavigationOutcome.Cancelled, result!.Outcome);
    }

    [STATestMethod]
    public void Navigate_AskWithoutConfirmationDelegate_Throws()
    {
        // Arrange
        using var window = new TestWindow();
        var viewModel = new TestViewModel
        {
            BuildOptionsCallback = builder => builder.SetCanNavigateFrom(NavigateFromPermission.Ask),
        };
        var factory = DefaultFactory()
            .Register<TestViewA>(() => new TestViewA { DataContext = viewModel });
        var region = CreateRegion(window, factory);
        region.Navigate<TestViewA>();

        // Act & Assert
        Assert.ThrowsException<InvalidOperationException>(() => region.Navigate<TestViewB>());
        Assert.AreEqual(typeof(TestViewA).FullName, region.ActiveView?.Name);
    }

    [STATestMethod]
    public void Navigate_AskDeferredConfirmation_NavigatesAfterContinuation()
    {
        // Arrange
        using var window = new TestWindow();
        Action<bool>? capturedContinuation = null;
        var region = CreateAskRegion(window, (_, confirm) => capturedContinuation = confirm);
        region.Navigate<TestViewA>();
        NavigationResult? result = null;

        // Act
        region.Navigate<TestViewB>(r => result = r);

        // Assert
        Assert.AreEqual(typeof(TestViewA).FullName, region.ActiveView?.Name);
        Assert.IsNull(result);
        Assert.IsNotNull(capturedContinuation);

        // Act
        capturedContinuation!.Invoke(true);

        // Assert
        Assert.AreEqual(typeof(TestViewB).FullName, region.ActiveView?.Name);
        Assert.IsNotNull(result);
        Assert.AreEqual(NavigationOutcome.Success, result!.Outcome);
    }

    [STATestMethod]
    public void Navigate_AskCallback_ReceivesForwardContextWithTargetViewName()
    {
        // Arrange
        using var window = new TestWindow();
        NavigationContext? context = null;
        var region = CreateAskRegion(window, (ctx, confirm) =>
        {
            context = ctx;
            confirm(true);
        });
        region.Navigate<TestViewA>();

        // Act
        region.Navigate<TestViewB>();

        // Assert
        Assert.IsNotNull(context);
        Assert.AreSame(region, context!.Region);
        Assert.AreEqual(NavigationDirection.Forward, context.Direction);
        Assert.AreEqual(typeof(TestViewB).FullName, context.TargetViewName);
        Assert.IsNull(context.TargetView);
        Assert.AreEqual(typeof(TestViewA).FullName, context.ActiveView?.Name);
    }

    [STATestMethod]
    public void NavigateBack_AskCallback_ReceivesBackContextWithCachedTargetView()
    {
        // Arrange
        using var window = new TestWindow();
        NavigationContext? context = null;
        var viewModel = new TestViewModel
        {
            BuildOptionsCallback = builder =>
            {
                builder.SetCanNavigateFrom(NavigateFromPermission.Ask);
                builder.SetConfirmNavigation((ctx, confirm) =>
                {
                    context = ctx;
                    confirm(true);
                });
            },
        };
        var factory = DefaultFactory()
            .Register<TestViewB>(() => new TestViewB { DataContext = viewModel });
        var region = CreateRegion(window, factory);
        region.Navigate<TestViewA>();
        region.Navigate<TestViewB>();

        // Act
        region.NavigateBack();

        // Assert
        Assert.IsNotNull(context);
        Assert.AreEqual(NavigationDirection.Back, context!.Direction);
        Assert.AreEqual(typeof(TestViewA).FullName, context.TargetViewName);
        Assert.IsNotNull(context.TargetView);
        Assert.AreEqual(typeof(TestViewA).FullName, context.TargetView!.Name);
        Assert.AreEqual(typeof(TestViewB).FullName, context.ActiveView?.Name);
    }

    [STATestMethod]
    public void Navigate_NavigationContext_CarriesRegionAndDirection()
    {
        // Arrange
        using var window = new TestWindow();
        var contexts = new List<NavigationContext>();
        var alphaViewModel = new TestViewModel { NavigatedToCallback = contexts.Add };
        var bravoViewModel = new TestViewModel { NavigatedToCallback = contexts.Add };
        var factory = new RegionViewFactoryMock()
            .Register<TestViewA>(() => new TestViewA { DataContext = alphaViewModel })
            .Register<TestViewB>(() => new TestViewB { DataContext = bravoViewModel });
        var region = CreateRegion(window, factory);

        // Act
        region.Navigate<TestViewA>();
        region.Navigate<TestViewB>();
        region.NavigateBack();

        // Assert
        Assert.AreEqual(3, contexts.Count);
        Assert.AreEqual(NavigationDirection.Forward, contexts[0].Direction);
        Assert.AreEqual(NavigationDirection.Forward, contexts[1].Direction);
        Assert.AreEqual(NavigationDirection.Back, contexts[2].Direction);
        Assert.IsTrue(contexts.TrueForAll(context => ReferenceEquals(context.Region, region)));
    }

    [STATestMethod]
    public void Navigate_WithParameters_DeliversParametersBeforeOnNavigatedTo()
    {
        // Arrange
        using var window = new TestWindow();
        var viewModel = new TestViewModelWithParams();
        var factory = new RegionViewFactoryMock()
            .Register<TestViewA>(() => new TestViewA { DataContext = viewModel });
        var region = CreateRegion(window, factory);
        var parameters = new TestParameters { Value = "Payload" };

        // Act
        region.Navigate<TestViewA, TestParameters>(parameters);

        // Assert
        Assert.AreSame(parameters, viewModel.ReceivedParameters);
        CollectionAssert.AreEqual(new[] { "SetParameters", "OnNavigatedTo" }, viewModel.InvocationOrder);
    }

    [STATestMethod]
    public void NavigateBack_ViewWithKeepInCache_SurvivesInCache()
    {
        // Arrange
        using var window = new TestWindow();
        var bravoViewModel = new TestViewModel
        {
            BuildOptionsCallback = builder => builder.SetKeepInCache(true),
        };
        var factory = DefaultFactory()
            .Register<TestViewB>(() => new TestViewB { DataContext = bravoViewModel });
        var region = CreateRegion(window, factory);
        region.Navigate<TestViewA>();
        region.Navigate<TestViewB>();

        // Act
        region.NavigateBack();

        // Assert
        Assert.IsTrue(region.Cache.Contains<TestViewB>());
        Assert.IsFalse(region.History.Contains<TestViewB>());
    }

    [STATestMethod]
    public void Navigate_ViewWithoutKeepInHistory_RemovedFromHistoryOnLeave()
    {
        // Arrange
        using var window = new TestWindow();
        var alphaViewModel = new TestViewModel
        {
            BuildOptionsCallback = builder => builder.SetKeepInHistory(false),
        };
        var factory = DefaultFactory()
            .Register<TestViewA>(() => new TestViewA { DataContext = alphaViewModel });
        var region = CreateRegion(window, factory);
        region.Navigate<TestViewA>();

        // Act
        region.Navigate<TestViewB>();

        // Assert
        Assert.IsFalse(region.History.Contains<TestViewA>());
        Assert.IsFalse(region.Cache.Contains<TestViewA>());
        Assert.AreEqual(1, region.History.Count);
    }

    [STATestMethod]
    public void Clear_ResetsRegionState()
    {
        // Arrange
        using var window = new TestWindow();
        var region = CreateRegion(window);
        region.Navigate<TestViewA>();
        region.Navigate<TestViewB>();
        NavigationResult? result = null;

        // Act
        region.Clear(r => result = r);

        // Assert
        Assert.IsNull(region.ActiveView);
        Assert.IsNull(window.Host.Content);
        Assert.AreEqual(0, region.History.Count);
        Assert.AreEqual(0, region.Cache.Count);
        Assert.IsNotNull(result);
        Assert.AreEqual(NavigationOutcome.Success, result!.Outcome);
    }

    [STATestMethod]
    public void Navigate_RaisesEventsInExpectedOrder()
    {
        // Arrange
        using var window = new TestWindow();
        var region = CreateRegion(window);
        var events = new List<string>();
        region.ViewAdded += (_, _) => events.Add("ViewAdded");
        region.Navigating += (_, _) => events.Add("Navigating");
        region.ActiveViewChanged += (_, _) => events.Add("ActiveViewChanged");
        region.Navigated += (_, _) => events.Add("Navigated");

        // Act
        region.Navigate<TestViewA>();

        // Assert
        CollectionAssert.AreEqual(new[] { "ViewAdded", "Navigating", "ActiveViewChanged", "Navigated" }, events);
    }

    [STATestMethod]
    public void Navigate_RegionContext_AccessibleThroughNavigationContext()
    {
        // Arrange
        using var window = new TestWindow();
        object? receivedValue = null;
        var viewModel = new TestViewModel
        {
            NavigatedToCallback = context => receivedValue = context.Region.Context["UserId"],
        };
        var factory = new RegionViewFactoryMock()
            .Register<TestViewA>(() => new TestViewA { DataContext = viewModel });
        var region = CreateRegion(window, factory);
        region.Context.Add("UserId", 42);

        // Act
        region.Navigate<TestViewA>();

        // Assert
        Assert.AreEqual(42, receivedValue);
    }

    private static Region CreateRegion(TestWindow window, IRegionViewFactory? factory = null)
    {
        return new Region("NavigationTestRegion", window.Host, factory ?? DefaultFactory(), new RegionBehaviorsRegistry());
    }

    private static Region CreateAskRegion(TestWindow window, NavigationConfirmationCallback confirmNavigation)
    {
        var viewModel = new TestViewModel
        {
            BuildOptionsCallback = builder =>
            {
                builder.SetCanNavigateFrom(NavigateFromPermission.Ask);
                builder.SetConfirmNavigation(confirmNavigation);
            },
        };

        var factory = DefaultFactory()
            .Register<TestViewA>(() => new TestViewA { DataContext = viewModel });

        return CreateRegion(window, factory);
    }

    private static RegionViewFactoryMock DefaultFactory()
    {
        return new RegionViewFactoryMock()
            .Register<TestViewA>()
            .Register<TestViewB>()
            .Register<TestViewC>();
    }
}
