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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kaspirin.UI.Framework.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kaspirin.UI.Framework.Tests.TestSuites.Threading;

[TestClass]
public sealed class ActionQueueTests
{
    public ActionQueueTests()
    {
        _mockTimerFactory = new MockTimerFactory();
        _mockExecutor = new MockSyncedExecutor(_mockTimerFactory);
    }

    [TestMethod]
    public void ActionQueue_EnqueueAction_ExecutesAction()
    {
        var queue = new ActionQueue(_mockExecutor);
        var counter = 0;

        queue.Enqueue(() => counter++);
        Assert.AreEqual(1, counter);
    }

    [TestMethod]
    public void ActionQueue_EnqueueMultipleActions_ProcessesInOrder()
    {
        var queue = new ActionQueue(_mockExecutor);
        var order = new List<int>();

        queue.Enqueue(() => order.Add(1));
        queue.Enqueue(() => order.Add(2));
        queue.Enqueue(() => order.Add(3));

        CollectionAssert.AreEqual(new List<int> { 1, 2, 3 }, order);
    }

    [TestMethod]
    public void ActionQueue_EnqueueAfterProcessing_RestartsProcessor()
    {
        var queue = new ActionQueue(_mockExecutor);
        var counter = 0;

        queue.Enqueue(() => counter++);
        Assert.AreEqual(0, queue.Count);
        Assert.AreEqual(1, counter);

        queue.Enqueue(() => counter++);
        Assert.AreEqual(0, queue.Count);
        Assert.AreEqual(2, counter);
    }

    [TestMethod]
    public void ActionQueue_EnqueueWithException_Throws()
    {
        var queue = new ActionQueue(_mockExecutor);
        var queueEvent = new ManualResetEvent(false);
        var exceptionCaught = false;

        var queueTask = Task.Run(() => queue.Enqueue(() =>
        {
            queueEvent.WaitOne();
            throw new InvalidOperationException("Test exception");
        }));

        try
        {
            queueEvent.Set();
            queueTask.Wait();
        }
        catch (AggregateException ex)
        {
            exceptionCaught = true;
            Assert.IsTrue(ex.InnerExceptions.Any(e => e.Message == "Test exception"));
        }

        Assert.IsTrue(exceptionCaught);
    }

    private readonly MockTimerFactory _mockTimerFactory;
    private readonly MockSyncedExecutor _mockExecutor;
}