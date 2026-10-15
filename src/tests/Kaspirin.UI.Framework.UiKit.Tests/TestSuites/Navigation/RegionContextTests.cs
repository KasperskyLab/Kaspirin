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

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Navigation;

[TestClass]
public sealed class RegionContextTests : BaseTests
{
    [TestMethod]
    public void Add_NewKey_StoresValueAndRaisesContextChanged()
    {
        // Arrange
        var context = new RegionContext();
        var changedKeys = new List<string>();
        context.ContextChanged += changedKeys.Add;

        // Act
        context.Add("UserId", 42);

        // Assert
        Assert.AreEqual(1, context.Count);
        Assert.IsTrue(context.Contains("UserId"));
        Assert.AreEqual(42, context["UserId"]);
        CollectionAssert.AreEqual(new[] { "UserId" }, changedKeys);
    }

    [TestMethod]
    public void Add_ExistingKey_Throws()
    {
        // Arrange
        var context = new RegionContext();
        context.Add("UserId", 42);

        // Act & Assert
        Assert.ThrowsException<ArgumentException>(() => context.Add("UserId", 43));
    }

    [TestMethod]
    public void Update_ExistingKey_OverwritesValueAndRaisesContextChanged()
    {
        // Arrange
        var context = new RegionContext();
        var changedKeys = new List<string>();
        context.Add("UserId", 42);
        context.ContextChanged += changedKeys.Add;

        // Act
        context.Update("UserId", 43);

        // Assert
        Assert.AreEqual(43, context["UserId"]);
        CollectionAssert.AreEqual(new[] { "UserId" }, changedKeys);
    }

    [TestMethod]
    public void Update_NewKey_AddsValue()
    {
        // Arrange
        var context = new RegionContext();

        // Act
        context.Update("UserId", 42);

        // Assert
        Assert.AreEqual(42, context["UserId"]);
    }

    [TestMethod]
    public void Remove_ExistingKey_RemovesValueAndRaisesContextChanged()
    {
        // Arrange
        var context = new RegionContext();
        var changedKeys = new List<string>();
        context.Add("UserId", 42);
        context.ContextChanged += changedKeys.Add;

        // Act
        var removed = context.Remove("UserId");

        // Assert
        Assert.IsTrue(removed);
        Assert.IsFalse(context.Contains("UserId"));
        CollectionAssert.AreEqual(new[] { "UserId" }, changedKeys);
    }

    [TestMethod]
    public void Remove_MissingKey_ReturnsFalseAndDoesNotRaiseContextChanged()
    {
        // Arrange
        var context = new RegionContext();
        var changedKeys = new List<string>();
        context.ContextChanged += changedKeys.Add;

        // Act
        var removed = context.Remove("UserId");

        // Assert
        Assert.IsFalse(removed);
        Assert.AreEqual(0, changedKeys.Count);
    }

    [TestMethod]
    public void TryGet_ExistingKeyOfMatchingType_ReturnsTypedValue()
    {
        // Arrange
        var context = new RegionContext();
        context.Add("UserId", 42);

        // Act
        var found = context.TryGet<int>("UserId", out var value);

        // Assert
        Assert.IsTrue(found);
        Assert.AreEqual(42, value);
    }

    [TestMethod]
    public void TryGet_ExistingKeyOfWrongType_ReturnsFalse()
    {
        // Arrange
        var context = new RegionContext();
        context.Add("UserId", 42);

        // Act
        var found = context.TryGet<string>("UserId", out var value);

        // Assert
        Assert.IsFalse(found);
        Assert.IsNull(value);
    }

    [TestMethod]
    public void Indexer_MissingKey_ReturnsNull()
    {
        // Arrange
        var context = new RegionContext();

        // Act
        var value = context["UserId"];

        // Assert
        Assert.IsNull(value);
    }

    [TestMethod]
    public void Keys_ReturnsAllAddedKeys()
    {
        // Arrange
        var context = new RegionContext();
        context.Add("UserId", 42);
        context.Add("UserName", "John");

        // Act
        var keys = context.Keys;

        // Assert
        CollectionAssert.AreEquivalent(new[] { "UserId", "UserName" }, new List<string>(keys));
    }
}
