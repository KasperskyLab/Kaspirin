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
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Kaspirin.UI.Framework.UiKit.Animation.Internals;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using Kaspirin.UI.Framework.UiKit.Controls.Properties;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_NextImageButton, Type = typeof(ImageGalleryButton))]
    [TemplatePart(Name = PART_PrevImageButton, Type = typeof(ImageGalleryButton))]
    [TemplatePart(Name = PART_ScrollViewer, Type = typeof(ScrollViewer))]
    public sealed class ImageGalleryList : Control
    {
        public const string PART_NextImageButton = "PART_NextImageButton";
        public const string PART_PrevImageButton = "PART_PrevImageButton";
        public const string PART_ScrollViewer = "PART_ScrollViewer";

        public ImageGalleryList()
        {
            _animatedBindingFactory = ServiceLocator.Instance.GetService<AnimatedBindingFactory>();
            _animationProperties = UIKitConstants.ImageGalleryListAnimationProperties;

            _defaultAnimationDuration = _animationProperties.Duration;
        }

        #region ImageCommand

        public ICommand ImageCommand
        {
            get { return (ICommand)GetValue(ImageCommandProperty); }
            set { SetValue(ImageCommandProperty, value); }
        }

        public static readonly DependencyProperty ImageCommandProperty =
            DependencyProperty.Register("ImageCommand", typeof(ICommand), typeof(ImageGalleryList));

        #endregion

        #region ImageGap

        public double ImageGap
        {
            get { return (double)GetValue(ImageGapProperty); }
            set { SetValue(ImageGapProperty, value); }
        }

        public static readonly DependencyProperty ImageGapProperty =
            DependencyProperty.Register("ImageGap", typeof(double), typeof(ImageGalleryList));

        #endregion

        #region ImageSources

        public ObservableCollection<ImageSource>? ImageSources
        {
            get { return (ObservableCollection<ImageSource>?)GetValue(ImageSourcesProperty); }
            set { SetValue(ImageSourcesProperty, value); }
        }

        public static readonly DependencyProperty ImageSourcesProperty =
            DependencyProperty.Register("ImageSources", typeof(ObservableCollection<ImageSource>), typeof(ImageGalleryList));

        #endregion

        #region ImageFlowDirection

        public FlowDirection ImageFlowDirection
        {
            get { return (FlowDirection)GetValue(ImageFlowDirectionProperty); }
            set { SetValue(ImageFlowDirectionProperty, value); }
        }

        public static readonly DependencyProperty ImageFlowDirectionProperty =
            DependencyProperty.Register("ImageFlowDirection", typeof(FlowDirection), typeof(ImageGalleryList),
                new PropertyMetadata(FlowDirection.LeftToRight));

        #endregion

        #region ImageStretch

        public Stretch ImageStretch
        {
            get { return (Stretch)GetValue(ImageStretchProperty); }
            set { SetValue(ImageStretchProperty, value); }
        }

        public static readonly DependencyProperty ImageStretchProperty =
            DependencyProperty.Register("ImageStretch", typeof(Stretch), typeof(ImageGalleryList),
                new PropertyMetadata(Stretch.Uniform));

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            SetBinding(_horizontalOffsetProxyProperty, _animatedBindingFactory.CreateBinding(
                new Binding()
                {
                    Source = this,
                    Path = _horizontalOffsetSourceProperty.AsPath(),
                },
                this,
                _horizontalOffsetProxyProperty,
                _animationProperties));

            _scrollViewer = Guard.EnsureArgumentIsInstanceOfType<ScrollViewer>(GetTemplateChild(PART_ScrollViewer));
            _scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
            _scrollViewer.SetValue(ScrollViewerProps.CanMouseWheelScrollProperty, false);

            _nextButton = (ImageGalleryButton)GetTemplateChild(PART_NextImageButton);
            _nextButton.Click += NextButtonOnClick;

            _prevButton = (ImageGalleryButton)GetTemplateChild(PART_PrevImageButton);
            _prevButton.Click += PrevButtonOnClick;
        }

        private void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (_scrollViewer == null)
            {
                return;
            }

            if (_prevButton != null)
            {
                _prevButton.Visibility = _scrollViewer.CanScrollLeft() ? Visibility.Visible : Visibility.Collapsed;
            }

            if (_nextButton != null)
            {
                _nextButton.Visibility = _scrollViewer.CanScrollRight() ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void PrevButtonOnClick(object sender, RoutedEventArgs e)
        {
            MoveToNextImage(MoveDirection.Left);
        }

        private void NextButtonOnClick(object sender, RoutedEventArgs e)
        {
            MoveToNextImage(MoveDirection.Right);
        }

        private void MoveToNextImage(MoveDirection direction)
        {
            if (_scrollViewer == null)
            {
                return;
            }

            var images = _scrollViewer.FindVisualChildren<ImageGalleryListButton>().ToList();

            var visibleImages = images.Where(image => _scrollViewer.IsInViewport(image, isPartiallyVisible: true)).ToList();
            if (visibleImages.Any())
            {
                SetCurrentValue(_horizontalOffsetProxyProperty, _scrollViewer.HorizontalOffset);
                SetCurrentValue(_horizontalOffsetSourceProperty, _scrollViewer.HorizontalOffset);

                var nextImage = GetNextImage(_scrollViewer, images, visibleImages, direction);
                var nextOffset = GetNextOffset(_scrollViewer, images, visibleImages, nextImage);
                var nextDuration = GetNextAnimationDuration(_scrollViewer, nextImage, nextOffset, ImageGap, _defaultAnimationDuration);

                _animationProperties.Duration = nextDuration;

                SetCurrentValue(_horizontalOffsetSourceProperty, nextOffset);
            }
        }

        private static ImageGalleryListButton GetNextImage(ScrollViewer scrollViewer, IList<ImageGalleryListButton> images, IList<ImageGalleryListButton> visibleImages, MoveDirection direction)
        {
            var nextImageIndex = 0;

            var fullyVisibleImages = images.Where(image => scrollViewer.IsInViewport(image, isPartiallyVisible: false)).ToList();

            var firstVisibleImage = visibleImages[0];
            var firstVisibleImageIndex = images.IndexOf(firstVisibleImage);

            nextImageIndex = direction switch
            {
                MoveDirection.Left => fullyVisibleImages.Contains(firstVisibleImage) || fullyVisibleImages.None()
                            ? firstVisibleImageIndex - 1
                            : firstVisibleImageIndex,
                MoveDirection.Right => firstVisibleImageIndex + 1,
            };

            if (nextImageIndex < 0)
            {
                nextImageIndex = 0;
            }

            if (nextImageIndex > images.Count - 1)
            {
                nextImageIndex = images.Count - 1;
            }

            return images[nextImageIndex];
        }

        private static double GetNextOffset(ScrollViewer scrollViewer, List<ImageGalleryListButton> images, IList<ImageGalleryListButton> visibleImages, ImageGalleryListButton nextImage)
        {
            var nextImageIndex = images.IndexOf(nextImage);
            var nextImageBounds = scrollViewer.GetElementBounds(nextImage);

            var isLastImage = nextImageIndex == images.Count - 1;
            if (isLastImage && visibleImages.Count == 1 && visibleImages.Contains(nextImage))
            {
                return scrollViewer.HorizontalOffset + nextImageBounds.Right;
            }

            return scrollViewer.HorizontalOffset + nextImageBounds.Left;
        }

        private static TimeSpan GetNextAnimationDuration(ScrollViewer scrollViewer, ImageGalleryListButton nextImage, double nextOffset, double imageGap, TimeSpan defaultDuration)
        {
            var imageWidth = nextImage.ActualWidth + imageGap;

            var changeDelta = scrollViewer.HorizontalOffset - nextOffset;
            if (changeDelta == 0 || imageWidth == 0)
            {
                return TimeSpan.Zero;
            }

            var duration = Math.Abs(changeDelta) / imageWidth * defaultDuration.Milliseconds;

            return TimeSpan.FromMilliseconds(duration);
        }

        private static void OnHorizontalOffsetProxyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var offset = Guard.EnsureIsInstanceOfType<double>(e.NewValue);

            ((ImageGalleryList)d)._scrollViewer?.ScrollToHorizontalOffset(offset);
        }

        private enum MoveDirection
        {
            Left,
            Right
        }

        private static readonly DependencyProperty _horizontalOffsetSourceProperty = DependencyProperty.Register("HorizontalOffsetSource", typeof(double), typeof(ImageGalleryList));
        private static readonly DependencyProperty _horizontalOffsetProxyProperty = DependencyProperty.Register("HorizontalOffsetProxy", typeof(double), typeof(ImageGalleryList), new PropertyMetadata(0D, OnHorizontalOffsetProxyChanged));

        private readonly AnimatedBindingFactory _animatedBindingFactory;
        private readonly AnimationProperties _animationProperties;
        private readonly TimeSpan _defaultAnimationDuration;

        private ScrollViewer? _scrollViewer;
        private ImageGalleryButton? _nextButton;
        private ImageGalleryButton? _prevButton;
    }
}
