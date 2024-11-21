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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Controls.Behaviors;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_ActionButton, Type = typeof(ButtonBase))]
    public abstract class InputActionBase : Control
    {
        public const string PART_ActionButton = "PART_ActionButton";

        public override void OnApplyTemplate()
        {
            _repeatBehavior = new ButtonRepeatBehavior();
            _repeatBehavior.Repeat += OnRepeat;

            _button = (ButtonBase)GetTemplateChild(PART_ActionButton);
            _button.SetBinding(VisibilityProperty, new Binding() { Source = this, Path = _actionVisibilityProperty.AsPath() });
            _button.SetBinding(IsEnabledProperty, new Binding() { Source = this, Path = _actionIsEnabledProperty.AsPath() });
            _button.Click += OnButtonClick;
            _button.Focusable = false;

            Interaction.GetBehaviors(_button).Add(_repeatBehavior);

            _buttonPressedNotifier = new PropertyChangeNotifier<ButtonBase, bool>(_button, ButtonBase.IsPressedProperty);
            _buttonPressedNotifier.ValueChanged += OnButtonIsPressedChanged;
        }

        protected InputActionMode ActionMode { get; set; }

        protected void SetActionVisibility(bool isVisible)
            => SetValue(_actionVisibilityProperty, isVisible ? Visibility.Visible : Visibility.Collapsed);

        protected void SetActionEnabled(bool isEnabled)
            => SetValue(_actionIsEnabledProperty, isEnabled);

        protected virtual void OnClick()
        {
        }

        protected virtual void OnPressed(bool isPressed)
        {
        }

        private void OnRepeat()
        {
            if (ActionMode == InputActionMode.OnPressedWithRepeat)
            {
                OnPressed(true);
            }
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            if (ActionMode == InputActionMode.OnClick)
            {
                OnClick();
            }
        }

        private void OnButtonIsPressedChanged(ButtonBase sender, bool oldValue, bool newValue)
        {
            if (ActionMode == InputActionMode.OnPressed)
            {
                OnPressed(newValue);
            }
        }

        private static readonly DependencyProperty _actionVisibilityProperty = DependencyProperty.Register(
            "ActionVisibility",
            typeof(Visibility),
            typeof(InputActionBase),
            new PropertyMetadata(Visibility.Visible));

        private static readonly DependencyProperty _actionIsEnabledProperty = DependencyProperty.Register(
            "ActionIsEnabled",
            typeof(bool),
            typeof(InputActionBase),
            new PropertyMetadata(true));

        private ButtonRepeatBehavior? _repeatBehavior;
        private ButtonBase? _button;
        private PropertyChangeNotifier<ButtonBase, bool>? _buttonPressedNotifier;
    }
}
