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

using System.ComponentModel;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Services;

/// <summary>
///     Tracks the change in the dependency property for the control.
/// </summary>
/// <remarks>
///     A lighter version <see cref="PropertyChangeNotifier{TControl,TProperty}" />. Uses <see cref="DependencyPropertyDescriptor" />
///     instead of <see cref="INotifyPropertyChanged" />. To prevent memory leaks, call the <see cref="PropertyChangeNotifierSlim{TControl, TProperty}.Release" /> method at the end of use.
/// </remarks>
public sealed class PropertyChangeNotifierSlim : PropertyChangeNotifierSlim<FrameworkElement, object>
{
    public PropertyChangeNotifierSlim(FrameworkElement propertySource, DependencyProperty property)
        : base(propertySource, property)
    {
    }
}
