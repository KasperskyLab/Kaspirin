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
using System.Windows;
using System.Windows.Controls;

namespace Kaspirin.UI.Framework.UiKit.Extensions;

/// <summary>
///     Extension methods for <see cref="ScrollViewer" />.
/// </summary>
public static class ScrollViewerExtensions
{
    /// <summary>
    ///     Checks whether the content can be scrolled to the left.
    /// </summary>
    /// <param name="scrollViewer">
    ///     The scroll element.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if left scrolling is possible, otherwise <see langword="false" />.
    /// </returns>
    public static bool CanScrollLeft(this ScrollViewer scrollViewer)
    {
        return scrollViewer.HorizontalOffset > 0;
    }

    /// <summary>
    ///     Checks whether the content can scroll to the right.
    /// </summary>
    /// <param name="scrollViewer">
    ///     The scroll element.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if scrolling to the right is possible, otherwise - <see langword="false" />.
    /// </returns>
    public static bool CanScrollRight(this ScrollViewer scrollViewer)
    {
        return scrollViewer.HorizontalOffset + scrollViewer.ViewportWidth < scrollViewer.ExtentWidth;
    }

    /// <summary>
    ///     Checks whether the content can be scrolled up.
    /// </summary>
    /// <param name="scrollViewer">
    ///     The scroll element.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if scrolling up is possible, otherwise <see langword="false" />.
    /// </returns>
    public static bool CanScrollTop(this ScrollViewer scrollViewer)
    {
        return scrollViewer.VerticalOffset > 0;
    }

    /// <summary>
    ///     Checks whether the content can be scrolled down.
    /// </summary>
    /// <param name="scrollViewer">
    ///     The scroll element.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if scrolling down is possible, otherwise <see langword="false" />.
    /// </returns>
    public static bool CanScrollBottom(this ScrollViewer scrollViewer)
    {
        return scrollViewer.VerticalOffset + scrollViewer.ViewportHeight < scrollViewer.ExtentHeight;
    }

    /// <summary>
    ///     Returns the borders of the element in the coordinate system <see cref="ScrollViewer" />.
    /// </summary>
    /// <param name="scrollViewer">
    ///     The scroll element.
    /// </param>
    /// <param name="element">
    ///     The element whose boundaries are being calculated.
    /// </param>
    /// <returns>
    ///     Returns the rectangle that bounds the element.
    /// </returns>
    public static Rect GetElementBounds(this ScrollViewer scrollViewer, FrameworkElement element)
    {
        var elementRect = new Rect(0, 0, element.ActualWidth, element.ActualHeight);
        var elementBounds = element.TransformToAncestor(scrollViewer).TransformBounds(elementRect);

        return elementBounds;
    }

    /// <summary>
    ///     Checks whether the element is in the visible area <see cref="ScrollViewer" />.
    /// </summary>
    /// <param name="scrollViewer">
    ///     The scroll element.
    /// </param>
    /// <param name="element">
    ///     The element being checked.
    /// </param>
    /// <param name="isPartiallyVisible">
    ///     Determines whether to consider a partially visible element to be located in the visible area.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the element is in the visible area, otherwise <see langword="false" />.
    /// </returns>
    public static bool IsInViewport(this ScrollViewer scrollViewer, FrameworkElement element, bool isPartiallyVisible = true)
    {
        var elementBounds = GetElementBounds(scrollViewer, element);
        var scrollRect = new Rect(0, 0, scrollViewer.ActualWidth, scrollViewer.ActualHeight);

        return isPartiallyVisible
            ? scrollRect.IntersectsWith(elementBounds)
            : scrollRect.Contains(elementBounds);
    }

    /// <summary>
    ///     Scrolls the content so that the element is in the center of the visible area.
    /// </summary>
    /// <param name="scrollViewer">
    ///     The scroll element.
    /// </param>
    /// <param name="element">
    ///     The element to scroll to.
    /// </param>
    /// <param name="horizontalOffset">
    ///     Additional horizontal offset.
    /// </param>
    /// <param name="verticalOffset">
    ///     Additional vertical offset.
    /// </param>
    /// <param name="forced">
    ///     Force scrolling, even if the element is already visible.
    /// </param>
    /// <remarks>
    ///     If the element is already fully visible and <paramref name="forced" /> is <see langword="true" />,
    ///     then scrolling is not performed.
    /// </remarks>
    public static void ScrollToCenter(
        this ScrollViewer scrollViewer, 
        FrameworkElement? element, double 
        horizontalOffset = 0, 
        double verticalOffset = 0,
        bool forced = false)
    {
        if (scrollViewer == null || element == null)
        {
            return;
        }

        var isInViewport = scrollViewer.IsInViewport(element, isPartiallyVisible: false);
        if (isInViewport && !forced)
        {
            return;
        }

        var position = element.TransformToAncestor(scrollViewer).Transform(new Point(0, 0));

        var targetVerticalOffset = scrollViewer.VerticalOffset + position.Y - scrollViewer.ViewportHeight / 2 + element.RenderSize.Height / 2 + verticalOffset;
        targetVerticalOffset = Math.Max(0, Math.Min(targetVerticalOffset, scrollViewer.ScrollableHeight));

        var targetHorizontalOffset = scrollViewer.HorizontalOffset + position.X - scrollViewer.ViewportWidth / 2 + element.RenderSize.Width / 2 + horizontalOffset;
        targetHorizontalOffset = Math.Max(0, Math.Min(targetHorizontalOffset, scrollViewer.ScrollableWidth));

        scrollViewer.ScrollToVerticalOffset(targetVerticalOffset);
        scrollViewer.ScrollToHorizontalOffset(targetHorizontalOffset);
    }

