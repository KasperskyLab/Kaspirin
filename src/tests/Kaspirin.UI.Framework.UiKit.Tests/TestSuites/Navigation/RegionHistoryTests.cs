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

using System.Linq;
using Kaspirin.UI.Framework.UiKit.Navigation;
using Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Navigation.Infrastructure;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Navigation;

[TestClass]
public sealed class RegionHistoryTests : BaseTests
{
    [TestMethod]
    public void Push_TwoItems_CurrentIsLastPushed()
    {
        // Arrange
        var history = new RegionHistory();

        // Act
        history.Push("A");
        history.Push("B");

        // Assert
        Assert.AreEqual(2, history.Count);
        Assert.AreEqual("B", history.Current);
        Assert.AreEqual("A", history.Previous);
    }

    [TestMethod]
    public void Pop_ReturnsCurrentAndRemovesIt()
    {
        // Arrange
        var history = new RegionHistory();
        history.Push("A");
        history.Push("B");

        // Act
        var popped = history.Pop();

        // Assert
        Assert.AreEqual("B", popped);
        Assert.AreEqual("A", history.Current);
        Assert.IsNull(history.Previous);
    }

    [TestMethod]
    public void Pop_EmptyHistory_ReturnsNull()
    {
        // Arrange
        var history = new RegionHistory();

        // Act
        var popped = history.Pop();

        // Assert
        Assert.IsNull(popped);
    }

    [TestMethod]
    public void PopRange_RemovesRequestedNumberOfItems()
    {
        // Arrange
        var history = new RegionHistory();
        history.Push("A");
        history.Push("B");
        history.Push("C");

        // Act
        var popped = history.PopRange(2);

        // Assert
        CollectionAssert.AreEqual(new[] { "C", "B" }, popped.ToArray());
        Assert.AreEqual("A", history.Current);
    }

    [TestMethod]
    public void Contains_EmptyName_ReturnsFalse()
    {
        // Arrange
        var history = new RegionHistory();
        history.Push("A");

        // Act & Assert
        Assert.IsFalse(history.Contains(string.Empty));
        Assert.IsFalse(history.Contains(item: null));
    }

    [TestMethod]
    public void Contains_ByViewType_FindsPushedName()
    {
        // Arrange
        var history = new RegionHistory();
        history.Push(typeof(TestViewA).FullName!);

        // Act & Assert
        Assert.IsTrue(history.Contains<TestViewA>());
        Assert.IsFalse(history.Contains<TestViewB>());
    }

    [TestMethod]
    public void Remove_ByViewType_RemovesEntry()
    {
        // Arrange
        var history = new RegionHistory();
        history.Push(typeof(TestViewA).FullName!);
        history.Push(typeof(TestViewB).FullName!);

        // Act
        var removed = history.Remove<TestViewA>();

        // Assert
        Assert.IsTrue(removed);
        Assert.IsFalse(history.Contains<TestViewA>());
        Assert.AreEqual(1, history.Count);
    }

    [TestMethod]
    public void Remove_MissingName_ReturnsFalse()
    {
        // Arrange
        var history = new RegionHistory();
        history.Push("A");

        // Act & Assert
        Assert.IsFalse(history.Remove("B"));
    }

    [TestMethod]
    public void Clear_RemovesAllItems()
    {
        // Arrange
        var history = new RegionHistory();
        history.Push("A");
        history.Push("B");

        // Act
        history.Clear();

        // Assert
        Assert.AreEqual(0, history.Count);
        Assert.IsNull(history.Current);
    }

    [TestMethod]
    public void GetEnumerator_ReturnsItemsInHistoryOrder()
    {
        // Arrange
        var history = new RegionHistory();
        history.Push("A");
        history.Push("B");

        // Act
        var items = history.ToArray();

        // Assert
        CollectionAssert.AreEqual(new[] { "B", "A" }, items);
    }
}
