// Copyright © 2025 AO Kaspersky Lab.
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
using Kaspirin.UI.Framework.Tests.Mocks;

namespace Kaspirin.UI.Framework.Tests.TestSuites.Threading;

[TestClass]
public sealed class DeferredActionTests
{
    public DeferredActionTests()
    {
        _mockTimerFactory = new MockTimerFactory();
        _mockExecutor = new MockSyncedExecutor(_mockTimerFactory);

        _deferredActionFactory = DeferredActionFactory.Implementation;
    }

    [TestMethod]
    public void ThrottleMode_ShouldExecuteOnlyFirst()
    {
        // Arrange
        var capturedArgument = string.Empty;
        var actionInvokedCount = 0;

        var deferredAction = _deferredActionFactory.CreateOnTp<string>(
            action: arg =>
            {
                capturedArgument = arg;
                actionInvokedCount++;
            },
            delayBeforeAction: TimeSpan.FromMilliseconds(100),
            delayAfterAction: TimeSpan.Zero,
            name: string.Empty,
            mode: DeferredActionMode.Throttle,
            options: DeferredActionOptions.None,
            executor: _mockExecutor);

        // Act
        deferredAction.Execute("CallFirst");
        deferredAction.Execute("CallSecond");
        _mockTimerFactory.AdvanceTime(200);

        // Assert
        Assert.IsTrue(deferredAction.State == DeferredActionState.Idle);
        Assert.AreEqual(1, actionInvokedCount);
        Assert.AreEqual("CallFirst", capturedArgument);
    }

    [TestMethod]
    public void DebounceMode_ShouldExecuteOnlyLast()
    {
        // Arrange
        var capturedArgument = string.Empty;
        var actionInvokedCount = 0;

        var deferredAction = _deferredActionFactory.CreateOnTp<string>(
            action: arg =>
            {
                capturedArgument = arg;
                actionInvokedCount++;
            },
            delayBeforeAction: TimeSpan.FromMilliseconds(100),
            delayAfterAction: TimeSpan.Zero,
            name: string.Empty,
            mode: DeferredActionMode.Debounce,
            options: DeferredActionOptions.None,
            executor: _mockExecutor);

        // Act
        deferredAction.Execute("First");
        _mockTimerFactory.AdvanceTime(50);
        deferredAction.Execute("Second");
        _mockTimerFactory.AdvanceTime(50);
        deferredAction.Execute("Third");
        _mockTimerFactory.AdvanceTime(300);

        // Assert
        Assert.IsTrue(deferredAction.State == DeferredActionState.Idle);
        Assert.AreEqual(1, actionInvokedCount);
        Assert.AreEqual("Third", capturedArgument);
    }

    [TestMethod]
    public void DebounceMode_ShouldExecuteOnlyLast_WithRunLastSkipped()
    {
        // Arrange
        var capturedArgument = string.Empty;
        var actionInvokedCount = 0;

        var deferredAction = _deferredActionFactory.CreateOnTp<string>(
            action: arg =>
            {
                capturedArgument = arg;
                actionInvokedCount++;
            },
            delayBeforeAction: TimeSpan.FromMilliseconds(100),
            delayAfterAction: TimeSpan.FromMilliseconds(150),
            name: string.Empty,
            mode: DeferredActionMode.Debounce,
            options: DeferredActionOptions.RunLastSkippedAction,
            executor: _mockExecutor);

        // Act
        deferredAction.Execute("First");
        _mockTimerFactory.AdvanceTime(50);
        deferredAction.Execute("Second");
        _mockTimerFactory.AdvanceTime(150);

        // Assert
        Assert.AreEqual(1, actionInvokedCount);
        Assert.AreEqual("Second", capturedArgument);

        // Act
        deferredAction.Execute("Third");
        _mockTimerFactory.AdvanceTime(50);

        // Assert
        Assert.AreEqual(1, actionInvokedCount);
        Assert.AreEqual("Second", capturedArgument);

        deferredAction.Execute("Fourth");
        _mockTimerFactory.AdvanceTime(300);

        // Assert
        Assert.AreEqual(2, actionInvokedCount);
        Assert.AreEqual("Fourth", capturedArgument);

        _mockTimerFactory.AdvanceTime(200);
        Assert.IsTrue(deferredAction.State == DeferredActionState.Idle);
    }

