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
using System.Diagnostics;
using System.IO;

namespace Kaspirin.UI.Framework.Tests.TestSuites.Log;

[TestClass]
public sealed class TracerTests
{
    public TracerTests()
    {
        _component = "TestTracer";
        _stringPrefix = "CustomPrefix";
        _stringHash = "CustomPrefix";

        _objPrefix1 = _traceObj1.GetType().Name;
        _objPrefix2 = _traceObj2.GetType().Name + "CustomFunc";
        _objHash1 = _traceObj1.GetHashCode().ToString();
        _objHash2 = _traceObj2.GetHashCode().ToString() + "CustomFunc";

        _componentTracer = ComponentTracer.Get(_component);
        _componentTracerWithPrefix = ComponentTracer.Get(_component, _stringPrefix, _stringHash);
        _componentTracerWithInstance = ComponentTracer.Get(_component, _traceObj1, appendHash: true);
        _componentTracerWithParams = ComponentTracer.Get(new ComponentTracerParameters(_component)
        {
            HashSource = _traceObj2,
            HashFunc = AssertGetHashFunc,
            PrefixSource = _traceObj2,
            PrefixFunc = AssertGetPrefixFunc,
        });
    }

    [TestMethod]
    public void CheckTraceDebug()
    {
        var traceMessage = Guid.NewGuid().ToString();
        var traceMessageActions = new Action<ComponentTracer>[]
        {
            tracer => tracer.TraceDebug($"{traceMessage}"),
            tracer => tracer.TraceDebug(traceMessage),
        };

        VerifyDebugMessage(traceMessage, traceMessageActions);
    }

    [TestMethod]
    public void CheckTraceInformation()
    {
        var traceMessage = Guid.NewGuid().ToString();
        var traceMessageActions = new Action<ComponentTracer>[]
        {
            tracer => tracer.TraceInformation($"{traceMessage}"),
            tracer => tracer.TraceInformation(traceMessage),
        };

        VerifyInfoMessage(traceMessage, traceMessageActions);
    }

    [TestMethod]
    public void CheckTraceWarning()
    {
        var traceMessage = Guid.NewGuid().ToString();
        var traceMessageActions = new Action<ComponentTracer>[]
        {
            tracer => tracer.TraceWarning($"{traceMessage}"),
            tracer => tracer.TraceWarning(traceMessage),
        };

        VerifyWarningMessage(traceMessage, traceMessageActions);

    }

    [TestMethod]
    public void CheckTraceError()
    {
        var traceMessage = Guid.NewGuid().ToString();
        var traceMessageActions = new Action<ComponentTracer>[]
        {
            tracer => tracer.TraceError($"{traceMessage}"),
            tracer => tracer.TraceError(traceMessage),
        };

        VerifyErrorMessage(traceMessage, traceMessageActions);
    }

    [TestMethod]
    public void CheckTraceMethodCall()
    {
        var traceMessage = nameof(CheckTraceMethodCall);
        var traceMessageActions = new Action<ComponentTracer>[]
        {
            tracer => tracer.TraceMethodCall(),
        };

        VerifyDebugMessage(traceMessage, traceMessageActions);
    }

    [TestMethod]
    public void CheckTraceMethodStart()
    {
        var traceMessage = $"{nameof(CheckTraceMethodStart)}: method is started.";
        var traceMessageActions = new Action<ComponentTracer>[]
        {
            tracer => tracer.TraceMethodStart(),
        };

        VerifyDebugMessage(traceMessage, traceMessageActions);
    }

    [TestMethod]
    public void CheckTraceMethodStartWithArgument()
    {
        var traceMessage = Guid.NewGuid().ToString();
        var traceMessageForCheck = $"{nameof(CheckTraceMethodStartWithArgument)}: method is started. {traceMessage}";
        var traceMessageActions = new Action<ComponentTracer>[]
        {
            tracer => tracer.TraceMethodStart(traceMessage),
        };

        VerifyDebugMessage(traceMessageForCheck, traceMessageActions);
    }

