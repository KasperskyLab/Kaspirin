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
using System.Runtime.CompilerServices;
using Kaspirin.UI.Framework.Tests.TestHelpers;

namespace Kaspirin.UI.Framework.Tests.TestSuites.Storage.Mutable;

[TestClass]
public sealed class MutableParameterTests
{
    #region Construction

    [TestMethod]
    public void Constructor_ExposesIdAndInitialValueFromStorage()
    {
        var storage = new FakeParameterStorage();
        storage.Seed(FakeParameterId.First, TimeSpan.FromSeconds(1));

        var parameter = new MutableParameter<TimeSpan>(storage, FakeParameterId.First);

        Assert.AreEqual(FakeParameterId.First, parameter.Id);
        Assert.AreEqual(TimeSpan.FromSeconds(1), parameter.Value);
    }

    [TestMethod]
    public void Constructor_Throws_WhenStorageValueIsNotAssignableToValueType()
    {
        var storage = new FakeParameterStorage();
        storage.Seed(FakeParameterId.First, "not a TimeSpan");

        Assert.ThrowsException<GuardException>(() => new MutableParameter<TimeSpan>(storage, FakeParameterId.First));
    }

    #endregion

    #region ValueChanged

    [TestMethod]
    public void ValueChanged_OnOwnParameterChange_RaisedWithOldAndNewValues()
    {
        var storage = new FakeParameterStorage();
        storage.Seed(FakeParameterId.First, TimeSpan.FromSeconds(1));

        var parameter = new MutableParameter<TimeSpan>(storage, FakeParameterId.First);

        MutableParameter<TimeSpan>? sender = null;
        TimeSpan? oldValue = null;
        TimeSpan? newValue = null;
        var raised = 0;

        parameter.ValueChanged += (s, e) =>
        {
            sender = s;
            oldValue = e.OldValue;
            newValue = e.NewValue;
            raised++;
        };

        storage.SetValue(FakeParameterId.First, TimeSpan.FromSeconds(2));

        Assert.AreEqual(1, raised, "ValueChanged must be raised once when the own parameter changes.");
        Assert.AreSame(parameter, sender, "ValueChanged must pass the parameter itself as the sender.");
        Assert.AreEqual(TimeSpan.FromSeconds(1), oldValue, "OldValue must be the value before the change.");
        Assert.AreEqual(TimeSpan.FromSeconds(2), newValue, "NewValue must be the value after the change.");
        Assert.AreEqual(TimeSpan.FromSeconds(2), parameter.Value, "Value must reflect the latest stored value.");
    }

    [TestMethod]
    public void ValueChanged_NotRaised_WhenNewValueEqualsCurrent()
    {
        var storage = new FakeParameterStorage();
        storage.Seed(FakeParameterId.First, TimeSpan.FromSeconds(1));

        var parameter = new MutableParameter<TimeSpan>(storage, FakeParameterId.First);

        var raised = 0;
        parameter.ValueChanged += (_, _) => raised++;

        storage.SetValue(FakeParameterId.First, TimeSpan.FromSeconds(1));

        Assert.AreEqual(0, raised, "ValueChanged must not be raised when the value did not actually change.");
        Assert.AreEqual(TimeSpan.FromSeconds(1), parameter.Value);
    }

    [TestMethod]
    public void ValueChanged_NotRaised_WhenADifferentParameterChanges()
    {
        var storage = new FakeParameterStorage();
        storage.Seed(FakeParameterId.First, TimeSpan.FromSeconds(1));
        storage.Seed(FakeParameterId.Second, TimeSpan.FromSeconds(99));

        var parameter = new MutableParameter<TimeSpan>(storage, FakeParameterId.First);

        var raised = 0;
        parameter.ValueChanged += (_, _) => raised++;

        storage.SetValue(FakeParameterId.Second, TimeSpan.FromSeconds(2));

        Assert.AreEqual(0, raised, "Changing a different parameter must not raise ValueChanged for this one.");
    }

    [TestMethod]
    public void Value_Unchanged_WhenADifferentParameterChanges()
    {
        var storage = new FakeParameterStorage();
        storage.Seed(FakeParameterId.First, TimeSpan.FromSeconds(1));
        storage.Seed(FakeParameterId.Second, TimeSpan.FromSeconds(99));

        var parameter = new MutableParameter<TimeSpan>(storage, FakeParameterId.First);

        storage.SetValue(FakeParameterId.Second, TimeSpan.FromSeconds(2));

        Assert.AreEqual(
            TimeSpan.FromSeconds(1),
            parameter.Value,
            "A parameter must keep its own value when an unrelated parameter changes.");
    }

    [TestMethod]
    public void Value_TracksSuccessiveChangesOfOwnParameter()
    {
        var storage = new FakeParameterStorage();
        storage.Seed(FakeParameterId.First, TimeSpan.FromSeconds(1));

        var parameter = new MutableParameter<TimeSpan>(storage, FakeParameterId.First);

        var observed = new List<TimeSpan>();
        parameter.ValueChanged += (_, e) => observed.Add(e.NewValue);

        storage.SetValue(FakeParameterId.First, TimeSpan.FromSeconds(2));
        storage.SetValue(FakeParameterId.First, TimeSpan.FromSeconds(3));
        storage.SetValue(FakeParameterId.First, TimeSpan.FromSeconds(3)); // no-op
        storage.SetValue(FakeParameterId.First, TimeSpan.FromSeconds(4));

        CollectionAssert.AreEqual(
            new[] { TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(4) },
            observed);
        Assert.AreEqual(TimeSpan.FromSeconds(4), parameter.Value);
    }

    #endregion

    #region Lifetime

    [TestMethod]
    public void Storage_HoldsParameterWeakly_AllowingItToBeCollected()
    {
        var storage = new FakeParameterStorage();
        storage.Seed(FakeParameterId.First, TimeSpan.FromSeconds(1));

        var parameterRef = CreateCollectibleParameter(storage);

        Assert.IsTrue(
            TestGCOperations.TryCollect(parameterRef),
            "The storage must subscribe to the parameter weakly, so an unreferenced parameter can be collected.");

        storage.SetValue(FakeParameterId.First, TimeSpan.FromSeconds(2));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static WeakReference CreateCollectibleParameter(IMutableParameterStorage storage)
    {
        var parameter = new MutableParameter<TimeSpan>(storage, FakeParameterId.First);

        return new WeakReference(parameter);
    }

    #endregion

    #region Helpers

    private enum FakeParameterId
    {
        First,
        Second,
    }

    private sealed class FakeParameterStorage : IMutableParameterStorage
    {
        public event Action<Enum> ParameterChanged = _ => { };

        public void Seed(Enum parameterId, object value)
            => _values[parameterId] = value;

        public void SetValue(Enum parameterId, object value)
        {
            _values[parameterId] = value;

            ParameterChanged.Invoke(parameterId);
        }

        public object GetParameter(Enum parameterId)
            => _values[parameterId];

        private readonly Dictionary<Enum, object> _values = new();
    }

    #endregion
}
