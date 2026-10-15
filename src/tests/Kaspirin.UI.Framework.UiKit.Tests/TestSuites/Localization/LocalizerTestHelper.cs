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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SharpVectors.Converters;
using SharpVectors.Renderers.Wpf;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Localization;

internal static class LocalizerTestHelper
{
    public static readonly Uri ImageUriPng = new($"pack://application:,,,/{AssemblyName};component/resources/neutral/images/test/testimage.png");

    public static readonly Uri ImageUriSvg = new($"pack://application:,,,/{AssemblyName};component/resources/neutral/images/test/testimage.svg");

    public static readonly Uri ImageUriIco = new($"pack://application:,,,/{AssemblyName};component/resources/neutral/images/test/testimage.ico");

    public static readonly Uri FileUri = new($"pack://application:,,,/{AssemblyName};component/resources/neutral/files/test.txt");

    public static byte[] BitmapImageToBytes(Uri imageUri)
    {
        var bitmapFrame = BitmapFrame.Create(imageUri);

        return BitmapFrameToBytes(bitmapFrame);
    }

    public static byte[] BitmapImageToBytes(BitmapImage image)
    {
        var bitmapFrame = BitmapFrame.Create(image);

        return BitmapFrameToBytes(bitmapFrame);
    }

    internal static byte[] SvgImageToBytes(Uri imageUri)
    {
        var settings = new WpfDrawingSettings()
        {
            EnsureViewboxSize = true
        };

        using var stream = Application.GetResourceStream(imageUri).Stream;
        using var reader = new FileSvgReader(settings);

        var drawingImage = new DrawingImage(reader.Read(stream));

        return SvgImageToBytes(drawingImage);
    }

    internal static byte[] SvgImageToBytes(DrawingImage image)
    {
        var drawingString = XamlWriter.Save(image);

        drawingString = _guidRegex.Replace(drawingString, string.Empty);

        return Encoding.UTF8.GetBytes(drawingString);
    }

    internal static byte[] BitmapFrameToBytes(Uri imageUri, Size size)
    {
        using var stream = Application.GetResourceStream(imageUri).Stream;

        var bitmapFrame = BitmapFrame.Create(stream).Decoder.Frames.Single(f => f.Width == size.Width && f.Height == size.Height);

        return BitmapFrameToBytes(bitmapFrame);
    }

    internal static byte[] BitmapFrameToBytes(BitmapFrame image)
    {
        var data = new byte[] { };

        var encoder = new PngBitmapEncoder();
        encoder.Frames.Add(image);
        using var ms = new MemoryStream();
        encoder.Save(ms);
        data = ms.ToArray();

        return data;
    }

    private static string AssemblyName => Assembly.GetExecutingAssembly().GetName().Name!;

    private static readonly Regex _guidRegex = new("\\b[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}\\b", RegexOptions.Compiled);
}
