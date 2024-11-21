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
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit
{
    [MarkupExtensionReturnType(typeof(Style))]
    internal sealed class UIKitImplicitStyleExtension : MarkupExtension
    {
        public UIKitImplicitStyleExtension(string propertyName)
        {
            Normal = CreateGenericPropertyPath(propertyName);
            Hover = CreateGenericPropertyPath(propertyName + nameof(Hover));
            Pressed = CreateGenericPropertyPath(propertyName + nameof(Pressed));
            Disabled = CreateGenericPropertyPath(propertyName + nameof(Disabled));
            SelectedNormal = CreateGenericPropertyPath(propertyName + nameof(SelectedNormal));
            SelectedHover = CreateGenericPropertyPath(propertyName + nameof(SelectedHover));
            SelectedPressed = CreateGenericPropertyPath(propertyName + nameof(SelectedPressed));
            SelectedDisabled = CreateGenericPropertyPath(propertyName + nameof(SelectedDisabled));
        }

        public PropertyPath Normal { get; set; }

        public PropertyPath Hover { get; set; }

        public PropertyPath Pressed { get; set; }

        public PropertyPath Disabled { get; set; }

        public PropertyPath SelectedNormal { get; set; }

        public PropertyPath SelectedHover { get; set; }

        public PropertyPath SelectedPressed { get; set; }

        public PropertyPath SelectedDisabled { get; set; }

        public SetterBaseCollection NormalAdditionalSetters => _normalAdditionalSetters ??= new SetterBaseCollection();

        public SetterBaseCollection HoverAdditionalSetters => _hoverAdditionalSetters ??= new SetterBaseCollection();

        public SetterBaseCollection PressedAdditionalSetters => _pressedAdditionalSetters ??= new SetterBaseCollection();

        public SetterBaseCollection DisabledAdditionalSetters => _disabledAdditionalSetters ??= new SetterBaseCollection();

        public SetterBaseCollection SelectedNormalAdditionalSetters => _selectedNormalAdditionalSetters ??= new SetterBaseCollection();

        public SetterBaseCollection SelectedHoverAdditionalSetters => _selectedHoverAdditionalSetters ??= new SetterBaseCollection();

        public SetterBaseCollection SelectedPressedAdditionalSetters => _selectedPressedAdditionalSetters ??= new SetterBaseCollection();

        public SetterBaseCollection SelectedDisabledAdditionalSetters => _selectedDisabledAdditionalSetters ??= new SetterBaseCollection();

        public Style? BasedOnStyle { get; set; }

        public Type? StateAwareAncestorType { get; set; }

        public Type? TargetType { get; set; }

        public override object? ProvideValue(IServiceProvider serviceProvider)
        {
            var multibinding = new MultiBinding
            {
                Converter = new StateAwareStyleMultibindingConverter()
                {
                    BasedOnStyle = BasedOnStyle,
                    NormalAdditionalSetters = _normalAdditionalSetters,
                    HoverAdditionalSetters = _hoverAdditionalSetters,
                    PressedAdditionalSetters = _pressedAdditionalSetters,
                    DisabledAdditionalSetters = _disabledAdditionalSetters,
                    SelectedNormalAdditionalSetters = _selectedNormalAdditionalSetters,
                    SelectedHoverAdditionalSetters = _selectedHoverAdditionalSetters,
                    SelectedPressedAdditionalSetters = _selectedPressedAdditionalSetters,
                    SelectedDisabledAdditionalSetters = _selectedDisabledAdditionalSetters,
                    StateAwareAncestorType = StateAwareAncestorType,
                    TargetType = TargetType
                }
            };

            multibinding.Bindings.Add(CreateBinding(Normal));
            multibinding.Bindings.Add(CreateBinding(Hover));
            multibinding.Bindings.Add(CreateBinding(Pressed));
            multibinding.Bindings.Add(CreateBinding(Disabled));
            multibinding.Bindings.Add(CreateBinding(SelectedNormal));
            multibinding.Bindings.Add(CreateBinding(SelectedHover));
            multibinding.Bindings.Add(CreateBinding(SelectedPressed));
            multibinding.Bindings.Add(CreateBinding(SelectedDisabled));

            var result = multibinding.ProvideValue(serviceProvider);
            return result;
        }

        private static PropertyPath CreateGenericPropertyPath(string propertyName)
        {
            return UIKitPropertyStorage.GetOrCreate(propertyName, typeof(Style)).AsPath();
        }

        private static Binding CreateBinding(PropertyPath path)
        {
            return new Binding
            {
                Path = path,
                RelativeSource = new RelativeSource()
                {
                    Mode = RelativeSourceMode.TemplatedParent,
                }
            };
        }

        private sealed class StateAwareStyleMultibindingConverter : MultiValueConverterMarkupExtension<StateAwareStyleMultibindingConverter>
        {
            public Style? BasedOnStyle { get; set; }

            public SetterBaseCollection? NormalAdditionalSetters { get; set; }

            public SetterBaseCollection? HoverAdditionalSetters { get; set; }

            public SetterBaseCollection? PressedAdditionalSetters { get; set; }

            public SetterBaseCollection? DisabledAdditionalSetters { get; set; }

            public SetterBaseCollection? SelectedNormalAdditionalSetters { get; set; }

            public SetterBaseCollection? SelectedHoverAdditionalSetters { get; set; }

            public SetterBaseCollection? SelectedPressedAdditionalSetters { get; set; }

            public SetterBaseCollection? SelectedDisabledAdditionalSetters { get; set; }

            public Type? StateAwareAncestorType { get; set; }

            public Type? TargetType { get; set; }

            public override object? Convert(object?[] values, Type targetType, object? parameter, CultureInfo culture)
            {
                if (values == null || values.Length == 0)
                {
                    return DependencyProperty.UnsetValue;
                }

                if (values[0] is not Style normal)
                {
                    return DependencyProperty.UnsetValue;
                }

                var hover = values.Length >= 2 ? values[1] as Style : null;
                var pressed = values.Length >= 3 ? values[2] as Style : null;
                var disabled = values.Length >= 4 ? values[3] as Style : null;
                var selectedNormal = values.Length >= 5 ? values[4] as Style : null;
                var selectedHover = values.Length >= 6 ? values[5] as Style : null;
                var selectedPressed = values.Length >= 7 ? values[6] as Style : null;
                var selectedDisabled = values.Length >= 8 ? values[7] as Style : null;

                if (selectedNormal != null ||
                    selectedHover != null ||
                    selectedPressed != null ||
                    selectedDisabled != null)
                {
                    return StyleExtensions.MergeWithSelectableStateAware(
                        normal,
                        hover,
                        pressed,
                        disabled,
                        selectedNormal,
                        selectedHover,
                        selectedPressed,
                        selectedDisabled,
                        BasedOnStyle,
                        NormalAdditionalSetters,
                        HoverAdditionalSetters,
                        PressedAdditionalSetters,
                        DisabledAdditionalSetters,
                        SelectedNormalAdditionalSetters,
                        SelectedHoverAdditionalSetters,
                        SelectedPressedAdditionalSetters,
                        SelectedDisabledAdditionalSetters,
                        StateAwareAncestorType,
                        TargetType);
                }
                else
                {
                    return StyleExtensions.MergeWithStateAware(
                        normal,
                        hover,
                        pressed,
                        disabled,
                        BasedOnStyle,
                        NormalAdditionalSetters,
                        HoverAdditionalSetters,
                        PressedAdditionalSetters,
                        DisabledAdditionalSetters,
                        StateAwareAncestorType,
                        TargetType);
                }
            }
        }

        private SetterBaseCollection? _normalAdditionalSetters = null;
        private SetterBaseCollection? _hoverAdditionalSetters = null;
        private SetterBaseCollection? _pressedAdditionalSetters = null;
        private SetterBaseCollection? _disabledAdditionalSetters = null;
        private SetterBaseCollection? _selectedNormalAdditionalSetters = null;
        private SetterBaseCollection? _selectedHoverAdditionalSetters = null;
        private SetterBaseCollection? _selectedPressedAdditionalSetters = null;
        private SetterBaseCollection? _selectedDisabledAdditionalSetters = null;
    }
}
