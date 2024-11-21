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
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Kaspirin.UI.Framework.UiKit.Localization.Markup.Extensions.Images
{
    public class ImgExtension : LocalizationMarkupBase
    {
        public ImgExtension() : this(string.Empty) { }

        public ImgExtension(string key) : base(key) { }

        public ImgExtension(string key, string scope) : base(key, scope) { }

        public ImgExtensionMode Mode { get; set; }

        public Size BitmapFrameSize { get; set; } = Size.Empty;

        protected override object? ProvideFallback()
        {
            return Mode switch
            {
                ImgExtensionMode.BitmapImage => new BitmapImage(_fallbackImageUri),
                ImgExtensionMode.BitmapFrame => BitmapFrame.Create(_fallbackImageUri),
                _ => base.ProvideFallback()
            };
        }

        protected override object? ProvideValue()
        {
            var localizer = ProvideLocalizer<IImageLocalizer>();

            return Mode switch
            {
                ImgExtensionMode.BitmapImage => localizer.GetBitmapImage(Key),
                ImgExtensionMode.BitmapFrame => localizer.GetBitmapFrame(Key, BitmapFrameSize),
                ImgExtensionMode.SvgImage => localizer.GetSvgImage(Key),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        protected override ILocalizer PrepareLocalizer()
        {
            return LocalizationManager.Current.LocalizerFactory.Resolve<IImageLocalizer>(Scope);
        }

        private static readonly Uri _fallbackImageUri = new($"pack://application:,,,/{Assembly.GetExecutingAssembly().GetName().Name};" +
                                                             "component/Resources/neutral/images/fallback/Placeholder.png");
    }
}