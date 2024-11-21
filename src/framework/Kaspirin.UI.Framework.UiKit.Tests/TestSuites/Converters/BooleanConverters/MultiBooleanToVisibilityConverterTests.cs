// Copyright © 2024 AO Kaspersky Lab.
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

using Kaspirin.UI.Framework.UiKit.Converters.BooleanConverters;
using System;
using System.Globalization;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Converters.BooleanConverters;

[TestClass]
public sealed class MultiBooleanToVisibilityConverterTests
{
    [TestMethod]
    public void Convert_AllFalseValues_AndOperation_ReturnsCollapsed()
    {
        // Arrange
        var converter = new MultiBooleanToVisibilityConverter { Operation = MultiBooleanOperation.And };
        var values = new object?[] { false, false, false };
        var targetType = typeof(Visibility);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(values, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, typeof(Visibility));
        Assert.AreEqual(Visibility.Collapsed, result);
    }

    [TestMethod]
    public void Convert_AllFalseValues_OrOperation_ReturnsCollapsed()
    {
        // Arrange
        var converter = new MultiBooleanToVisibilityConverter { Operation = MultiBooleanOperation.Or };
        var values = new object?[] { false, false, false };
        var targetType = typeof(Visibility);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(values, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, typeof(Visibility));
        Assert.AreEqual(Visibility.Collapsed, result);
    }

    [TestMethod]
    public void Convert_AllTrueValues_AndOperation_ReturnsVisible()
    {
        // Arrange
        var converter = new MultiBooleanToVisibilityConverter { Operation = MultiBooleanOperation.And };
        var values = new object?[] { true, true, true };
        var targetType = typeof(Visibility);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(values, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, typeof(Visibility));
        Assert.AreEqual(Visibility.Visible, result);
    }

    [TestMethod]
    public void Convert_AllTrueValues_OrOperation_ReturnsVisible()
    {
        // Arrange
        var converter = new MultiBooleanToVisibilityConverter { Operation = MultiBooleanOperation.Or };
        var values = new object?[] { true, true, true };
        var targetType = typeof(Visibility);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(values, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, typeof(Visibility));
        Assert.AreEqual(Visibility.Visible, result);
    }

    [TestMethod]
    public void Convert_EmptyValues_AndOperation_ReturnsVisible()
    {
        // Arrange
        var converter = new MultiBooleanToVisibilityConverter { Operation = MultiBooleanOperation.And };
        var values = Array.Empty<object>();
        var targetType = typeof(Visibility);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(values, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, typeof(Visibility));
        Assert.AreEqual(Visibility.Visible, result);
    }

    [TestMethod]
    public void Convert_EmptyValues_OrOperation_ReturnsCollapsed()
    {
        // Arrange
        var converter = new MultiBooleanToVisibilityConverter { Operation = MultiBooleanOperation.Or };
        var values = Array.Empty<object>();
        var targetType = typeof(Visibility);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(values, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, typeof(Visibility));
        Assert.AreEqual(Visibility.Collapsed, result);
    }

    [TestMethod]
    public void Convert_InvalidValue_ThrowsException()
    {
        // Arrange
        var converter = new MultiBooleanToVisibilityConverter();
        var values = new object?[] { "invalid" };
        var targetType = typeof(bool);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act + assert
        Assert.ThrowsException<FormatException>(() => converter.Convert(values, targetType, parameter, culture));
    }

    [TestMethod]
    public void Convert_MixedBooleanValues_AndOperation_ReturnsCollapsed()
    {
        // Arrange
        var converter = new MultiBooleanToVisibilityConverter { Operation = MultiBooleanOperation.And };
        var values = new object?[] { true, false, true };
        var targetType = typeof(Visibility);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(values, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, typeof(Visibility));
        Assert.AreEqual(Visibility.Collapsed, result);
    }

    [TestMethod]
    public void Convert_MixedBooleanValues_OrOperation_ReturnsVisible()
    {
        // Arrange
        var converter = new MultiBooleanToVisibilityConverter { Operation = MultiBooleanOperation.Or };
        var values = new object?[] { true, false, true };
        var targetType = typeof(Visibility);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(values, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, typeof(Visibility));
        Assert.AreEqual(Visibility.Visible, result);
    }

    [TestMethod]
    public void Convert_NullValue_ReturnsCollapsed()
    {
        // Arrange
        var converter = new MultiBooleanToVisibilityConverter();
        var values = new object?[] { default };
        var targetType = typeof(bool);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(values, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, typeof(Visibility));
        Assert.AreEqual(Visibility.Collapsed, result);
    }

    [TestMethod]
    public void Convert_NullValues_ThrowsException()
    {
        // Arrange
        var converter = new MultiBooleanToVisibilityConverter();
        var values = default(object?[]);
        var targetType = typeof(bool);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act + assert
        Assert.ThrowsException<GuardException>(() => converter.Convert(values!, targetType, parameter, culture));
    }
}