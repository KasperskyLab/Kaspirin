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
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

[TemplatePart(Name = PART_Panel, Type = typeof(Panel))]
public sealed class ImagePanel : Control
{
    public const string PART_Panel = "PART_Panel";

    public ImagePanel()
    {
        this.WhenLoaded(() =>
        {
            LaunchChangeImageAnimation();
        });
    }

    #region ImageFlowDirection

    public FlowDirection ImageFlowDirection
    {
        get => (FlowDirection)GetValue(ImageFlowDirectionProperty);
        set => SetValue(ImageFlowDirectionProperty, value);
    }

    public static readonly DependencyProperty ImageFlowDirectionProperty = DependencyProperty.Register(
        nameof(ImageFlowDirection),
        typeof(FlowDirection),
        typeof(ImagePanel),
        new PropertyMetadata(FlowDirection.LeftToRight));

    #endregion

    #region ImageStretch

    public Stretch ImageStretch
    {
        get => (Stretch)GetValue(ImageStretchProperty);
        set => SetValue(ImageStretchProperty, value);
    }

    public static readonly DependencyProperty ImageStretchProperty = DependencyProperty.Register(
        nameof(ImageStretch),
        typeof(Stretch),
        typeof(ImagePanel),
        new PropertyMetadata(Stretch.None));

    #endregion

    #region Source

    public ImageSource? ImageSource
    {
        get => (ImageSource?)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
        nameof(ImageSource),
        typeof(ImageSource),
        typeof(ImagePanel),
        new PropertyMetadata(default(ImageSource), OnImageSourceChanged));

    private static void OnImageSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((ImagePanel)d).StoreImageSources(e.OldValue as ImageSource, e.NewValue as ImageSource);
        ((ImagePanel)d).LaunchChangeImageAnimation();
    }

    #endregion

    public TimeSpan FadeDuration { get; set; } = TimeSpan.FromMilliseconds(100);

    public ImagePanelFadeType FadeType { get; set; } = ImagePanelFadeType.FadeIn;

    public Func<ImageSource, Image>? CreateImageCallback { get; set; }

    public override void OnApplyTemplate()
    {
        var panel = Guard.EnsureIsInstanceOfType<Grid>(GetTemplateChild(PART_Panel));

        panel.Children.Add(_fadeOutContainer);
        panel.Children.Add(_currentImageHolder);
        panel.Children.Add(_fadeInContainer);
    }

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
            image = CreateImage(imageSource);
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
            image = CreateImage(imageSource);
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

    private Image CreateImage(ImageSource imageSource)
    {
        var image = new Image();
        image.SetBinding(Image.FlowDirectionProperty, new Binding() { Source = this, Path = ImagePanel.ImageFlowDirectionProperty.AsPath() });
        image.SetBinding(Image.StretchProperty, new Binding() { Source = this, Path = ImagePanel.ImageStretchProperty.AsPath() });
        image.HorizontalAlignment = HorizontalAlignment.Center;
        image.VerticalAlignment = VerticalAlignment.Center;
        image.Source = imageSource;

        return image;
    }

    private readonly Grid _fadeOutContainer = new();
    private readonly Grid _fadeInContainer = new();
    private readonly Grid _currentImageHolder = new();

    private Storyboard _storyboard = new();
    private ImageSource? _oldImage;
    private ImageSource? _newImage;
    private static readonly PropertyPath _opacityPath = new(OpacityProperty);
}
