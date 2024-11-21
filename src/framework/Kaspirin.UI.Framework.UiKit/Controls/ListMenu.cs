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

using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using Kaspirin.UI.Framework.UiKit.Controls.Properties;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_ScrollViewer, Type = typeof(ScrollViewer))]
    [StyleTypedProperty(Property = "ItemContainerStyle", StyleTargetType = typeof(ListMenuItem))]
    public class ListMenu : SelectorList<ListMenuItem>
    {
        public const string PART_ScrollViewer = "PART_ScrollViewer";

        public ListMenu()
        {
            SetValue(Grid.IsSharedSizeScopeProperty, true);

            this.WhenLoaded(ApplyIsScrollEnabled);
        }

        #region IsVirtualizing

        public bool IsVirtualizing
        {
            get => (bool)GetValue(IsVirtualizingProperty);
            set => SetValue(IsVirtualizingProperty, value);
        }

        public static readonly DependencyProperty IsVirtualizingProperty = DependencyProperty.Register(
            nameof(IsVirtualizing),
            typeof(bool),
            typeof(ListMenu),
            new PropertyMetadata(default(bool)));

        #endregion

        #region IsScrollEnabled

        public bool IsScrollEnabled
        {
            get => (bool)GetValue(IsScrollEnabledProperty);
            set => SetValue(IsScrollEnabledProperty, value);
        }

        public static readonly DependencyProperty IsScrollEnabledProperty = DependencyProperty.Register(
            nameof(IsScrollEnabled),
            typeof(bool),
            typeof(ListMenu),
            new PropertyMetadata(true, OnIsScrollEnabledChanged));

        private static void OnIsScrollEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((ListMenu)d).OnIsScrollEnabledChanged();

        #endregion

        public override void OnApplyTemplate()
        {
            _scrollViewer = (ScrollViewer)GetTemplateChild(PART_ScrollViewer);
            _scrollViewer.SetBinding(ScrollViewer.CanContentScrollProperty, new Binding() { Source = this, Path = IsVirtualizingProperty.AsPath() });
            _scrollViewer.SetValue(ScrollViewerProps.OuterVerticalScrollBarProperty, true);
            _scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            _scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            _scrollViewer.IsDeferredScrollingEnabled = false;

            ApplyIsScrollEnabled();
        }

        private void OnIsScrollEnabledChanged()
        {
            ApplyIsScrollEnabled();
        }

        private void ApplyIsScrollEnabled()
        {
            if (_scrollViewer != null)
            {
                _scrollViewer.SetValue(ScrollViewerProps.CanMouseWheelScrollProperty, IsScrollEnabled);
            }
        }

        private ScrollViewer? _scrollViewer;
    }
}
