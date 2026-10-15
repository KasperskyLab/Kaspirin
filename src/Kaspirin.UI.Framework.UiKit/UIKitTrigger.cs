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
using System.Windows;
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit;

internal sealed class UIKitTrigger : DataTrigger
{
    static UIKitTrigger()
    {
        _targetObject = new();
        _targetProperty = new();
        _target = new(_targetObject, _targetProperty);
    }

    public UIKitTrigger()
    {
        Mode = RelativeSourceMode.TemplatedParent;
    }

    public string? Id
    {
        get => _id;
        set
        {
            _id = value;
            UpdateBiding();
        }
    }

    public Type? Type
    {
        get => _type;
        set
        {
            _type = value;
            UpdateBiding();
        }
    }

    public RelativeSourceMode Mode
    {
        get => _mode;
        set
        {
            _mode = value;
            UpdateBiding();
        }
    }

    public Type? AncestorType
    {
        get => _ancestorType;
        set
        {
            _ancestorType = value;
            UpdateBiding();
        }
    }

    public IValueConverter? Converter
    {
        get => _converter;
        set
        {
            _converter = value;
            UpdateBiding();
        }
    }

    public object? ConverterParameter
    {
        get => _converterParameter;
        set
        {
            _converterParameter = value;
            UpdateBiding();
        }
    }

    private void UpdateBiding()
    {
        if (Id == null || Type == null)
        {
            return;
        }

        Guard.IsNotNull(Id);
        Guard.IsNotNull(Type);

        var uiKitBinding = new UIKitBinding(Id)
        {
            Type = Type,
            Mode = Mode,
            AncestorType = AncestorType,
            Converter = Converter,
            ConverterParameter = ConverterParameter,
        }.ProvideValue(_target);

        Binding = Guard.EnsureIsInstanceOfType<BindingBase>(uiKitBinding);
    }

    private static readonly Setter _targetObject;
    private static readonly object _targetProperty;
    private static readonly DependencyTarget _target;

    private string? _id;
    private Type? _type;
    private RelativeSourceMode _mode;
    private Type? _ancestorType;
    private IValueConverter? _converter;
    private object? _converterParameter;
}
