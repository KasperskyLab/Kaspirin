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

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Converters.BooleanConverters;

[TestClass]
public sealed class MultiBooleanToObjectConverterTests
{
    [TestMethod]
    public void Convert_AllFalseValues_AndOperation_ReturnsFalseObject()
    {
        // Arrange
        var converter = new MultiBooleanToObjectConverter { Operation = MultiBooleanOperation.And, False = "False" };
        var values = new object?[] { false, false, false };
        var targetType = typeof(object);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(values, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, typeof(string));
        Assert.AreEqual("False", result);
    }

    [TestMethod]
    public void Convert_AllFalseValues_OrOperation_ReturnsFalseObject()
    {
        // Arrange
        var converter = new MultiBooleanToObjectConverter { Operation = MultiBooleanOperation.Or, False = "False" };
        var values = new object?[] { false, false, false };
        var targetType = typeof(object);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(values, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, typeof(string));
        Assert.AreEqual("False", result);
    }

    [TestMethod]
    public void Convert_AllTrueValues_AndOperation_ReturnsTrueObject()
    {
        // Arrange
        var converter = new MultiBooleanToObjectConverter { Operation = MultiBooleanOperation.And, True = "True" };
        var values = new object?[] { true, true, true };
        var targetType = typeof(object);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(values, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, typeof(string));
        Assert.AreEqual("True", result);
    }

    [TestMethod]
    public void Convert_AllTrueValues_OrOperation_ReturnsTrueObject()
    {
        // Arrange
        var converter = new MultiBooleanToObjectConverter { Operation = MultiBooleanOperation.Or, True = "True" };
        var values = new object?[] { true, true, true };
        var targetType = typeof(object);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(values, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, typeof(string));
        Assert.AreEqual("True", result);
    }

    [TestMethod]
    public void Convert_EmptyValues_AndOperation_ReturnsTrueObject()
    {
        // Arrange
        var converter = new MultiBooleanToObjectConverter { Operation = MultiBooleanOperation.And, True = "True" };
        var values = Array.Empty<object>();
        var targetType = typeof(object);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(values, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, typeof(string));
        Assert.AreEqual("True", result);
    }

    [TestMethod]
    public void Convert_EmptyValues_OrOperation_ReturnsFalseObject()
    {
        // Arrange
        var converter = new MultiBooleanToObjectConverter { Operation = MultiBooleanOperation.Or, False = "False" };
        var values = Array.Empty<object>();
        var targetType = typeof(object);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(values, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, typeof(string));
        Assert.AreEqual("False", result);
    }

    [TestMethod]
    public void Convert_InvalidValue_ThrowsException()
    {
        // Arrange
        var converter = new MultiBooleanToObjectConverter();
        var values = new object?[] { "invalid" };
        var targetType = typeof(bool);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act + assert
        Assert.ThrowsException<FormatException>(() => converter.Convert(values, targetType, parameter, culture));
    }

    [TestMethod]
    public void Convert_MixedBooleanValues_AndOperation_ReturnsFalseObject()
    {
        // Arrange
        var converter = new MultiBooleanToObjectConverter { Operation = MultiBooleanOperation.And, False = "False" };
        var values = new object?[] { true, false, true };
        var targetType = typeof(object);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(values, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, typeof(string));
        Assert.AreEqual("False", result);
    }

    [TestMethod]
    public void Convert_MixedBooleanValues_OrOperation_ReturnsTrueObject()
    {
        // Arrange
        var converter = new MultiBooleanToObjectConverter { Operation = MultiBooleanOperation.Or, True = "True" };
        var values = new object?[] { true, false, true };
        var targetType = typeof(object);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(values, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, typeof(string));
        Assert.AreEqual("True", result);
    }

    [TestMethod]
    public void Convert_NullValue_ReturnsFalseObject()
    {
        // Arrange
        var converter = new MultiBooleanToObjectConverter { False = "False" };
        var values = new object?[] { default };
        var targetType = typeof(bool);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(values, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, typeof(string));
        Assert.AreEqual("False", result);
    }

    [TestMethod]
    public void Convert_NullValues_ThrowsException()
    {
        // Arrange
        var converter = new MultiBooleanToObjectConverter();
        var values = default(object?[]);
        var targetType = typeof(bool);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act + assert
        Assert.ThrowsException<GuardException>(() => converter.Convert(values!, targetType, parameter, culture));
    }
}