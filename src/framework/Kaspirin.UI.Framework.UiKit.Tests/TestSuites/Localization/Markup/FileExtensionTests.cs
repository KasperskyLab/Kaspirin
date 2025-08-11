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
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Localization.Markup;

[TestClass]
public sealed class FileExtensionTests : BaseTests
{
    [STATestMethod]
    public void FileExtension()
    {
        var file = new FileExtension
        {
            Key = "Test.txt",
            Scope = "Files",
            Mode = FileExtensionMode.Text
        };
        var fileContent = "Test content";

        var textBlock = new TextBlock();
        var textTarget = new DependencyTarget(textBlock, TextBlock.TextProperty);
        var textBinding = file.ProvideValue(textTarget) as MultiBindingExpression;

        Assert.IsNotNull(textBinding);

        BindingOperations.SetBinding(textBlock, TextBlock.TextProperty, textBinding.ParentMultiBinding);

        Assert.AreEqual(fileContent, textBlock.Text);
    }
}