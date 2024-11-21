// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 1/10/2024.
// Scope of modification:
//   - Code adaptation to project requirements.

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Interactivity.Core
{
    public abstract class AttachableCollection<T> : FreezableCollection<T> where T : DependencyObject
    {
        internal AttachableCollection()
        {
            var notifyCollectionChanged = (INotifyCollectionChanged)this;
            notifyCollectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(OnCollectionChanged);

            _snapshot = new Collection<T>();
        }

        public DependencyObject? AssociatedObject
        {
            get
            {
                ReadPreamble();
                return _associatedObject;
            }
        }

        public void Attach(DependencyObject? dependencyObject)
        {
            if (AssociatedObject == dependencyObject)
            {
                return;
            }

            if (AssociatedObject != null)
            {
                throw new InvalidOperationException("An instance of a AttachableCollection cannot be attached to more than one object at a time.");
            }

            WritePreamble();
            _associatedObject = dependencyObject;
            WritePostscript();

            OnAttached();
        }

        public void Detach()
        {
            OnDetaching();
            WritePreamble();
            _associatedObject = null;
            WritePostscript();
        }

        internal abstract void ItemAdded(T item);

        internal abstract void ItemRemoved(T item);

        protected abstract void OnAttached();

        protected abstract void OnDetaching();

        private void VerifyAdd(T item)
        {
            if (_snapshot.Contains(item))
            {
                throw new InvalidOperationException($"Cannot add the same instance of \"{typeof(T).Name}\" to a \"{GetType().Name}\" more than once.");
            }
        }

        private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            var newItems = e.NewItems ?? new List<T>();
            var oldItems = e.OldItems ?? new List<T>();

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (T item in newItems)
                    {
                        try
                        {
                            VerifyAdd(item);
                            ItemAdded(item);
                        }
                        finally
                        {
                            _snapshot.Insert(IndexOf(item), item);
                        }
                    }

                    break;

                case NotifyCollectionChangedAction.Replace:
                    foreach (T item in oldItems)
                    {
                        ItemRemoved(item);
                        _snapshot.Remove(item);
                    }

                    foreach (T item in newItems)
                    {
                        try
                        {
                            VerifyAdd(item);
                            ItemAdded(item);
                        }
                        finally
                        {
                            _snapshot.Insert(IndexOf(item), item);
                        }
                    }

                    break;

                case NotifyCollectionChangedAction.Remove:
                    foreach (T item in oldItems)
                    {
                        ItemRemoved(item);
                        _snapshot.Remove(item);
                    }

                    break;

                case NotifyCollectionChangedAction.Reset:
                    foreach (var item in _snapshot)
                    {
                        ItemRemoved(item);
                    }

                    _snapshot = new Collection<T>();
                    foreach (var item in this)
                    {
                        VerifyAdd(item);
                        ItemAdded(item);
                    }

                    break;
                case NotifyCollectionChangedAction.Move:
                default:
                    Guard.Fail("Unsupported collection operation attempted.");
                    break;
            }
        }

        private Collection<T> _snapshot;
        private DependencyObject? _associatedObject;
    }
}
