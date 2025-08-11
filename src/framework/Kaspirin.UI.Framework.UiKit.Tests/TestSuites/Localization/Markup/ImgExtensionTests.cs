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

using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Localization.Markup;

[TestClass]
public sealed class ImgExtensionTests : BaseTests
{
    [STATestMethod]
    public void ImgExtension()
    {
        var img = new ImgExtension
        {
            Key = "TestImage.png",
            Scope = "Test"
        };

        var image = new Image();
        var imageTarget = new DependencyTarget(image, Image.SourceProperty);
        var imageBinding = img.ProvideValue(imageTarget) as MultiBindingExpression;

        Assert.IsNotNull(imageBinding);

        BindingOperations.SetBinding(image, Image.SourceProperty, imageBinding.ParentMultiBinding);

        var actualBytes = LocalizerTestHelper.BitmapImageToBytes((BitmapImage)image.Source);
        var expectedBytes = LocalizerTestHelper.BitmapImageToBytes(LocalizerTestHelper.ImageUriPng);

        Assert.IsTrue(actualBytes.SequenceEqual(expectedBytes));
    }
}
