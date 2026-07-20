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
using System.Collections.Generic;

namespace Kaspirin.UI.Framework.Tests.TestSuites.Guards;

[TestClass]
public sealed class GuardTests
{
    #region DisableGuard

    [TestMethod]
    public void DisableGuard_PreventsAllGuardFailures()
    {
        try
        {
            // Arrange
            Guard.DisableGuard = true;

            // Act – none of the calls should throw
            Guard.Assert(false);
            Guard.Fail("msg");
            Guard.Argument(false);
            Guard.ArgumentIsNotNull<object>(null);
            Guard.ArgumentIsNotNullOrEmpty(null);
            Guard.ArgumentIsNotNullOrWhiteSpace(null);
            Guard.ArgumentCollectionIsNotNullOrEmpty<int>(null);
            Guard.ArgumentIsInstanceOfType<string>(42);
            Guard.IsNotNull<object>(null);
            Guard.IsNotNullOrEmpty(null);
            Guard.IsNotNullOrWhiteSpace(null);
            Guard.IsEmptyGuid(Guid.NewGuid());
            Guard.IsNotEmptyGuid(Guid.Empty);
        }
        finally
        {
            // Cleanup
            Guard.DisableGuard = false;
        }
    }

    #endregion

    #region GuardFailed event

    [TestMethod]
    public void GuardFailed_EventIsRaised_WithCorrectMessage()
    {
        // Arrange
        string? capturedMessage = null;
        GuardFailedEventArgs? capturedArgs = null;
        var testMessage = "Test message";

        try
        {
            Guard.GuardFailed += handler;

            // Act
            var ex = ExpectException(() => Guard.Argument(false, testMessage));

            // Assert
            Assert.IsNotNull(capturedMessage, "GuardFailed event was not raised.");
            Assert.IsTrue(capturedMessage.Contains(testMessage), "Event message does not contain custom text.");
            Assert.AreSame(ex, capturedArgs?.OriginalException, "Original exception from event differs from thrown exception.");
        }
        finally
        {
            // Cleanup
            Guard.GuardFailed -= handler;
        }

        void handler(object? s, GuardFailedEventArgs e)
        {
            capturedMessage = e.Message;
            capturedArgs = e;
        }
    }

    #endregion

    #region Argument / Assert

    [TestMethod]
    public void Argument_True_DoesNotThrow()
    {
        Guard.Argument(true);
        Guard.Argument(true, "msg");
    }

    [TestMethod]
    public void Argument_False_WithConditionExpression()
    {
        var exp = false;
        var ex = ExpectException(() => Guard.Argument(exp));
        Assert.IsTrue(ex.Message.Contains(nameof(exp)), "Message should contain the caller expression.");
    }

    [TestMethod]
    public void Argument_False_ContainsMessage()
    {
        var testMessage = "Test message";
        var ex = ExpectException(() => Guard.Argument(false, testMessage));
        Assert.IsTrue(ex.Message.Contains(testMessage));
    }

    [TestMethod]
    public void Assert_True_DoesNotThrow()
    {
        Guard.Assert(true);
        Guard.Assert(true, "msg");
    }

    [TestMethod]
    public void Assert_False_WithConditionExpression()
    {
        var exp = false;
        var ex = ExpectException(() => Guard.Assert(exp));
        Assert.IsTrue(ex.Message.Contains(nameof(exp)), "Message should contain the caller expression.");
    }

    #endregion

    #region Fail

    [TestMethod]
    public void Fail_AlwaysThrows_WithFormattedMessage()
    {
        var ex = ExpectException(() => Guard.Fail("Number {0} and {1}", 1, 2));
        Assert.IsTrue(ex.Message.Contains("Number 1 and 2"));
    }

    #endregion

    #region ArgumentCollectionIsNotNullOrEmpty / EnsureArgumentCollectionIsNotNullOrEmpty

