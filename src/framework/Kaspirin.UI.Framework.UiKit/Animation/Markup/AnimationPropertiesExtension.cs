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

namespace Kaspirin.UI.Framework.UiKit.Animation.Markup
{
    public sealed class AnimationPropertiesExtension : MarkupExtension
    {
        public TimeSpan Duration { get; set; }

        public TimeSpan Delay { get; set; }

        public IEasingFunction? Easing { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new AnimationProperties()
            {
                Duration = Duration,
                Delay = Delay,
                Easing = Easing
            };
        }
    }
}
