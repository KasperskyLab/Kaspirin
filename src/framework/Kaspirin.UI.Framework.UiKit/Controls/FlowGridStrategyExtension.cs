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

[MarkupExtensionReturnType(typeof(FlowGridStrategy))]
public sealed class FlowGridStrategyExtension : MarkupExtension
{
    public FlowGridStrategyExtension()
    {
        _strategy = new FlowGridStrategy();
    }

    public int? ColumnCount { get; set; }

    public double? FromWidth { get; set; }

    public double? ToWidth { get; set; }

    public string? SealRowInclude { get; set; }

    public string? SealRowExclude { get; set; }

    public string? Name { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        _strategy.ColumnCount = ColumnCount ?? _strategy.ColumnCount;
        _strategy.FromWidth = FromWidth ?? _strategy.FromWidth;
        _strategy.ToWidth = ToWidth ?? _strategy.ToWidth;
        _strategy.Name = Name ?? _strategy.Name;
        _strategy.SealRowExclude = SealRowExclude ?? _strategy.SealRule.Exclude;
        _strategy.SealRowInclude = SealRowInclude ?? _strategy.SealRule.Include;

        return _strategy;
    }

    private readonly FlowGridStrategy _strategy;
}
