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

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Localization;

[TestClass]
public sealed class LocalizationManagerTests
{
    [TestMethod]
    public void Initialize()
    {
        var culture = "ru-RU";
        var cultureTail = "";

        LocalizationManager.Initialize(culture);

        Assert.AreEqual(culture, LocalizationManager.DisplayCulture.Culture);
        Assert.AreEqual(cultureTail, LocalizationManager.DisplayCulture.CultureTail);
    }

    [TestMethod]
    public void InitializeWithCustomization()
    {
        var cultureFull = "ru-RU-xTest";
        var culture = "ru-RU";
        var cultureTail = "xTest";

        LocalizationManager.Initialize(cultureFull);

        Assert.AreEqual(culture, LocalizationManager.DisplayCulture.Culture);
        Assert.AreEqual(cultureTail, LocalizationManager.DisplayCulture.CultureTail);
    }

    [TestMethod]
    [ExpectedException(typeof(LocException))]
    public void Dispose()
    {
        var fileScope = "files";
        var fileKey = "test.txt";

        var culture = "ru-RU";

        LocalizationManager.Initialize(culture);
        var localizer = LocalizationManager.GetLocalizer<IFileLocalizer>(fileScope);

        localizer.GetStream(fileKey);

        LocalizationManager.Initialize(culture);

        localizer.GetStream(fileKey); //Expected exception.
    }
}
