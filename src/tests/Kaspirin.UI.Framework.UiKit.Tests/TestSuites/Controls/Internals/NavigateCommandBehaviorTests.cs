// Copyright © 2026 AO Kaspersky Lab.
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
using System.ComponentModel;
using System.Linq;
using System.Windows.Documents;
using Kaspirin.UI.Framework.UiKit.Controls;
using Kaspirin.UI.Framework.UiKit.Controls.Behaviors;
using Kaspirin.UI.Framework.UiKit.Controls.Properties;
using Button = System.Windows.Controls.Button;
using TextBlock = System.Windows.Controls.TextBlock;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Controls.Internals;

[TestClass]
public sealed class NavigateCommandBehaviorTests : BaseTests
{
    [STATestMethod]
    public void BigButton_WithExternalCommand_ShowsExternalIcon()
    {
        // Arrange
        var button = Initialized(new BigButton { Content = "Text", Command = External() });

        // Act
        ButtonBaseNavigateCommandBehavior.SetIsEnabled(button, true);

        // Assert
        Assert.AreEqual(UIKitIcon_24.LinkExternal, button.Icon);
    }

    [STATestMethod]
    public void SmallButton_WithExternalCommand_ShowsExternalIcon()
    {
        // Arrange
        var button = Initialized(new SmallButton { Content = "Text", Command = External() });

        // Act
        ButtonBaseNavigateCommandBehavior.SetIsEnabled(button, true);

        // Assert
        Assert.AreEqual(UIKitIcon_16.LinkExternal, button.Icon);
    }

    [STATestMethod]
    public void Button_WithExternalCommand_ShowsExternalIcon()
    {
        // Arrange
        var button = Initialized(new Button { Content = "Text", Command = External() });

        // Act
        ButtonBaseNavigateCommandBehavior.SetIsEnabled(button, true);

        // Assert
        Assert.AreEqual(UIKitIcon_16.LinkExternal, ButtonProps.GetIcon(button));
    }

    [STATestMethod]
    public void MenuItem_WithExternalCommand_ShowsExternalIcon()
    {
        // Arrange
        var menuItem = Initialized(new MenuItem { Command = External() });

        // Act
        MenuItemBaseNavigateCommandBehavior.SetIsEnabled(menuItem, true);

        // Assert
        Assert.AreEqual(UIKitIcon_16.LinkExternal, menuItem.Icon);
    }

    [STATestMethod]
    public void Hyperlink_WithExternalCommand_ShowsExternalIcon()
    {
        // Arrange
        var hyperlink = Initialized(new Hyperlink { Command = External() });

        // Act
        HyperlinkNavigateCommandBehavior.SetIsEnabled(hyperlink, true);

        // Assert
        Assert.IsTrue(HasExternalIcon(hyperlink));
    }

    [STATestMethod]
    public void Button_WithExternalCommand_WithoutContent_DoesNotShowIcon()
    {
        // Arrange
        var button = Initialized(new Button { Command = External() });

        // Act
        ButtonBaseNavigateCommandBehavior.SetIsEnabled(button, true);

        // Assert
        Assert.AreEqual(UIKitIcon_16.UIKitUnset, ButtonProps.GetIcon(button));
    }

    [STATestMethod]
    public void BigButton_WithExternalCommand_WithoutContent_DoesNotChangeIcon()
    {
        // Arrange
        var button = Initialized(new BigButton { Icon = UIKitIcon_24.Plus, Command = External() });

        // Act
        ButtonBaseNavigateCommandBehavior.SetIsEnabled(button, true);

        // Assert
        Assert.AreEqual(UIKitIcon_24.Plus, button.Icon);
    }

    [STATestMethod]
    public void Button_ContentAssignedAfterBehaviorEnabled_ShowsIcon()
    {
        // Arrange
        var button = Initialized(new Button { Command = External() });
        ButtonBaseNavigateCommandBehavior.SetIsEnabled(button, true);
        Assert.AreEqual(UIKitIcon_16.UIKitUnset, ButtonProps.GetIcon(button));

        // Act
        button.Content = "Text";

        // Assert
        Assert.AreEqual(UIKitIcon_16.LinkExternal, ButtonProps.GetIcon(button));
    }