    [TestMethod]
    public void ArgumentCollectionIsNotNullOrEmpty_Null_Throws()
    {
        var ex = ExpectException(() => Guard.ArgumentCollectionIsNotNullOrEmpty<int>(null));
        Assert.IsTrue(ex.Message.Contains("null or empty"));
    }

    [TestMethod]
    public void ArgumentCollectionIsNotNullOrEmpty_Empty_Throws()
    {
        var ex = ExpectException(() => Guard.ArgumentCollectionIsNotNullOrEmpty<int>(new List<int>()));
        Assert.IsTrue(ex.Message.Contains("null or empty"));
    }

    [TestMethod]
    public void ArgumentCollectionIsNotNullOrEmpty_Valid_DoesNotThrow()
    {
        Guard.ArgumentCollectionIsNotNullOrEmpty(new List<int> { 1 });
    }

    [TestMethod]
    public void EnsureArgumentCollectionIsNotNullOrEmpty_ReturnsSameInstance()
    {
        var list = new List<string> { "a", "b" };
        var result = Guard.EnsureArgumentCollectionIsNotNullOrEmpty(list);
        Assert.AreSame(list, result);
    }

    #endregion

    #region ArgumentIsInstanceOfType / EnsureArgumentIsInstanceOfType

    [TestMethod]
    public void ArgumentIsInstanceOfType_CorrectType_DoesNotThrow()
    {
        Guard.ArgumentIsInstanceOfType<string>("test");
    }

    [TestMethod]
    public void ArgumentIsInstanceOfType_IncorrectType_Throws()
    {
        var ex = ExpectException(() => Guard.ArgumentIsInstanceOfType<string>(42));
        Assert.IsTrue(ex.Message.Contains("should be an instance of type"));
    }

    [TestMethod]
    public void EnsureArgumentIsInstanceOfType_ReturnsCastedValue()
    {
        var result = Guard.EnsureArgumentIsInstanceOfType<string>("hello");
        Assert.AreEqual("hello", result);
    }

    #endregion

    #region ArgumentIsNotNull / EnsureArgumentIsNotNull

    [TestMethod]
    public void ArgumentIsNotNull_Null_Throws()
    {
        var ex = ExpectException(() => Guard.ArgumentIsNotNull<object>(null));
        Assert.IsTrue(ex.Message.Contains("must not be null"));
    }

    [TestMethod]
    public void ArgumentIsNotNull_NonNull_DoesNotThrow()
    {
        Guard.ArgumentIsNotNull("value");
    }

    [TestMethod]
    public void EnsureArgumentIsNotNull_ReturnsOriginalReference()
    {
        var obj = new object();
        var result = Guard.EnsureArgumentIsNotNull(obj);
        Assert.AreSame(obj, result);
    }

    #endregion

    #region ArgumentIsNotNullOrEmpty / EnsureArgumentIsNotNullOrEmpty

    [TestMethod]
    public void ArgumentIsNotNullOrEmpty_Null_Throws()
    {
        var ex = ExpectException(() => Guard.ArgumentIsNotNullOrEmpty(null));
        Assert.IsTrue(ex.Message.Contains("must not be null or empty"));
    }

    [TestMethod]
    public void ArgumentIsNotNullOrEmpty_EmptyString_Throws()
    {
        var ex = ExpectException(() => Guard.ArgumentIsNotNullOrEmpty(string.Empty));
        Assert.IsTrue(ex.Message.Contains("must not be null or empty"));
    }

    [TestMethod]
    public void ArgumentIsNotNullOrEmpty_Valid_DoesNotThrow()
    {
        Guard.ArgumentIsNotNullOrEmpty("text");
    }

    [TestMethod]
    public void EnsureArgumentIsNotNullOrEmpty_ReturnsSameString()
    {
        const string txt = "abc";
        var result = Guard.EnsureArgumentIsNotNullOrEmpty(txt);
        Assert.AreEqual(txt, result);
    }

    #endregion

    #region ArgumentIsNotNullOrWhiteSpace / EnsureArgumentIsNotNullOrWhiteSpace

