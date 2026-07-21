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
using System.Collections.Generic;

namespace Kaspirin.UI.Framework.UiKit.Controls;

internal sealed class FlowGridSealRule
{
    public FlowGridSealRule()
    {
        _include = "All";
        _exclude = "None";
    }

    public string Include
    {
        get => _include;
        set => _include = ValidateExpression(value);
    }

    public string Exclude
    {
        get => _exclude;
        set => _exclude = ValidateExpression(value);
    }

    public static bool operator ==(FlowGridSealRule left, FlowGridSealRule right)
    {
        return string.Equals(left._include, right._include, StringComparison.Ordinal) &&
               string.Equals(left._exclude, right._exclude, StringComparison.Ordinal);
    }

    public static bool operator !=(FlowGridSealRule left, FlowGridSealRule right)
    {
        return !(left == right);
    }

    public override bool Equals(object? obj)
    {
        if (obj is FlowGridSealRule other)
        {
            return this == other;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_include, _exclude);
    }

    public HashSet<int> GetRows(int rowCount)
    {
        var result = new HashSet<int>();
        if (rowCount <= 0)
        {
            return result;
        }

        var includeRows = ResolveExpression(_include, rowCount);
        var excludeRows = ResolveExpression(_exclude, rowCount);

        includeRows.ExceptWith(excludeRows);

        return includeRows;
    }

    private static string ValidateExpression(string? expr)
    {
        expr = string.IsNullOrWhiteSpace(expr) ? "None" : expr!.Trim();

        if (IsKeyword(expr))
        {
            return expr;
        }

        foreach (var token in expr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            if (token.Contains("-"))
            {
                var p = token.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (p.Length != 2 ||
                    !int.TryParse(p[0], out var from) ||
                    !int.TryParse(p[1], out var to) ||
                    from < 0 || to < 0)
                {
                    throw new ArgumentException($"Invalid range token '{token}'.");
                }
            }
            else
            {
                if (!int.TryParse(token, out var n) || n < 0)
                {
                    throw new ArgumentException($"Invalid row token '{token}'.");
                }
            }
        }

        return expr;
    }

    private static HashSet<int> ResolveExpression(string expr, int rowCount)
    {
        var result = new HashSet<int>();

        if (rowCount <= 0)
        {
            return result;
        }

        if (expr.Equals("None", StringComparison.CurrentCultureIgnoreCase))
        {
            return result;
        }

        if (expr.Equals("All", StringComparison.CurrentCultureIgnoreCase))
        {
            for (var i = 0; i < rowCount; i++)
            {
                result.Add(i);
            }

            return result;
        }

        if (expr.Equals("First", StringComparison.CurrentCultureIgnoreCase))
        {
            result.Add(0);
            return result;
        }

        if (expr.Equals("Last", StringComparison.CurrentCultureIgnoreCase))
        {
            result.Add(rowCount - 1);
            return result;
        }

        foreach (var token in expr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            if (token.Contains("-"))
            {
                var p = token.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                var from = int.Parse(p[0]);
                var to = int.Parse(p[1]);
                if (to < from)
                {
                    (from, to) = (to, from);
                }

                from = Math.Max(0, from);
                to = Math.Min(rowCount - 1, to);

                for (var i = from; i <= to; i++)
                {
                    result.Add(i);
                }
            }
            else
            {
                var row = int.Parse(token);
                if (row >= 0 && row < rowCount)
                {
                    result.Add(row);
                }
            }
        }

        return result;
    }

    private static bool IsKeyword(string expr) =>
        expr.Equals("All", StringComparison.OrdinalIgnoreCase) ||
        expr.Equals("None", StringComparison.OrdinalIgnoreCase) ||
        expr.Equals("First", StringComparison.OrdinalIgnoreCase) ||
        expr.Equals("Last", StringComparison.OrdinalIgnoreCase);

    private string _include;
    private string _exclude;
}