    [STATestMethod]
    public void Button_ContentClearedAfterIconShown_RestoresIcon()
    {
        // Arrange
        var button = Initialized(new Button { Content = "Text", Command = External() });
        ButtonBaseNavigateCommandBehavior.SetIsEnabled(button, true);
        Assert.AreEqual(UIKitIcon_16.LinkExternal, ButtonProps.GetIcon(button));

        // Act
        button.Content = null;

        // Assert
        Assert.AreEqual(UIKitIcon_16.UIKitUnset, ButtonProps.GetIcon(button));
    }

    [STATestMethod]
    public void Button_WithLocalCommand_DoesNotShowIcon()
    {
        // Arrange
        var button = Initialized(new Button { Content = "Text", Command = Local() });

        // Act
        ButtonBaseNavigateCommandBehavior.SetIsEnabled(button, true);

        // Assert
        Assert.AreEqual(UIKitIcon_16.UIKitUnset, ButtonProps.GetIcon(button));
    }

    [STATestMethod]
    public void Button_WithExternalCommand_BehaviorDisabled_DoesNotShowIcon()
    {
        // Arrange
        var button = Initialized(new Button { Content = "Text", Command = External() });

        // Act & Assert
        Assert.AreEqual(UIKitIcon_16.UIKitUnset, ButtonProps.GetIcon(button));
    }

    [STATestMethod]
    public void Hyperlink_WithLocalCommand_DoesNotAddIcon()
    {
        // Arrange
        var hyperlink = Initialized(new Hyperlink { Command = Local() });

        // Act
        HyperlinkNavigateCommandBehavior.SetIsEnabled(hyperlink, true);

        // Assert
        Assert.IsFalse(HasExternalIcon(hyperlink));
    }

    [STATestMethod]
    public void Button_CommandAssignedAfterBehaviorEnabled_ShowsIcon()
    {
        // Arrange
        var button = Initialized(new Button { Content = "Text" });
        ButtonBaseNavigateCommandBehavior.SetIsEnabled(button, true);
        Assert.AreEqual(UIKitIcon_16.UIKitUnset, ButtonProps.GetIcon(button));

        // Act
        button.Command = External();

        // Assert
        Assert.AreEqual(UIKitIcon_16.LinkExternal, ButtonProps.GetIcon(button));
    }

    [STATestMethod]
    public void Button_IsExternalBecomesTrue_AddsIconDynamically()
    {
        // Arrange
        var isExternal = false;
        var command = new NavigateCommand(() => { }, () => true, () => isExternal);
        var button = Initialized(new Button { Content = "Text", Command = command });
        ButtonBaseNavigateCommandBehavior.SetIsEnabled(button, true);
        Assert.AreEqual(UIKitIcon_16.UIKitUnset, ButtonProps.GetIcon(button));

        // Act
        isExternal = true;
        command.RaiseIsExternalChanged();

        // Assert
        Assert.AreEqual(UIKitIcon_16.LinkExternal, ButtonProps.GetIcon(button));
    }

    [STATestMethod]
    public void Button_BehaviorDisabledAfterIconShown_RestoresIcon()
    {
        // Arrange
        var button = Initialized(new Button { Content = "Text", Command = External() });
        ButtonBaseNavigateCommandBehavior.SetIsEnabled(button, true);
        Assert.AreEqual(UIKitIcon_16.LinkExternal, ButtonProps.GetIcon(button));

        // Act
        ButtonBaseNavigateCommandBehavior.SetIsEnabled(button, false);

        // Assert
        Assert.AreEqual(UIKitIcon_16.UIKitUnset, ButtonProps.GetIcon(button));
    }

