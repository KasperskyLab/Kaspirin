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

namespace Kaspirin.UI.Framework.UiKit.Extensions
{
    public static class ScrollViewerExtensions
    {
        public static bool CanScrollLeft(this ScrollViewer scrollViewer)
        {
            return scrollViewer.HorizontalOffset > 0;
        }

        public static bool CanScrollRight(this ScrollViewer scrollViewer)
        {
            return scrollViewer.HorizontalOffset + scrollViewer.ViewportWidth < scrollViewer.ExtentWidth;
        }

        public static bool CanScrollTop(this ScrollViewer scrollViewer)
        {
            return scrollViewer.VerticalOffset > 0;
        }

        public static bool CanScrollBottom(this ScrollViewer scrollViewer)
        {
            return scrollViewer.VerticalOffset + scrollViewer.ViewportHeight < scrollViewer.ExtentHeight;
        }

        public static Rect GetElementBounds(this ScrollViewer scrollViewer, FrameworkElement element)
        {
            var elementRect = new Rect(0, 0, element.ActualWidth, element.ActualHeight);
            var elementBounds = element.TransformToAncestor(scrollViewer).TransformBounds(elementRect);

            return elementBounds;
        }

        public static bool IsInViewport(this ScrollViewer scrollViewer, FrameworkElement element, bool isPartiallyVisible = true)
        {
            var elementBounds = GetElementBounds(scrollViewer, element);
            var scrollRect = new Rect(0, 0, scrollViewer.ActualWidth, scrollViewer.ActualHeight);

            return isPartiallyVisible
                ? scrollRect.IntersectsWith(elementBounds)
                : scrollRect.Contains(elementBounds);
        }
    }
}
