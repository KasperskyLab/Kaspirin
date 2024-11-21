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
using System.Text.RegularExpressions;

namespace Kaspirin.UI.Framework.UiKit.Icons
{
    internal sealed class UIKitIconBuilder
    {
        public UIKitIconBuilder(Enum icon)
        {
            IconKey = _iconTemplate;
            IconScope = UIKitConstants.IconsScope;

            if (icon is UIKitIcon_12 icon12)
            {
                SetName(icon12.ToString());
                SetSize("12");
            }
            else if (icon is UIKitIcon_16 icon16)
            {
                SetName(icon16.ToString());
                SetSize("16");
            }
            else if (icon is UIKitIcon_24 icon24)
            {
                SetName(icon24.ToString());
                SetSize("24");
            }
            else if (icon is UIKitIcon_32 icon32)
            {
                SetName(icon32.ToString());
                SetSize("32");
            }
            else if (icon is UIKitIcon_48 icon48)
            {
                SetName(icon48.ToString());
                SetSize("48");
            }
            else
            {
                throw new ArgumentException($"Converting value '{icon}' is not valid UIKit icon value");
            }

            Parse();
        }

        public UIKitIconBuilder(string key)
        {
            IconKey = key;
            IconScope = UIKitConstants.IconsScope;

            Parse();
        }

        public string? IconName { get; private set; }
        public int IconSize { get; private set; }
        public string IconKey { get; private set; }
        public string IconScope { get; private set; }
        public Enum? Icon { get; private set; }

        private void SetName(string name)
        {
            IconKey = IconKey.Replace(NameMask, name);
        }

        private void SetSize(string name)
        {
            IconKey = IconKey.Replace(SizeMask, name);
        }

        private void Parse()
        {
            var match = _iconRegex.Match(IconKey);
            if (match.Success)
            {
                IconSize = int.Parse(match.Groups[2].Value);
                Icon = ConvertToEnum(match.Groups[1].Value, IconSize);
                IconName = Icon?.ToString();
            }
            else
            {
                IconName = null;
                IconSize = default;
                Icon = null;
            }
        }

        private static Enum? ConvertToEnum(string iconName, int iconSize)
        {
            return iconSize switch
            {
                12 => (Enum)Enum.Parse(typeof(UIKitIcon_12), iconName, ignoreCase: true),
                16 => (Enum)Enum.Parse(typeof(UIKitIcon_16), iconName, ignoreCase: true),
                24 => (Enum)Enum.Parse(typeof(UIKitIcon_24), iconName, ignoreCase: true),
                32 => (Enum)Enum.Parse(typeof(UIKitIcon_32), iconName, ignoreCase: true),
                48 => (Enum)Enum.Parse(typeof(UIKitIcon_48), iconName, ignoreCase: true),
                _ => null,
            };
        }

        private const string NameMask = "%NAME%";
        private const string SizeMask = "%SIZE%";

        private readonly string _iconTemplate = $"UIKitIcon_{NameMask}_{SizeMask}.svg";

        private readonly Regex _iconRegex = new("UIKitIcon_(.*)_(.*).svg", RegexOptions.IgnoreCase);
    }
}
