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

using System;
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.Controls;

[MarkupExtensionReturnType(typeof(FlowGridStrategyCollection))]
public sealed class FlowGridStrategiesExtension : MarkupExtension
{
    public FlowGridStrategiesExtension()
    {
        _collection = new FlowGridStrategyCollection();
    }

    public FlowGridStrategy? Strategy1
    {
        get => GetStrategy(index: 0);
        set => SetStrategy(index: 0, Guard.EnsureIsNotNull(value));
    }

    public FlowGridStrategy? Strategy2
    {
        get => GetStrategy(index: 1);
        set => SetStrategy(index: 1, Guard.EnsureIsNotNull(value));
    }

    public FlowGridStrategy? Strategy3
    {
        get => GetStrategy(index: 2);
        set => SetStrategy(index: 2, Guard.EnsureIsNotNull(value));
    }

    public FlowGridStrategy? Strategy4
    {
        get => GetStrategy(index: 3);
        set => SetStrategy(index: 3, Guard.EnsureIsNotNull(value));
    }

    public FlowGridStrategy? Strategy5
    {
        get => GetStrategy(index: 4);
        set => SetStrategy(index: 4, Guard.EnsureIsNotNull(value));
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return _collection;
    }

    private FlowGridStrategy? GetStrategy(int index)
    {
        if (_collection.Count > index)
        {
            return _collection[index];
        }

        return null;
    }

    private void SetStrategy(int index, FlowGridStrategy strategy)
    {
        if (_collection.Count > index)
        {
            _collection[index] = strategy;
        }
        else
        {
            _collection.Add(strategy);
        }
    }

    private readonly FlowGridStrategyCollection _collection;
}
