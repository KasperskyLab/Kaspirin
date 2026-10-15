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
using System.Threading;
using Kaspirin.UI.Framework.Tests.TestHelpers;

namespace Kaspirin.UI.Framework.Tests.TestSuites.Mvvm;

[TestClass]
public sealed class NavigateCommandTests
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
    public void Execute_InvokesAction()
    {
        // Arrange
        var actionExecuted = false;
        void action() => actionExecuted = true;
        var command = new NavigateCommand(action, isExternalMethod: () => false);

        // Act
        command.Execute();

        // Assert
        Assert.IsTrue(actionExecuted);
    }

    [TestMethod]
    public void Execute_InvokesActionWithParameter()
    {
        // Arrange
        var actionExecuted = false;
        var expectedParameter = new object();
        void action(object? param)
        {
            actionExecuted = true;
            Assert.AreEqual(expectedParameter, param);
        }

        var command = new NavigateCommand<object>(action, isExternalMethod: () => false);

        // Act
        command.Execute(expectedParameter);

        // Assert
        Assert.IsTrue(actionExecuted);
    }

    [TestMethod]
    public void CanExecute_ReturnsCorrectValueBasedOnCanExecuteMethod()
    {
        // Arrange
        var canExecute = true;
        bool canExecuteMethod() => canExecute;
        var command = new NavigateCommand(() => { }, canExecuteMethod, isExternalMethod: () => false);

        // Act & Assert
        Assert.IsTrue(command.CanExecute());

        canExecute = false;
        Assert.IsFalse(command.CanExecute());
    }

    [TestMethod]
    public void CanExecute_ReturnsCorrectValueBasedOnCanExecuteMethodWithParameter()
    {
        // Arrange
        var canExecute = true;
        bool canExecuteMethod(object? param) => canExecute;
        var command = new NavigateCommand<object>(param => { }, canExecuteMethod, isExternalMethod: () => false);

        // Act & Assert
        Assert.IsTrue(command.CanExecute(new object()));

        canExecute = false;
        Assert.IsFalse(command.CanExecute(new object()));
    }

    [TestMethod]
    public void IsExternal_ReturnsFalseWhenConstructedAsNonExternal()
    {
        // Arrange
        var command = new NavigateCommand(() => { }, isExternalMethod: () => false);

        // Act & Assert
        Assert.IsFalse(command.IsExternal());
    }

    [TestMethod]
    public void IsExternal_ReturnsTrueWhenConstructedAsExternal()
    {
        // Arrange
        var command = new NavigateCommand(() => { }, isExternalMethod: () => true);

        // Act & Assert
        Assert.IsTrue(command.IsExternal());
    }

    [TestMethod]
    public void IsExternal_ReturnsCorrectValueBasedOnIsExternalMethod()
    {
        // Arrange
        var isExternal = true;
        bool isExternalMethod() => isExternal;
        var command = new NavigateCommand(() => { }, () => true, isExternalMethod);

        // Act & Assert
        Assert.IsTrue(command.IsExternal());

        isExternal = false;
        Assert.IsFalse(command.IsExternal());
    }

    [TestMethod]
    public void IsExternal_ReturnsTrueForGenericCommandConstructedAsExternal()
    {
        // Arrange
        var command = new NavigateCommand<object>(param => { }, isExternalMethod: () => true);

        // Act & Assert
        Assert.IsTrue(command.IsExternal());
    }

    [TestMethod]
    public void Execute_PassesNullToReferenceTypeParameter()
    {
        // Arrange
        var actionExecuted = false;
        void action(string? param)
        {
            actionExecuted = true;
            Assert.IsNull(param);
        }

        var command = new NavigateCommand<string>(action, isExternalMethod: () => false);

        // Act
        command.Execute((object?)null);

        // Assert
        Assert.IsTrue(actionExecuted);
    }

    [TestMethod]
    public void Execute_ThrowsArgumentNullExceptionWhenNullPassedToValueTypeParameter()
    {
        // Arrange
        var command = new NavigateCommand<int>(param => { }, isExternalMethod: () => false);

        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => command.Execute(null));
    }

    [TestMethod]
    public void Execute_ThrowsArgumentExceptionWhenParameterTypeMismatch()
    {
        // Arrange
        var command = new NavigateCommand<int>(param => { }, isExternalMethod: () => false);

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => command.Execute("not an int"));
    }

    [TestMethod]
    public void Execute_PassesNullToNullableValueTypeParameter()
    {
        // Arrange
        var actionExecuted = false;
        void action(int? param)
        {
            actionExecuted = true;
            Assert.IsFalse(param.HasValue);
        }

        var command = new NavigateCommand<int?>(action, isExternalMethod: () => false);

        // Act
        command.Execute(null);

        // Assert
        Assert.IsTrue(actionExecuted);
    }

    [TestMethod]
    public void Constructor_ThrowsWhenIsExternalMethodIsNull()
    {
        // Act & Assert
        Assert.ThrowsException<GuardException>(
            () => new NavigateCommand(() => { }, () => true, (Func<bool>)null!));
    }

    [TestMethod]
    public void RaiseIsExternalChanged_IsExternalChangedRaisedSync()
    {
        // Arrange
        var eventFlag = new ManualResetEventSlim();
        var testThreadId = Thread.CurrentThread.ManagedThreadId;
        var testExecutor = new TestExecutor
        {
            IsDispatcherThread = true
        };

        Executers.DispatcherExecutor = testExecutor;

        var commandThreadId = 0;
        var command = new NavigateCommand(() => { }, isExternalMethod: () => false);
        command.IsExternalChanged += (sender, args) =>
        {
            commandThreadId = Thread.CurrentThread.ManagedThreadId;
            eventFlag.Set();
        };

        // Act
        command.RaiseIsExternalChanged();

        // Assert
        Assert.IsTrue(eventFlag.Wait(TimeSpan.FromSeconds(1)));
        Assert.AreEqual(commandThreadId, testThreadId);
    }

    [TestMethod]
    public void RaiseIsExternalChanged_IsExternalChangedRaisedAsync()
    {
        // Arrange
        var eventFlag = new ManualResetEventSlim();
        var testThreadId = Thread.CurrentThread.ManagedThreadId;
        var testExecutor = new TestExecutor
        {
            IsDispatcherThread = false
        };

        Executers.DispatcherExecutor = testExecutor;

        var commandThreadId = 0;
        var command = new NavigateCommand(() => { }, isExternalMethod: () => false);
        command.IsExternalChanged += (sender, args) =>
        {
            commandThreadId = Thread.CurrentThread.ManagedThreadId;
            eventFlag.Set();
        };

        // Act
        command.RaiseIsExternalChanged();

        // Assert
        Assert.IsTrue(eventFlag.Wait(TimeSpan.FromSeconds(1)));
        Assert.AreNotEqual(commandThreadId, 0);
        Assert.AreNotEqual(commandThreadId, testThreadId);
    }

    private IDispatcherExecutor? _originExecutor;
}
