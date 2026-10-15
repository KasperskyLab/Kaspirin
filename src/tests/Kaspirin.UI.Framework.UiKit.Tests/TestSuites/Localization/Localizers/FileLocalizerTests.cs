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

using System.IO;
using System.Linq;
using System.Text;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Localization.Localizers;

[TestClass]
public sealed class FileLocalizerTests : BaseTests
{
    [TestMethod]
    public void GetFileText()
    {
        var fileScope = "files";
        var fileKey = "test.txt";
        var fileContent = "Test content";
        var fileText = LocalizationManager.GetLocalizer<IFileLocalizer>(fileScope).GetText(fileKey, Encoding.UTF8);

        Assert.AreEqual(fileContent, fileText);
    }

    [TestMethod]
    public void GetFileContent()
    {
        var fileScope = "files";
        var fileKey = "test.txt";
        var fileContent = "Test content";
        var fileBytes = LocalizationManager.GetLocalizer<IFileLocalizer>(fileScope).GetContent(fileKey);

        Assert.IsNotNull(fileBytes);
        Assert.IsTrue(Encoding.UTF8.GetBytes(fileContent).SequenceEqual(fileBytes));
    }

    [TestMethod]
    public void GetFileStream()
    {
        var fileScope = "files";
        var fileKey = "test.txt";
        var fileContent = "Test content";
        var fileStream = LocalizationManager.GetLocalizer<IFileLocalizer>(fileScope).GetStream(fileKey);

        Assert.IsNotNull(fileStream);
        Assert.AreEqual(fileContent, new StreamReader(fileStream).ReadToEnd());
    }

    [TestMethod]
    public void GetUri()
    {
        var fileScope = "files";
        var fileKey = "test.txt";
        var fileUri = LocalizationManager.GetLocalizer<IFileLocalizer>(fileScope).GetUri(fileKey);

        Assert.IsNotNull(fileUri);
        Assert.AreEqual(LocalizerTestHelper.FileUri, fileUri);
    }

    [TestMethod]
    public void GetKeys()
    {
        var fileScope = "files";
        var fileKey = "test.txt";
        var fileScopeKeys = LocalizationManager.GetLocalizer<IFileLocalizer>(fileScope).GetKeys();

        Assert.IsTrue(fileScopeKeys.Count() == 1);
        Assert.AreEqual(fileKey, fileScopeKeys[0]);
    }

    [TestMethod]
    public void ContainsKey()
    {
        var fileScope = "files";
        var fileKey = "test.txt";
        var result = LocalizationManager.GetLocalizer<IFileLocalizer>(fileScope).ContainsKey(fileKey);

        Assert.IsTrue(result);
    }
}
