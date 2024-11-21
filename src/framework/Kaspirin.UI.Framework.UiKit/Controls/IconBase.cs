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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_Image, Type = typeof(Image))]
    public abstract class IconBase : Control
    {
        public const string PART_Image = "PART_Image";

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Image = Guard.EnsureIsNotNull((Image)GetTemplateChild(PART_Image));
        }

        #region IsUnset

        public bool IsUnset
        {
            get { return (bool)GetValue(IsUnsetProperty); }
            protected set { SetValue(IsUnsetPropertyKey, value); }
        }

        protected static readonly DependencyPropertyKey IsUnsetPropertyKey =
            DependencyProperty.RegisterReadOnly("IsUnset", typeof(bool), typeof(IconBase), new PropertyMetadata(false));

        public static readonly DependencyProperty IsUnsetProperty = IsUnsetPropertyKey.DependencyProperty;

        #endregion

        #region IconForeground

        public Brush IconForeground
        {
            get => (Brush)GetValue(IconForegroundProperty);
            set => SetValue(IconForegroundProperty, value);
        }

        public static readonly DependencyProperty IconForegroundProperty = DependencyProperty.Register(
            nameof(IconForeground),
            typeof(Brush),
            typeof(IconBase),
            new PropertyMetadata(default(Brush), OnIconForegroundChanged));

        private static void OnIconForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((IconBase)d).OnIconForegroundChanged();

        #endregion

        protected abstract void OnIconForegroundChanged();

        protected Image? Image { get; private set; }
    }
}
