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
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Localization.Localizers;

[TestClass]
public sealed class XamlLocalizerTests : BaseTests
{
    [TestMethod]
    public void GetResource()
    {
        var resScope = "TestPalette";
        var resKey = "TestColor";
        var resValue = LocalizationManager.GetLocalizer<IXamlLocalizer>(resScope).GetResource(resKey);

        Assert.IsNotNull(resValue);
        Assert.IsInstanceOfType(resValue, typeof(SolidColorBrush));

        var actualBrush = (SolidColorBrush)resValue;
        var actualValue = actualBrush.Color.ToString();
        var expectedValue = "#12345678";

        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void GetResourceWithPrefix()
    {
        LocalizationManager.ChangePrefix("prefix");

        var resScope = "TestPalette";
        var resKey = "TestColor";
        var resValue = LocalizationManager.GetLocalizer<IXamlLocalizer>(resScope).GetResource(resKey);

        Assert.IsNotNull(resValue);
        Assert.IsInstanceOfType(resValue, typeof(SolidColorBrush));

        var actualBrush = (SolidColorBrush)resValue;
        var actualValue = actualBrush.Color.ToString();
        var expectedValue = "#44332211";

        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void GetResourceWithPrefixAndTheme()
    {
        LocalizationManager.ChangePrefix("prefix");
        LocalizationManager.ChangeTheme("dark");

        var resScope = "TestPalette";
        var resKey = "TestColor";
        var resValue = LocalizationManager.GetLocalizer<IXamlLocalizer>(resScope).GetResource(resKey);

        Assert.IsNotNull(resValue);
        Assert.IsInstanceOfType(resValue, typeof(SolidColorBrush));

        var actualBrush = (SolidColorBrush)resValue;
        var actualValue = actualBrush.Color.ToString();
        var expectedValue = "#11223344";

        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestMethod]
    public void GetKeys()
    {
        var resScope = "TestPalette";
        var resKey = "TestColor";
        var resScopeKeys = LocalizationManager.GetLocalizer<IXamlLocalizer>(resScope).GetKeys();

        Assert.IsTrue(resScopeKeys.Count() == 1);
        Assert.AreEqual(resKey, resScopeKeys[0]);
    }

    [TestMethod]
    public void ContainsKey()
    {
        var resScope = "TestPalette";
        var resKey = "TestColor";
        var result = LocalizationManager.GetLocalizer<IXamlLocalizer>(resScope).ContainsKey(resKey);

        Assert.IsTrue(result);
    }
}
