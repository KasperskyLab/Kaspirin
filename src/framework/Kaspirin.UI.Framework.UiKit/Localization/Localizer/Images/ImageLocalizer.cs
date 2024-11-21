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
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SharpVectors.Converters;
using SharpVectors.Renderers.Wpf;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Images
{
    public sealed class ImageLocalizer : LocalizerBase, IImageLocalizer
    {
        public ImageLocalizer(LocalizerParameters parameters) : base(parameters) { }

        #region IImageLocalizer

        public BitmapImage? GetBitmapImage(string key)
        {
            Guard.ArgumentIsNotNull(key);

            try
            {
                var cacheKey = CreateImageKey(key, ScopeInfo);

                var bitmapImage = TryGetImageFromCache<BitmapImage>(cacheKey);
                if (bitmapImage == null)
                {
                    var imageUri = GetValue<Uri>(key);

                    bitmapImage = CreateBitmapImage(imageUri);

                    CacheImage(cacheKey, bitmapImage);
                }

                return bitmapImage;
            }
            catch (Exception e)
            {
                e.ProcessAsLocalizerException($"failed to provide BitmapImage for key='{key}'.");
                return null;
            }
        }

        public BitmapFrame? GetBitmapFrame(string key)
        {
            return GetBitmapFrame(key, Size.Empty);
        }

        public BitmapFrame? GetBitmapFrame(string key, Size frameSize)
        {
            Guard.ArgumentIsNotNull(key);

            try
            {
                var cacheKey = CreateImageKey(key, ScopeInfo, frameSize.ToString());

                var bitmapFrame = TryGetImageFromCache<BitmapFrame>(cacheKey);
                if (bitmapFrame == null)
                {
                    var imageUri = GetValue<Uri>(key);

                    bitmapFrame = CreateBitmapFrame(imageUri, frameSize);

                    CacheImage(cacheKey, bitmapFrame);
                }

                return bitmapFrame;
            }
            catch (Exception e)
            {
                e.ProcessAsLocalizerException($"failed to provide BitmapFrame for key='{key}'.");
                return null;
            }
        }

        public DrawingImage? GetSvgImage(string key)
        {
            Guard.ArgumentIsNotNull(key);

            try
            {
                var cacheKey = CreateImageKey(key, ScopeInfo);

                var drawingImage = TryGetImageFromCache<DrawingImage>(cacheKey);
                if (drawingImage == null)
                {
                    var imageUri = GetValue<Uri>(key);
                    var imageBytes = ResourceProvider.ReadResource(imageUri);

                    drawingImage = CreateDrawingImage(imageBytes);

                    CacheImage(cacheKey, drawingImage);
                }

                return drawingImage;
            }
            catch (Exception e)
            {
                e.ProcessAsLocalizerException($"failed to provide SvgImage for key='{key}'.");
                return null;
            }
        }

        #endregion

        protected override IScope CreateScopeObject(Uri scopeUri)
        {
            return new ImageScope(scopeUri, ResourceProvider);
        }

        private TImageSource? TryGetImageFromCache<TImageSource>(string key)
            where TImageSource : ImageSource
        {
            if (_imageCache.TryGet(key, out var imageReference))
            {
                if (imageReference!.TryGetTarget(out var img))
                {
                    if (img is DrawingImage drawingImage)
                    {
                        img = drawingImage.CloneCurrentValue();
                    }

                    return (TImageSource)img;
                }
            }

            return null;
        }

        private void CacheImage(string key, ImageSource imageSource)
        {
            _imageCache.Put(key, new(imageSource));
        }

        private static string CreateImageKey(string key, ScopeMetainfo scopeInfo, string parameter = "")
        {
            return $"{key}+{scopeInfo.Scope}+{parameter}";
        }

        private static BitmapImage CreateBitmapImage(Uri imageUri)
        {
            var bitmapImage = new BitmapImage(imageUri);
            if (bitmapImage.CanFreeze)
            {
                bitmapImage.Freeze();
            }

            return bitmapImage;
        }

        private static BitmapFrame CreateBitmapFrame(Uri imageUri, Size frameSize)
        {
            var bitmapFrame = BitmapFrame.Create(imageUri);

            if (frameSize != Size.Empty)
            {
                var sizedBitmapFrame = bitmapFrame.Decoder.Frames
                    .OrderByDescending(f => f.Width)
                    .ThenByDescending(f => f.Height)
                    .LastOrDefault(f =>
                        f.Width.LargerOrNearlyEqual(frameSize.Width) && f.Height.LargerOrNearlyEqual(frameSize.Height));

                if (sizedBitmapFrame != null)
                {
                    bitmapFrame = sizedBitmapFrame;
                }
            }

            if (bitmapFrame.CanFreeze)
            {
                bitmapFrame.Freeze();
            }

            return bitmapFrame;
        }

        private static DrawingImage CreateDrawingImage(byte[] imageBytes)
        {
            using var imageStream = new MemoryStream(imageBytes);

            var settings = new WpfDrawingSettings
            {
                EnsureViewboxSize = true
            };

            using var reader = new FileSvgReader(settings);
            var drawingGroup = reader.Read(imageStream);

            return new DrawingImage(drawingGroup);
        }

        private readonly LruCache<string, WeakReference<ImageSource>> _imageCache = new(100);
    }
}