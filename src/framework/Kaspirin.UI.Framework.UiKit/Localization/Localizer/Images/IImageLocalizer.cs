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
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Images
{
    public interface IImageLocalizer : ILocalizer
    {
        BitmapImage? GetBitmapImage(string key);

        BitmapFrame? GetBitmapFrame(string key);

        BitmapFrame? GetBitmapFrame(string key, Size frameSize);

        DrawingImage? GetSvgImage(string key);
    }
}