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

namespace Kaspirin.UI.Framework.Tests.TestSuites.Extensions;

[TestClass]
public sealed class StringExtensionTests
{
    #region ShiftRight Tests

    [TestMethod]
    public void ShiftRight_WithZeroIndentLevel_ReturnsOriginalString()
    {
        // Arrange
        const string original = "Hello\nWorld";

        // Act
        var result = original.ShiftRight(0);

        // Assert
        Assert.AreEqual(original, result);
    }

    [TestMethod]
    public void ShiftRight_WithEmptyString_ReturnsEmptyString()
    {
        // Arrange
        const string original = "";

        // Act
        var result = original.ShiftRight(2);

        // Assert
        Assert.AreEqual("", result);
    }

    [TestMethod]
    public void ShiftRight_WithSingleLine_AddsIndent()
    {
        // Arrange
        const string original = "Hello World";

        // Act
        var result = original.ShiftRight(1);

        // Assert
        Assert.AreEqual("\tHello World", result);
    }

    [TestMethod]
    public void ShiftRight_WithMultiLine_AddsIndentToAllLines()
    {
        // Arrange
        var original = $"Line 1{Environment.NewLine}Line 2{Environment.NewLine}Line 3";

        // Act
        var result = original.ShiftRight(2);

        // Assert
        Assert.AreEqual($"\t\tLine 1{Environment.NewLine}\t\tLine 2{Environment.NewLine}\t\tLine 3", result);
    }

    #endregion

    #region TrimEnd Tests

    [TestMethod]
    public void TrimEnd_WithEmptyString_ReturnsEmptyString()
    {
        // Arrange
        const string original = "";

        // Act
        var result = original.TrimEnd("test");

        // Assert
        Assert.AreEqual("", result);
    }

    [TestMethod]
    public void TrimEnd_WithSuffixPresent_RemovesSuffix()
    {
        // Arrange
        const string original = "Hello World!";

        // Act
        var result = original.TrimEnd("!");

        // Assert
        Assert.AreEqual("Hello World", result);
    }

    [TestMethod]
    public void TrimEnd_WithSuffixNotPresent_ReturnsOriginalString()
    {
        // Arrange
        const string original = "Hello World";

        // Act
        var result = original.TrimEnd("!");

        // Assert
        Assert.AreEqual(original, result);
    }

    #endregion

    #region TrimStart Tests

    [TestMethod]
    public void TrimStart_WithEmptyString_ReturnsEmptyString()
    {
        // Arrange
        const string original = "";

        // Act
        var result = original.TrimStart("test");

        // Assert
        Assert.AreEqual("", result);
    }

    [TestMethod]
    public void TrimStart_WithPrefixPresent_RemovesPrefix()
    {
        // Arrange
        const string original = "Hello World";

        // Act
        var result = original.TrimStart("Hello ");

        // Assert
        Assert.AreEqual("World", result);
    }

    [TestMethod]
    public void TrimStart_WithPrefixNotPresent_ReturnsOriginalString()
    {
        // Arrange
        const string original = "Hello World";

        // Act
        var result = original.TrimStart("World ");

        // Assert
        Assert.AreEqual(original, result);
    }

    #endregion
}
