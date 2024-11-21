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
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using Kaspirin.UI.Framework.UiKit.Controls.VisualStates;

namespace Kaspirin.UI.Framework.UiKit.Extensions
{
    public static class StyleExtensions
    {
        /// <summary>
        ///     Combines two styles passed as parameters.
        /// </summary>
        /// <param name="style1">
        ///     The first style to combine.
        /// </param>
        /// <param name="style2">
        ///     The second style is for combining.
        /// </param>
        /// <remarks>
        ///     The <paramref name="style1" /> object will be modified to include information from <paramref name="style2" />.
        ///     In case of collisions, <paramref name="style2" /> takes precedence.
        /// </remarks>
        public static void Merge(this Style style1, Style style2)
        {
            // Source: https://stackoverflow.com/questions/5745001/xaml-combine-styles-going-beyond-basedon

            Guard.ArgumentIsNotNull(style1);
            Guard.ArgumentIsNotNull(style2);

            if (style1.TargetType.IsAssignableFrom(style2.TargetType))
            {
                style1.TargetType = style2.TargetType;
            }

            if (style2.BasedOn != null)
            {
                Merge(style1, style2.BasedOn);
            }

            foreach (var currentSetter in style2.Setters)
            {
                style1.Setters.Add(currentSetter);
            }

            foreach (var currentTrigger in style2.Triggers)
            {
                style1.Triggers.Add(currentTrigger);
            }

            // This code is only needed when using DynamicResources.
            foreach (var key in style2.Resources.Keys)
            {
                style1.Resources[key] = style2.Resources[key];
            }
        }

        public static Style MergeWithStateAware(
            Style normalStyle,
            Style? hoverStyle = null,
            Style? pressedStyle = null,
            Style? disabledStyle = null,
            Style? basedOnStyle = null,
            SetterBaseCollection? normalAdditionalSetters = null,
            SetterBaseCollection? hoverAdditionalSetters = null,
            SetterBaseCollection? pressedAdditionalSetters = null,
            SetterBaseCollection? disabledAdditionalSetters = null,
            Type? stateAwareAncestorType = null,
            Type? targetType = null,
            bool fallbackToNormalStyle = true)
        {
            Guard.ArgumentIsNotNull(normalStyle, "Normal style is not set");

            targetType ??= normalStyle.TargetType;
            stateAwareAncestorType ??= targetType;

            var mergedStyle = new Style() { TargetType = targetType };

            if (basedOnStyle != null)
            {
                Guard.Assert(basedOnStyle.TargetType.IsAssignableFrom(mergedStyle.TargetType),
                    $"TargetType {basedOnStyle.TargetType} of base style is not compatible " +
                    $"with TargetType {mergedStyle.TargetType} of merged style");
                mergedStyle.BasedOn = basedOnStyle;
            }

            if (fallbackToNormalStyle)
            {
                mergedStyle.Merge(normalStyle);

                foreach (var setter in normalAdditionalSetters ?? Enumerable.Empty<SetterBase>())
                {
                    mergedStyle.Setters.Add(setter);
                }
            }
            else
            {
                MergeState(mergedStyle, normalStyle, State.Normal, stateAwareAncestorType, normalAdditionalSetters);
            }

            var stateStyle = hoverStyle != null && (!fallbackToNormalStyle || hoverStyle != normalStyle) ? hoverStyle : null;
            MergeState(mergedStyle, stateStyle, State.Hover, stateAwareAncestorType, hoverAdditionalSetters);

            stateStyle = pressedStyle != null && (!fallbackToNormalStyle || pressedStyle != normalStyle) ? pressedStyle : null;
            MergeState(mergedStyle, stateStyle, State.Pressed, stateAwareAncestorType, pressedAdditionalSetters);

            stateStyle = disabledStyle != null && (!fallbackToNormalStyle || disabledStyle != normalStyle) ? disabledStyle : null;
            MergeState(mergedStyle, stateStyle, State.Disabled, stateAwareAncestorType, disabledAdditionalSetters);

            return mergedStyle;
        }

