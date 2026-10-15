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
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace Kaspirin.UI.Framework.UiKit.Animation.Markup.Easing;

/// <summary>
///     The base class of markup extensions for initializing classes implementing the <see cref="IEasingFunction" /> interface.
/// </summary>
[MarkupExtensionReturnType(typeof(IEasingFunction))]
public abstract class EasingExtensionBase : MarkupExtension
{
    /// <summary>
    ///     Animation execution mode.
    /// </summary>
    public EasingMode Mode { get; set; }

    /// <inheritdoc/>
    public sealed override object? ProvideValue(IServiceProvider? serviceProvider)
        => CreateEasing();

    /// <summary>
    ///     Creates an animation function that implements <see cref="IEasingFunction" />.
    /// </summary>
    /// <returns>
    ///     An instance of <see cref="IEasingFunction" />.
    /// </returns>
    protected abstract IEasingFunction CreateEasing();
}
