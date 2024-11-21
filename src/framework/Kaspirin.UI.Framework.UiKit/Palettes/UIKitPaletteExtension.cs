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
using System.Windows.Markup;
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Palettes
{
    [MarkupExtensionReturnType(typeof(Brush))]
    public abstract class UIKitPaletteExtension<TPaletteEnum> : MarkupExtension where TPaletteEnum : struct, Enum
    {
        public UIKitPaletteExtensionMode Mode { get; set; }

        public TPaletteEnum Id { get; set; }

        public override object? ProvideValue(IServiceProvider serviceProvider)
        {
            return new PaletteExtension()
            {
                IsColor = Mode == UIKitPaletteExtensionMode.Color,
                Key = Map(Id),
                Scope = UIKitConstants.PaletteScope
            }.ProvideValue(serviceProvider);
        }

        protected abstract string Map(TPaletteEnum id);

        private sealed class PaletteExtension : LocalizationMarkupBase
        {
            public PaletteExtension() : base(string.Empty) { }

            public bool IsColor { get; set; }

            protected override object? ProvideValue()
            {
                var resource = ProvideLocalizer<IXamlLocalizer>().GetResource(Key);
                if (resource is Color paletteColor)
                {
                    if (IsColor)
                    {
                        return paletteColor;
                    }
                    else
                    {
                        return new SolidColorBrush(paletteColor).GetAsFrozen();
                    }
                }

                return resource;
            }

            protected override ILocalizer PrepareLocalizer()
            {
                return LocalizationManager.Current.LocalizerFactory.Resolve<IXamlLocalizer>(Scope);
            }
        }
    }
}
