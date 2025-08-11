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

using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Options;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Localization.Markup;

[TestClass]
public sealed class LocExtensionTests : BaseTests
{
    [STATestMethod]
    public void LocExtension()
    {
        var locValue = "test value";
        var loc = new LocExtension
        {
            Scope = Scope,
            Key = "TestKey"
        };

        var textBlock = new TextBlock();
        var textBlockTarget = new DependencyTarget(textBlock, TextBlock.TextProperty);
        var textBlockBinding = loc.ProvideValue(textBlockTarget) as MultiBindingExpression;

        Assert.IsNotNull(textBlockBinding);

        BindingOperations.SetBinding(textBlock, TextBlock.TextProperty, textBlockBinding.ParentMultiBinding);

        Assert.AreEqual(locValue, textBlock.Text);
    }

    [STATestMethod]
    public void LocExtensionWithChangeCulture()
    {
        var locValue = "test value";
        var loc = new LocExtension
        {
            Scope = Scope,
            Key = "TestKey"
        };

        var textBlock = new TextBlock();
        var textBlockTarget = new DependencyTarget(textBlock, TextBlock.TextProperty);
        var textBlockBinding = loc.ProvideValue(textBlockTarget) as MultiBindingExpression;

        Assert.IsNotNull(textBlockBinding);

        BindingOperations.SetBinding(textBlock, TextBlock.TextProperty, textBlockBinding.ParentMultiBinding);

        LocalizationManager.ChangeCulture("ru-RU");

        locValue = "test value (ru)";
        Assert.AreEqual(locValue, textBlock.Text);

        LocalizationManager.ChangeCulture("en-US");

        locValue = "test value";
        Assert.AreEqual(locValue, textBlock.Text);
    }

    [STATestMethod]
    public void LocExtensionWithParameter()
    {
        var loc = new LocExtension
        {
            Scope = Scope,
            Key = "TestKeyWithChangedParam",
            Param = new LocParameter
            {
                ParamName = "StringValue",
                ParamSource = new Binding
                {
                    Source = new ParamSource { IntValue = 10, StringValue = "str1" },
                    Path = new PropertyPath("StringValue")
                }
            }
        };

        var textBlock = new TextBlock();
        var textBlockTarget = new DependencyTarget(textBlock, TextBlock.TextProperty);
        var textBlockBinding = loc.ProvideValue(textBlockTarget) as MultiBindingExpression;

        Assert.IsNotNull(textBlockBinding);

        BindingOperations.SetBinding(textBlock, TextBlock.TextProperty, textBlockBinding.ParentMultiBinding);

        Assert.AreEqual("begin str1 end", textBlock.Text);
    }

    [TestMethod]
    public void LocExtensionWithParameterConstant()
    {
        var loc = new LocExtension
        {
            Scope = Scope,
            Key = "TestKeyWithChangedParam",
            Param = new LocParameter
            {
                ParamName = "StringValue",
                ParamSource = new Binding { Source = "str1" }
            }
        };

        Assert.AreEqual("begin str1 end", loc.ProvideConstantStringValue());
    }

    [TestMethod]
    public void LocExtensionWithOptions()
    {
        var loc1 = new LocExtension("FirstLetterMustBeLower", Scope)
        {
            Option = LocOptionFactory.CreateChangeCaseOption(ChangeCaseOptionMode.LowercaseFirstLetterOnly)
        };

        Assert.AreEqual("aBCDEF", loc1.ProvideConstantStringValue());

        var loc2 = new LocExtension("FirstLetterMustBeUpper", Scope)
        {
            Option = LocOptionFactory.CreateChangeCaseOption(ChangeCaseOptionMode.UppercaseFirstLetterOnly)
        };

        Assert.AreEqual("Abcdef", loc2.ProvideConstantStringValue());

        var loc3 = new LocExtension("AllLetterMustBeLower", Scope)
        {
            Option = LocOptionFactory.CreateChangeCaseOption(ChangeCaseOptionMode.LowercaseAll)
        };

        Assert.AreEqual("abcdef", loc3.ProvideConstantStringValue());

        var loc4 = new LocExtension("AllLettersMustBeUpper", Scope)
        {
            Option = LocOptionFactory.CreateChangeCaseOption(ChangeCaseOptionMode.UppercaseAll)
        };

        Assert.AreEqual("ABCDEF", loc4.ProvideConstantStringValue());
    }

