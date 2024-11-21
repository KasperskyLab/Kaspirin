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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.Localization
{
    public sealed class LocalizationCultureInfo
    {

        public LocalizationCultureInfo(string cultureString)
        {
            SetCulture(cultureString);
        }

        /// <summary>
        /// For apps that target .NET Framework 4 or later and are running under Windows 10 or later, 
        /// culture names that correspond to valid BCP-47 language tags are supported.
        /// <para/>BCP-47 standard: https://www.rfc-editor.org/rfc/bcp/bcp47.txt
        /// </summary>
        /// <param name="cultureString"></param>
        public void SetCulture(string cultureString)
        {
            FullLocalization = Guard.EnsureArgumentIsNotNull(cultureString);

            var normalizedCulture = NormalizeCultureName(cultureString);

            var customizationPos = normalizedCulture.IndexOf("-x", StringComparison.InvariantCultureIgnoreCase); // Looking for private use subtag
            if (customizationPos > 0)
            {
                CultureTail = normalizedCulture.Substring(customizationPos + 1);
                Culture = normalizedCulture.Substring(0, customizationPos);
            }
            else
            {
                CultureTail = string.Empty;
                Culture = normalizedCulture;
            }

            var culture = CultureInfo.GetCultures(CultureTypes.AllCultures).FirstOrDefault(c => c.Name == Culture);
            if (culture != null)
            {
                CultureInfo = CreateCultureInfoWithOverrides(culture.Name);
                LanguageTag = GetNeutralCulture(culture).IetfLanguageTag;
                Region = culture.IsNeutralCulture ? string.Empty : ParseRegion(CultureInfo.Name);
            }
            else
            {
                CultureInfo = GetNeutralCulture(CultureInfo.CreateSpecificCulture(Culture));
                LanguageTag = CultureInfo.IetfLanguageTag;
                Region = Culture.Length <= LanguageTag.Length ? string.Empty : ParseRegion(Culture.Substring(LanguageTag.Length + 1));
            }

            XmlLanguage = GetXmlLanguage(CultureInfo);
            FlowDirection = GetFlowDirection(CultureInfo);
            CultureParts = GetCultureParts(Culture).Concat(GetCultureParts(CultureTail)).ToArray();
            CultureChanged.Invoke(this, EventArgs.Empty);
        }

        public CultureInfo CultureInfo { get; private set; } = default!;
        public XmlLanguage XmlLanguage { get; private set; } = default!;
        public FlowDirection FlowDirection { get; private set; }

        public string FullLocalization { get; private set; } = default!;
        public string[] CultureParts { get; private set; } = default!;
        public string Culture { get; private set; } = default!;
        public string CultureTail { get; private set; } = default!;
        public string LanguageTag { get; private set; } = default!;
        public string Region { get; private set; } = default!;

        public static XmlLanguage InvariantXmlLanguage { get; } = XmlLanguage.GetLanguage(CultureInfo.InvariantCulture.IetfLanguageTag);

        public event EventHandler CultureChanged = (_, _) => { };

        private static string ParseRegion(string region)
        {
            try
            {
                return new RegionInfo(region).TwoLetterISORegionName;
            }
            catch
            {
                return string.Empty;
            }
        }

        private static CultureInfo GetNeutralCulture(CultureInfo culture)
        {
            Guard.ArgumentIsNotNull(culture);
            while (!culture.IsNeutralCulture && !Equals(culture, CultureInfo.InvariantCulture))
            {
                culture = culture.Parent;
            }

            return culture;
        }

        private static XmlLanguage GetXmlLanguage(CultureInfo culture)
        {
            var languageTag = culture.IetfLanguageTag;
            var language = XmlLanguage.GetLanguage(languageTag);

            var compatibleCultureInfoField = typeof(XmlLanguage).GetField("_compatibleCulture", BindingFlags.Instance | BindingFlags.NonPublic);
            if (compatibleCultureInfoField is not null)
            {
                compatibleCultureInfoField.SetValue(language, culture);
            }

            if (culture.IsNeutralCulture)
            {
                var specificCultureInfoField = typeof(XmlLanguage).GetField("_specificCulture", BindingFlags.Instance | BindingFlags.NonPublic);
                if (specificCultureInfoField is not null)
                {
                    specificCultureInfoField.SetValue(language, CreateCultureInfoWithOverrides(language.GetSpecificCulture().Name));
                }
            }

            return language;
        }

        private static FlowDirection GetFlowDirection(CultureInfo cultureInfo)
            => cultureInfo.TextInfo.IsRightToLeft
                ? FlowDirection.RightToLeft
                : FlowDirection.LeftToRight;

        private static IEnumerable<string> GetCultureParts(string cultureString)
            => string.IsNullOrWhiteSpace(cultureString)
                ? new string[] { }
                : cultureString.Split('-');

        private static CultureInfo CreateCultureInfoWithOverrides(string name)
        {
            var culture = new CultureInfo(name);

            if (string.Equals(culture.Name, "en", StringComparison.OrdinalIgnoreCase))
            {
                CultureInfo.CurrentCulture.ClearCachedData();
                if (CultureInfo.CurrentCulture.NativeName.StartsWith("English", StringComparison.OrdinalIgnoreCase))
                {
                    culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                }
            }
            else if (string.Equals(culture.Name, "en-GB", StringComparison.OrdinalIgnoreCase))
            {
                culture.DateTimeFormat.AMDesignator = string.Empty;
                culture.DateTimeFormat.PMDesignator = string.Empty;
            }
            else if (string.Equals(culture.Name, "en-US", StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(culture.TwoLetterISOLanguageName, "de", StringComparison.OrdinalIgnoreCase))
            {
                culture.NumberFormat.PercentPositivePattern = 1;
                culture.NumberFormat.PercentNegativePattern = 1;
            }
            else if (string.Equals(culture.Name, "ru-RU", StringComparison.OrdinalIgnoreCase))
            {
                culture.DateTimeFormat.MonthDayPattern = "dd MMMM";
            }
            else if (string.Equals(culture.TwoLetterISOLanguageName, "ar", StringComparison.OrdinalIgnoreCase))
            {
                culture.NumberFormat.DigitSubstitution = DigitShapes.None;
            }

            return culture;
        }

        private static string NormalizeCultureName(string cultureName)
        {
            return cultureName.ToLowerInvariant() switch
            {
                "zh-cn" => "zh-Hans-CN",
                "zh-mo" => "zh-Hans-MO",
                "zh-sg" => "zh-Hans-SG",
                "zh-tw" => "zh-Hant-TW",
                "zh-hk" => "zh-Hant-HK",
                _ => cultureName
            };
        }
    }
}