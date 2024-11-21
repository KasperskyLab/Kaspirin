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

using System.Globalization;

namespace Kaspirin.UI.Framework.UiKit.Tests.Common
{
    public abstract class LocalizationManagerDependentTests
    {
        protected void InitializeLocalizationManager(string displayCulture, string? formatCulture = default)
        {
            Guard.ArgumentIsNotNull(displayCulture);

            var localizationManagerParameters = new LocalizationParameters(displayCulture)
            {
                FormatCulture = formatCulture ?? displayCulture,
            };

            LocalizationManager.Initialize(localizationManagerParameters);
        }

        protected void AssertAreEqualInDisplayCulture(string? expected, string? actual, bool ignoreCase = true)
            => AssertAreEqualInSpecificCulture(expected, actual, LocalizationManager.Current.DisplayCulture.CultureInfo, ignoreCase);

        protected void AssertAreEqualInFormatCulture(string? expected, string? actual, bool ignoreCase = true)
            => AssertAreEqualInSpecificCulture(expected, actual, LocalizationManager.Current.FormatCulture.CultureInfo, ignoreCase);

        private void AssertAreEqualInSpecificCulture(string? expected, string? actual, CultureInfo cultureInfo, bool ignoreCase)
            => Assert.IsTrue(Equals(expected, actual, cultureInfo, ignoreCase), $"Expected: {expected}, actual: {actual}");

        public static bool Equals(string? string1, string? string2, CultureInfo cultureInfo, bool ignoreCase)
        {
            Guard.ArgumentIsNotNull(cultureInfo);

            var compareResult = cultureInfo.CompareInfo.Compare(
                string1,
                string2,
                CompareOptions.IgnoreSymbols | (ignoreCase
                    ? CompareOptions.IgnoreCase
                    : CompareOptions.None));

            return compareResult == 0;
        }
    }
}
