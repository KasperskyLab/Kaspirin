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

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Localization.Markup;

[TestClass]
public sealed class ResExtensionTests : BaseTests
{
    [STATestMethod]
    public void ResExtension()
    {
        var res = new ResExtension
        {
            Key = "TestColor",
            Scope = "TestPalette"
        };

        var textBlock = new TextBlock();
        var textBlockTarget = new DependencyTarget(textBlock, TextBlock.BackgroundProperty);
        var textBlockBinding = res.ProvideValue(textBlockTarget) as MultiBindingExpression;

        Assert.IsNotNull(textBlockBinding);

        BindingOperations.SetBinding(textBlock, TextBlock.BackgroundProperty, textBlockBinding.ParentMultiBinding);

        Assert.IsInstanceOfType(textBlock.Background, typeof(SolidColorBrush));

        var actualBrush = (SolidColorBrush)textBlock.Background;
        Assert.IsTrue(actualBrush.IsFrozen);

        var actualValue = actualBrush.Color.ToString();
        var expectedValue = "#12345678";

        Assert.AreEqual(expectedValue, actualValue);
    }

    [STATestMethod]
    public void ResExtensionWithChangeTheme()
    {
        var res = new ResExtension
        {
            Key = "TestColor",
            Scope = "TestPalette"
        };

        var textBlock = new TextBlock();
        var textBlockTarget = new DependencyTarget(textBlock, TextBlock.BackgroundProperty);
        var textBlockBinding = res.ProvideValue(textBlockTarget) as MultiBindingExpression;

        Assert.IsNotNull(textBlockBinding);

        BindingOperations.SetBinding(textBlock, TextBlock.BackgroundProperty, textBlockBinding.ParentMultiBinding);

        Assert.IsInstanceOfType(textBlock.Background, typeof(SolidColorBrush));

        LocalizationManager.ChangeTheme("dark");

        var actualBrush = (SolidColorBrush)textBlock.Background;
        var actualValue = actualBrush.Color.ToString();
        var expectedValue = "#87654321";
        Assert.AreEqual(expectedValue, actualValue);

        LocalizationManager.ChangeTheme(null);

        actualBrush = (SolidColorBrush)textBlock.Background;
        actualValue = actualBrush.Color.ToString();
        expectedValue = "#12345678";
        Assert.AreEqual(expectedValue, actualValue);
    }

    [STATestMethod]
    public void ResExtensionNoFreezing()
    {
        var res = new ResExtension
        {
            Key = "TestColor",
            Scope = "TestPalette",
            Freeze = false,
        };

        var textBlock = new TextBlock();
        var textBlockTarget = new DependencyTarget(textBlock, TextBlock.BackgroundProperty);
        var textBlockBinding = res.ProvideValue(textBlockTarget) as MultiBindingExpression;

        Assert.IsNotNull(textBlockBinding);

        BindingOperations.SetBinding(textBlock, TextBlock.BackgroundProperty, textBlockBinding.ParentMultiBinding);

        Assert.IsInstanceOfType(textBlock.Background, typeof(SolidColorBrush));

        var actualBrush = (SolidColorBrush)textBlock.Background;
        Assert.IsFalse(actualBrush.IsFrozen);

        var actualValue = actualBrush.Color.ToString();
        var expectedValue = "#12345678";

        Assert.AreEqual(expectedValue, actualValue);
    }
}
