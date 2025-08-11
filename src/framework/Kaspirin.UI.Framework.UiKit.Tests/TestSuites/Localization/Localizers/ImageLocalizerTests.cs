// Copyright © 2025 AO Kaspersky Lab.
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

using System.Linq;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Localization.Localizers;

[TestClass]
public sealed class ImageLocalizerTests : BaseTests
{
    [TestMethod]
    public void GetBitmapImage()
    {
        var imageScope = "test";
        var imageKey = "TestImage.png";

        var bitmapImage = LocalizationManager.GetLocalizer<IImageLocalizer>(imageScope).GetBitmapImage(imageKey);

        Assert.IsNotNull(bitmapImage);

        var actualBytes = LocalizerTestHelper.BitmapImageToBytes(bitmapImage);
        var expectedBytes = LocalizerTestHelper.BitmapImageToBytes(LocalizerTestHelper.ImageUriPng);

        Assert.IsTrue(actualBytes.SequenceEqual(expectedBytes));
    }

    [TestMethod]
    public void GetSvgImage()
    {
        var imageScope = "test";
        var imageKey = "TestImage.svg";

        var drawingImage = LocalizationManager.GetLocalizer<IImageLocalizer>(imageScope).GetSvgImage(imageKey);

        Assert.IsNotNull(drawingImage);

        var actualBytes = LocalizerTestHelper.SvgImageToBytes(drawingImage);
        var expectedBytes = LocalizerTestHelper.SvgImageToBytes(LocalizerTestHelper.ImageUriSvg);

        Assert.IsTrue(actualBytes.SequenceEqual(expectedBytes));
    }

    [TestMethod]
    public void GetBitmapFrame()
    {
        var imageScope = "test";
        var imageKey = "TestImage.ico";
        var imageSize = new Size(76, 58);

        var bitmapFrame = LocalizationManager.GetLocalizer<IImageLocalizer>(imageScope).GetBitmapFrame(imageKey);

        Assert.IsNotNull(bitmapFrame);

        Assert.AreEqual(imageSize.Width, bitmapFrame.Width);
        Assert.AreEqual(imageSize.Height, bitmapFrame.Height);

        var actualBytes = LocalizerTestHelper.BitmapFrameToBytes(bitmapFrame);
        var expectedBytes = LocalizerTestHelper.BitmapFrameToBytes(LocalizerTestHelper.ImageUriIco, imageSize);

        Assert.IsTrue(actualBytes.SequenceEqual(expectedBytes));
    }

    [TestMethod]
    public void GetBitmapFrameWithSize()
    {
        var imageScope = "test";
        var imageKey = "TestImage.ico";
        var imageSize = new Size(50, 38);

        var bitmapFrame = LocalizationManager.GetLocalizer<IImageLocalizer>(imageScope).GetBitmapFrame(imageKey, imageSize);

        Assert.IsNotNull(bitmapFrame);

        Assert.AreEqual(imageSize.Width, bitmapFrame.Width);
        Assert.AreEqual(imageSize.Height, bitmapFrame.Height);

        var actualBytes = LocalizerTestHelper.BitmapFrameToBytes(bitmapFrame);
        var expectedBytes = LocalizerTestHelper.BitmapFrameToBytes(LocalizerTestHelper.ImageUriIco, imageSize);

        Assert.IsTrue(actualBytes.SequenceEqual(expectedBytes));
    }

    [TestMethod]
    public void GetUri()
    {
        var imageScope = "test";
        var imageKey = "TestImage.ico";
        var imageUri = LocalizationManager.GetLocalizer<IImageLocalizer>(imageScope).GetUri(imageKey);

        Assert.AreEqual(LocalizerTestHelper.ImageUriIco, imageUri);
    }

    [TestMethod]
    public void GetKeys()
    {
        var imageScope = "test";
        var imageKey = "TestImage.ico";
        var imageScopeKeys = LocalizationManager.GetLocalizer<IImageLocalizer>(imageScope).GetKeys();

        Assert.IsTrue(imageScopeKeys.Count() == 3);
        Assert.AreEqual(imageKey, imageScopeKeys[0], true);
    }

    [TestMethod]
    public void ContainsKey()
    {
        var imageScope = "test";
        var imageKey = "TestImage.ico";
        var result = LocalizationManager.GetLocalizer<IImageLocalizer>(imageScope).ContainsKey(imageKey);

        Assert.IsTrue(result);
    }
}