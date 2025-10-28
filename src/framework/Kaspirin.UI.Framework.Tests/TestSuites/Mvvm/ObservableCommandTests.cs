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

namespace Kaspirin.UI.Framework.Tests.TestSuites.Mvvm;

[TestClass]
public sealed class ObservableCommandTests
{
    [TestMethod]
    public void Execute_InvokesAction()
    {
        // Arrange
        var actionExecuted = false;
        var viewModel = new ObservableCommandTestViewModel();
        viewModel.CommandExecuted += parameter => actionExecuted = true;

        // Act
        viewModel.Command.Execute();

        // Assert
        Assert.IsTrue(actionExecuted);
    }

    [TestMethod]
    public void Execute_InvokesActionWithParameter()
    {
        // Arrange
        var expectedParameter = new object();
        var actionExecuted = false;
        var viewModel = new ObservableCommandTestViewModel();

        viewModel.CommandExecuted += parameter =>
        {
            actionExecuted = true;
            Assert.AreEqual(expectedParameter, parameter);
        };

        // Act
        viewModel.Command.Execute(expectedParameter);

        // Assert
        Assert.IsTrue(actionExecuted);
    }

    [TestMethod]
    public void CanExecute_ReturnsCorrectValue()
    {
        // Arrange
        var viewModel = new ObservableCommandTestViewModel
        {
            CanExecuteProperty1 = false,
            CanExecuteProperty2 = false,
        };

        // Act & Assert
        Assert.IsFalse(viewModel.Command.CanExecute());

        // Change canExecute to false
        viewModel.CanExecuteProperty1 = true;
        viewModel.CanExecuteProperty2 = true;
        Assert.IsTrue(viewModel.Command.CanExecute());
    }

    [TestMethod]
    public void PropertyObservation()
    {
        // Arrange
        var viewModel = new ObservableCommandTestViewModel();
        var eventRaised = false;
        viewModel.Command.CanExecuteChanged += (sender, args) => eventRaised = true;

        // Act & Assert
        viewModel.CanExecuteProperty1 = !viewModel.CanExecuteProperty1;
        Assert.IsTrue(eventRaised);

        eventRaised = false;
        viewModel.CanExecuteProperty2 = !viewModel.CanExecuteProperty2;
        Assert.IsTrue(eventRaised);
    }

    private sealed class ObservableCommandTestViewModel : BaseViewModel
    {
        public ObservableCommandTestViewModel()
        {
            _canExecuteProperty1 = true;
            _canExecuteProperty2 = true;
        }

        public event Action<object?>? CommandExecuted;

        public ObservableCommand<object?> Command => new(
            parameter => CommandExecuted?.Invoke(parameter),
            () => CanExecuteProperty1 && CanExecuteProperty2);

        public bool CanExecuteProperty1
        {
            get => _canExecuteProperty1;
            set => SetProperty(ref _canExecuteProperty1, value);
        }

        public bool CanExecuteProperty2
        {
            get => _canExecuteProperty2;
            set => SetProperty(ref _canExecuteProperty2, value);
        }

        private bool _canExecuteProperty1;
        private bool _canExecuteProperty2;
    }
}
