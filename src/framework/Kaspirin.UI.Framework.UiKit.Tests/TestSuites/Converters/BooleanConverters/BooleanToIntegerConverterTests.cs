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
public sealed class BooleanToIntegerConverterTests
{
    [TestMethod]
    public void Convert_TrueValue_Returns1()
    {
        // Arrange
        var converter = new BooleanToIntegerConverter();
        var value = true;
        var targetType = typeof(int);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, targetType);
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void Convert_FalseValue_Returns0()
    {
        // Arrange
        var converter = new BooleanToIntegerConverter();
        var value = false;
        var targetType = typeof(int);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, targetType);
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Convert_NullValue_Returns0()
    {
        // Arrange
        var converter = new BooleanToIntegerConverter();
        var value = default(object);
        var targetType = typeof(int);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, targetType);
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Convert_NonBooleanValue_ThrowsException()
    {
        // Arrange
        var converter = new BooleanToIntegerConverter();
        var value = "invalid";
        var targetType = typeof(int);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act + assert
        Assert.ThrowsException<FormatException>(() => converter.Convert(value, targetType, parameter, culture));
    }
}