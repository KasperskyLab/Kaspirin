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
using System.Threading;

namespace Kaspirin.UI.Framework.Tests.TestSuites.Mvvm;

[TestClass]
public sealed class BaseViewModelTests
{
    [TestInitialize]
    public void InitializeTests()
    {
        _originExecutor = Executers.Implementation;

        Executers.Implementation = new TestExecutor();
    }

    [TestCleanup]
    public void CleanupTests()
    {
        Executers.Implementation = Guard.EnsureIsNotNull(_originExecutor);
    }

    [TestMethod]
    public void SetProperty_ChangesValueAndRaisesPropertyChanged()
    {
        // Arrange
        var viewModel = new TestViewModel();
        var propertyChangedRaised = false;
        viewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(viewModel.TestStringProperty))
            {
                propertyChangedRaised = true;
            }
        };

        // Act
        viewModel.TestStringProperty = "New Value";

        // Assert
        Assert.AreEqual("New Value", viewModel.TestStringProperty);
        Assert.IsTrue(viewModel.OnPropertyChangedCalled);
        Assert.IsTrue(propertyChangedRaised);
    }

    [TestMethod]
    public void SetProperty_SameValueDoesNotRaisePropertyChanged()
    {
        // Arrange
        var viewModel = new TestViewModel
        {
            TestStringProperty = "Initial Value"
        };

        var propertyChangedRaised = false;
        viewModel.PropertyChanged += (sender, args) =>
        {
            if (args.PropertyName == nameof(viewModel.TestStringProperty))
            {
                propertyChangedRaised = true;
            }
        };

        // Act
        viewModel.TestStringProperty = "Initial Value";

        // Assert
        Assert.IsFalse(propertyChangedRaised);
    }

    [TestMethod]
    public void SetProperty_PropertyChangedRaisedSync()
    {
        // Arrange
        var eventFlag = new ManualResetEventSlim();
        var testThreadId = Thread.CurrentThread.ManagedThreadId;
        var textExecutor = new TestExecutor
        {
            IsUiThread = true
        };

        Executers.Implementation = textExecutor;
        var viewModelThreadId = 0;
        var viewModel = new TestViewModel();
        viewModel.PropertyChanged += (sender, args) =>
        {
            viewModelThreadId = Thread.CurrentThread.ManagedThreadId;
            eventFlag.Set();
        };

        // Act
        viewModel.TestStringProperty = "New Value";

        // Assert
        Assert.IsTrue(eventFlag.Wait(TimeSpan.FromSeconds(1)));
        Assert.AreEqual(viewModelThreadId, testThreadId);
    }

    [TestMethod]
    public void SetProperty_PropertyChangedRaisedAsync()
    {
        // Arrange
        var eventFlag = new ManualResetEventSlim();
        var testThreadId = Thread.CurrentThread.ManagedThreadId;
        var textExecutor = new TestExecutor
        {
            IsUiThread = false
        };

        Executers.Implementation = textExecutor;

        var viewModelThreadId = 0;
        var viewModel = new TestViewModel();
        viewModel.PropertyChanged += (sender, args) =>
        {
            viewModelThreadId = Thread.CurrentThread.ManagedThreadId;
            eventFlag.Set();
        };

        // Act
        viewModel.TestStringProperty = "New Value";

        // Assert
        Assert.IsTrue(eventFlag.Wait(TimeSpan.FromSeconds(1)));
        Assert.AreNotEqual(viewModelThreadId, 0);
        Assert.AreNotEqual(viewModelThreadId, testThreadId);
    }

    [TestMethod]
    public void RaisePropertyChanged_ForOneProperty()
    {
        // Arrange
        var viewModel = new TestViewModel();
        var propertyChangedRaised = false;
        viewModel.PropertyChanged += (sender, args) =>
        {
            propertyChangedRaised = true;
        };

        // Act
        viewModel.RaisePropertyChangedForTest(nameof(viewModel.TestStringProperty));

        // Assert
        Assert.IsTrue(propertyChangedRaised);
    }

    [TestMethod]
    public void RaisePropertyChanged_ForAllProperties()
    {
        // Arrange
        var viewModel = new TestViewModel();
        var propertyChangedRaised = false;
        viewModel.PropertyChanged += (sender, args) =>
        {
            if (string.IsNullOrEmpty(args.PropertyName))
            {
                propertyChangedRaised = true;
            }
        };

        // Act
        viewModel.RaiseAllPropertiesChangedForTest();

        // Assert
        Assert.IsTrue(propertyChangedRaised);
    }

    private IUiThreadExecutor? _originExecutor;
}
