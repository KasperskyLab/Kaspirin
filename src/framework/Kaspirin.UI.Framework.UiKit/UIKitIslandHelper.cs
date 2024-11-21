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
using System.Windows.Controls;
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Controls;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit
{
    internal static class UIKitIslandHelper
    {
        #region Level

        public static readonly DependencyPropertyKey LevelPropertyKey =
            DependencyProperty.RegisterReadOnly("Level", typeof(IslandLevel), typeof(IslandPropsHolder), new PropertyMetadata(IslandLevel.First));

        private static readonly DependencyProperty _levelEditableProperty =
            DependencyProperty.RegisterAttached("LevelEditable", typeof(IslandLevel), typeof(IslandPropsHolder), new PropertyMetadata(IslandLevel.First, OnLevelEditableChanged));

        private static void OnLevelEditableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Control)d).SetValue(LevelPropertyKey, e.NewValue);
        }

        #endregion

        #region Type

        public static readonly DependencyPropertyKey TypePropertyKey =
            DependencyProperty.RegisterReadOnly("Type", typeof(IslandType), typeof(IslandPropsHolder), new PropertyMetadata(IslandType.Primary));

        private static readonly DependencyProperty _typeEditableProperty =
            DependencyProperty.RegisterAttached("TypeEditable", typeof(IslandType), typeof(IslandPropsHolder), new PropertyMetadata(IslandType.Primary, OnTypeEditableChanged));

        private static void OnTypeEditableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((Control)d).SetValue(TypePropertyKey, e.NewValue);
        }

        #endregion

        public static void Initialize(Control control)
        {
            Guard.ArgumentIsNotNull(control);

            control.WhenLoaded(() =>
            {
                InvalidateLevel(control);
            });

            control.WhenInitialized(() =>
            {
                InvalidateType(control);
            });
        }

        private static void InvalidateLevel(Control control)
        {
            var nestingLevel = _islandContainers.SelectMany(ct => control.TraverseVisualParents().Where(p => ct.IsAssignableFrom(p.GetType()))).Count();
            if (nestingLevel > _maxNestingLevel)
            {
                throw new InvalidOperationException($"Invalid {control.GetType().Name} level. Max level {_maxNestingLevel}. Actual level {nestingLevel + 1}");
            }

            control.SetValue(_levelEditableProperty, (IslandLevel)nestingLevel);
        }

        private static void InvalidateType(Control control)
        {
            control.SetBinding(_typeEditableProperty, new Binding()
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(IslandLayer), 1),
                Path = IslandLayer.TypeProperty.AsPath(),
                FallbackValue = IslandType.Primary
            });
        }

        private sealed class IslandPropsHolder : UIElement { }

        private static readonly List<Type> _islandContainers = new()
        {
            typeof(Island),
            typeof(IslandButton),
            typeof(IslandToggleButton),
        };

        private static readonly int _maxNestingLevel = Enum.GetValues(typeof(IslandLevel)).Length;
    }
}
