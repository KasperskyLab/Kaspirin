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
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Kaspirin.UI.Framework.Storage
{
    /// <summary>
    ///     The base class for organizing a data warehouse.
    /// </summary>
    /// <remarks>
    ///     The heirs of the <see cref="PersistentStorageBase" /> class can implement work with the data warehouse as follows:
    ///     <code>
    ///     public class ExampleStorage : PersistentStorageBase
    ///     {
    ///     public bool Example
    ///     {
    ///     get => Read(() => Example);
    ///     set => Save(() => Example, value);
    ///     }
    ///     }
    ///     </code>
    /// </remarks>
    public abstract class PersistentStorageBase
    {
        /// <summary>
        ///     An event that is triggered when property values in the storage are changed.
        /// </summary>
        public event PersistentStorageChangedHandler Changed = (key, value) => { };

        /// <summary>
        ///     Initializes a new instance of the <see cref="PersistentStorageBase" /> class.
        /// </summary>
        /// <param name="keyValueStorage">
        ///     An instance of <see cref="IKeyValueStorage" /> that will be used to store data.
        /// </param>
        protected PersistentStorageBase(IKeyValueStorage keyValueStorage)
        {
            Guard.ArgumentIsNotNull(keyValueStorage);
            _keyValueStorage = keyValueStorage;
        }

        /// <summary>
        ///     Checks if there is a value for the specified property in the storage.
        /// </summary>
        /// <typeparam name="TProperty">
        ///     The type of the property.
        /// </typeparam>
        /// <param name="propertyLambda">
        ///     An expression representing a property.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the value is stored for the property, otherwise <see langword="false" />.
        /// </returns>
        protected bool HasValue<TProperty>(Expression<Func<TProperty>> propertyLambda)
        {
            var key = GetPropertyName(propertyLambda);
            return HasValue(key);
        }

        /// <summary>
        ///     Checks if there is a value for the specified key in the storage.
        /// </summary>
        /// <param name="key">
        ///     The key for verification.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the value is stored for the property, otherwise <see langword="false" />.
        /// </returns>
        protected bool HasValue(string key)
        {
            return _keyValueStorage.HasValue(key);
        }

        /// <summary>
        ///     Retrieves the property value from the storage.
        /// </summary>
        /// <param name="propertyLambda">
        ///     An expression defining a property.
        /// </param>
        /// <remarks>
        ///     The default value can be specified using the <see cref="PersistentStorageAttribute" /> attribute.
        /// </remarks>
        /// <returns>
        ///     The value from the store, or the default value if the property is not found or cannot be converted.
        /// </returns>
        protected DateTime Read(Expression<Func<DateTime>> propertyLambda)
        {
            var key = GetPropertyName(propertyLambda);
            var defaultValue = GetDefaultValue(propertyLambda, default(DateTime));
            var convertedDefaultValue = defaultValue.ToUnixTimeStamp();
            var value = _keyValueStorage.GetValue(key, convertedDefaultValue);
            if (value != null)
            {
                return int.TryParse(value.ToString(), NumberStyles.Number, NumberFormatInfo.InvariantInfo, out var result)
                    ? result.FromUnixTimeStamp()
                    : defaultValue;
            }

            return defaultValue;
        }

        /// <summary>
        ///     Retrieves the property value from the storage.
        /// </summary>
        /// <param name="propertyLambda">
        ///     An expression defining a property.
        /// </param>
        /// <remarks>
        ///     The default value can be specified using the <see cref="PersistentStorageAttribute" /> attribute.
        /// </remarks>
        /// <returns>
        ///     The value from the repository, or the default value if the property is not found.
        /// </returns>
        protected string? Read(Expression<Func<string>> propertyLambda)
        {
            var key = GetPropertyName(propertyLambda);
            var defaultValue = GetDefaultValue(propertyLambda, string.Empty);
            var value = _keyValueStorage.GetValue(key, defaultValue);
            if (value != null)
            {
                return value.ToString();
            }

            return defaultValue;
        }

        /// <summary>
        ///     Retrieves the property value from the storage.
        /// </summary>
        /// <param name="propertyLambda">
        ///     An expression defining a property.
        /// </param>
        /// <remarks>
        ///     The default value can be specified using the <see cref="PersistentStorageAttribute" /> attribute.
        /// </remarks>
        /// <returns>
        ///     The value from the store, or the default value if the property is not found or cannot be converted.
        /// </returns>
        protected bool Read(Expression<Func<bool>> propertyLambda)
        {
            var key = GetPropertyName(propertyLambda);
            var defaultValue = GetDefaultValue(propertyLambda, default(bool));
            var value = _keyValueStorage.GetValue(key, defaultValue);
            if (value != null)
            {
                return int.TryParse(value.ToString(), NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out var result)
                    ? result > 0
                    : defaultValue;
            }

            return defaultValue;
        }

        /// <summary>
        ///     Retrieves the property value from the storage.
        /// </summary>
        /// <param name="propertyLambda">
        ///     An expression defining a property.
        /// </param>
        /// <remarks>
        ///     The default value can be specified using the <see cref="PersistentStorageAttribute" /> attribute.
        /// </remarks>
        /// <returns>
        ///     The value from the store, or the default value if the property is not found or cannot be converted.
        /// </returns>
        protected int Read(Expression<Func<int>> propertyLambda)
        {
            var key = GetPropertyName(propertyLambda);
            var defaultValue = GetDefaultValue(propertyLambda, default(int));
            var value = _keyValueStorage.GetValue(key, defaultValue);
            if (value != null)
            {
                return int.TryParse(value.ToString(), NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out var result)
                    ? result
                    : defaultValue;
            }

            return defaultValue;
        }

        /// <summary>
        ///     Retrieves the value from the storage.
        /// </summary>
        /// <param name="key">
        ///     The key is for reading.
        /// </param>
        /// <returns>
        ///     The value from the repository, or <see langword="default" /> if the property is not found.
        /// </returns>
        protected object? Read(string key)
        {
            if (_keyValueStorage.HasValue(key))
            {
                return _keyValueStorage.GetValue(key, default);
            }

            return default;
        }

        /// <summary>
        ///     Saves the value of the property in the storage.
        /// </summary>
        /// <param name="propertyLambda">
        ///     An expression defining a property.
        /// </param>
        /// <param name="value">
        ///     The stored value.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the save was successful, otherwise <see langword="false" />.
        /// </returns>
        protected bool Save(Expression<Func<DateTime>> propertyLambda, DateTime value)
        {
            var propInfo = GetPropertyInfo(propertyLambda);
            var valueString = value.ToUnixTimeStamp().ToString(CultureInfo.InvariantCulture);

            return Save(propInfo, value, valueString);
        }

        /// <summary>
        ///     Saves the value of the property in the storage.
        /// </summary>
        /// <param name="propertyLambda">
        ///     An expression defining a property.
        /// </param>
        /// <param name="value">
        ///     The stored value.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the save was successful, otherwise <see langword="false" />.
        /// </returns>
        protected bool Save(Expression<Func<string>> propertyLambda, string value)
        {
            var propInfo = GetPropertyInfo(propertyLambda);

            return Save(propInfo, value, value);
        }

        /// <summary>
        ///     Saves the value of the property in the storage.
        /// </summary>
        /// <param name="propertyLambda">
        ///     An expression defining a property.
        /// </param>
        /// <param name="value">
        ///     The stored value.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the save was successful, otherwise <see langword="false" />.
        /// </returns>
        protected bool Save(Expression<Func<bool>> propertyLambda, bool value)
        {
            var propInfo = GetPropertyInfo(propertyLambda);
            var valueInt = value ? 1 : 0;
            var valueString = valueInt.ToString();

            return Save(propInfo, valueInt, valueString);
        }

        /// <summary>
        ///     Saves the value of the property in the storage.
        /// </summary>
        /// <param name="propertyLambda">
        ///     An expression defining a property.
        /// </param>
        /// <param name="value">
        ///     The stored value.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the save was successful, otherwise <see langword="false" />.
        /// </returns>
        protected bool Save(Expression<Func<int>> propertyLambda, int value)
        {
            var propInfo = GetPropertyInfo(propertyLambda);
            var valueString = value.ToString(CultureInfo.InvariantCulture);

            return Save(propInfo, value, valueString);
        }

        /// <summary>
        ///     Saves the value in the storage by the specified key.
        /// </summary>
        /// <param name="key">
        ///     The key to save.
        /// </param>
        /// <param name="value">
        ///     The stored value.
        /// </param>
        /// <returns>
        ///     Returns <see langword="true" /> if the save was successful, otherwise <see langword="false" />.
        /// </returns>
        protected bool Save(string key, string value)
        {
            return SaveCore(key, value);
        }

        private bool Save(PropertyInfo propertyInfo, object value, string serializedValue)
        {
            var storageValue = ShouldSkipSerialization(propertyInfo)
                ? value
                : serializedValue;

            var propertyName = GetPropertyName(propertyInfo);

            return SaveCore(propertyName, storageValue);
        }

        private bool SaveCore(string key, object value)
        {
            var result = _keyValueStorage.SetValue(key, value);
            if (result)
            {
                Changed.Invoke(key, value);
            }

            return result;
        }

        private TValue GetDefaultValue<TProperty, TValue>(Expression<Func<TProperty>> propertyLambda, TValue fallbackValue)
        {
            var propInfo = GetPropertyInfo(propertyLambda);

            var attribute = propInfo.GetCustomAttributes(typeof(PersistentStorageAttribute), true).SingleOrDefault() as PersistentStorageAttribute;
            if (attribute != null)
            {
                var defaultValue = attribute.DefaultValue;
                if (defaultValue != null)
                {
                    return (TValue)defaultValue;
                }
            }

            return fallbackValue;
        }

        private PropertyInfo GetPropertyInfo<TProperty>(Expression<Func<TProperty>> propertyLambda)
        {
            var member = propertyLambda.Body as MemberExpression;
            Guard.ArgumentIsNotNull(member, $"Expression '{propertyLambda}' refers to a method, not a property.");

            var propInfo = member.Member as PropertyInfo;
            Guard.ArgumentIsNotNull(propInfo, $"Expression '{propertyLambda}' refers to a field, not a property.");

            var type = GetType();
            var reflectionType = propInfo.ReflectedType;

            Guard.Assert(reflectionType == null || reflectionType == type || type.IsSubclassOf(reflectionType),
                string.Format(
                    "Expression '{0}' refers to a property that is not from type {1}.",
                    propertyLambda,
                    type));

            return propInfo;
        }

        private string GetPropertyName<TProperty>(Expression<Func<TProperty>> propertyLambda)
        {
            var propInfo = GetPropertyInfo(propertyLambda);
            return GetPropertyName(propInfo);
        }

        private static string GetPropertyName(PropertyInfo propInfo)
        {
            return propInfo.Name;
        }

        private static bool ShouldSkipSerialization(PropertyInfo propInfo)
        {
            var attribute = propInfo.GetCustomAttributes(typeof(PersistentStorageAttribute), true).SingleOrDefault() as PersistentStorageAttribute;
            if (attribute != null)
            {
                return attribute.SkipSerialization;
            }

            return false;
        }

        private readonly IKeyValueStorage _keyValueStorage;
    }
}
