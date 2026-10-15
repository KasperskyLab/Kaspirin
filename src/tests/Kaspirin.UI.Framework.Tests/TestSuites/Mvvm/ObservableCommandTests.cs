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
        var executed = false;
        var viewModel = new TestViewModel { Flag = false };
        viewModel.Executed += _ => executed = true;
        var command = viewModel.CreateNegationCommand();

        // Act
        command.Execute();

        // Assert
        Assert.IsTrue(executed);
    }

    [TestMethod]
    public void Constructor_ThrowsWhenExpressionObservesNoProperty()
    {
        // Act & Assert
        Assert.ThrowsException<GuardException>(() => new ObservableCommand(() => { }, () => true));
    }

    [TestMethod]
    public void Execute_InvokesActionWithParameter()
    {
        // Arrange
        var expectedParameter = "payload";
        object? actualParameter = null;
        var viewModel = new TestViewModel();
        viewModel.Executed += parameter => actualParameter = parameter;
        var command = viewModel.CreateParameterizedCommand();

        // Act
        command.Execute(expectedParameter);

        // Assert
        Assert.AreEqual(expectedParameter, actualParameter);
    }

    [TestMethod]
    public void Execute_DoesNotInvokeActionWhenCanExecuteIsFalse()
    {
        // Arrange
        var executed = false;
        var viewModel = new TestViewModel { Flag = true };
        viewModel.Executed += _ => executed = true;
        var command = viewModel.CreateNegationCommand();

        // Act
        command.Execute();

        // Assert
        Assert.IsFalse(command.CanExecute());
        Assert.IsFalse(executed);
    }

    [TestMethod]
    public void CanExecute_EvaluatesNegationExpression()
    {
        // Arrange
        var viewModel = new TestViewModel { Flag = false };
        var command = viewModel.CreateNegationCommand();

        // Act & Assert
        Assert.IsTrue(command.CanExecute());

        viewModel.Flag = true;
        Assert.IsFalse(command.CanExecute());
    }

    [TestMethod]
    public void CanExecute_EvaluatesLogicalAndExpression()
    {
        // Arrange
        var viewModel = new TestViewModel { Flag = true, First = 1 };
        var command = viewModel.CreateLogicalAndCommand();

        // Act & Assert
        Assert.IsTrue(command.CanExecute());

        viewModel.Flag = false;
        Assert.IsFalse(command.CanExecute());

        viewModel.Flag = true;
        viewModel.First = 0;
        Assert.IsFalse(command.CanExecute());
    }

    [TestMethod]
    public void CanExecute_EvaluatesMethodCallAndNegationExpression()
    {
        // Arrange
        var viewModel = new TestViewModel { Text = "hello", Flag = false };
        var command = viewModel.CreateTextAndFlagCommand();

        // Act & Assert
        Assert.IsTrue(command.CanExecute());

        viewModel.Text = "   ";
        Assert.IsFalse(command.CanExecute());

        viewModel.Text = "hello";
        viewModel.Flag = true;
        Assert.IsFalse(command.CanExecute());
    }

    [TestMethod]
    public void CanExecute_EvaluatesComparisonExpression()
    {
        // Arrange
        var viewModel = new TestViewModel { First = 10, Second = 5 };
        var command = viewModel.CreateComparisonCommand();

        // Act & Assert
        Assert.IsTrue(command.CanExecute());

        viewModel.Second = 20;
        Assert.IsFalse(command.CanExecute());
    }

    [TestMethod]
    public void CanExecute_EvaluatesTernaryExpression()
    {
        // Arrange
        var viewModel = new TestViewModel { Flag = true, First = 1, Second = 0 };
        var command = viewModel.CreateTernaryCommand();

        // Act & Assert
        Assert.IsTrue(command.CanExecute());

        viewModel.First = 0;
        Assert.IsFalse(command.CanExecute());

        viewModel.Flag = false;
        Assert.IsFalse(command.CanExecute());

        viewModel.Second = 1;
        Assert.IsTrue(command.CanExecute());
    }

    [TestMethod]
    public void CanExecute_EvaluatesTypeCheckExpression()
    {
        // Arrange
        var viewModel = new TestViewModel { Reference = "text" };
        var command = viewModel.CreateTypeCheckCommand();

        // Act & Assert
        Assert.IsTrue(command.CanExecute());

        viewModel.Reference = 42;
        Assert.IsFalse(command.CanExecute());
    }

    [TestMethod]
    public void PropertyObservation_RaisesCanExecuteChangedForEachObservedProperty()
    {
        // Arrange
        var viewModel = new TestViewModel { Text = "hello", Flag = false };
        var command = viewModel.CreateTextAndFlagCommand();
        var raised = false;
        command.CanExecuteChanged += (sender, args) => raised = true;

        // Act & Assert
        viewModel.Text = "world";
        Assert.IsTrue(raised);

        raised = false;
        viewModel.Flag = true;
        Assert.IsTrue(raised);
    }

    [TestMethod]
    public void PropertyObservation_DoesNotRaiseCanExecuteChangedForUnobservedProperty()
    {
        // Arrange
        var viewModel = new TestViewModel { Text = "hello", Flag = false };
        var command = viewModel.CreateTextAndFlagCommand();
        var raised = false;
        command.CanExecuteChanged += (sender, args) => raised = true;

        // Act
        viewModel.First = 100;

        // Assert
        Assert.IsFalse(raised);
    }

    [TestMethod]
    public void Generic_ExecuteAndCanExecuteUseTypedParameter()
    {
        // Arrange
        string? actualParameter = null;
        var viewModel = new TestViewModel { Flag = false };
        viewModel.Executed += parameter => actualParameter = (string?)parameter;
        var command = viewModel.CreateParameterizedCommand();

        // Act & Assert
        Assert.IsTrue(command.CanExecute("payload"));

        command.Execute("payload");
        Assert.AreEqual("payload", actualParameter);

        viewModel.Flag = true;
        Assert.IsFalse(command.CanExecute("payload"));
    }

    private sealed class TestViewModel : BaseViewModel
    {
        public event Action<object?>? Executed;

        public string? Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public bool Flag
        {
            get => _flag;
            set => SetProperty(ref _flag, value);
        }

        public int First
        {
            get => _first;
            set => SetProperty(ref _first, value);
        }

        public int Second
        {
            get => _second;
            set => SetProperty(ref _second, value);
        }

        public object? Reference
        {
            get => _reference;
            set => SetProperty(ref _reference, value);
        }

        public ObservableCommand CreateNegationCommand()
            => new(() => Executed?.Invoke(null), () => !Flag);

        public ObservableCommand CreateLogicalAndCommand()
            => new(() => Executed?.Invoke(null), () => Flag && First > 0);

        public ObservableCommand CreateTextAndFlagCommand()
            => new(() => Executed?.Invoke(null), () => Text.IsNotNullOrWhiteSpace() && !Flag);

        public ObservableCommand CreateComparisonCommand()
            => new(() => Executed?.Invoke(null), () => First > Second);

        public ObservableCommand CreateTernaryCommand()
            => new(() => Executed?.Invoke(null), () => Flag ? First > 0 : Second > 0);

        public ObservableCommand CreateTypeCheckCommand()
            => new(() => Executed?.Invoke(null), () => Reference is string);

        public ObservableCommand<string> CreateParameterizedCommand()
            => new(parameter => Executed?.Invoke(parameter), () => !Flag);

        private string? _text;
        private bool _flag;
        private int _first;
        private int _second;
        private object? _reference;
    }
}
