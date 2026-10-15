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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Controls.Properties;

namespace Kaspirin.UI.Framework.UiKit.Controls.Behaviors;

public sealed class ButtonBaseNavigateCommandBehavior : BaseNavigateCommandBehavior<ButtonBase, ButtonBaseNavigateCommandBehavior>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        _contentNotifier = new PropertyChangeNotifier(Guard.EnsureIsNotNull(AssociatedObject), ContentControl.ContentProperty);
        _contentNotifier.ValueChanged += OnContentChanged;
    }

    protected override void OnDetaching()
    {
        if (_contentNotifier != null)
        {
            _contentNotifier.ValueChanged -= OnContentChanged;
            _contentNotifier.Dispose();
            _contentNotifier = null;
        }

        base.OnDetaching();
    }

    protected override DependencyProperty CommandProperty => ButtonBase.CommandProperty;

    protected override bool CanAddIcon()
        => AssociatedObject?.Content != null;

    protected override void AddIcon()
    {
        var button = Guard.EnsureIsNotNull(AssociatedObject);

        if (button is BigButton)
        {
            ApplyIcon(BigButton.IconProperty, UIKitIcon_24.LinkExternal);
            ApplyIconLocation(BigButton.IconLocationProperty);
        }
        else if (button is SmallButton)
        {
            ApplyIcon(SmallButton.IconProperty, UIKitIcon_16.LinkExternal);
            ApplyIconLocation(SmallButton.IconLocationProperty);
        }
        else if (button is Button)
        {
            ApplyIcon(ButtonProps.IconProperty, UIKitIcon_16.LinkExternal);
            ApplyIconLocation(ButtonProps.IconLocationProperty);
        }
    }

    protected override void RemoveIcon()
    {
        RestoreIcon();
        RestoreIconLocation();
    }

    private void OnContentChanged(DependencyObject source, object? oldValue, object? newValue)
        => UpdateIcon();

    private void ApplyIconLocation(DependencyProperty property)
    {
        var element = Guard.EnsureIsNotNull(AssociatedObject);

        _originalIconLocation = element.ReadLocalValue(property);
        _iconLocationProperty = property;

        element.SetValue(property, ButtonIconLocation.Right);
    }

    private void RestoreIconLocation()
    {
        if (_originalIconLocation == null)
        {
            return;
        }

        var element = Guard.EnsureIsNotNull(AssociatedObject);

        if (_originalIconLocation is BindingExpressionBase originalBinding)
        {
            BindingOperations.SetBinding(element, _iconLocationProperty, originalBinding.ParentBindingBase);
        }
        else if (_originalIconLocation == null || _originalIconLocation == DependencyProperty.UnsetValue)
        {
            element.ClearValue(_iconLocationProperty);
        }
        else
        {
            element.SetValue(_iconLocationProperty, _originalIconLocation);
        }

        _iconLocationProperty = null;
        _originalIconLocation = null;
    }

    private PropertyChangeNotifier? _contentNotifier;
    private DependencyProperty? _iconLocationProperty;
    private object? _originalIconLocation;
}