    [TestMethod]
    public void CheckTraceMethodFinish()
    {
        var traceMessage = $"{nameof(CheckTraceMethodFinish)}: method is finished.";
        var traceMessageActions = new Action<ComponentTracer>[]
        {
            tracer => tracer.TraceMethodFinish(),
        };

        VerifyDebugMessage(traceMessage, traceMessageActions);
    }

    [TestMethod]
    public void CheckTraceMethodFinishWithArgument()
    {
        var traceMessage = Guid.NewGuid().ToString();
        var traceMessageForCheck = $"{nameof(CheckTraceMethodFinishWithArgument)}: method is finished. {traceMessage}";
        var traceMessageActions = new Action<ComponentTracer>[]
        {
            tracer => tracer.TraceMethodFinish(traceMessage),
        };

        VerifyDebugMessage(traceMessageForCheck, traceMessageActions);
    }

    [TestMethod]
    public void CheckTraceMethodDebug()
    {
        var traceMessage = Guid.NewGuid().ToString();
        var traceMessageForCheck = $"{nameof(CheckTraceMethodDebug)}: {traceMessage}";
        var traceMessageActions = new Action<ComponentTracer>[]
        {
            tracer => tracer.TraceMethodDebug($"{traceMessage}"),
            tracer => tracer.TraceMethodDebug(traceMessage),
        };

        VerifyDebugMessage(traceMessageForCheck, traceMessageActions);
    }

    [TestMethod]
    public void CheckTraceMethodInfo()
    {
        var traceMessage = Guid.NewGuid().ToString();
        var traceMessageForCheck = $"{nameof(CheckTraceMethodInfo)}: {traceMessage}";
        var traceMessageActions = new Action<ComponentTracer>[]
        {
            tracer => tracer.TraceMethodInfo($"{traceMessage}"),
            tracer => tracer.TraceMethodInfo(traceMessage),
        };

        VerifyInfoMessage(traceMessageForCheck, traceMessageActions);
    }

    [TestMethod]
    public void CheckTraceMethodWarning()
    {
        var traceMessage = Guid.NewGuid().ToString();
        var traceMessageForCheck = $"{nameof(CheckTraceMethodWarning)}: {traceMessage}";
        var traceMessageActions = new Action<ComponentTracer>[]
        {
            tracer => tracer.TraceMethodWarning($"{traceMessage}"),
            tracer => tracer.TraceMethodWarning(traceMessage),
        };

        VerifyWarningMessage(traceMessageForCheck, traceMessageActions);
    }

    [TestMethod]
    public void CheckTraceMethodError()
    {
        var traceMessage = Guid.NewGuid().ToString();
        var traceMessageForCheck = $"{nameof(CheckTraceMethodError)}: {traceMessage}";
        var traceMessageActions = new Action<ComponentTracer>[]
        {
            tracer => tracer.TraceMethodError($"{traceMessage}"),
            tracer => tracer.TraceMethodError(traceMessage),
        };

        VerifyErrorMessage(traceMessageForCheck, traceMessageActions);
    }

    [TestMethod]
    public void CheckAppendHash()
    {
        // Arrange
        var obj1 = new TestClass { Value = 1 };
        var obj2 = new TestClass { Value = 2 };
        var obj3 = new TestClass { Value = 1 }; // Identical to obj1

        // Act
        var trace1 = CollectTracesForAction(() => ComponentTracer.Get("Prefix", obj1, appendHash: true).TraceInformation(nameof(obj1)));
        var trace2 = CollectTracesForAction(() => ComponentTracer.Get("Prefix", obj2, appendHash: true).TraceInformation(nameof(obj2)));
        var trace3 = CollectTracesForAction(() => ComponentTracer.Get("Prefix", obj3, appendHash: true).TraceInformation(nameof(obj3)));

        // Assert
        Assert.IsTrue(trace1.Contains(obj1.GetHashCode().ToString()));
        Assert.IsTrue(trace2.Contains(obj2.GetHashCode().ToString()));
        Assert.IsTrue(trace3.Contains(obj1.GetHashCode().ToString()));
        Assert.IsTrue(trace1.Contains(nameof(obj1)));
        Assert.IsTrue(trace2.Contains(nameof(obj2)));
        Assert.IsTrue(trace3.Contains(nameof(obj3)));
    }

