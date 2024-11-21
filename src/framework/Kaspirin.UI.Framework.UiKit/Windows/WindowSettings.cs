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

namespace Kaspirin.UI.Framework.UiKit.Windows
{
    public sealed class WindowSettings : IEquatable<WindowSettings>
    {
        public string Id { get; set; } = string.Empty;
        public int Height { get; set; } = 0;
        public int Width { get; set; } = 0;
        public Point? Position { get; set; } = null;
        public bool IsMaximized { get; set; } = false;
        public Dpi WindowDpi { get; set; } = Dpi.Default;

        public override string ToString()
        {
            return $"[{Id}: Size={Width}x{Height} Pos(LxT)={Position} IsMaximized={IsMaximized} WindowDpi={WindowDpi}]";
        }

        bool IEquatable<WindowSettings>.Equals(WindowSettings? other)
        {
            return other switch
            {
                null => false,
                _ => Id.Equals(other.Id, StringComparison.InvariantCulture)
                && Height == other.Height
                && Width == other.Width
                && (Position == null && other.Position == null || (Position?.Equals(other.Position) ?? false))
                && IsMaximized == other.IsMaximized
                && WindowDpi.Equals(other.WindowDpi)
            };
        }
    }
}
