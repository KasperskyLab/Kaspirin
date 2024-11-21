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

namespace Kaspirin.UI.Framework.UiKit.Windows
{
    public sealed class Dpi : IEquatable<Dpi>
    {
        public static readonly Dpi Default = new(DefaultDpi, DefaultDpi);

        public double ScaleX { get; }
        public double ScaleY { get; }

        public double X { get; }
        public double Y { get; }

        public Dpi(double x, double y)
        {
            X = x;
            Y = y;

            ScaleX = X / DefaultDpi;
            ScaleY = Y / DefaultDpi;
        }

        public bool Equals(Dpi? other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override bool Equals(object? other) => Equals(other as Dpi);

        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();

        public override string ToString() => $"({X},{Y})";

        private const int DefaultDpi = 96;
    }
}