    private void VerifyDebugMessage(string traceMessage, Action<ComponentTracer>[] traceMessageActions)
    {
        VerifyMessage(traceMessage, traceMessageActions, TraceEventType.Verbose);
    }

    private void VerifyInfoMessage(string traceMessage, Action<ComponentTracer>[] traceMessageActions)
    {
        VerifyMessage(traceMessage, traceMessageActions, TraceEventType.Information);
    }

    private void VerifyWarningMessage(string traceMessage, Action<ComponentTracer>[] traceMessageActions)
    {
        VerifyMessage(traceMessage, traceMessageActions, TraceEventType.Warning);
    }

    private void VerifyErrorMessage(string traceMessage, Action<ComponentTracer>[] traceMessageActions)
    {
        VerifyMessage(traceMessage, traceMessageActions, TraceEventType.Error);
    }

    private void VerifyMessage(string traceMessage, Action<ComponentTracer>[] traceMessageActions, TraceEventType eventType)
    {
        switch (eventType)
        {
            case TraceEventType.Error:
                MessageIsLoggedForEventTypes(traceMessageActions, traceMessage, TraceEventType.Warning, TraceEventType.Error);
                MessageIsNotLoggedForEventTypes(traceMessageActions, traceMessage, 0);
                break;
            case TraceEventType.Warning:
                MessageIsLoggedForEventTypes(traceMessageActions, traceMessage, TraceEventType.Information, TraceEventType.Warning);
                MessageIsNotLoggedForEventTypes(traceMessageActions, traceMessage, TraceEventType.Error);
                break;
            case TraceEventType.Information:
                MessageIsLoggedForEventTypes(traceMessageActions, traceMessage, TraceEventType.Verbose, TraceEventType.Information);
                MessageIsNotLoggedForEventTypes(traceMessageActions, traceMessage, TraceEventType.Warning);
                break;
            case TraceEventType.Verbose:
                MessageIsLoggedForEventTypes(traceMessageActions, traceMessage, TraceEventType.Verbose);
                MessageIsNotLoggedForEventTypes(traceMessageActions, traceMessage, TraceEventType.Information);
                break;
            case TraceEventType.Critical:
            case TraceEventType.Start:
            case TraceEventType.Stop:
            case TraceEventType.Suspend:
            case TraceEventType.Resume:
            case TraceEventType.Transfer:
            default:
                throw new UnexpectedValueException(eventType);
        }
    }

    private void MessageIsLoggedForEventTypes(
        Action<ComponentTracer>[] traceMessageActions,
        string traceMessage,
        params TraceEventType[] appliedEventTypes)
    {
        foreach (var eventType in appliedEventTypes)
        {
            CheckLoggingMessagesForEventType(traceMessageActions, traceMessage, eventType, checkIsLogged: true);
        }
    }

    private void MessageIsNotLoggedForEventTypes(
        Action<ComponentTracer>[] traceMessageActions,
        string traceMessage,
        params TraceEventType[] appliedEventTypes)
    {
        foreach (var eventType in appliedEventTypes)
        {
            CheckLoggingMessagesForEventType(traceMessageActions, traceMessage, eventType, checkIsLogged: false);
        }
    }

