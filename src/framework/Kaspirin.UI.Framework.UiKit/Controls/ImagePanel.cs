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
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class ImagePanel : Grid
    {
        public ImagePanel()
        {
            _fadeOutContainer = new Grid();
            _fadeInContainer = new Grid();
            _currentImageHolder = new Grid();
            _storyboard = new Storyboard();

            Children.Add(_fadeOutContainer);
            Children.Add(_currentImageHolder);
            Children.Add(_fadeInContainer);

            this.WhenLoaded(() =>
            {
                LaunchChangeImageAnimation();
            });
        }

        #region Source

        public ImageSource? Source
        {
            get { return (ImageSource?)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(ImagePanel),
                new PropertyMetadata(null, OnImageSourceChanged));

        private static void OnImageSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ImagePanel)d).StoreImageSources(e.OldValue as ImageSource, e.NewValue as ImageSource);
            ((ImagePanel)d).LaunchChangeImageAnimation();
        }

        #endregion

        public TimeSpan FadeDuration { get; set; } = TimeSpan.FromMilliseconds(100);

        public ImagePanelFadeType FadeType { get; set; } = ImagePanelFadeType.FadeIn;

        public Func<ImageSource, Image>? CreateImageCallback { get; set; }

        private void StoreImageSources(ImageSource? oldImage, ImageSource? newImage)
        {
            _oldImage = oldImage;
            _newImage = newImage;
        }

        private void LaunchChangeImageAnimation()
        {
            _storyboard = new Storyboard();

            PrepareFadeOut(_oldImage);
            PrepareFadeIn(_newImage);

            _storyboard.Begin();
        }

        private void PrepareFadeIn(ImageSource? imageSource)
        {
            _currentImageHolder.Children.Clear();

            if (imageSource == null)
            {
                return;
            }

            var image = CreateImageCallback?.Invoke(imageSource);
            if (image == null)
            {
                return;
            }

            var animation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                FillBehavior = FillBehavior.HoldEnd,
                Duration = FadeDuration.CoerceDuration()
            };

            animation.SetValue(Storyboard.TargetProperty, image);
            animation.SetValue(Storyboard.TargetPropertyProperty, _opacityPath);
            animation.Completed += (s, e) =>
            {
                _fadeInContainer.Children.Remove(image);
                _currentImageHolder.Children.Clear();
                _currentImageHolder.Children.Add(image);
            };
            animation.Freeze();

            _storyboard.Children.Add(animation);
            _fadeInContainer.Children.Add(image);
        }

        private void PrepareFadeOut(ImageSource? imageSource)
        {
            if (imageSource == null)
            {
                return;
            }

            var image = CreateImageCallback?.Invoke(imageSource);
            if (image == null)
            {
                return;
            }

            var storyboard = new Storyboard();

            var animation = new DoubleAnimation
            {
                From = 1,
                To = FadeType == ImagePanelFadeType.FadeIn ? 1 : 0,
                FillBehavior = FillBehavior.HoldEnd,
                Duration = FadeDuration.CoerceDuration()
            };

            animation.SetValue(Storyboard.TargetProperty, image);
            animation.SetValue(Storyboard.TargetPropertyProperty, _opacityPath);
            animation.Completed += (s, e) =>
            {
                _fadeOutContainer.Children.Remove(image);
            };
            animation.Freeze();

            _storyboard.Children.Add(animation);
            _fadeOutContainer.Children.Add(image);
        }

        private readonly Grid _fadeOutContainer;
        private readonly Grid _fadeInContainer;
        private readonly Grid _currentImageHolder;

        private Storyboard _storyboard;
        private ImageSource? _oldImage;
        private ImageSource? _newImage;
        private static readonly PropertyPath _opacityPath = new(OpacityProperty);
    }
}