    [STATestMethod]
    public void Hyperlink_BehaviorDisabledAfterIconShown_RemovesIcon()
    {
        // Arrange
        var hyperlink = Initialized(new Hyperlink { Command = External() });
        HyperlinkNavigateCommandBehavior.SetIsEnabled(hyperlink, true);
        Assert.IsTrue(HasExternalIcon(hyperlink));

        // Act
        HyperlinkNavigateCommandBehavior.SetIsEnabled(hyperlink, false);

        // Assert
        Assert.IsFalse(HasExternalIcon(hyperlink));
    }

    [STATestMethod]
    public void Hyperlink_InTextBlockWithSmallLineHeight_ShowsSmallIcon()
    {
        // Arrange
        var hyperlink = HyperlinkIn(new TextBlock { LineHeight = 16 });

        // Act
        HyperlinkNavigateCommandBehavior.SetIsEnabled(hyperlink, true);

        // Assert
        AssertSingleIcon(hyperlink, typeof(TextIcon12));
    }

    [STATestMethod]
    public void Hyperlink_InTextBlockWithMediumLineHeight_ShowsMediumIcon()
    {
        // Arrange
        var hyperlink = HyperlinkIn(new TextBlock { LineHeight = 24 });

        // Act
        HyperlinkNavigateCommandBehavior.SetIsEnabled(hyperlink, true);

        // Assert
        AssertSingleIcon(hyperlink, typeof(TextIcon16));
    }

    [STATestMethod]
    public void Hyperlink_InTextBlockWithLargeLineHeight_ShowsLargeIcon()
    {
        // Arrange
        var hyperlink = HyperlinkIn(new TextBlock { LineHeight = 32 });

        // Act
        HyperlinkNavigateCommandBehavior.SetIsEnabled(hyperlink, true);

        // Assert
        AssertSingleIcon(hyperlink, typeof(TextIcon24));
    }

    [STATestMethod]
    public void Hyperlink_InTextBlockWithSmallFontSize_ShowsSmallIcon()
    {
        // Arrange
        var hyperlink = HyperlinkIn(new TextBlock { FontSize = 12 });

        // Act
        HyperlinkNavigateCommandBehavior.SetIsEnabled(hyperlink, true);

        // Assert
        AssertSingleIcon(hyperlink, typeof(TextIcon12));
    }

    [STATestMethod]
    public void Hyperlink_InTextBlockWithMediumFontSize_ShowsMediumIcon()
    {
        // Arrange
        var hyperlink = HyperlinkIn(new TextBlock { FontSize = 18 });

        // Act
        HyperlinkNavigateCommandBehavior.SetIsEnabled(hyperlink, true);

        // Assert
        AssertSingleIcon(hyperlink, typeof(TextIcon16));
    }

    [STATestMethod]
    public void Hyperlink_InTextBlockWithLargeFontSize_ShowsLargeIcon()
    {
        // Arrange
        var hyperlink = HyperlinkIn(new TextBlock { FontSize = 24 });

        // Act
        HyperlinkNavigateCommandBehavior.SetIsEnabled(hyperlink, true);

        // Assert
        AssertSingleIcon(hyperlink, typeof(TextIcon24));
    }

    private static Hyperlink HyperlinkIn(TextBlock textBlock)
    {
        var hyperlink = new Hyperlink { Command = External() };
        textBlock.Inlines.Add(hyperlink);

        return hyperlink;
    }

    private static void AssertSingleIcon(Hyperlink hyperlink, Type iconType)
    {
        var icon = hyperlink.Inlines.OfType<TextInlineBase>().Single();
        Assert.IsInstanceOfType(icon, iconType);
    }

    private static NavigateCommand External()
        => new(() => { }, isExternalMethod: () => true);

    private static NavigateCommand Local()
        => new(() => { }, isExternalMethod: () => false);

    private static T Initialized<T>(T element)
        where T : ISupportInitialize
    {
        element.BeginInit();
        element.EndInit();

        return element;
    }

    private static bool HasExternalIcon(Hyperlink hyperlink)
        => hyperlink.Inlines.OfType<TextInlineBase>().Any();
}
