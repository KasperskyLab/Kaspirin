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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Kaspirin.UI.Framework.UiKit
{
    internal sealed class UIKitContentPresenter : ContentPresenter
    {
        static UIKitContentPresenter()
        {
            ContentProperty.OverrideMetadata(typeof(UIKitContentPresenter), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnContentChanged)));
        }

        public UIKitContentPresenter()
        {
            ContentImpicitStyles = new ObservableCollection<UIKitStyleHolder>();
            ContentImpicitStyles.CollectionChanged += OnContentImpicitStylesChanged;
        }

        #region ContentImpicitStyles

        public ObservableCollection<UIKitStyleHolder> ContentImpicitStyles
        {
            get { return (ObservableCollection<UIKitStyleHolder>)GetValue(ContentImpicitStylesProperty); }
            set { SetValue(ContentImpicitStylesProperty, value); }
        }

        public static readonly DependencyProperty ContentImpicitStylesProperty =
            DependencyProperty.Register("ContentImpicitStyles", typeof(ObservableCollection<UIKitStyleHolder>), typeof(UIKitContentPresenter));

        #endregion

        internal void SetImplicitStyles(DependencyObject d)
        {
            var styles = ContentImpicitStyles.Select(sr => sr.StyleRef);

            var resources = GetElementResources(d);
            if (resources != null)
            {
                AddOrUpdateStyles(styles, resources);
            }
        }

        private void OnContentImpicitStylesChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null)
            {
                foreach (var sh in e.OldItems.Cast<UIKitStyleHolder>())
                {
                    sh.StyleRefChanged -= OnStyleHolderChanged;
                    RemoveStyleInjection(sh.StyleRef);
                }
            }

            if (e.NewItems != null)
            {
                foreach (var sh in e.NewItems.Cast<UIKitStyleHolder>())
                {
                    sh.StyleRefChanged += OnStyleHolderChanged;
                    AddStyleInjection(sh.StyleRef);
                }
            }
        }

        private void OnStyleHolderChanged(Style? newStyle, Style? oldStyle)
        {
            RemoveStyleInjection(oldStyle);
            AddStyleInjection(newStyle);
        }

        private void AddStyleInjection(Style? style)
        {
            if (style == null)
            {
                return;
            }

            var controlRes = GetElementResources(this);
            if (controlRes != null)
            {
                AddOrUpdateStyle(style, controlRes);
            }

            var contentRes = GetElementResources(Content);
            if (contentRes != null)
            {
                AddOrUpdateStyle(style, contentRes);
            }
        }

        private void RemoveStyleInjection(Style? style)
        {
            if (style == null)
            {
                return;
            }

            var controlRes = GetElementResources(this);
            if (controlRes != null)
            {
                RemoveStyle(style, controlRes);
            }

            var contentRes = GetElementResources(Content);
            if (contentRes != null)
            {
                RemoveStyle(style, contentRes);
            }
        }

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is UIElement uiElement)
            {
                uiElement.WhenLoaded(() => ((UIKitContentPresenter)d).SetImplicitStyles(uiElement));
            }
        }

        private static void RemoveStyle(Style style, ResourceDictionary rd)
        {
            var styleKey = style.TargetType;

            var md = rd.MergedDictionaries.FirstOrDefault();
            if (md != null && md.Contains(styleKey))
            {
                md.Remove(styleKey);
            }
        }
        private static void AddOrUpdateStyles(IEnumerable<Style> styles, ResourceDictionary rd)
        {
            foreach (var style in styles)
            {
                AddOrUpdateStyle(style, rd);
            }
        }

        private static void AddOrUpdateStyle(Style style, ResourceDictionary rd)
        {
            var styleKey = style.TargetType;

            var md = rd.MergedDictionaries.FirstOrDefault();
            if (md == null)
            {
                md = new ResourceDictionary { { styleKey, style } };
                rd.MergedDictionaries.Add(md);
            }
            else
            {
                if (md.Contains(styleKey))
                {
                    if (md[styleKey] != style)
                    {
                        md[styleKey] = style;
                    }
                }
                else
                {
                    md.Add(styleKey, style);
                }
            }
        }

        private static ResourceDictionary? GetElementResources(object content)
        {
            ResourceDictionary? resources = null;

            if (content is FrameworkElement fe)
            {
                resources = fe.Resources;
            }
            else if (content is FrameworkContentElement fce)
            {
                resources = fce.Resources;
            }

            return resources;
        }
    }
}
