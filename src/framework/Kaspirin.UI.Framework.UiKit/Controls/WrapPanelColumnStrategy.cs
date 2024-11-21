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

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class WrapPanelColumnStrategy
    {
        public static WrapPanelColumnStrategy Default { get; } = new WrapPanelColumnStrategy();

        public WrapPanelColumnStrategy()
        {
            Count = 1;
            FromWidth = 0;
            ToWidth = double.PositiveInfinity;
        }

        public RowAlignType AlignType { get; set; }

        public uint Count
        {
            get => _count;
            set => _count = value > 0 ? value : 1;
        }

        public double FromWidth
        {
            get => _fromWidth;
            set => _fromWidth = value.LesserOrNearlyEqual(ToWidth) ? value : ToWidth;
        }

        public double ToWidth
        {
            get => _toWidth;
            set => _toWidth = value.LargerOrNearlyEqual(FromWidth) ? value : FromWidth;
        }

        public enum RowAlignType
        {
            Auto,
            SkipAll,
            SkipLast
        }

        private uint _count;
        private double _fromWidth;
        private double _toWidth;
    }
}
