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

namespace Kaspirin.UI.Framework.Tests.TestSuites.Threading;

[TestClass]
public sealed class LimitedTaskSchedulerTests
{
    [TestMethod]
    public void NegativeCountAllowedTasks()
    {
        ForbiddenCountTasks(-1);
    }

    [TestMethod]
    public void ZeroAllowedTasks()
    {
        ForbiddenCountTasks(0);
    }

    [TestMethod]
    public void MinimumCountTasksAvailable()
    {
        AllowedCountTasks(1);
    }

    [TestMethod]
    public void PositiveCountAllowedTasks()
    {
        AllowedCountTasks(100);
    }

    [TestMethod]
    public void MaxAllowedTasks()
    {
        AllowedCountTasks(1_000_000);
    }

    [TestMethod]
    public void OverflowAllowedTasks()
    {
        ForbiddenCountTasks(int.MaxValue);
    }

    private static void AllowedCountTasks(int value)
    {
        var taskScheduler = new LimitedTaskScheduler(value);

        Assert.IsTrue(value == taskScheduler.MaximumConcurrencyLevel);
    }

    private static void ForbiddenCountTasks(int value)
    {
        Assert.ThrowsException<GuardException>(() => new LimitedTaskScheduler(value));
    }
}
