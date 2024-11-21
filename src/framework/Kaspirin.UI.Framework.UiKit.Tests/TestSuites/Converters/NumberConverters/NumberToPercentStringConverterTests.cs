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
public sealed class NumberToPercentStringConverterTests : LocalizationManagerDependentTests
{
    [TestMethod]
    public void Convert_NullValue_ReturnsUnsetValue()
    {
        // Arrange
        InitializeLocalizationManager("en-US");

        var converter = new NumberToPercentStringConverter();
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

        var converter = new NumberToPercentStringConverter();
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
        var values = new Dictionary<(ushort Precision, object Value), string>()
        {
            //  Precision = 0
            { ( Precision: 0, -5), "-500%" },
            { ( Precision: 0, -1), "-100%" },
            { ( Precision: 0, -0.999), "-100%" },
            { ( Precision: 0, -0.153), "-15%" },
            { ( Precision: 0, -0.035), "-4%" },
            { ( Precision: 0, 0), "0%" },
            { ( Precision: 0, 0.069), "7%" },
            { ( Precision: 0, 0.2), "20%" },
            { ( Precision: 0, 0.999), "100%" },
            { ( Precision: 0, 1), "100%" },
            { ( Precision: 0, 5), "500%" },

            //  Precision = 1
            { ( Precision: 1, -5), "-500.0%" },
            { ( Precision: 1, -1), "-100.0%" },
            { ( Precision: 1, -0.9999), "-100.0%" },
            { ( Precision: 1, -0.999), "-99.9%" },
            { ( Precision: 1, -0.254), "-25.4%" },
            { ( Precision: 1, -0.0546), "-5.5%" },
            { ( Precision: 1, 0), "0.0%" },
            { ( Precision: 1, 0.0799), "8.0%" },
            { ( Precision: 1, 0.1), "10.0%" },
#if NETCOREAPP
            { ( Precision: 1, 0.5555), "55.5%" },
#else
            { ( Precision: 1, 0.5555), "55.6%" },
#endif
            { ( Precision: 1, 0.999), "99.9%" },
            { ( Precision: 1, 0.9999), "100.0%" },
            { ( Precision: 1, 1), "100.0%" },
            { ( Precision: 1, 5), "500.0%" },
        };

        InitializeLocalizationManager("en-US");

        foreach (var kvp in values)
        {
            // Arrange
            var converter = new NumberToPercentStringConverter { Precision = kvp.Key.Precision };

            var value = kvp.Key.Value;
            var targetType = typeof(string);
            var parameter = default(object);
            var culture = CultureInfo.InvariantCulture;

            // Act
            var result = converter.Convert(value, targetType, parameter, culture);

            // Assert
            Assert.IsInstanceOfType(result, targetType);
            Assert.AreEqual(kvp.Value, result, $"Precision={kvp.Key.Precision}");
        }
    }

    [TestMethod]
    public void Convert_ValidValues_RuRuFormatCulture_ReturnsFormattedString()
    {
        var values = new Dictionary<(ushort Precision, object Value), string>()
        {
            //  Precision = 0
#if NETCOREAPP
            { ( Precision: 0, -5), "-500 %" },
            { ( Precision: 0, -1), "-100 %" },
            { ( Precision: 0, -0.999), "-100 %" },
            { ( Precision: 0, -0.153), "-15 %" },
            { ( Precision: 0, -0.035), "-4 %" },
            { ( Precision: 0, 0), "0 %" },
            { ( Precision: 0, 0.069), "7 %" },
            { ( Precision: 0, 0.2), "20 %" },
            { ( Precision: 0, 0.999), "100 %" },
            { ( Precision: 0, 1), "100 %" },
            { ( Precision: 0, 5), "500 %" },
#else
            { ( Precision: 0, -5), "-500%" },
            { ( Precision: 0, -1), "-100%" },
            { ( Precision: 0, -0.999), "-100%" },
            { ( Precision: 0, -0.153), "-15%" },
            { ( Precision: 0, -0.035), "-4%" },
            { ( Precision: 0, 0), "0%" },
            { ( Precision: 0, 0.069), "7%" },
            { ( Precision: 0, 0.2), "20%" },
            { ( Precision: 0, 0.999), "100%" },
            { ( Precision: 0, 1), "100%" },
            { ( Precision: 0, 5), "500%" },
#endif

            //  Precision = 1
#if NETCOREAPP
            { ( Precision: 1, -5), "-500,0 %" },
            { ( Precision: 1, -1), "-100,0 %" },
            { ( Precision: 1, -0.9999), "-100,0 %" },
            { ( Precision: 1, -0.999), "-99,9 %" },
            { ( Precision: 1, -0.254), "-25,4 %" },
            { ( Precision: 1, -0.0546), "-5,5 %" },
            { ( Precision: 1, 0), "0,0 %" },
            { ( Precision: 1, 0.0799), "8,0 %" },
            { ( Precision: 1, 0.1), "10,0 %" },
            { ( Precision: 1, 0.5555), "55,5 %" },
            { ( Precision: 1, 0.999), "99,9 %" },
            { ( Precision: 1, 0.9999), "100,0 %" },
            { ( Precision: 1, 1), "100,0 %" },
            { ( Precision: 1, 5), "500,0 %" },
#else
            { ( Precision: 1, -5), "-500,0%" },
            { ( Precision: 1, -1), "-100,0%" },
            { ( Precision: 1, -0.9999), "-100,0%" },
            { ( Precision: 1, -0.999), "-99,9%" },
            { ( Precision: 1, -0.254), "-25,4%" },
            { ( Precision: 1, -0.0546), "-5,5%" },
            { ( Precision: 1, 0), "0,0%" },
            { ( Precision: 1, 0.0799), "8,0%" },
            { ( Precision: 1, 0.1), "10,0%" },
            { ( Precision: 1, 0.5555), "55,6%" },
            { ( Precision: 1, 0.999), "99,9%" },
            { ( Precision: 1, 0.9999), "100,0%" },
            { ( Precision: 1, 1), "100,0%" },
            { ( Precision: 1, 5), "500,0%" },
#endif
        };

        InitializeLocalizationManager("ru-RU");

        foreach (var kvp in values)
        {
            // Arrange
            var converter = new NumberToPercentStringConverter { Precision = kvp.Key.Precision };

            var value = kvp.Key.Value;
            var targetType = typeof(string);
            var parameter = default(object);
            var culture = CultureInfo.InvariantCulture;

            // Act
            var result = converter.Convert(value, targetType, parameter, culture);

            // Assert
            Assert.IsInstanceOfType(result, targetType);
            Assert.AreEqual(kvp.Value, result, $"Precision= {kvp.Key.Precision}");
        }
    }
}
