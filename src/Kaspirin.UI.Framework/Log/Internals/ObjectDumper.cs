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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Kaspirin.UI.Framework.Log.Internals;

internal sealed class ObjectDumper
{
    private ObjectDumper(int indentSize, int maximumEnumerableElementsToDump = -1, int startIndentLevel = 0, int maxDepthLevel = -1)
    {
        Guard.Argument(indentSize >= 0);

        _currentIndentLevel = startIndentLevel;
        _indentSize = indentSize;
        _maximumEnumerableElementsToDump = maximumEnumerableElementsToDump;
        _maxDepthLevel = maxDepthLevel;
        _stringBuilder = new StringBuilder();
    }

    public static string Dump(object? element, int startIndentLevel = 0, int maxDepthLevel = -1)
    {
        return Dump(element, DefaultMaximumEnumerableElementsToDump, DefaultIdentSize, startIndentLevel, maxDepthLevel);
    }

    private static string Dump(object? element, int maximumEnumerableElementsToDump, int indentSize, int startIndentLevel, int maxDepthLevel)
    {
        try
        {
            var instance = new ObjectDumper(indentSize, maximumEnumerableElementsToDump, startIndentLevel, maxDepthLevel);
            return instance.DumpElement(element);
        }
        catch (Exception e)
        {
            e.TraceExceptionSuppressed();
            return string.Empty;
        }
    }

    private string DumpElement(object? element)
    {
        if (TryIncreaseDepthLevel())
        {
            if (TryAddElementToVisited(element))
            {
                if (element == null || element is ValueType || element is string)
                {
                    Write(FormatValue(element));
                }
                else
                {
                    var objectType = element.GetType();
                    if (!typeof(IEnumerable).IsAssignableFrom(objectType))
                    {
                        Write($"({objectType})");
                        _currentIndentLevel++;
                    }

                    if (element is IEnumerable enumerableElement)
                    {
                        DumpEnumerableElement(enumerableElement);
                    }
                    else
                    {
                        DumpObjectElement(element);
                    }

                    if (!typeof(IEnumerable).IsAssignableFrom(objectType))
                    {
                        _currentIndentLevel--;
                    }
                }

                RemoveElementFromVisited(element);
            }
            else
            {
                Write(CircularReferenceMarker);
            }

            DecreaseDepthLevel();
        }
        else
        {
            Write(MaxLogDepthIsExceededMarker);
        }

        return _stringBuilder.ToString();
    }

    private void RemoveElementFromVisited(object? element)
    {
        if (element != null)
        {
            _visitedElements.Remove(element);
        }
    }

    private bool TryAddElementToVisited(object? element)
    {
        return element == null || _visitedElements.Add(element);
    }

    private void DecreaseDepthLevel()
    {
        _currentDepthLevel--;
    }

    private bool TryIncreaseDepthLevel()
    {
        if (_maxDepthLevel < 0 || _currentDepthLevel < _maxDepthLevel)
        {
            _currentDepthLevel++;
            return true;
        }

        return false;
    }

    private void DumpObjectElement(object element)
    {
        if (element is null)
        {
            Write("no value");
            return;
        }

        var elementType = element.GetType();
        if (elementType.IsGenericType && elementType.GetGenericTypeDefinition().Name == "Option`1")
        {
            var hasValueProp = element.GetType().GetProperty("HasValue");
            var hasValue = (bool)Guard.EnsureArgumentIsNotNull(Guard.EnsureArgumentIsNotNull(hasValueProp).GetValue(element, null));

            if (!hasValue)
            {
                Write("no value");
                return;
            }
        }

        var data = element
            .GetType()
            .GetMembers(BindingFlags.Public | BindingFlags.Instance)
            .Where(member => member.MemberType == MemberTypes.Field || member.MemberType == MemberTypes.Property)
            .Select(member =>
            new
            {
                Name = member.Name,
                Type = member.MemberType == MemberTypes.Field
                    ? ((FieldInfo)member).FieldType
                    : ((PropertyInfo)member).PropertyType,
                Value = member.MemberType == MemberTypes.Field
                    ? ((FieldInfo)member).GetValue(element)
                    : ((PropertyInfo)member).GetValue(element, null)
            });

        foreach (var dataElement in data)
        {
            DumpMember(dataElement.Type, dataElement.Name, dataElement.Value);
        }
    }

    private void DumpMember(Type type, string name, object? value)
    {
        if (type.IsValueType || type == typeof(string))
        {
            Write($"{name}: {FormatValue(value)}");
        }
        else
        {
            var isEnumerable = typeof(IEnumerable).IsAssignableFrom(type);
            Write($"{name}: {(isEnumerable ? FormatValue(value) : "{ }")}");
            DumpSubElement(value);
        }
    }

    private void DumpEnumerableElement(IEnumerable enumerableElement)
    {
        var index = 0;
        foreach (var item in enumerableElement)
        {
            if (_maximumEnumerableElementsToDump != -1 && index >= _maximumEnumerableElementsToDump)
            {
                return;
            }

            if (item is IEnumerable && item is not string)
            {
                DumpSubElement(item);
            }
            else
            {
                DumpElement(item);
            }

            index++;
        }
    }

    private void DumpSubElement(object? item)
    {
        _currentIndentLevel++;
        DumpElement(item);
        _currentIndentLevel--;
    }

    private void Write(string value) =>
        _stringBuilder.AppendLine(new string(' ', _currentIndentLevel * _indentSize) + value);

    private string FormatValue(object? o)
    {
        if (o == null)
        {
            return "null";
        }

        if (o is DateTime dateTime)
        {
            // For calendars which support specific date range call ToShortDateString() may generate exceptions.
            // BUG #5075309
            return dateTime.ToString(CultureInfo.InvariantCulture);
        }

        if (o is string s)
        {
            return string.Format("\"{0}\"", s.ShiftRight(_currentIndentLevel, exceptFirstLine: true));
        }

        if (o is ValueType)
        {
            var hexViewTypes = new[] { typeof(int), typeof(uint), typeof(short), typeof(ushort), typeof(long), typeof(ulong), typeof(byte) };
            return hexViewTypes.Any(t => o.GetType().IsAssignableFrom(t))
                ? $"{o} (hex 0x{o:X})"
                : Convert.ToString(o, CultureInfo.InvariantCulture) ?? "";
        }

        if (o is IEnumerable enumerable)
        {
            return "... Count " + enumerable.Cast<object>().Count();
        }

        return "{ }";
    }

    public const string CircularReferenceMarker = "[circular reference]";
    public const string MaxLogDepthIsExceededMarker = "[max log depth is exceeded]";

    private const int DefaultIdentSize = 2;
    private const int DefaultMaximumEnumerableElementsToDump = -1;

    private int _currentIndentLevel;
    private int _currentDepthLevel = 0;

    private readonly int _indentSize;
    private readonly int _maximumEnumerableElementsToDump;
    private readonly int _maxDepthLevel;
    private readonly StringBuilder _stringBuilder;
    private readonly HashSet<object> _visitedElements = new();
}