    /// <summary>
    ///     Scrolls the content to the top edge of the visible area.
    /// </summary>
    /// <param name="scrollViewer">
    ///     The scroll element.
    /// </param>
    /// <param name="element">
    ///     The element to scroll to.
    /// </param>
    /// <param name="horizontalOffset">
    ///     Additional horizontal offset.
    /// </param>
    /// <param name="verticalOffset">
    ///     Additional vertical offset.
    /// </param>
    /// <param name="forced">
    ///     Force scrolling, even if the element is already visible.
    /// </param>
    /// <remarks>
    ///     If the element is already fully visible and <paramref name="forced" /> is <see langword="true" />,
    ///     then scrolling is not performed.
    /// </remarks>
    public static void ScrollToTop(
        this ScrollViewer scrollViewer,
        FrameworkElement? element, double
        horizontalOffset = 0,
        double verticalOffset = 0,
        bool forced = false)
    {
        if (scrollViewer == null || element == null)
        {
            return;
        }

        var isInViewport = scrollViewer.IsInViewport(element, isPartiallyVisible: false);
        if (isInViewport && !forced)
        {
            return;
        }

        var position = element.TransformToAncestor(scrollViewer).Transform(new Point(0, 0));

        var targetVerticalOffset = scrollViewer.VerticalOffset + position.Y + verticalOffset;
        targetVerticalOffset = Math.Max(0, Math.Min(targetVerticalOffset, scrollViewer.ScrollableHeight));

        var targetHorizontalOffset = scrollViewer.HorizontalOffset + position.X - scrollViewer.ViewportWidth / 2 + element.RenderSize.Width / 2 + horizontalOffset;
        targetHorizontalOffset = Math.Max(0, Math.Min(targetHorizontalOffset, scrollViewer.ScrollableWidth));

        scrollViewer.ScrollToVerticalOffset(targetVerticalOffset);
        scrollViewer.ScrollToHorizontalOffset(targetHorizontalOffset);
    }

    /// <summary>
    ///     Scrolls the content so that the element is at the bottom edge of the visible area.
    /// </summary>
    /// <param name="scrollViewer">
    ///     The scroll element.
    /// </param>
    /// <param name="element">
    ///     The element to scroll to.
    /// </param>
    /// <param name="horizontalOffset">
    ///     Additional horizontal offset.
    /// </param>
    /// <param name="verticalOffset">
    ///     Additional vertical offset.
    /// </param>
    /// <param name="forced">
    ///     Force scrolling, even if the element is already visible.
    /// </param>
    /// <remarks>
    ///     If the element is already fully visible and <paramref name="forced" /> is <see langword="true" />,
    ///     then scrolling is not performed.
    /// </remarks>
    public static void ScrollToBottom(
        this ScrollViewer scrollViewer,
        FrameworkElement? element, double
        horizontalOffset = 0,
        double verticalOffset = 0,
        bool forced = false)
    {
        if (scrollViewer == null || element == null)
        {
            return;
        }

        var isInViewport = scrollViewer.IsInViewport(element, isPartiallyVisible: false);
        if (isInViewport && !forced)
        {
            return;
        }

        var position = element.TransformToAncestor(scrollViewer).Transform(new Point(0, 0));

        var targetVerticalOffset = scrollViewer.VerticalOffset + position.Y + element.RenderSize.Height - scrollViewer.ViewportHeight + verticalOffset;
        targetVerticalOffset = Math.Max(0, Math.Min(targetVerticalOffset, scrollViewer.ScrollableHeight));

        var targetHorizontalOffset = scrollViewer.HorizontalOffset + position.X - scrollViewer.ViewportWidth / 2 + element.RenderSize.Width / 2 + horizontalOffset;
        targetHorizontalOffset = Math.Max(0, Math.Min(targetHorizontalOffset, scrollViewer.ScrollableWidth));

        scrollViewer.ScrollToVerticalOffset(targetVerticalOffset);
        scrollViewer.ScrollToHorizontalOffset(targetHorizontalOffset);
    }
}
