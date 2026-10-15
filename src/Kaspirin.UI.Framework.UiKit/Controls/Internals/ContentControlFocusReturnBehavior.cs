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
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals;

/// <summary>
///     The behavior for <see cref="ContentControl" />, which returns keyboard focus to the inside
///     of the content when changing content.
/// </summary>
internal sealed class ContentControlFocusReturnBehavior : Behavior<ContentControl, ContentControlFocusReturnBehavior>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        var contentControl = Guard.EnsureIsNotNull(AssociatedObject);

        _contentNotifier = new PropertyChangeNotifier(contentControl, ContentControl.ContentProperty);
        _contentNotifier.ValueChanged += OnContentChanged;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        if (_contentNotifier != null)
        {
            _contentNotifier.ValueChanged -= OnContentChanged;
            _contentNotifier.Dispose();
            _contentNotifier = null;
        }
    }

    private void OnContentChanged(DependencyObject source, object? oldValue, object? newValue)
    {
        var contentControl = Guard.EnsureIsNotNull(AssociatedObject);

        if (contentControl.IsKeyboardFocusWithin)
        {
            Executers.InUiAsync(RestoreFocus, DispatcherPriority.Input);
        }
    }

    private void RestoreFocus()
    {
        var contentControl = AssociatedObject;
        if (contentControl != null && contentControl.Focusable && !contentControl.IsKeyboardFocusWithin)
        {
            contentControl.Focus();
        }
    }

    private PropertyChangeNotifier? _contentNotifier;
}
