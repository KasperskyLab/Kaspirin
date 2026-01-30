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
using System.Windows;
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Animation.Internals;
using Kaspirin.UI.Framework.UiKit.MarkupExtensions;

namespace Kaspirin.UI.Framework.UiKit.Animation.Markup;

/// <summary>
///     Triggers an animated change in the target property for each change in the binding value <see cref="Source" />.
/// </summary>
public sealed class AnimatedBindingExtension : ExtendedMarkupExtension
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="AnimatedBindingExtension" /> class.
    /// </summary>
    public AnimatedBindingExtension()
    {
        _animationManager = ServiceLocator.GetService<IAnimationManager>();
        _animatedBindingFactory = ServiceLocator.GetService<AnimatedBindingFactory>();

        Properties = _animationManager.GetAnimationProperties();
    }

    /// <summary>
    ///     The binding used to get the values.
    /// </summary>
    public BindingBase? Source { get; set; }

    /// <summary>
    ///     Animation properties.
    /// </summary>
    public AnimationProperties Properties { get; set; }

    /// <inheritdoc/>
    protected override object? ProvideForControl(IServiceProvider serviceProvider, DependencyObject targetObject, DependencyProperty targetProperty)
    {
        Guard.IsNotNull(Source);

        return _animatedBindingFactory.CreateBinding(Source, targetObject, targetProperty, Properties)?.ProvideValue(serviceProvider);
    }

    private readonly IAnimationManager _animationManager;
    private readonly AnimatedBindingFactory _animatedBindingFactory;
}