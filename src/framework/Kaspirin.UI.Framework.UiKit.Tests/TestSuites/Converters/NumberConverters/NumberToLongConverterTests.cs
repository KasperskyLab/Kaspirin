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
using Kaspirin.UI.Framework.UiKit.Tests.Common;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Converters.NumberConverters;

[TestClass]
public sealed class NumberToLongConverterTests : LocalizationManagerDependentTests
{
    [TestMethod]
    public void Convert_NullValue_ReturnsUnsetValue()
    {
        // Arrange
        InitializeLocalizationManager("en-US");

        var converter = new NumberToLongConverter();
        var value = default(object);
        var targetType = typeof(string);
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
        InitializeLocalizationManager("en-US");

        var converter = new NumberToLongConverter();
        var value = "invalid";
        var targetType = typeof(string);
        var parameter = default(object);
        var culture = CultureInfo.InvariantCulture;

        // Act
        var result = converter.Convert(value, targetType, parameter, culture);

        // Assert
        Assert.AreEqual(DependencyProperty.UnsetValue, result);
    }

    [TestMethod]
    public void Convert_ValidValues_EnUsFormatCulture_ReturnsFormattedString()
    {
        var values = new Dictionary<object, string>()
        {
            { sbyte.MinValue, "-128" },
            { byte.MaxValue, "255" },
            { short.MinValue, "-32,768" },
            { ushort.MaxValue, "65,535" },
            { int.MinValue, "-2,147,483,648" },
            { uint.MaxValue, "4,294,967,295" },
            { long.MinValue, "-9,223,372,036,854,775,808" },
            { ulong.MaxValue, "18,446,744,073,709,551,615" },
            { 5.3f, "5" },
            { 5.5f, "6" },
            { 5.7f, "6" },
            { 1_248.2d, "1,248" },
#if NETCOREAPP
            { 1_248.5d, "1,248" },
#else
            { 1_248.5d, "1,249" },
#endif
            { 1_248.8d, "1,249" },
            { 999_999_999.11m, "999,999,999" },
            { 999_999_999.55m, "1,000,000,000" },
            { 999_999_999.88m, "1,000,000,000" }
        };

        InitializeLocalizationManager("en-US");

        foreach (var kvp in values)
        {
            // Arrange
            var converter = new NumberToLongConverter();
            var value = kvp.Key;
            var targetType = typeof(string);
            var parameter = default(object);
            var culture = CultureInfo.InvariantCulture;

            // Act
            var result = converter.Convert(value, targetType, parameter, culture);

            // Assert
            Assert.IsInstanceOfType(result, targetType);
            AssertAreEqualInFormatCulture(kvp.Value, (string?)result);
        }
    }

    [TestMethod]
    public void Convert_ValidValues_RuRuFormatCulture_ReturnsFormattedString()
    {
        var values = new Dictionary<object, string>()
        {
            { sbyte.MinValue, "-128" },
            { byte.MaxValue, "255" },
            { short.MinValue, "-32,768" },
            { ushort.MaxValue, "65,535" },
            { int.MinValue, "-2 147 483 648" },
            { uint.MaxValue, "4 294 967 295" },
            { long.MinValue, "-9 223 372 036 854 775 808" },
            { ulong.MaxValue, "18 446 744 073 709 551 615" },
            { 5.3f, "5" },
            { 5.5f, "6" },
            { 5.7f, "6" },
            { 1_248.2d, "1 248" },
#if NETCOREAPP
            { 1_248.5d, "1 248" },
#else
            { 1_248.5d, "1 249" },
#endif
            { 1_248.8d, "1 249" },
            { 999_999_999.11m, "999 999 999" },
            { 999_999_999.55m, "1 000 000 000" },
            { 999_999_999.88m, "1 000 000 000" }
        };

        InitializeLocalizationManager("ru-RU");

        foreach (var kvp in values)
        {
            // Arrange
            var converter = new NumberToLongConverter();
            var value = kvp.Key;
            var targetType = typeof(string);
            var parameter = default(object);
            var culture = CultureInfo.InvariantCulture;

            // Act
            var result = converter.Convert(value, targetType, parameter, culture);

            // Assert
            Assert.IsInstanceOfType(result, targetType);
            AssertAreEqualInFormatCulture(kvp.Value, (string?)result);
        }
    }
}