    private void CheckLoggingMessagesForEventType(
        Action<ComponentTracer>[] traceMessageActions,
        string traceMessage,
        TraceEventType appliedEventType,
        bool checkIsLogged)
    {
        foreach (var traceMessageAction in traceMessageActions)
        {
            CheckLoggingMessageForEventType(_componentTracer, traceMessageAction, traceMessage, appliedEventType, checkIsLogged);
            CheckLoggingMessageForEventType(_componentTracer, traceMessageAction, _component, appliedEventType, checkIsLogged);

            CheckLoggingMessageForEventType(_componentTracerWithPrefix, traceMessageAction, traceMessage, appliedEventType, checkIsLogged);
            CheckLoggingMessageForEventType(_componentTracerWithPrefix, traceMessageAction, _component, appliedEventType, checkIsLogged);
            CheckLoggingMessageForEventType(_componentTracerWithPrefix, traceMessageAction, _stringPrefix, appliedEventType, checkIsLogged);
            CheckLoggingMessageForEventType(_componentTracerWithPrefix, traceMessageAction, _stringHash, appliedEventType, checkIsLogged);

            CheckLoggingMessageForEventType(_componentTracerWithInstance, traceMessageAction, traceMessage, appliedEventType, checkIsLogged);
            CheckLoggingMessageForEventType(_componentTracerWithInstance, traceMessageAction, _component, appliedEventType, checkIsLogged);
            CheckLoggingMessageForEventType(_componentTracerWithInstance, traceMessageAction, _objPrefix1, appliedEventType, checkIsLogged);
            CheckLoggingMessageForEventType(_componentTracerWithInstance, traceMessageAction, _objHash1, appliedEventType, checkIsLogged);

            CheckLoggingMessageForEventType(_componentTracerWithParams, traceMessageAction, traceMessage, appliedEventType, checkIsLogged);
            CheckLoggingMessageForEventType(_componentTracerWithParams, traceMessageAction, _component, appliedEventType, checkIsLogged);
            CheckLoggingMessageForEventType(_componentTracerWithParams, traceMessageAction, _objPrefix2, appliedEventType, checkIsLogged);
            CheckLoggingMessageForEventType(_componentTracerWithParams, traceMessageAction, _objHash2, appliedEventType, checkIsLogged);
        }
    }

    private void CheckLoggingMessageForEventType(
        ComponentTracer tracer,
        Action<ComponentTracer> traceMessageAction,
        string testMessage,
        TraceEventType appliedEventType,
        bool checkIsLogged)
    {
        var oldTraceLevel = ComponentTracer.MaxTraceLevel;

        try
        {
            ComponentTracer.MaxTraceLevel = appliedEventType;

            var collectedTraces = CollectTracesForAction(() =>
            {
                traceMessageAction(tracer);
            });

            if (checkIsLogged)
            {
                Assert.IsTrue(collectedTraces.Contains(testMessage),
                    $"Collected traces \"{collectedTraces}\" don't contain expected test message \"{testMessage}\". Applied TraceEventType: {appliedEventType}");
            }
            else
            {
                Assert.IsFalse(collectedTraces.Contains(testMessage),
                    $"Collected traces \"{collectedTraces}\" contain test message \"{testMessage}\", but it is not expected. Applied TraceEventType: {appliedEventType}");
            }
        }
        finally
        {
            ComponentTracer.MaxTraceLevel = oldTraceLevel;
        }
    }

    private string AssertGetPrefixFunc(object arg)
    {
        Assert.AreEqual(arg, _traceObj2);

        return _objPrefix2;
    }

    private string AssertGetHashFunc(object arg)
    {
        Assert.AreEqual(arg, _traceObj2);

        return _objHash2;
    }

    private string CollectTracesForAction(Action action)
    {
        using var stringWriter = new StringWriter();
        using var listener = new TextWriterTraceListener(stringWriter);

        try
        {
            Trace.Listeners.Add(listener);
            action();
        }
        finally
        {
            Trace.Listeners.Remove(listener);
        }

        return stringWriter.ToString();
    }

    private sealed class TestClass
    {
        public int Value { get; set; }

        public override int GetHashCode() => Value.GetHashCode() + 5_000_000;
    }

    private static readonly TestClass _traceObj1 = new() { Value = 1 };
    private static readonly TestClass _traceObj2 = new() { Value = 2 };

    private readonly ComponentTracer _componentTracer;
    private readonly ComponentTracer _componentTracerWithPrefix;
    private readonly ComponentTracer _componentTracerWithInstance;
    private readonly ComponentTracer _componentTracerWithParams;
    private readonly string _component;
    private readonly string _stringPrefix;
    private readonly string _stringHash;
    private readonly string _objPrefix1;
    private readonly string _objPrefix2;
    private readonly string _objHash1;
    private readonly string _objHash2;
}
