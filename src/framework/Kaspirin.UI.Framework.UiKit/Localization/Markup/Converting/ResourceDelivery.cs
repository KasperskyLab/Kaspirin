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

using Kaspirin.UI.Framework.UiKit.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Localization.Markup.Converting
{
    public sealed class ResourceDelivery : FrameworkElement
    {
        public ResourceDelivery(IResourceProvider resourceProvider, Binding sourceBinding)
        {
            _resourceProvider = Guard.EnsureArgumentIsNotNull(resourceProvider);
            _sourceBinding = Guard.EnsureArgumentIsNotNull(sourceBinding);
        }

        static ResourceDelivery()
        {
            UnsetValue = new object();

            ResourceValueProperty = DependencyProperty.Register(
                nameof(ResourceValue),
                typeof(object),
                typeof(ResourceDelivery),
                new PropertyMetadata(UnsetValue));
        }

        public LocalizationMarkupBase? CurrentBaseExtension { get; private set; }

        public static readonly DependencyProperty RetainedDeliversProperty = DependencyProperty.Register(
                "RetainedDelivers",
                typeof(List<Retention>),
                typeof(DependencyObject),
                new FrameworkPropertyMetadata(null));

        public object? ResourceValue
        {
            get => GetValue(ResourceValueProperty);
            set => SetValue(ResourceValueProperty, value);
        }

        public static readonly DependencyProperty ResourceValueProperty;

        public object? SourceValue
        {
            get => GetValue(SourceValueProperty);
            set => SetValue(SourceValueProperty, value);
        }

        public static readonly DependencyProperty SourceValueProperty = DependencyProperty.Register(
            "SourceValue",
            typeof(object),
            typeof(ResourceDelivery),
            new PropertyMetadata(SourceValueChanged));

        public BindingBase CreateBinding(object? targetObject)
        {
            return targetObject switch
            {
                LocParameter => CreateParameterBinding(),
                _ => CreateMultiBinding(targetObject)
            };
        }

        public static bool HasUnsetParameters(object?[] parameters)
        {
            return parameters.Contains(UnsetValue);
        }

        public static bool IsUnsetParameter(object? parameter)
        {
            return ReferenceEquals(parameter, UnsetValue);
        }

        public void UpdateParameterDataContext(object? newDataContext)
        {
            var oldDataContext = DataContext;
            DataContext = newDataContext;

            if (oldDataContext == null && newDataContext != null)
            {
                // dataContext is setted now
                // if dataContext is setted first time, SourceValue binding will be updated asyncronously
                // but we need to get value of SourceValue syncronously, so set binding after dataContext isn't null:

                SetBinding(SourceValueProperty, _sourceBinding);
            }
        }

        internal ResourceDelivery CreateCopyResourceDelivery()
        {
            var result = new ResourceDelivery(_resourceProvider, _sourceBinding.Clone());
            result._sourceBinding.Converter = null;
            return result;
        }

        private MultiBinding CreateMultiBinding(object? targetObject)
        {
            var resBinding = new MultiBinding();

            var setter = targetObject as Setter;
            if (setter != null)
            {
                resBinding.Bindings.Add(
                    new Binding
                    {
                        RelativeSource = new RelativeSource(RelativeSourceMode.Self),
                        Converter = new DelegateConverter(control =>
                        {
                            OnApplyingToTargetObject(control as DependencyObject, setter);
                            return DependencyProperty.UnsetValue;
                        }),
                        Mode = BindingMode.OneWay
                    });
            }
            else
            {
                AttachTo(targetObject as DependencyObject);
            }

            resBinding.Bindings.Add(
                new Binding
                {
                    Path = new PropertyPath(nameof(DataContext)),
                    RelativeSource = new RelativeSource(RelativeSourceMode.Self),
                    Converter = new DelegateConverter(newDataContext =>
                    {
                        DataContext = newDataContext;
                        return DependencyProperty.UnsetValue;
                    })
                });

            var converter = new DelegateConverter(newKey =>
            {
                if (!string.IsNullOrEmpty(_sourceBinding.StringFormat))
                {
                    newKey = string.Format(_sourceBinding.StringFormat, newKey);
                }

                UpdateResource(newKey);
                return DependencyProperty.UnsetValue;
            });

            if (_sourceBinding.Converter != null)
            {
                _sourceBinding.Converter = new CompositeConverter(_sourceBinding.Converter, converter);
            }
            else
            {
                _sourceBinding.Converter = converter;
            }

            resBinding.Bindings.Add(_sourceBinding);
            resBinding.Bindings.Add(CreateResourceBinding());
            resBinding.Mode = BindingMode.OneWay;
            resBinding.Converter = new DelegateMultiConverter(values => values.Last());

            return resBinding;
        }

        internal Binding CreateParameterBinding()
        {
            // Can't provide MultiBinding to localization parameter,
            // so provide Binding, but deliver
            // updates from sourceBinding via DP SourceValueProperty
            // see SetSourceBinding

            return CreateResourceBinding();
        }

        private Binding CreateResourceBinding()
        {
            return new Binding
            {
                Source = this,
                Path = new PropertyPath(nameof(ResourceValue)),

                // We need to create hard reference from Binding to current ResourceDelivery,
                // because of property Binding.Source create weakReference to source object internally
                // and GC collects current ResourceDelivery
                FallbackValue = this
            };
        }

        private void OnApplyingToTargetObject(DependencyObject? newTargetObject, Setter setter)
        {
            // prevents binding sharing over style trigger setters
            var targetObject = (DependencyObject?)_targetObject?.Target;
            if (targetObject != null)
            {
                if (newTargetObject != null && targetObject != newTargetObject)
                {
                    DetachFrom(targetObject);

                    var newResDelivery = CreateCopyResourceDelivery();
                    newResDelivery._targetObject = _targetObject;

                    var newBinding = newResDelivery.CreateBinding(targetObject);

                    BindingOperations.SetBinding(targetObject, setter.Property, newBinding);
                    newResDelivery.AttachTo(targetObject);

                    AttachTo(newTargetObject);
                }
            }
            else
            {
                AttachTo(newTargetObject);
            }

            _targetObject = newTargetObject == null ? null : new(newTargetObject);
        }

        private void UpdateResource(object? key)
        {
            // uncomment when switched to new metadata switching (Shift + F11)

            //if (CurrentBaseExtension != null // &&
            //    // _currentKey.Equals(key))
            //    )
            //{
            //    return;
            //}

            // _currentKey = key;
            var resource = _resourceProvider.GetResource(key);

            CurrentBaseExtension = resource as LocalizationMarkupBase;

            if (resource is IBindingProvider bindingProvider)
            {
                BindingOperations.SetBinding(this, ResourceValueProperty, bindingProvider.ProvideBinding());
            }
            else
            {
                ResourceValue = resource;
            }
        }

        private static void SourceValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var resourceDelivery = (ResourceDelivery)d;

            if (e.NewValue != null)
            {
                resourceDelivery.UpdateResource(e.NewValue);
            }
        }

        private void AttachTo(DependencyObject? targetObject)
        {
            if (targetObject != null)
            {
                var retention = (Retention?)_retention?.Target;
                if (retention == null)
                {
                    retention = new Retention(this);
                    _retention = new WeakReference(retention);
                }

                var retainedDelivers = (List<Retention>)targetObject!.GetValue(RetainedDeliversProperty);
                if (retainedDelivers == null)
                {
                    retainedDelivers = new List<Retention>
                    {
                        retention!
                    };
                    targetObject?.SetValue(RetainedDeliversProperty, retainedDelivers);
                }
                else
                {
                    retainedDelivers.Add(retention!);
                }
            }
        }

        private void DetachFrom(DependencyObject targetObject)
        {
            var retention = (Retention?)_retention?.Target;
            if (retention != null)
            {
                var retainedDelivers = (List<Retention>)targetObject.GetValue(RetainedDeliversProperty);
                retainedDelivers?.Remove(retention);
            }
        }

        private void ClearData()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                ClearValue(DataContextProperty);
                _retention = null;
                _targetObject = null;
            }));
        }

        /// <summary>
        /// ResourceDelivery that is used in Setter live long time and isn't collected when collected target control binded to it
        /// We need to track the moment when target control is collected to clear DataContext property,
        /// because ViewModel is retained by follow path: Style->Setter->Binding->ResourceDelivery->DataContext->ViewModel.
        /// </summary>
        private sealed class Retention
        {
            public Retention(ResourceDelivery delivery)
            {
                _delivery = delivery;
            }

            ~Retention()
            {
                _delivery.ClearData();
            }

            private readonly ResourceDelivery _delivery;
        }

        private readonly IResourceProvider _resourceProvider;
        private readonly Binding _sourceBinding;
        private WeakReference? _targetObject = null; //DependencyObject
        private WeakReference? _retention = null; //Retention

        private static readonly object UnsetValue;
    }
}