    [TestMethod]
    public void DebounceMode_WithDelayAfterAction_ShouldResetAfterDelay()
    {
        // Arrange
        var capturedArgument = string.Empty;
        var actionInvokedCount = 0;

        var deferredAction = _deferredActionFactory.CreateOnTp<string>(
            action: arg =>
            {
                capturedArgument = arg;
                actionInvokedCount++;
            },
            delayBeforeAction: TimeSpan.Zero,
            delayAfterAction: TimeSpan.FromMilliseconds(200),
            name: string.Empty,
            mode: DeferredActionMode.Debounce,
            options: DeferredActionOptions.RunLastSkippedAction,
            executor: _mockExecutor);

        // Act
        deferredAction.Execute("Test");
        _mockTimerFactory.AdvanceTime(50);
        deferredAction.Execute("Another");
        _mockTimerFactory.AdvanceTime(200);

        // Assert
        Assert.AreEqual(2, actionInvokedCount);
        Assert.AreEqual("Another", capturedArgument);

        // Act
        _mockTimerFactory.AdvanceTime(200);

        // Assert
        Assert.IsTrue(deferredAction.State == DeferredActionState.Idle);
    }

    [TestMethod]
    public void ThrottleMode_WithDelayBeforeAction_ShouldExecuteAfterDelay()
    {
        // Arrange
        var capturedArgument = string.Empty;
        var actionInvokedCount = 0;

        var deferredAction = _deferredActionFactory.CreateOnTp<string>(
            action: arg =>
            {
                capturedArgument = arg;
                actionInvokedCount++;
            },
            delayBeforeAction: TimeSpan.FromMilliseconds(100),
            delayAfterAction: TimeSpan.Zero,
            name: string.Empty,
            mode: DeferredActionMode.Throttle,
            options: DeferredActionOptions.None,
            executor: _mockExecutor);

        // Act
        deferredAction.Execute("Test");
        _mockTimerFactory.AdvanceTime(50);

        // Assert
        Assert.AreEqual(0, actionInvokedCount);
        Assert.AreEqual(string.Empty, capturedArgument);

        // Act
        _mockTimerFactory.AdvanceTime(200);

        // Assert
        Assert.IsTrue(deferredAction.State == DeferredActionState.Idle);
        Assert.AreEqual(1, actionInvokedCount);
        Assert.AreEqual("Test", capturedArgument);
    }

    [TestMethod]
    public void ThrottleMode_Cancel_ShouldStopPendingActions()
    {
        // Arrange
        var capturedArgument = string.Empty;
        var actionInvokedCount = 0;

        var deferredAction = _deferredActionFactory.CreateOnTp<string>(
            action: arg =>
            {
                capturedArgument = arg;
                actionInvokedCount++;
            },
            delayBeforeAction: TimeSpan.FromMilliseconds(100),
            delayAfterAction: TimeSpan.Zero,
            name: string.Empty,
            mode: DeferredActionMode.Throttle,
            options: DeferredActionOptions.None,
            executor: _mockExecutor);

        // Act
        deferredAction.Execute("Test");
        deferredAction.Cancel();
        _mockTimerFactory.AdvanceTime(200);

        // Assert
        Assert.AreEqual(0, actionInvokedCount);
        Assert.AreEqual(string.Empty, capturedArgument);
    }

    [TestMethod]
    public void ThrottleMode_IsActive_ShouldReturnCorrectStatus()
    {
        // Arrange
        var deferredAction = _deferredActionFactory.CreateOnTp<string>(
            action: arg => { },
            delayBeforeAction: TimeSpan.FromMilliseconds(100),
            delayAfterAction: TimeSpan.FromMilliseconds(50),
            name: string.Empty,
            mode: DeferredActionMode.Throttle,
            options: DeferredActionOptions.RunLastSkippedAction,
            executor: _mockExecutor);

        // Act & Assert
        Assert.IsTrue(deferredAction.State == DeferredActionState.Idle);

        deferredAction.Execute("Test");
        Assert.IsTrue(deferredAction.State == DeferredActionState.DelayBeforeAction);

        _mockTimerFactory.AdvanceTime(200);
        Assert.IsTrue(deferredAction.State == DeferredActionState.Idle);
    }

    private readonly MockTimerFactory _mockTimerFactory;
    private readonly MockSyncedExecutor _mockExecutor;
    private readonly IDeferredActionFactory _deferredActionFactory;
}
