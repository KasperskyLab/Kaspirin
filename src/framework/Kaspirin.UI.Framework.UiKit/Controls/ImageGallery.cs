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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_NextImageButton, Type = typeof(ImageGalleryButton))]
    [TemplatePart(Name = PART_PrevImageButton, Type = typeof(ImageGalleryButton))]
    [TemplatePart(Name = PART_CloseButton, Type = typeof(ImageGalleryButton))]
    [TemplatePart(Name = PART_ImagePanel, Type = typeof(ImagePanel))]
    [TemplatePart(Name = PART_Carousel, Type = typeof(Carousel))]
    [TemplatePart(Name = PART_Counter, Type = typeof(TextBlock))]
    public class ImageGallery : Control
    {
        public const string PART_NextImageButton = "PART_NextImageButton";
        public const string PART_PrevImageButton = "PART_PrevImageButton";
        public const string PART_CloseButton = "PART_CloseButton";
        public const string PART_ImagePanel = "PART_ImagePanel";
        public const string PART_Carousel = "PART_Carousel";
        public const string PART_Counter = "PART_Counter";

        public ImageGallery()
        {
            Loaded += OnLoaded;
        }

        #region ImageSources

        public ObservableCollection<ImageSource>? ImageSources
        {
            get { return (ObservableCollection<ImageSource>?)GetValue(ImageSourcesProperty); }
            set { SetValue(ImageSourcesProperty, value); }
        }

        public static readonly DependencyProperty ImageSourcesProperty =
            DependencyProperty.Register("ImageSources", typeof(ObservableCollection<ImageSource>), typeof(ImageGallery),
                new PropertyMetadata(null, OnImageSourcesChanged));

        private static void OnImageSourcesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var oldSource = e.OldValue as ObservableCollection<ImageSource>;
            if (oldSource != null)
            {
                oldSource.CollectionChanged -= ((ImageGallery)d).OnImageSourcesCollectionChanged;
            }

            var newSource = e.NewValue as ObservableCollection<ImageSource>;
            if (newSource != null)
            {
                newSource.CollectionChanged -= ((ImageGallery)d).OnImageSourcesCollectionChanged;
                newSource.CollectionChanged += ((ImageGallery)d).OnImageSourcesCollectionChanged;
            }

            ((ImageGallery)d).UpdateSelectedImage();
            ((ImageGallery)d).UpdateNavigationMode();
        }

        private void OnImageSourcesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateSelectedImage();
            UpdateNavigationMode();
        }

        #endregion

        #region ImageFlowDirection

        public FlowDirection ImageFlowDirection
        {
            get { return (FlowDirection)GetValue(ImageFlowDirectionProperty); }
            set { SetValue(ImageFlowDirectionProperty, value); }
        }

        public static readonly DependencyProperty ImageFlowDirectionProperty =
            DependencyProperty.Register("ImageFlowDirection", typeof(FlowDirection), typeof(ImageGallery),
                new PropertyMetadata(FlowDirection.LeftToRight));

        #endregion

        #region ImageStretch

        public Stretch ImageStretch
        {
            get { return (Stretch)GetValue(ImageStretchProperty); }
            set { SetValue(ImageStretchProperty, value); }
        }

        public static readonly DependencyProperty ImageStretchProperty =
            DependencyProperty.Register("ImageStretch", typeof(Stretch), typeof(ImageGallery),
                new PropertyMetadata(Stretch.UniformToFill));

        #endregion

        #region SelectedImage

        public ImageSource? SelectedImage
        {
            get { return (ImageSource?)GetValue(SelectedImageProperty); }
            set { SetValue(SelectedImageProperty, value); }
        }

        public static readonly DependencyProperty SelectedImageProperty =
            DependencyProperty.Register("SelectedImage", typeof(ImageSource), typeof(ImageGallery), new PropertyMetadata(null, OnSelectedImageChanged));

        private static void OnSelectedImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ImageGallery)d).UpdateSelectedImage();
        }

        #endregion

        #region NavigationMode

        public ImageGalleryNavigationMode NavigationMode
        {
            get { return (ImageGalleryNavigationMode)GetValue(NavigationModeProperty); }
            set { SetValue(NavigationModeProperty, value); }
        }

        public static readonly DependencyProperty NavigationModeProperty =
            DependencyProperty.Register("NavigationMode", typeof(ImageGalleryNavigationMode), typeof(ImageGallery),
                new PropertyMetadata(ImageGalleryNavigationMode.ArrowsAndCarousel, OnNavigationModeChanged));

        private static void OnNavigationModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ImageGallery)d).UpdateNavigationMode();
        }

        #endregion

        #region ActualNavigationMode

        public ImageGalleryNavigationMode ActualNavigationMode
        {
            get { return (ImageGalleryNavigationMode)GetValue(ActualNavigationModeProperty); }
            private set { SetValue(_actualNavigationModePropertyKey, value); }
        }

        private static readonly DependencyPropertyKey _actualNavigationModePropertyKey =
            DependencyProperty.RegisterReadOnly("ActualNavigationMode", typeof(ImageGalleryNavigationMode), typeof(ImageGallery),
                new PropertyMetadata(ImageGalleryNavigationMode.ArrowsAndCarousel));

        public static readonly DependencyProperty ActualNavigationModeProperty =
            _actualNavigationModePropertyKey.DependencyProperty;

        #endregion

        #region CloseCommand

        public ICommand CloseCommand
        {
            get { return (ICommand)GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }

        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(ImageGallery));

        #endregion

        public override void OnApplyTemplate()
        {
            _carousel = (Carousel)GetTemplateChild(PART_Carousel);
            _carousel.SetBinding(Carousel.ItemsSourceProperty, new Binding() { Source = this, Path = ImageGallery.ImageSourcesProperty.AsPath() });
            _carousel.SetBinding(Carousel.SelectedItemProperty, new Binding() { Source = this, Path = ImageGallery.SelectedImageProperty.AsPath(), Mode = BindingMode.TwoWay });

            _nextButton = (ImageGalleryButton)GetTemplateChild(PART_NextImageButton);
            _nextButton.Click += NextButtonOnClick;

            _prevButton = (ImageGalleryButton)GetTemplateChild(PART_PrevImageButton);
            _prevButton.Click += PrevButtonOnClick;

            _closeButton = (ImageGalleryButton)GetTemplateChild(PART_CloseButton);
            _closeButton.IsCancel = true;

            _imagePanel = (ImagePanel)GetTemplateChild(PART_ImagePanel);
            _imagePanel.CreateImageCallback = CreateImage;
            _imagePanel.SetBinding(ImagePanel.SourceProperty, new Binding() { Source = this, Path = SelectedImageProperty.AsPath() });

            _counter = (TextBlock)GetTemplateChild(PART_Counter);

            _counter.SetValue(TextBlock.TextProperty, new LocExtension
            {
                Key = "ImageGallery_Counter",
                Scope = "UiKit",
                Params = new LocParameterCollection
                {
                    new LocParameter { ParamName = "CurrentIndex", ParamSource = new Binding(){ Source = _carousel, Path = Carousel.SelectedIndexProperty.AsPath(), Converter = _counterIncreaseConverter } },
                    new LocParameter { ParamName = "TotalIndex", ParamSource = new Binding(){ Source = _carousel, Path = _itemsCountPath } },
                }
            }.ProvideValue(_counter, TextBlock.TextProperty));
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateSelectedImage();
            UpdateNavigationMode();
        }

        private void PrevButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (_carousel == null)
            {
                return;
            }

            var currentIndex = _carousel.SelectedIndex;
            var maxIndex = _carousel.Items.Count - 1;

            var nextIndex = currentIndex - 1;
            if (nextIndex < 0)
            {
                SelectedImage = (ImageSource)_carousel.Items[maxIndex];
            }
            else
            {
                SelectedImage = (ImageSource)_carousel.Items[nextIndex];
            }
        }

        private void NextButtonOnClick(object sender, RoutedEventArgs e)
        {
            if (_carousel == null)
            {
                return;
            }

            var currentIndex = _carousel.SelectedIndex;
            var maxIndex = _carousel.Items.Count - 1;

            var nextIndex = currentIndex + 1;
            if (nextIndex > maxIndex)
            {
                SelectedImage = (ImageSource)_carousel.Items[0];
            }
            else
            {
                SelectedImage = (ImageSource)_carousel.Items[nextIndex];
            }
        }

        private void UpdateNavigationMode()
        {
            if (!IsLoaded)
            {
                return;
            }

            if (ImageSources == null || ImageSources.Count < 2)
            {
                ActualNavigationMode = ImageGalleryNavigationMode.Disabled;
            }
            else if (ImageSources.Count > 8 && NavigationMode == ImageGalleryNavigationMode.ArrowsAndCarousel)
            {
                ActualNavigationMode = ImageGalleryNavigationMode.ArrowsAndCounter;
            }
            else
            {
                ActualNavigationMode = NavigationMode;
            }
        }

        private void UpdateSelectedImage()
        {
            if (!IsLoaded)
            {
                return;
            }

            if (ImageSources == null || !ImageSources.Any())
            {
                SelectedImage = null;
                return;
            }

            if (SelectedImage == null)
            {
                SelectedImage = ImageSources.First();
            }

            if (SelectedImage != null && !ImageSources.Contains(SelectedImage))
            {
                SelectedImage = ImageSources.First();
            }
        }

        private Image CreateImage(ImageSource imageSource)
        {
            var image = new Image();
            image.SetBinding(Image.FlowDirectionProperty, new Binding() { Source = this, Path = ImageGallery.ImageFlowDirectionProperty.AsPath() });
            image.SetBinding(Image.StretchProperty, new Binding() { Source = this, Path = ImageGallery.ImageStretchProperty.AsPath() });
            image.HorizontalAlignment = HorizontalAlignment.Center;
            image.Source = imageSource;

            return image;
        }

        private static readonly IValueConverter _counterIncreaseConverter = new DelegateConverter(value =>
        {
            var count = Convert.ToInt32(value);
            return ++count;
        });

        private static readonly PropertyPath _itemsCountPath = new("Items.Count");

        private ImageGalleryButton? _nextButton;
        private ImageGalleryButton? _prevButton;
        private ImageGalleryButton? _closeButton;
        private Carousel? _carousel;
        private TextBlock? _counter;
        private ImagePanel? _imagePanel;
    }
}
