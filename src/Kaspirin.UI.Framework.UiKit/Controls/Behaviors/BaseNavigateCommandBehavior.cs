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
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Kaspirin.UI.Framework.UiKit.Controls.Behaviors;

public abstract class BaseNavigateCommandBehavior<TElement, TBehavior> : Behavior<TElement, TBehavior>
    where TElement : DependencyObject
    where TBehavior : Behavior<TElement>, new()
{
    protected override void OnAttached()
    {
        base.OnAttached();

        _commandNotifier = new PropertyChangeNotifier(Guard.EnsureIsNotNull(AssociatedObject), CommandProperty);
        _commandNotifier.ValueChanged += OnCommandChanged;

        SubscribeToCommand();
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        if (_commandNotifier != null)
        {
            _commandNotifier.ValueChanged -= OnCommandChanged;
            _commandNotifier.Dispose();
            _commandNotifier = null;
        }

        UnsubscribeFromCommand();

        if (_iconAdded)
        {
            RemoveIcon();
            _iconAdded = false;
        }
    }

    protected abstract DependencyProperty CommandProperty { get; }

    protected abstract void AddIcon();

    protected abstract void RemoveIcon();

    protected virtual NavigateCommand? GetCommand()
        => (AssociatedObject as ICommandSource)?.Command as NavigateCommand;

    protected virtual bool CanAddIcon()
        => true;

    protected void UpdateIcon()
    {
        if (_command != null && _command.IsExternal() && CanAddIcon())
        {
            if (!_iconAdded)
            {
                AddIcon();
                _iconAdded = true;
            }
        }
        else if (_iconAdded)
        {
            RemoveIcon();
            _iconAdded = false;
        }
    }

    protected void ApplyIcon(DependencyProperty property, Enum value)
    {
        var element = Guard.EnsureIsNotNull(AssociatedObject);

        _originalIcon = element.ReadLocalValue(property);
        _iconProperty = property;

        element.SetValue(property, value);
    }

    protected void RestoreIcon()
    {
        if (_iconProperty == null)
        {
            return;
        }

        var element = Guard.EnsureIsNotNull(AssociatedObject);

        if (_originalIcon is BindingExpressionBase originalBinding)
        {
            BindingOperations.SetBinding(element, _iconProperty, originalBinding.ParentBindingBase);
        }
        else if (_originalIcon == null || _originalIcon == DependencyProperty.UnsetValue)
        {
            element.ClearValue(_iconProperty);
        }
        else
        {
            element.SetValue(_iconProperty, _originalIcon);
        }

        _iconProperty = null;
        _originalIcon = null;
    }

    private void OnCommandChanged(DependencyObject source, object? oldValue, object? newValue)
        => SubscribeToCommand();

    private void SubscribeToCommand()
    {
        var command = GetCommand();
        if (ReferenceEquals(command, _command))
        {
            UpdateIcon();

            return;
        }

        UnsubscribeFromCommand();

        _command = command;
        if (_command != null)
        {
            _command.IsExternalChanged += OnIsExternalChanged;
        }

        UpdateIcon();
    }

    private void UnsubscribeFromCommand()
    {
        if (_command != null)
        {
            _command.IsExternalChanged -= OnIsExternalChanged;
            _command = null;
        }
    }

    private void OnIsExternalChanged(object? sender, EventArgs e)
        => UpdateIcon();

    private PropertyChangeNotifier? _commandNotifier;
    private NavigateCommand? _command;
    private DependencyProperty? _iconProperty;
    private object? _originalIcon;
    private bool _iconAdded;
}
