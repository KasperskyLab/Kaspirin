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

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Runtime.InteropServices;

namespace Kaspirin.UI.Framework.NativeMethods.Common
{
    /// <summary>
    ///     
    /// <seealso href="https://learn.microsoft.com/en-us/windows/win32/api/windef/ns-windef-rect">Learn more</seealso>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    public struct NativeRectangle
    {
        public int Left;

        public int Top;

        public int Right;

        public int Bottom;

        public static readonly NativeRectangle Empty = new();

        public NativeRectangle(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public NativeRectangle(NativeRectangle source)
        {
            Left = source.Left;
            Top = source.Top;
            Right = source.Right;
            Bottom = source.Bottom;
        }

        public readonly int Width => Math.Abs(Right - Left);

        public readonly int Height => Bottom - Top;

        public readonly bool IsEmpty => Left >= Right || Top >= Bottom; // Bug: On Bidi OS (hebrew arabic) left > right

        public override readonly int GetHashCode()
            => Left.GetHashCode() +
               Top.GetHashCode() +
               Right.GetHashCode() +
               Bottom.GetHashCode();

        public override readonly string ToString()
            => this == Empty
                ? "NativeRectangle {Empty}"
                : $"NativeRectangle {{ " +
                    $"left : {Left} | " +
                    $"top : {Top} | " +
                    $"right : {Right} | " +
                    $"bottom : {Bottom} " +
                  $"}}";

        public override readonly bool Equals(object? obj)
            => obj is NativeRectangle rect && this == rect;

        public static bool operator ==(NativeRectangle rect1, NativeRectangle rect2)
            => rect1.Left == rect2.Left && rect1.Top == rect2.Top && rect1.Right == rect2.Right && rect1.Bottom == rect2.Bottom;

        public static bool operator !=(NativeRectangle rect1, NativeRectangle rect2)
            => !(rect1 == rect2);

        public static NativeRectangle Intersect(NativeRectangle a, NativeRectangle b)
        {
            var x1 = Math.Max(a.Left, b.Left);
            var x2 = Math.Min(a.Right, b.Right);
            var y1 = Math.Max(a.Top, b.Top);
            var y2 = Math.Min(a.Bottom, b.Bottom);

            if (x2 >= x1 && y2 >= y1)
            {
                return new NativeRectangle(x1, y1, x2 - x1, y2 - y1);
            }
            else
            {
                return Empty;
            }
        }

        public static NativeRectangle Union(NativeRectangle a, NativeRectangle b)
            => new(
                Math.Min(a.Left, b.Left),
                Math.Min(a.Top, b.Top),
                Math.Max(a.Right, b.Right),
                Math.Max(a.Bottom, b.Bottom));
    }
}
