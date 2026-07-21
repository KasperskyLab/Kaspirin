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

#pragma warning disable IDE0251 // Make member 'readonly'

using System;

namespace Kaspirin.UI.Framework.UiKit.Controls;

public sealed class FlowGridStrategy
{
    public FlowGridStrategy()
    {
        Name = string.Empty;
        SealRule = new();

        _columnCount = 1;
        _fromWidth = 0;
        _toWidth = double.PositiveInfinity;
    }

    public string Name { get; set; }

    public string SealRowInclude
    {
        get => SealRule.Include;
        set => SealRule.Include = value;
    }

    public string SealRowExclude
    {
        get => SealRule.Exclude;
        set => SealRule.Exclude = value;
    }

    public int ColumnCount
    {
        get => _columnCount;
        set => _columnCount = value > 0 ? value : 1;
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

    internal FlowGridSealRule SealRule { get; }

    public static bool operator ==(FlowGridStrategy left, FlowGridStrategy right)
    {
        return left.SealRule == right.SealRule &&
               left._columnCount == right._columnCount &&
               left._fromWidth.NearlyEqual(right._fromWidth) &&
               left._toWidth.NearlyEqual(right._toWidth);
    }

    public static bool operator !=(FlowGridStrategy left, FlowGridStrategy right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        if (obj is FlowGridStrategy other)
        {
            return this == other;
        }

        return false;
    }

    public override int GetHashCode()
        => HashCode.Combine(SealRule, _columnCount, _fromWidth, _toWidth, Name);

    private int _columnCount;
    private double _fromWidth;
    private double _toWidth;
}