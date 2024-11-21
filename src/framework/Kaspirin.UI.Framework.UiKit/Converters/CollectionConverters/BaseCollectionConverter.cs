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
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Converters.CollectionConverters
{
    public abstract class BaseCollectionConverter<T> : ValueConverterMarkupExtension<BaseCollectionConverter<T>>
    {
        protected BaseCollectionConverter(T? trueValue, T? falseValue)
        {
            True = trueValue;
            False = falseValue;
        }

        public T? True { get; set; }

        public T? False { get; set; }

        public CollectionConverterOperation Operation { get; set; }

        public CollectionConverterItemType ItemType { get; set; }

        public override object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var operationResult = ItemType switch
            {
                CollectionConverterItemType.Any => EvaluateOperation<object>(value, parameter),
                CollectionConverterItemType.Int32 => EvaluateOperation<int>(value, parameter),
                CollectionConverterItemType.Int32Nullable => EvaluateOperation<int?>(value, parameter),
                CollectionConverterItemType.Int64 => EvaluateOperation<long>(value, parameter),
                CollectionConverterItemType.Int64Nullable => EvaluateOperation<long?>(value, parameter),
                CollectionConverterItemType.UInt32 => EvaluateOperation<uint>(value, parameter),
                CollectionConverterItemType.UInt32Nullable => EvaluateOperation<uint?>(value, parameter),
                CollectionConverterItemType.UInt64 => EvaluateOperation<ulong>(value, parameter),
                CollectionConverterItemType.UInt64Nullable => EvaluateOperation<ulong?>(value, parameter),
                CollectionConverterItemType.Double => EvaluateOperation<double>(value, parameter),
                CollectionConverterItemType.DoubleNullable => EvaluateOperation<double?>(value, parameter),
                CollectionConverterItemType.String => EvaluateOperation<string>(value, parameter),
                _ => throw new NotSupportedException($"Collection item type '{ItemType}' is not supported."),
            };

            return operationResult
                ? True
                : False;
        }

        private bool EvaluateOperation<TItem>(object? value, object? parameter)
        {
            GetCollectionAndItem<TItem>(value, parameter, out var collection, out var item, out var actualItemType);

            if (item is null)
            {
                var isNullable = !actualItemType.IsValueType || actualItemType.UnderlyingSystemType == _genericNullableType;
                if (!isNullable)
                {
                    return false;
                }
            }

            return Operation switch
            {
                CollectionConverterOperation.Contains => collection.Contains(item!),
                CollectionConverterOperation.IsFirst => !collection.None() && EqualityComparer<TItem>.Default.Equals(collection.First(), item),
                CollectionConverterOperation.IsLast => !collection.None() && EqualityComparer<TItem>.Default.Equals(collection.Last(), item),
                CollectionConverterOperation.IsMin => !collection.None() && EqualityComparer<TItem>.Default.Equals(collection.Min(), item),
                CollectionConverterOperation.IsMax => !collection.None() && EqualityComparer<TItem>.Default.Equals(collection.Max(), item),
                _ => throw new NotSupportedException($"Collection operation '{Operation}' is not supported."),
            };
        }

        private void GetCollectionAndItem<TItem>(object? value, object? parameter, out ICollection<TItem> collection, out TItem? item, out Type actualItemType)
        {
            actualItemType = typeof(TItem);

            if (actualItemType == typeof(object))
            {
                var collectionType = Guard.EnsureArgumentIsNotNull(value).GetType();
                var collectionInterfaceType = Guard.EnsureIsNotNull(
                    collectionType.GetInterface(_genericCollectionInterfaceType.Name),
                    $"Collection must implement {_genericCollectionInterfaceType.Name}");

                actualItemType = collectionInterfaceType.GetGenericArguments().GuardedSingle();

                collection = ((System.Collections.ICollection)value).Cast<TItem>().ToArray();
                item = GetItem<TItem>(parameter, actualItemType);
            }
            else
            {
                collection = Guard.EnsureArgumentIsInstanceOfType<ICollection<TItem>>(value);
                item = GetItem<TItem>(parameter, typeof(TItem));
            }
        }

        private TItem? GetItem<TItem>(object? parameter, Type itemType)
            => parameter is null
                ? default
                : (TItem)Guard.EnsureIsNotNull(
                    TypeDescriptor.GetConverter(itemType).ConvertFrom(parameter),
                    $"Unable to convert parameter value '{parameter}' of type {parameter.GetType().Name} to type {itemType.Name}");

        private static readonly Type _genericCollectionInterfaceType = typeof(ICollection<>);
        private static readonly Type _genericNullableType = typeof(Nullable<>);
    }
}