    [TestMethod]
    public void ArgumentIsNotNullOrWhiteSpace_Null_Throws()
    {
        var ex = ExpectException(() => Guard.ArgumentIsNotNullOrWhiteSpace(null));
        Assert.IsTrue(ex.Message.Contains("must not be null, empty or consists only of white-space"));
    }

    [TestMethod]
    public void ArgumentIsNotNullOrWhiteSpace_WhiteSpace_Throws()
    {
        var ex = ExpectException(() => Guard.ArgumentIsNotNullOrWhiteSpace("   "));
        Assert.IsTrue(ex.Message.Contains("must not be null, empty or consists only of white-space"));
    }

    [TestMethod]
    public void ArgumentIsNotNullOrWhiteSpace_Valid_DoesNotThrow()
    {
        Guard.ArgumentIsNotNullOrWhiteSpace("value");
        Guard.ArgumentIsNotNullOrWhiteSpace("  value  ");
    }

    [TestMethod]
    public void EnsureArgumentIsNotNullOrWhiteSpace_ReturnsSameString()
    {
        const string txt = "xyz";
        var result = Guard.EnsureArgumentIsNotNullOrWhiteSpace(txt);
        Assert.AreEqual(txt, result);
    }

    #endregion

    #region IsEmptyGuid / IsNotEmptyGuid

    [TestMethod]
    public void IsEmptyGuid_WithEmptyGuid_DoesNotThrow()
    {
        Guard.IsEmptyGuid(Guid.Empty);
    }

    [TestMethod]
    public void IsEmptyGuid_WithNonEmptyGuid_Throws()
    {
        var ex = ExpectException(() => Guard.IsEmptyGuid(Guid.NewGuid()));
        Assert.IsTrue(ex.Message.Contains("must be empty"));
    }

    [TestMethod]
    public void IsNotEmptyGuid_WithNonEmptyGuid_DoesNotThrow()
    {
        Guard.IsNotEmptyGuid(Guid.NewGuid());
    }

    [TestMethod]
    public void IsNotEmptyGuid_WithEmptyGuid_Throws()
    {
        var ex = ExpectException(() => Guard.IsNotEmptyGuid(Guid.Empty));
        Assert.IsTrue(ex.Message.Contains("must not be empty"));
    }

    #endregion

    #region EnsureIs

    [TestMethod]
    public void EnsureIsNotNull_ReturnsOriginalReference()
    {
        var obj = new object();
        var result = Guard.EnsureIsNotNull(obj);
        Assert.AreSame(obj, result);
    }

    [TestMethod]
    public void EnsureIsNotNull_ThrowsWhenNull()
    {
        var ex = ExpectException(() => Guard.EnsureIsNotNull<object>(null));
        Assert.IsTrue(ex.Message.Contains("must not be null"));
    }

    [TestMethod]
    public void EnsureIsInstanceOfType_ReturnsCastedValue()
    {
        var result = Guard.EnsureIsInstanceOfType<string>("hello");
        Assert.AreEqual("hello", result);
    }

    [TestMethod]
    public void EnsureIsInstanceOfType_ThrowsWhenWrongType()
    {
        var ex = ExpectException(() => Guard.EnsureIsInstanceOfType<string>(123));
        Assert.IsTrue(ex.Message.Contains("should be an instance of type"));
    }

    #endregion

    #region EnsureAssertion

    [TestMethod]
    public void EnsureAssertion_ValidCondition_ReturnsValue()
    {
        var result = Guard.EnsureAssertion(5, i => i > 0);
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void EnsureAssertion_InvalidCondition_Throws()
    {
        var ex = ExpectException(() => Guard.EnsureAssertion(5, i => i < 0));
        Assert.IsTrue(ex.Message.Contains("must satisfy the"));
    }

    #endregion

    private static GuardException ExpectException(Action action)
    {
        try
        {
            action();
        }
        catch (GuardException ex)
        {
            return ex;
        }

        throw new AssertFailedException(
            $"Expected exception of type {typeof(GuardException).FullName} but no exception was thrown.");
    }
}
