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
using System.Linq;
using System.Reflection;

namespace Kaspirin.UI.Framework.Extensions.Reflection;

/// <summary>
///     Extension methods for <see cref="MemberInfo" />.
/// </summary>
public static class MemberInfoExtensions
{
    /// <summary>
    ///     Returns all attributes of the specified type applied to <paramref name="MemberInfo" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of attribute to get.
    /// </typeparam>
    /// <param name="memberInfo">
    ///     A member of the type from which attributes are requested.
    /// </param>
    /// <param name="onlyExactlyAttrType">
    ///     If <see langword="true" />, only attributes of the exact type <typeparamref name="T" /> are taken into account.
    /// </param>
    /// <returns>
    ///     An array of attributes of the type <typeparamref name="T" />. If there are no such attributes, an empty array is returned.
    /// </returns>
    public static T[] GetAttributesOfType<T>(this MemberInfo memberInfo, bool onlyExactlyAttrType = false) where T : Attribute
    {
        var attributes = memberInfo.GetCustomAttributes(typeof(T), true);
        if (attributes.Length == 0)
        {
            return EmptyArray.For<T>();
        }

        var attributesOfType = (T[])attributes;

        if (onlyExactlyAttrType)
        {
            attributesOfType = attributesOfType
                .Where(attr => attr.GetType() == typeof(T))
                .ToArray();
        }

        return attributesOfType;
    }

    /// <summary>
    ///     Returns the only attribute of the specified type applied to <paramref name="MemberInfo" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of attribute to get.
    /// </typeparam>
    /// <param name="memberInfo">
    ///     A member of the type from which the attribute is requested.
    /// </param>
    /// <param name="onlyExactlyAttrType">
    ///     If <see langword="true" />, only attributes of the exact type <typeparamref name="T" /> are taken into account.
    /// </param>
    /// <returns>
    ///     An instance of the <typeparamref name="T" /> attribute.
    /// </returns>
    /// <exception cref="GuardException">
    ///     It is thrown if more than one attribute of the required type is found.
    /// </exception>
    public static T? GetSingleAttributeOrDefault<T>(this MemberInfo memberInfo, bool onlyExactlyAttrType = false) where T : Attribute
    {
        var attribute = GetFirstOrDefault<T>(memberInfo, new Options
        {
            OnlyExactlyAttrType = onlyExactlyAttrType,
            ThrowIfNotSingle = true
        });

        return attribute;
    }

    /// <summary>
    ///     Checks whether the <paramref name="MemberInfo" /> attribute of the specified type is present.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the attribute being checked.
    /// </typeparam>
    /// <param name="memberInfo">
    ///     A member of the type in which the search is performed.
    /// </param>
    /// <param name="onlyExactlyAttrType">
    ///     If <see langword="true" />, only attributes of the exact type <typeparamref name="T" /> are taken into account.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if at least one attribute of the required type is found, otherwise - <see langword="false" />.
    /// </returns>
    public static bool HasAttribute<T>(this MemberInfo memberInfo, bool onlyExactlyAttrType = false) where T : Attribute
    {
        var attribute = GetFirstOrDefault<T>(memberInfo, new Options
        {
            OnlyExactlyAttrType = onlyExactlyAttrType,
            ThrowIfNotSingle = false,
            ThrowIfNoAttributes = false
        });

        return attribute != null;
    }

    private static T? GetFirstOrDefault<T>(
        MemberInfo memberInfo,
        Options options) where T : Attribute
    {
        var attributes = GetAttributesOfType<T>(memberInfo, options.OnlyExactlyAttrType);

        Guard.Assert(!options.ThrowIfNoAttributes || attributes.Any(), $"No required attribute {typeof(T).Name}");
        Guard.Assert(!options.ThrowIfNotSingle || attributes.Length <= 1, $"More than once attribute {typeof(T).Name}");

        return attributes.FirstOrDefault();
    }

    private sealed class Options
    {
        public bool OnlyExactlyAttrType { get; init; }
        public bool ThrowIfNotSingle { get; init; }
        public bool ThrowIfNoAttributes { get; init; }
    }
}