        public static Style MergeWithSelectableStateAware(
            Style normalStyle,
            Style? hoverStyle = null,
            Style? pressedStyle = null,
            Style? disabledStyle = null,
            Style? selectedNormalStyle = null,
            Style? selectedHoverStyle = null,
            Style? selectedPressedStyle = null,
            Style? selectedDisabledStyle = null,
            Style? basedOnStyle = null,
            SetterBaseCollection? normalAdditionalSetters = null,
            SetterBaseCollection? hoverAdditionalSetters = null,
            SetterBaseCollection? pressedAdditionalSetters = null,
            SetterBaseCollection? disabledAdditionalSetters = null,
            SetterBaseCollection? selectedNormalAdditionalSetters = null,
            SetterBaseCollection? selectedHoverAdditionalSetters = null,
            SetterBaseCollection? selectedPressedAdditionalSetters = null,
            SetterBaseCollection? selectedDisabledAdditionalSetters = null,
            Type? stateAwareAncestorType = null,
            Type? targetType = null,
            bool fallbackToNormalStyle = true)
        {
            Guard.ArgumentIsNotNull(normalStyle, "Normal style is not set");

            targetType ??= normalStyle.TargetType;
            stateAwareAncestorType ??= targetType;

            var mergedStyle = new Style() { TargetType = targetType };

            if (basedOnStyle != null)
            {
                Guard.Assert(basedOnStyle.TargetType.IsAssignableFrom(mergedStyle.TargetType),
                    $"TargetType {basedOnStyle.TargetType} of base style is not compatible " +
                    $"with TargetType {mergedStyle.TargetType} of merged style");
                mergedStyle.BasedOn = basedOnStyle;
            }

            if (fallbackToNormalStyle)
            {
                mergedStyle.Merge(normalStyle);

                foreach (var setter in normalAdditionalSetters ?? Enumerable.Empty<SetterBase>())
                {
                    mergedStyle.Setters.Add(setter);
                }
            }
            else
            {
                MergeState(mergedStyle, normalStyle, SelectableState.Normal, stateAwareAncestorType, normalAdditionalSetters);
            }

            var stateStyle = hoverStyle != null && (!fallbackToNormalStyle || hoverStyle != normalStyle) ? hoverStyle : null;
            MergeState(mergedStyle, stateStyle, SelectableState.Hover, stateAwareAncestorType, hoverAdditionalSetters);

            stateStyle = pressedStyle != null && (!fallbackToNormalStyle || pressedStyle != normalStyle) ? pressedStyle : null;
            MergeState(mergedStyle, stateStyle, SelectableState.Pressed, stateAwareAncestorType, pressedAdditionalSetters);

            stateStyle = disabledStyle != null && (!fallbackToNormalStyle || disabledStyle != normalStyle) ? disabledStyle : null;
            MergeState(mergedStyle, stateStyle, SelectableState.Disabled, stateAwareAncestorType, disabledAdditionalSetters);

            stateStyle = selectedNormalStyle != null && (!fallbackToNormalStyle || selectedNormalStyle != normalStyle) ? selectedNormalStyle : null;
            MergeState(mergedStyle, stateStyle, SelectableState.SelectedNormal, stateAwareAncestorType, selectedNormalAdditionalSetters);

            stateStyle = selectedHoverStyle != null && (!fallbackToNormalStyle || selectedHoverStyle != normalStyle) ? selectedHoverStyle : null;
            MergeState(mergedStyle, stateStyle, SelectableState.SelectedHover, stateAwareAncestorType, selectedHoverAdditionalSetters);

            stateStyle = selectedPressedStyle != null && (!fallbackToNormalStyle || selectedPressedStyle != normalStyle) ? selectedPressedStyle : null;
            MergeState(mergedStyle, stateStyle, SelectableState.SelectedPressed, stateAwareAncestorType, selectedPressedAdditionalSetters);

            stateStyle = selectedDisabledStyle != null && (!fallbackToNormalStyle || selectedDisabledStyle != normalStyle) ? selectedDisabledStyle : null;
            MergeState(mergedStyle, stateStyle, SelectableState.SelectedDisabled, stateAwareAncestorType, selectedDisabledAdditionalSetters);

            return mergedStyle;
        }

        public static T GetPropertyValueOrDefault<T>(this Style style, DependencyProperty property)
        {
            return style.GetPropertyValue<T>(property) ?? (T)property.DefaultMetadata.DefaultValue;
        }

        public static T? GetPropertyValue<T>(this Style style, DependencyProperty property)
        {
            var setter = style.Setters.OfType<Setter>().GuardedSingleOrDefault(s => s.Property == property);
            if (setter != null)
            {
                var setterValue = setter.Value;
                return setterValue switch
                {
                    MultiBinding multiBinding => GetPropertyValueFromMetadata(multiBinding),
                    _ => (T)setter.Value
                };
            }
            else if (style.BasedOn != null)
            {
                return GetPropertyValue<T>(style.BasedOn, property);
            }

            return default;

            static T? GetPropertyValueFromMetadata(MultiBinding metadataBinding)
            {
                var meta = MetadataHelper.GetPropertyMetadata(metadataBinding);
                if (meta != null)
                {
                    var localizer = LocalizationManager.Current.LocalizerFactory.Resolve(meta.Scope, meta.LocalizerType);
                    var value = localizer.GetValue<T>(meta.Key);
                    if (value != null)
                    {
                        return (T)value;
                    }
                }

                return default;
            }
        }

        private static void MergeState(
            Style mergedStyle,
            Style? stateStyle,
            Enum state,
            Type stateAwareAncestorType,
            SetterBaseCollection? additionalSetters)
        {
            var setters = GetSetters(stateStyle)
                .Concat(additionalSetters ?? Enumerable.Empty<SetterBase>())
                .ToArray();

            if (setters.Length == 0)
            {
                return;
            }

            if (stateStyle != null)
            {
                Guard.Assert(stateStyle.TargetType.IsAssignableFrom(mergedStyle.TargetType),
                    $"TargetType {stateStyle.TargetType} of style for state {state} is not compatible " +
                    $"with TargetType {mergedStyle.TargetType} of merged style");
            }

            var stateProperty = state is State
                ? StateService.StateProperty
                : StateService.SelectableStateProperty;

            var stateBinding = new Binding()
            {
                Path = stateProperty.AsPath(),
                RelativeSource = new RelativeSource() { Mode = RelativeSourceMode.FindAncestor, AncestorType = stateAwareAncestorType }
            };

            var stateDataTrigger = new DataTrigger()
            {
                Binding = stateBinding,
                Value = state
            };

            foreach (var setter in setters)
            {
                stateDataTrigger.Setters.Add(setter);
            }

            mergedStyle.Triggers.Add(stateDataTrigger);
        }

        private static IEnumerable<SetterBase> GetSetters(Style? style, bool includeBaseStyle = true)
        {
            if (style is null)
            {
                yield break;
            }

            if (includeBaseStyle)
            {
                foreach (var baseSetter in GetSetters(style.BasedOn, includeBaseStyle))
                {
                    yield return baseSetter;
                }
            }

            foreach (var setter in style.Setters)
            {
                yield return setter;
            }
        }
    }
}