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
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Localization.Markup.Converting
{
    public class ResourceCollection : IDictionary
    {
        public object? Fallback { get; set; }
        public LocParameterCollection? ParamsCollection { get; set; }

        public static bool True => true;
        public static bool False => false;

        #region IDictionary

        public ICollection Keys => RealDictionary.Keys;

        public ICollection Values => RealDictionary.Values;

        public bool IsReadOnly => RealDictionary.IsReadOnly;

        public bool IsFixedSize => RealDictionary.IsFixedSize;

        public int Count => RealDictionary.Count;

        public object SyncRoot => RealDictionary.SyncRoot;

        public bool IsSynchronized => RealDictionary.IsSynchronized;

        public object? this[object? key]
        {
            get => GetResourceItem(key);
            set
            {
                CheckKeyIsAllowed(key);
                RealDictionary[key] = value;
            }
        }

        public bool Contains(object key)
        {
            Guard.ArgumentIsNotNull(key);

            return RealDictionary.Contains(key);
        }

        public void Add(object key, object? value)
        {
            CheckKeyIsAllowed(key);

            RealDictionary.Add(key, value);
        }

        public void Clear() => RealDictionary.Clear();

        public IDictionaryEnumerator GetEnumerator() => RealDictionary.GetEnumerator();

        public void Remove(object key)
        {
            Guard.ArgumentIsNotNull(key);

            RealDictionary.Remove(key);
        }

        public void CopyTo(Array array, int index)
        {
            Guard.ArgumentIsNotNull(array);

            RealDictionary.CopyTo(array, index);
        }

        IEnumerator IEnumerable.GetEnumerator() => RealDictionary.GetEnumerator();

        #endregion

        public virtual object? GetResourceItem(object? key)
        {
            if (key != null)
            {
                if (RealDictionary.Contains(key))
                {
                    return RealDictionary[key];
                }

                if (RealDictionary.Contains(key.GetType()))
                {
                    return RealDictionary[key.GetType()];
                }
            }

            if (Fallback != null)
            {
                return Fallback;
            }

            return DependencyProperty.UnsetValue;
        }

        protected virtual void CheckKeyIsAllowed([NotNull] object? key)
        {
            Guard.ArgumentIsNotNull(key);
            Guard.Assert(key is Enum or Type or bool or string,
                $"{nameof(key)} = {key} of type {key.GetType()}. This type isn't of allowed. Allowed types: Enum, Type, Boolean, String.");
        }

        protected IDictionary RealDictionary { get; } = new Dictionary<object, object>();
    }
}
