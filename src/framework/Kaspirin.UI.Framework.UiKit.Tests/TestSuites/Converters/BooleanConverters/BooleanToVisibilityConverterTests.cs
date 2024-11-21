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
public sealed class BooleanToVisibilityConverterTests
{
    [TestMethod]
    public void Convert_TrueValue_ReturnsVisible()
    {
        // Arrange
        var converter = new BooleanToVisibilityConverter();
        var value = true;
        var targetType = typeof(Visibility);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, targetType);
        Assert.AreEqual(Visibility.Visible, result);
    }

    [TestMethod]
    public void Convert_FalseValue_ReturnsCollapsed()
    {
        // Arrange
        var converter = new BooleanToVisibilityConverter();
        var value = false;
        var targetType = typeof(Visibility);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, targetType);
        Assert.AreEqual(Visibility.Collapsed, result);
    }

    [TestMethod]
    public void Convert_NullValue_ReturnsCollapsed()
    {
        // Arrange
        var converter = new BooleanToVisibilityConverter();
        var value = default(object);
        var targetType = typeof(Visibility);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, targetType);
        Assert.AreEqual(Visibility.Collapsed, result);
    }

    [TestMethod]
    public void Convert_NonBooleanValue_ThrowsException()
    {
        // Arrange
        var converter = new BooleanToVisibilityConverter();
        var value = "invalid";
        var targetType = typeof(Visibility);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act + assert
        Assert.ThrowsException<FormatException>(() => converter.Convert(value, targetType, parameter, culture));
    }
}