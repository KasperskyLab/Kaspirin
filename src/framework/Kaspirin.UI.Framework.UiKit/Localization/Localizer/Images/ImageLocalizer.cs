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
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SharpVectors.Converters;
using SharpVectors.Renderers.Wpf;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Images;

public class ImageLocalizer : BaseLocalizer<ResourceItem>, IImageLocalizer
{
    public ImageLocalizer(LocalizerParameters parameters) : base(parameters) { }

    public override void ResetCache()
    {
        base.ResetCache();

        _imageCache.Clear();
    }

    public virtual BitmapImage? GetBitmapImage(string key)
    {
        Guard.ArgumentIsNotNull(key);

        try
        {
            var cacheKey = CreateImageKey(key, ScopeInfo);

            var bitmapImage = TryGetImageFromCache<BitmapImage>(cacheKey);
            if (bitmapImage == null)
            {
                var imageResource = GetValue(key);
                var imageStream = ResourceProvider.ReadResourceStream(imageResource);

                bitmapImage = CreateBitmapImage(imageStream);

                CacheImage(cacheKey, bitmapImage);
            }

            return bitmapImage;
        }
        catch (Exception e)
        {
            e.ProcessAsLocalizerException($"failed to provide BitmapImage for key '{key}' in scope '{ScopeInfo.Scope}'.");
            return null;
        }
    }

    public virtual BitmapFrame? GetBitmapFrame(string key)
    {
        return GetBitmapFrame(key, Size.Empty);
    }

    public virtual BitmapFrame? GetBitmapFrame(string key, Size frameSize)
    {
        Guard.ArgumentIsNotNull(key);

        try
        {
            var cacheKey = CreateImageKey(key, ScopeInfo, frameSize.ToString());

            var bitmapFrame = TryGetImageFromCache<BitmapFrame>(cacheKey);
            if (bitmapFrame == null)
            {
                var imageResource = GetValue(key);
                var imageStream = ResourceProvider.ReadResourceStream(imageResource);

                bitmapFrame = CreateBitmapFrame(imageStream, frameSize);

                CacheImage(cacheKey, bitmapFrame);
            }

            return bitmapFrame;
        }
        catch (Exception e)
        {
            e.ProcessAsLocalizerException($"failed to provide BitmapFrame for key '{key}' in scope '{ScopeInfo.Scope}'.");
            return null;
        }
    }

    public virtual DrawingImage? GetSvgImage(string key)
    {
        Guard.ArgumentIsNotNull(key);

        try
        {
            var cacheKey = CreateImageKey(key, ScopeInfo);

            var drawingImage = TryGetImageFromCache<DrawingImage>(cacheKey);
            if (drawingImage == null)
            {
                var imageResource = GetValue(key);
                var imageStream = ResourceProvider.ReadResourceStream(imageResource);

                drawingImage = CreateDrawingImage(imageStream);

                CacheImage(cacheKey, drawingImage);
            }

            return drawingImage.CloneCurrentValue();
        }
        catch (Exception e)
        {
            e.ProcessAsLocalizerException($"failed to provide DrawingImage from svg for key '{key}' in scope '{ScopeInfo.Scope}'.");
            return null;
        }
    }

    public virtual Uri? GetUri(string key)
    {
        Guard.ArgumentIsNotNull(key);

        try
        {
            return GetValue(key).Uri.AbsoluteUri;
        }
        catch (Exception e)
        {
            e.ProcessAsLocalizerException($"failed to provide image URI for key '{key}' in scope '{ScopeInfo.Scope}'.");
            return null;
        }
    }

    public virtual Stream? GetStream(string key)
    {
        Guard.ArgumentIsNotNull(key);

        try
        {
            var imageResource = GetValue(key);
            var imageStream = ResourceProvider.ReadResourceStream(imageResource);

            return imageStream;
        }
        catch (Exception e)
        {
            e.ProcessAsLocalizerException($"failed to provide image Stream for key '{key}' in scope '{ScopeInfo.Scope}'.");
            return null;
        }
    }

    protected override IScope<ResourceItem> CreateDirectoryScopeObject(IEnumerable<ResourceItem> resources)
    {
        return new ImageScope(resources);
    }

    private TImageSource? TryGetImageFromCache<TImageSource>(string key)
        where TImageSource : ImageSource
    {
        if (_imageCache.TryGet(key, out var imageReference))
        {
            if (imageReference!.TryGetTarget(out var imageSource))
            {
                return (TImageSource)imageSource;
            }
        }

        return null;
    }

    private void CacheImage(string key, ImageSource imageSource)
    {
        _imageCache.Put(key, new(imageSource));
    }

    private static string CreateImageKey(string key, ScopeMetaInfo scopeInfo, string parameter = "")
    {
        return $"{key}+{scopeInfo.Scope}+{parameter}";
    }

    private static BitmapImage CreateBitmapImage(Stream imageStream)
    {
        var bitmapImage = new BitmapImage();
        bitmapImage.BeginInit();
        bitmapImage.StreamSource = imageStream;
        bitmapImage.EndInit();

        if (bitmapImage.CanFreeze)
        {
            bitmapImage.Freeze();
        }

        return bitmapImage;
    }

    private static BitmapFrame CreateBitmapFrame(Stream imageStream, Size frameSize)
    {
        var bitmapFrame = BitmapFrame.Create(imageStream);

        if (frameSize != Size.Empty)
        {
            var sizedBitmapFrame = bitmapFrame.Decoder.Frames
                .OrderByDescending(f => f.Width)
                .ThenByDescending(f => f.Height)
                .LastOrDefault(f => f.Width.LargerOrNearlyEqual(frameSize.Width) &&
                                    f.Height.LargerOrNearlyEqual(frameSize.Height));

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

    private static DrawingImage CreateDrawingImage(Stream imageStream)
    {
        var settings = new WpfDrawingSettings
        {
            EnsureViewboxSize = true
        };

        using var reader = new FileSvgReader(settings);
        var drawingGroup = reader.Read(imageStream);

        return new DrawingImage(drawingGroup).CloneCurrentValue();
    }

    private readonly LruCache<string, WeakReference<ImageSource>> _imageCache = new(100);
}