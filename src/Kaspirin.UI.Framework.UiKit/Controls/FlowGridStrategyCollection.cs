// Copyright © 2026 AO Kaspersky Lab.
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

using System.Collections.ObjectModel;
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Controls;

public sealed class FlowGridStrategyCollection : ObservableCollection<FlowGridStrategy>
{
    public FlowGridStrategy ChooseSuitableStrategy(double panelWidth)
    {
        return this.LastOrDefault(s => panelWidth.LargerOrNearlyEqual(s.FromWidth) &&
                                       panelWidth.LesserOrNearlyEqual(s.ToWidth)) ?? new();
    }

    protected override void InsertItem(int index, FlowGridStrategy item)
    {
        if (item.Name.IsEmpty())
        {
            item.Name = $"Strategy_{index}";
        }

        base.InsertItem(index, item);
    }
}
