// Copyright © 2025 AO Kaspersky Lab.
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
using System.Reflection;

namespace Kaspirin.UI.Framework.Reflection
{
    /// <summary>
    ///     Provides a convenient way to work with static class members through reflection.
    /// </summary>
    public static class TypeAccessor
    {
        /// <summary>
        ///     Reads the value of an automatic property.
        /// </summary>
        /// <param name="type">
        ///     The type of the property owner.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property.
        /// </param>
        /// <param name="newValue">
        ///     The new value of the property.
        /// </param>
        /// <param name="throwOnError">
        ///     If <see langword="true" />, an exception will be thrown if the property is not found. Otherwise, <see langword="default" /> will be returned.
        /// </param>
        public static void SetAutoPropertyFieldValue(Type type, string propertyName, object newValue, bool throwOnError = true)
            => GetAutoPropertyField(type, propertyName, throwOnError)?.SetValue(null, newValue);

        /// <summary>
        ///     Reads the value of the property.
        /// </summary>
        /// <typeparam name="TValue">
        ///     The type of the property value.
        /// </typeparam>
        /// <param name="type">
        ///     The type of the property owner.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property.
        /// </param>
        /// <param name="throwOnError">
        ///     If <see langword="true" />, an exception will be thrown if the property is not found. Otherwise, <see langword="default" /> will be returned.
        /// </param>
        /// <returns>
        ///     The value of the read property.
        /// </returns>
        public static TValue? GetPropertyValue<TValue>(Type type, string propertyName, bool throwOnError = true)
        {
            var propertyValue = GetPropertyValue(type, propertyName, throwOnError);
            return (propertyValue == null) ? default : Guard.EnsureIsInstanceOfType<TValue>(propertyValue, null, "propertyValue");
        }

        /// <summary>
        ///     Reads the value of the property.
        /// </summary>
        /// <param name="type">
        ///     The type of the property owner.
        /// </param>
        /// <param name="propertyName">
        ///     The name of the property.
        /// </param>
        /// <param name="throwOnError">
        ///     If <see langword="true" />, an exception will be thrown if the property is not found. Otherwise, <see langword="default" /> will be returned.
        /// </param>
        /// <returns>
        ///     The value of the read property.
        /// </returns>
        public static object? GetPropertyValue(Type type, string propertyName, bool throwOnError = true)
            => GetProperty(type, propertyName, throwOnError)?.GetValue(null, null);

        /// <summary>
        ///     Reads the value of the field.
        /// </summary>
        /// <param name="type">
        ///     The type of the property owner.
        /// </param>
        /// <param name="fieldName">
        ///     The name of the field.
        /// </param>
        /// <param name="throwOnError">
        ///     If <see langword="true" />, an exception will be thrown if the field is not found. Otherwise, <see langword="default" /> will be returned.
        /// </param>
        /// <returns>
        ///     The value of the read field.
        /// </returns>
        public static object? GetFieldValue(Type type, string fieldName, bool throwOnError = true)
            => GetField(type, fieldName, throwOnError)?.GetValue(type);

        /// <summary>
        ///     Reads the value of the field.
        /// </summary>
        /// <typeparam name="TValue">
        ///     The type of the field value.
        /// </typeparam>
        /// <param name="type">
        ///     The type of the owner of the field.
        /// </param>
        /// <param name="fieldName">
        ///     The name of the field.
        /// </param>
        /// <param name="throwOnError">
        ///     If <see langword="true" />, an exception will be thrown if the field is not found. Otherwise, <see langword="default" /> will be returned.
        /// </param>
        /// <returns>
        ///     The value of the read field.
        /// </returns>
        public static TValue? GetFieldValue<TValue>(Type type, string fieldName, bool throwOnError = true)
        {
            var fieldValue = GetFieldValue(type, fieldName, throwOnError);
            return fieldValue == null ? default : Guard.EnsureIsInstanceOfType<TValue>(fieldValue);
        }

        /// <summary>
        ///     Sets the field value.
        /// </summary>
        /// <param name="type">
        ///     The type of the owner of the field.
        /// </param>
        /// <param name="fieldName">
        ///     The name of the property.
        /// </param>
        /// <param name="value">
        ///     New field value.
        /// </param>
        /// <param name="throwOnError">
        ///     If <see langword="true" />, an exception will be thrown if the field is not found. Otherwise, <see langword="default" /> will be returned.
        /// </param>
        /// <returns>
        ///     The value of the read field.
        /// </returns>
        public static void SetFieldValue(Type type, string fieldName, object? value, bool throwOnError = true)
            => GetField(type, fieldName, throwOnError)?.SetValue(null, value);

        private static PropertyInfo? GetProperty(Type type, string propertyName, bool throwOnError)
        {
            var property = type.GetProperty(propertyName, _flags);
            Guard.Assert(!throwOnError || property != null);

            if (property == null)
            {
                ComponentTracer.Get(nameof(TypeAccessor)).TraceError($"Property {propertyName} wasn't found in {type}");
            }

            return property;
        }

        private static FieldInfo? GetAutoPropertyField(Type type, string propertyName, bool throwOnError)
            => GetField(type, $"<{propertyName}>k__BackingField", throwOnError);

        private static FieldInfo? GetField(Type type, string fieldName, bool throwOnError)
        {
            var field = type.GetField(fieldName, _flags);
            Guard.Assert(!throwOnError || field != null);

            if (field == null)
            {
                ComponentTracer.Get(nameof(TypeAccessor)).TraceError($"Field {fieldName} wasn't found in {type}");
            }

            return field;
        }

        private static readonly BindingFlags _flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
    }
}