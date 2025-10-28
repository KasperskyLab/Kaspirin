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

namespace Kaspirin.UI.Framework.Tests.TestSuites.Mvvm;

[TestClass]
public sealed class DelegateCommandTests
{
    [TestMethod]
    public void Execute_InvokesAction()
    {
        // Arrange
        var actionExecuted = false;
        void action() => actionExecuted = true;
        var command = new DelegateCommand(action);

        // Act
        command.Execute();

        // Assert
        Assert.IsTrue(actionExecuted);
    }

    [TestMethod]
    public void Execute_InvokesActionWithParameter()
    {
        // Arrange
        var actionExecuted = false;
        var expectedParameter = new object();
        void action(object? param)
        {
            actionExecuted = true;
            Assert.AreEqual(expectedParameter, param);
        }

        var command = new DelegateCommand<object>(action);

        // Act
        command.Execute(expectedParameter);

        // Assert
        Assert.IsTrue(actionExecuted);
    }

    [TestMethod]
    public void CanExecute_ReturnsTrueByDefault()
    {
        // Arrange
        var command = new DelegateCommand(() => { });

        // Act & Assert
        Assert.IsTrue(command.CanExecute());
    }

    [TestMethod]
    public void CanExecute_ReturnsCorrectValueBasedOnCanExecuteMethod()
    {
        // Arrange
        var canExecute = true;
        bool canExecuteMethod() => canExecute;
        var command = new DelegateCommand(() => { }, canExecuteMethod);

        // Act & Assert
        Assert.IsTrue(command.CanExecute());

        // Change canExecute to false
        canExecute = false;
        Assert.IsFalse(command.CanExecute());
    }

    [TestMethod]
    public void CanExecute_ReturnsCorrectValueBasedOnCanExecuteMethodWithParameter()
    {
        // Arrange
        var canExecute = true;
        bool canExecuteMethod(object? param) => canExecute;
        var command = new DelegateCommand<object>((param) => { }, canExecuteMethod);

        // Act & Assert
        Assert.IsTrue(command.CanExecute(new object()));

        // Change canExecute to false
        canExecute = false;
        Assert.IsFalse(command.CanExecute(new object()));
    }
}
