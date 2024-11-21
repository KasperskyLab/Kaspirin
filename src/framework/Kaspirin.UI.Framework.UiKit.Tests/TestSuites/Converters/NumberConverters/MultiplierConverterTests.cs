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

using Kaspirin.UI.Framework.UiKit.Converters.NumberConverters;
using System;
using System.Globalization;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Converters.NumberConverters;

[TestClass]
public sealed class MultiplierConverterTests
{
    [TestMethod]
    public void Convert_NullValue_ReturnsUnsetValue()
    {
        // Arrange
        var converter = new MultiplierConverter();
        var value = default(object);
        var targetType = typeof(double);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.AreEqual(DependencyProperty.UnsetValue, result);
    }

    [TestMethod]
    public void Convert_InvalidValue_ReturnsUnsetValue()
    {
        // Arrange
        var converter = new MultiplierConverter();
        var value = "invalid";
        var targetType = typeof(double);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act + assert
        Assert.ThrowsException<FormatException>(() => converter.Convert(value, targetType, parameter, culture));
    }

    [TestMethod]
    public void Convert_ValidValue_NegativeFactor_ReturnsCorrectResult()
    {
        // Arrange
        var converter = new MultiplierConverter { Factor = -5 };
        var value = 2.0;
        var targetType = typeof(double);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, targetType);
        Assert.AreEqual(value * converter.Factor, result);
    }

    [TestMethod]
    public void Convert_ValidValue_ZeroFactor_ReturnsCorrectResult()
    {
        // Arrange
        var converter = new MultiplierConverter { Factor = 0 };
        var value = 5.0;
        var targetType = typeof(double);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, targetType);
        Assert.AreEqual(value * converter.Factor, result);
    }

    [TestMethod]
    public void Convert_ValidValue_PositiveFactor_ReturnsCorrectResult()
    {
        // Arrange
        var converter = new MultiplierConverter { Factor = 5 };
        var value = 10.0;
        var targetType = typeof(double);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, targetType);
        Assert.AreEqual(value * converter.Factor, result);
    }
}
