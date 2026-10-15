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
using System.Collections.Generic;
using System.Threading;
using Kaspirin.UI.Framework.Tests.TestHelpers;

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

    [TestMethod]
    public void Execute_RaisesExecutingBeforeExecuteCore()
    {
        // Arrange
        var sequence = new List<string>();
        var command = new TestCommand(_ => sequence.Add("execute"));
        command.Executing += (_, __) => sequence.Add("executing");

        // Act
        command.Execute(null);

        // Assert
        CollectionAssert.AreEqual(new[] { "executing", "execute" }, sequence);
    }

    [TestMethod]
    public void Execute_PassesParameterToExecuting()
    {
        // Arrange
        var expectedParameter = new object();
        object? actualParameter = null;
        var command = new TestCommand(_ => { });
        command.Executing += (s, e) => actualParameter = e.Parameter;

        // Act
        command.Execute(expectedParameter);

        // Assert
        Assert.AreEqual(expectedParameter, actualParameter);
    }

    [TestMethod]
    public void Execute_InvokesAllExecutingHandlers()
    {
        // Arrange
        var firstInvoked = 0;
        var secondInvoked = 0;
        var command = new TestCommand(_ => { });
        command.Executing += (_, __) => firstInvoked++;
        command.Executing += (_, __) => secondInvoked++;

        // Act
        command.Execute(null);

        // Assert
        Assert.AreEqual(1, firstInvoked);
        Assert.AreEqual(1, secondInvoked);
    }

    [TestMethod]
    public void Execute_DoesNotInvokeUnsubscribedExecutingHandler()
    {
        // Arrange
        var invoked = 0;
        var command = new TestCommand(_ => { });
        void Handler(object? sender, CommandEventArgs args) => invoked++;
        command.Executing += Handler;
        command.Executing -= Handler;

        // Act
        command.Execute(null);

        // Assert
        Assert.AreEqual(0, invoked);
    }

    [TestMethod]
    public void Execute_RaisesExecutingButSkipsExecuteCoreWhenCanExecuteIsFalse()
    {
        // Arrange
        var executingInvoked = false;
        var actionInvoked = false;
        var command = new TestCommand(_ => actionInvoked = true, _ => false);
        command.Executing += (_, __) => executingInvoked = true;

        // Act
        command.Execute(null);

        // Assert
        Assert.IsTrue(executingInvoked);
        Assert.IsFalse(actionInvoked);
    }

    [TestMethod]
    public void Execute_InvokesHooksInExpectedOrder()
    {
        // Arrange
        var sequence = new List<string>();
        var command = new TestCommand(_ => sequence.Add("core"));
        void StaticExecuting(object sender, CommandEventArgs e) => sequence.Add("static-executing");
        void StaticExecuted(object sender, CommandEventArgs e) => sequence.Add("static-executed");
        command.Executing += (_, __) => sequence.Add("executing");
        command.Executed += (_, __) => sequence.Add("executed");
        BaseCommand.AddExecutingHandler(StaticExecuting);
        BaseCommand.AddExecutedHandler(StaticExecuted);

        try
        {
            // Act
            command.Execute(null);

            // Assert
            CollectionAssert.AreEqual(
                new[] { "static-executing", "executing", "core", "executed", "static-executed" },
                sequence);
        }
        finally
        {
            BaseCommand.RemoveExecutingHandler(StaticExecuting);
            BaseCommand.RemoveExecutedHandler(StaticExecuted);
        }
    }

    [TestMethod]
    public void Execute_PassesSameEventArgsToAllHooks()
    {
        // Arrange
        var seen = new List<CommandEventArgs>();
        var command = new TestCommand(_ => { });
        void StaticExecuting(object sender, CommandEventArgs e) => seen.Add(e);
        void StaticExecuted(object sender, CommandEventArgs e) => seen.Add(e);
        command.Executing += (_, e) => seen.Add(e);
        command.Executed += (_, e) => seen.Add(e);
        BaseCommand.AddExecutingHandler(StaticExecuting);
        BaseCommand.AddExecutedHandler(StaticExecuted);

        try
        {
            // Act
            command.Execute(null);

            // Assert
            Assert.AreEqual(4, seen.Count);
            Assert.IsTrue(seen.TrueForAll(args => ReferenceEquals(args, seen[0])));
        }
        finally
        {
            BaseCommand.RemoveExecutingHandler(StaticExecuting);
            BaseCommand.RemoveExecutedHandler(StaticExecuted);
        }
    }

    [TestMethod]
    public void Execute_StaticExecutingHandledSkipsInstancePipelineButRunsStaticExecuted()
    {
        // Arrange
        var sequence = new List<string>();
        var command = new TestCommand(_ => sequence.Add("core"));
        void StaticExecuting(object sender, CommandEventArgs e)
        {
            sequence.Add("static-executing");
            e.Handled = true;
        }

        void StaticExecuted(object sender, CommandEventArgs e) => sequence.Add("static-executed");
        command.Executing += (_, __) => sequence.Add("executing");
        command.Executed += (_, __) => sequence.Add("executed");
        BaseCommand.AddExecutingHandler(StaticExecuting);
        BaseCommand.AddExecutedHandler(StaticExecuted);

        try
        {
            // Act
            command.Execute(null);

            // Assert
            CollectionAssert.AreEqual(new[] { "static-executing", "static-executed" }, sequence);
        }
        finally
        {
            BaseCommand.RemoveExecutingHandler(StaticExecuting);
            BaseCommand.RemoveExecutedHandler(StaticExecuted);
        }
    }

    [TestMethod]
    public void Execute_ExecutingHandledSkipsCoreAndExecutedButRunsStaticExecuted()
    {
        // Arrange
        var sequence = new List<string>();
        var command = new TestCommand(_ => sequence.Add("core"));
        void StaticExecuted(object sender, CommandEventArgs e) => sequence.Add("static-executed");
        command.Executing += (_, e) =>
        {
            sequence.Add("executing");
            e.Handled = true;
        };
        command.Executed += (_, __) => sequence.Add("executed");
        BaseCommand.AddExecutedHandler(StaticExecuted);

        try
        {
            // Act
            command.Execute(null);

            // Assert
            CollectionAssert.AreEqual(new[] { "executing", "static-executed" }, sequence);
        }
        finally
        {
            BaseCommand.RemoveExecutedHandler(StaticExecuted);
        }
    }

    [TestMethod]
    public void Execute_StaticExecutedReceivesHandledFlagWhenVetoed()
    {
        // Arrange
        bool? handledSeenByStaticExecuted = null;
        var command = new TestCommand(_ => { });
        void StaticExecuting(object sender, CommandEventArgs e) => e.Handled = true;
        void StaticExecuted(object sender, CommandEventArgs e) => handledSeenByStaticExecuted = e.Handled;
        BaseCommand.AddExecutingHandler(StaticExecuting);
        BaseCommand.AddExecutedHandler(StaticExecuted);

        try
        {
            // Act
            command.Execute(null);

            // Assert
            Assert.IsTrue(handledSeenByStaticExecuted);
        }
        finally
        {
            BaseCommand.RemoveExecutingHandler(StaticExecuting);
            BaseCommand.RemoveExecutedHandler(StaticExecuted);
        }
    }

    [TestMethod]
    public void RemoveExecutingHandler_StopsGlobalInvocation()
    {
        // Arrange
        var invoked = 0;
        var command = new TestCommand(_ => { });
        void StaticExecuting(object sender, CommandEventArgs e) => invoked++;
        BaseCommand.AddExecutingHandler(StaticExecuting);
        BaseCommand.RemoveExecutingHandler(StaticExecuting);

        // Act
        command.Execute(null);

        // Assert
        Assert.AreEqual(0, invoked);
    }

    [TestMethod]
    public void RemoveExecutedHandler_StopsGlobalInvocation()
    {
        // Arrange
        var invoked = 0;
        var command = new TestCommand(_ => { });
        void StaticExecuted(object sender, CommandEventArgs e) => invoked++;
        BaseCommand.AddExecutedHandler(StaticExecuted);
        BaseCommand.RemoveExecutedHandler(StaticExecuted);

        // Act
        command.Execute(null);

        // Assert
        Assert.AreEqual(0, invoked);
    }

    private IDispatcherExecutor? _originExecutor;
}
