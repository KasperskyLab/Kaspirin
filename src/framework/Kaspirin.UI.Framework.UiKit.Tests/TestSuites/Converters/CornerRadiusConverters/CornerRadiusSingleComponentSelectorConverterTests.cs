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

using Kaspirin.UI.Framework.UiKit.Converters.CornerRadiusConverters;
using System.Globalization;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Converters.CornerRadiusConverters;

[TestClass]
public sealed class CornerRadiusSingleComponentSelectorConverterTests
{
    [TestMethod]
    public void Convert_EqualComponentsValue_ReturnsSingleComponent()
    {
        // Arrange
        var converter = new CornerRadiusSingleComponentSelectorConverter();
        var value = new CornerRadius(1.0, 1.0, 1.0, 1.0);
        var targetType = typeof(double);
        var parameter = CornerRadiusSingleComponent.All;
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, targetType);
        Assert.AreEqual(1.0, result);
    }

    [TestMethod]
    public void Convert_NonEqualComponentsValue_ThrowsException()
    {
        // Arrange
        var converter = new CornerRadiusSingleComponentSelectorConverter();
        var value = new CornerRadius(1.0, 2.0, 3.0, 4.0);
        var targetType = typeof(double);
        var parameter = CornerRadiusSingleComponent.All;
        var culture = CultureInfo.InvariantCulture;

        // Act + assert
        Assert.ThrowsException<GuardException>(() => converter.Convert(value, targetType, parameter, culture));
    }

    [TestMethod]
    public void Convert_ValidValue_ReturnsTopLeftComponent()
    {
        // Arrange
        var converter = new CornerRadiusSingleComponentSelectorConverter();
        var value = new CornerRadius(1.0, 2.0, 3.0, 4.0);
        var targetType = typeof(double);
        var parameter = CornerRadiusSingleComponent.TopLeft;
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, targetType);
        Assert.AreEqual(1.0, result);
    }

    [TestMethod]
    public void Convert_ValidValue_ReturnsTopRightComponent()
    {
        // Arrange
        var converter = new CornerRadiusSingleComponentSelectorConverter();
        var value = new CornerRadius(1.0, 2.0, 3.0, 4.0);
        var targetType = typeof(double);
        var parameter = CornerRadiusSingleComponent.TopRight;
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, targetType);
        Assert.AreEqual(2.0, result);
    }

    [TestMethod]
    public void Convert_ValidValue_ReturnsBottomRightComponent()
    {
        // Arrange
        var converter = new CornerRadiusSingleComponentSelectorConverter();
        var value = new CornerRadius(1.0, 2.0, 3.0, 4.0);
        var targetType = typeof(double);
        var parameter = CornerRadiusSingleComponent.BottomRight;
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, targetType);
        Assert.AreEqual(3.0, result);
    }

    [TestMethod]
    public void Convert_ValidValue_ReturnsBottomLeftComponent()
    {
        // Arrange
        var converter = new CornerRadiusSingleComponentSelectorConverter();
        var value = new CornerRadius(1.0, 2.0, 3.0, 4.0);
        var targetType = typeof(double);
        var parameter = CornerRadiusSingleComponent.BottomLeft;
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.IsInstanceOfType(result, targetType);
        Assert.AreEqual(4.0, result);
    }

    [TestMethod]
    public void Convert_InvalidValue_ReturnsUnsetValue()
    {
        // Arrange
        var converter = new CornerRadiusSingleComponentSelectorConverter();
        var value = "invalid";
        var targetType = typeof(double);
        var parameter = default(CornerRadiusSingleComponent);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.AreEqual(DependencyProperty.UnsetValue, result);
    }

    [TestMethod]
    public void Convert_InvalidParameter_ReturnsUnsetValue()
    {
        // Arrange
        var converter = new CornerRadiusSingleComponentSelectorConverter();
        var value = default(CornerRadius);
        var targetType = typeof(double);
        var parameter = "invalid";
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.AreEqual(DependencyProperty.UnsetValue, result);
    }

    [TestMethod]
    public void Convert_NullValue_ThrowsException()
    {
        // Arrange
        var converter = new CornerRadiusSingleComponentSelectorConverter();
        var value = default(object);
        var targetType = typeof(double);
        var parameter = default(CornerRadiusSingleComponent);
        var culture = CultureInfo.InvariantCulture;

        // Act + assert
        Assert.ThrowsException<GuardException>(() => converter.Convert(value, targetType, parameter, culture));
    }

    [TestMethod]
    public void Convert_NullParameter_ThrowsException()
    {
        // Arrange
        var converter = new CornerRadiusSingleComponentSelectorConverter();
        var value = default(CornerRadius);
        var targetType = typeof(double);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act + assert
        Assert.ThrowsException<GuardException>(() => converter.Convert(value, targetType, parameter, culture));
    }
}