    [STATestMethod]
    public void LocExtensionWithParameters()
    {
        var paramObj = new ParamSource { IntValue = 10, StringValue = "str1" };

        var loc = new LocExtension
        {
            Scope = Scope,
            Key = "TestKeyWithChangedParamWithInt",
            Params = new LocParameterCollection
            {
                new LocParameter
                {
                    ParamName = "StringValue",
                    ParamSource = new Binding
                    {
                        Source = paramObj,
                        Path = new PropertyPath("StringValue")
                    }
                },
                new LocParameter
                {
                    ParamName = "IntValue",
                    ParamSource = new Binding
                    {
                        Source = paramObj,
                        Path = new PropertyPath("IntValue")
                    }
                }
            }
        };

        var textBlock = new TextBlock();
        var textBlockTarget = new DependencyTarget(textBlock, TextBlock.TextProperty);
        var textBlockBinding = loc.ProvideValue(textBlockTarget) as MultiBindingExpression;

        Assert.IsNotNull(textBlockBinding);

        BindingOperations.SetBinding(textBlock, TextBlock.TextProperty, textBlockBinding.ParentMultiBinding);

        Assert.AreEqual("begin str1_10 end", textBlock.Text);

        paramObj.StringValue = "str2";
        Assert.AreEqual("begin str2_10 end", textBlock.Text);

        paramObj.IntValue = 20;
        Assert.AreEqual("begin str2_20 end", textBlock.Text);
    }

    [STATestMethod]
    public void LocExtensionWithResourceDelivery()
    {
        var paramObj = new ParamSource { EnumValue = TestValues.Test2 };

        var collectionDelivery = new ResFromCollectionExtension
        {
            Source = new Binding
            {
                Source = paramObj.EnumValue
            },
            Collection = new ResourceCollection
            {
                { TestValues.Test1, "str1" },
                { TestValues.Test2, "str2" }
            }
        };
        var locParameter = new LocParameter
        {
            ParamName = "EnumValue"
        };

        var locParameterTarget = new DependencyTarget(locParameter, locParameter.GetType().GetRuntimeProperty("ParamSource"));
        var locParameterBinding = collectionDelivery.ProvideValue(locParameterTarget) as Binding;

        Assert.IsNotNull(locParameterBinding);

        locParameter.ParamSource = locParameterBinding;

        var loc = new LocExtension
        {
            Scope = Scope,
            Key = "TestKeyWithChangedParamWithEnum",
            Param = locParameter
        };

        var textBlock = new TextBlock();
        var textBlockTarget = new DependencyTarget(textBlock, TextBlock.TextProperty);
        var textBlockBinding = loc.ProvideValue(textBlockTarget) as MultiBindingExpression;

        Assert.IsNotNull(textBlockBinding);

        BindingOperations.SetBinding(textBlock, TextBlock.TextProperty, textBlockBinding.ParentMultiBinding);

        // updating data context triggering LocExt to update complex bindings(i.e. ResourceDelivery)
        // attached to textblock's property in order to supply values
        // see BaseLocalizationMarkupExtension.ProvideBinding()
        textBlock.DataContext = new object();

        Assert.AreEqual("begin str2 end", textBlock.Text);
    }

    private const string Scope = "Test";

    private enum TestValues
    {
        Test1,
        Test2
    }

    private sealed class ParamSource : BaseViewModel
    {
        public string? StringValue
        {
            get => _stringValue;
            set => SetProperty(ref _stringValue, value);
        }

        public int IntValue
        {
            get => _intValue;
            set => SetProperty(ref _intValue, value);
        }

        public TestValues EnumValue
        {
            get => _enumValue;
            set => SetProperty(ref _enumValue, value);
        }

        private string? _stringValue;
        private int _intValue;
        private TestValues _enumValue;
    }
}
