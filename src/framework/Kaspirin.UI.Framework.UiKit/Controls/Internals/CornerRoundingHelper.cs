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
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal class CornerRoundingHelper
    {
        public CornerRoundingHelper(DependencyObject target, Action callback, string? propertyName = null)
        {
            Guard.ArgumentIsNotNull(target);
            Guard.ArgumentIsNotNull(callback);

            _target = target;
            _cornerRaduisChangedCallback = callback;
            _cornerRaduisProperty = GetCornerRadiusProperty(target.GetType(), propertyName);

            _target.SetValue(_cornerRoundingHelperHolderProperty, this);
            _target.WhenLoaded(() =>
            {
                RaiseCornerRadiusChanged();
            });
        }

        public static readonly DependencyProperty DisableRoundingTopLeftProperty =
            DependencyProperty.RegisterAttached("DisableRoundingTopLeft", typeof(bool), typeof(CornerRoundingHelper),
                new PropertyMetadata(false, InvalidateCornerRadius));

        public static readonly DependencyProperty DisableRoundingTopRightProperty =
            DependencyProperty.RegisterAttached("DisableRoundingTopRight", typeof(bool), typeof(CornerRoundingHelper),
                new PropertyMetadata(false, InvalidateCornerRadius));

        public static readonly DependencyProperty DisableRoundingBottomLeftProperty =
            DependencyProperty.RegisterAttached("DisableRoundingBottomLeft", typeof(bool), typeof(CornerRoundingHelper),
                new PropertyMetadata(false, InvalidateCornerRadius));

        public static readonly DependencyProperty DisableRoundingBottomRightProperty =
            DependencyProperty.RegisterAttached("DisableRoundingBottomRight", typeof(bool), typeof(CornerRoundingHelper),
                new PropertyMetadata(false, InvalidateCornerRadius));

        private static readonly DependencyProperty _cornerRoundingHelperHolderProperty =
            DependencyProperty.RegisterAttached("CornerRoundingHelperHolder", typeof(CornerRoundingHelper), typeof(CornerRoundingHelper));

        public CornerRadius GetCornerRadius()
        {
            var cornerRadiustValue = _target.GetValue<CornerRadius>(_cornerRaduisProperty);

            return new CornerRadius
            {
                BottomLeft = _target.GetValue<bool>(DisableRoundingBottomLeftProperty) ? 0 : cornerRadiustValue.BottomLeft,
                BottomRight = _target.GetValue<bool>(DisableRoundingBottomRightProperty) ? 0 : cornerRadiustValue.BottomRight,
                TopLeft = _target.GetValue<bool>(DisableRoundingTopLeftProperty) ? 0 : cornerRadiustValue.TopLeft,
                TopRight = _target.GetValue<bool>(DisableRoundingTopRightProperty) ? 0 : cornerRadiustValue.TopRight,
            };
        }

        private DependencyProperty GetCornerRadiusProperty(Type type, string? propertyName)
        {
            propertyName ??= $"{type.Name}_CornerRadius";

            if (_cornerRadiusPropertyStorage.TryGetValue(propertyName, out var cornerRadiusProperty) is false)
            {
                Guard.AssertIsUiThread();

                cornerRadiusProperty = UIKitPropertyStorage.GetOrCreate(propertyName, typeof(CornerRadius));
                cornerRadiusProperty.OverrideMetadata(type, new PropertyMetadata(InvalidateCornerRadius));
                _cornerRadiusPropertyStorage[propertyName] = cornerRadiusProperty;
            }

            return cornerRadiusProperty;
        }

        private void RaiseCornerRadiusChanged()
        {
            _cornerRaduisChangedCallback.Invoke();
        }

        private static void InvalidateCornerRadius(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.GetValue<CornerRoundingHelper>(_cornerRoundingHelperHolderProperty).RaiseCornerRadiusChanged();
        }

        private static readonly Dictionary<string, DependencyProperty> _cornerRadiusPropertyStorage = new();

        private readonly DependencyObject _target;
        private readonly DependencyProperty _cornerRaduisProperty;
        private readonly Action _cornerRaduisChangedCallback;
    }
}
