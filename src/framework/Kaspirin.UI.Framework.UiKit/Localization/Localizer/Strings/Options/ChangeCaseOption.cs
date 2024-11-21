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


namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Options
{
    public sealed class ChangeCaseOption : IStringLocalizerOption
    {
        public ChangeCaseOption(ChangeCaseOptionMode mode)
        {
            Mode = mode;
        }

        public ChangeCaseOptionMode Mode { get; }

        public string Apply(string value)
        {
            return ChangeCase(value);
        }

        private string ChangeCase(string resourceString)
        {
            if (string.IsNullOrEmpty(resourceString))
            {
                return resourceString;
            }

            if (Mode == ChangeCaseOptionMode.LowercaseFirstLetterOnly ||
                Mode == ChangeCaseOptionMode.LowercaseAll)
            {
                if (Mode == ChangeCaseOptionMode.LowercaseAll || resourceString.Length == 1)
                {
                    return resourceString.ToLower(LocalizationManager.Current.DisplayCulture.CultureInfo);
                }

                return char.ToLower(resourceString[0], LocalizationManager.Current.DisplayCulture.CultureInfo) + resourceString.Substring(1);
            }

            if (Mode == ChangeCaseOptionMode.UppercaseFirstLetterOnly ||
                Mode == ChangeCaseOptionMode.UppercaseAll)
            {
                if (Mode == ChangeCaseOptionMode.UppercaseAll || resourceString.Length == 1)
                {
                    return resourceString.ToUpper(LocalizationManager.Current.DisplayCulture.CultureInfo);
                }

                return char.ToUpper(resourceString[0], LocalizationManager.Current.DisplayCulture.CultureInfo) + resourceString.Substring(1);
            }

            return resourceString;
        }
    }
}
