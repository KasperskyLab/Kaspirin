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
using System.Windows;
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.MarkupExtensions.Common
{
    /// <summary>
    /// Helpful markup extension that simplifies definition of Thickness in XAML (usually used for Margin and Padding properties).<para/>
    /// All values (except <see cref="Offset"/>) are relative and have multiplier semantics.<para/>
    /// At the same time <see cref="Offset"/> is absolute and already in WPF units.
    /// </summary>
    [MarkupExtensionReturnType(typeof(Thickness))]
    public sealed class UIKitThicknessExtension : MarkupExtension
    {
        public int Uniform { get; set; }

        public int? Horizontal { get; set; }
        public int? Vertical { get; set; }

        public int? Left { get; set; }
        public int? Top { get; set; }
        public int? Right { get; set; }
        public int? Bottom { get; set; }

        public Thickness Offset { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var uniform = GetValueByLevel(Uniform);

            var horizontal = GetValueByLevel(Horizontal) ?? uniform;
            var vertical = GetValueByLevel(Vertical) ?? uniform;

            var left = (GetValueByLevel(Left) ?? horizontal) + Offset.Left;
            var top = (GetValueByLevel(Top) ?? vertical) + Offset.Top;
            var right = (GetValueByLevel(Right) ?? horizontal) + Offset.Right;
            var bottom = (GetValueByLevel(Bottom) ?? vertical) + Offset.Bottom;

            return new Thickness(left, top, right, bottom);
        }

        private static int? GetValueByLevel(int? level)
            => level is not null
                ? GetValueByLevel(level.Value)
                : null;

        private static int GetValueByLevel(int level)
            => level * BaselineUnit;

        private const int BaselineUnit = 4;
    }
}
