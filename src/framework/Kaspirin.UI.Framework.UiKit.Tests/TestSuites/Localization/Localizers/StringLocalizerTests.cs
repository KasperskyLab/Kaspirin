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
using System.Collections.Generic;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Localization.Localizers;

[TestClass]
public sealed class StringLocalizerTests : BaseTests
{
    [TestMethod]
    public void GetString()
    {
        var locScope = "Test";
        var locKey = "ShouldLocalizeByKey";
        var locValue = LocalizationManager.GetLocalizer<IStringLocalizer>(locScope).GetString(locKey);

        Assert.AreEqual("test value", locValue);
    }

    [TestMethod]
    public void GetStringWithCustomization()
    {
        var locScope = "Test";
        var locKey = "TestKey";

        LocalizationManager.ChangeCulture("ru-RU");

        Assert.AreEqual("test value (ru)", LocalizationManager.GetLocalizer<IStringLocalizer>(locScope).GetString(locKey));

        LocalizationManager.ChangeCulture("ru-RU-xTest");

        Assert.AreEqual("test value (customized)", LocalizationManager.GetLocalizer<IStringLocalizer>(locScope).GetString(locKey));

        LocalizationManager.ChangeCulture("en-US");

        Assert.AreEqual("test value", LocalizationManager.GetLocalizer<IStringLocalizer>(locScope).GetString(locKey));
    }

    [TestMethod]
    public void GetStringWithParameters1()
    {
        var locScope = "Test";
        var locKey = "ShouldLocalizeByKeyWithOneParameter";
        var locParams1 = new StringParamResolver(new Dictionary<string, object?>() { { "ten", 10 } });
        var locValue = LocalizationManager.GetLocalizer<IStringLocalizer>(locScope).GetString(locKey, locParams1);

        Assert.AreEqual("begin 10 end", locValue);

        var locParams2 = new StringParamResolver(key => key == "ten" ? 10 : null);
        var locValue2 = LocalizationManager.GetLocalizer<IStringLocalizer>(locScope).GetString(locKey, locParams2);

        Assert.AreEqual("begin 10 end", locValue2);

        var locParams3 = new StringParamResolver("ten", 10);
        var locValue3 = LocalizationManager.GetLocalizer<IStringLocalizer>(locScope).GetString(locKey, locParams3);

        Assert.AreEqual("begin 10 end", locValue3);

        var locParams4 = new StringParamResolver("ten", 10);
        var locValue4 = LocalizationManager.GetLocalizer<IStringLocalizer>(locScope).GetString(locKey, locParams4);

        Assert.AreEqual("begin 10 end", locValue4);

        var locParams5 = new Dictionary<string, Lazy<object?>>() { { "ten", new(() => 10) } };
        var locValue5 = LocalizationManager.GetLocalizer<IStringLocalizer>(locScope).GetString(locKey, locParams4);

        Assert.AreEqual("begin 10 end", locValue5);
    }

    [TestMethod]
    public void GetStringWithParameters2()
    {
        var locScope = "Test";
        var locKey = "ShouldLocalizeByKeyWithOneParameter";
        var locParams = new Dictionary<string, object?>() { { "ten", 10 } };
        var locValue = LocalizationManager.GetLocalizer<IStringLocalizer>(locScope).GetString(locKey, locParams);

        Assert.AreEqual("begin 10 end", locValue);
    }

    [TestMethod]
    public void GetKeys()
    {
        var locScope = "Test";
        var locKey1 = "ShouldLocalizeByKey";
        var locKey2 = "ShouldLocalizeByKeyWithOneParameter";
        var locScopeKeys = LocalizationManager.GetLocalizer<IStringLocalizer>(locScope).GetKeys();

        Assert.IsTrue(locScopeKeys.Contains(locKey1));
        Assert.IsTrue(locScopeKeys.Contains(locKey2));
    }

    [TestMethod]
    public void ContainsKey()
    {
        var locScope = "Test";
        var locKey = "ShouldLocalizeByKey";
        var result = LocalizationManager.GetLocalizer<IStringLocalizer>(locScope).ContainsKey(locKey);

        Assert.IsTrue(result);
    }
}
