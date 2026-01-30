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
using System.Threading;

namespace Kaspirin.UI.Framework.Tests.TestSuites.Mvvm;

[TestClass]
public sealed class BaseCommandTests
{

    [TestInitialize]
    public void InitializeTests()
    {
        _originExecutor = Executers.DispatcherExecutor;

        Executers.DispatcherExecutor = new TestExecutor();
    }

    [TestCleanup]
    public void CleanupTests()
    {
        Executers.DispatcherExecutor = Guard.EnsureIsNotNull(_originExecutor);
    }

    [TestMethod]
    public void RaiseCanExecuteChanged_CanExecuteChangedRaisedSync()
    {
        // Arrange
        var eventFlag = new ManualResetEventSlim();
        var testThreadId = Thread.CurrentThread.ManagedThreadId;
        var textExecutor = new TestExecutor
        {
            IsDispatcherThread = true
        };

        Executers.DispatcherExecutor = textExecutor;

        var commandThreadId = 0;
        var command = new TestCommand();
        command.CanExecuteChanged += (sender, args) =>
        {
            commandThreadId = Thread.CurrentThread.ManagedThreadId;
            eventFlag.Set();
        };

        // Act
        command.RaiseCanExecuteChanged();

        // Assert
        Assert.IsTrue(eventFlag.Wait(TimeSpan.FromSeconds(1)));
        Assert.AreEqual(commandThreadId, testThreadId);
    }

    [TestMethod]
    public void RaiseCanExecuteChanged_CanExecuteChangedRaisedAsync()
    {
        // Arrange
        var eventFlag = new ManualResetEventSlim();
        var testThreadId = Thread.CurrentThread.ManagedThreadId;
        var textExecutor = new TestExecutor
        {
            IsDispatcherThread = false
        };

        Executers.DispatcherExecutor = textExecutor;

        var commandThreadId = 0;
        var command = new TestCommand();
        command.CanExecuteChanged += (sender, args) =>
        {
            commandThreadId = Thread.CurrentThread.ManagedThreadId;
            eventFlag.Set();
        };

        // Act
        command.RaiseCanExecuteChanged();

        // Assert
        Assert.IsTrue(eventFlag.Wait(TimeSpan.FromSeconds(1)));
        Assert.AreNotEqual(commandThreadId, 0);
        Assert.AreNotEqual(commandThreadId, testThreadId);
    }

    private IDispatcherExecutor? _originExecutor;
}
