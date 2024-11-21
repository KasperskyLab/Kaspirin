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

namespace Kaspirin.UI.Framework.UiKit.Animation.Markup
{
    public sealed class AnimatedBindingExtension : ExtendedMarkupExtension
    {
        public AnimatedBindingExtension()
        {
            _animationSettingsProvider = ServiceLocator.Instance.GetService<IAnimationSettingsProvider>();
            _animatedBindingFactory = ServiceLocator.Instance.GetService<AnimatedBindingFactory>();

            Properties = _animationSettingsProvider.DefaultAnimationProperties;
        }

        public BindingBase? Source { get; set; }

        public AnimationProperties Properties { get; set; }

        protected override object? ProvideForControl(IServiceProvider serviceProvider, DependencyObject targetObject, DependencyProperty targetProperty)
        {
            Guard.IsNotNull(Source);

            return _animatedBindingFactory.CreateBinding(Source, targetObject, targetProperty, Properties)?.ProvideValue(serviceProvider);
        }

        private readonly IAnimationSettingsProvider _animationSettingsProvider;
        private readonly AnimatedBindingFactory _animatedBindingFactory;
    }
}
