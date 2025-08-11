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
        _componentTracer = ComponentTracer.Get("TestTracer");
        _componentTracerWithPrefix = ComponentTracer.Get("TestTracer", "custom prefix");
        _componentTracerWithInstance = ComponentTracer.Get(this);
    }

    [TestMethod]
    public void CheckDebugTraces()
    {
        var traceMessageActions = new Action<string>[]
        {
            (message) => _componentTracer.TraceDebug($"{message}"),
            (message) => _componentTracer.TraceDebug(message),
            (message) => _componentTracerWithPrefix.TraceDebug($"{message}"),
            (message) => _componentTracerWithPrefix.TraceDebug(message),
            (message) => _componentTracerWithInstance.TraceDebug($"{message}"),
            (message) => _componentTracerWithInstance.TraceDebug(message)
        };

        MessageIsLoggedForEventTypes(traceMessageActions, null, TraceEventType.Verbose);
        MessageIsNotLoggedForEventTypes(traceMessageActions, TraceEventType.Information);
    }

    [TestMethod]
    public void CheckInfoTraces()
    {
        var traceMessageActions = new Action<string>[]
        {
            (message) => _componentTracer.TraceInformation($"{message}"),
            (message) => _componentTracer.TraceInformation(message),
            (message) => _componentTracerWithPrefix.TraceInformation($"{message}"),
            (message) => _componentTracerWithPrefix.TraceInformation(message),
            (message) => _componentTracerWithInstance.TraceInformation($"{message}"),
            (message) => _componentTracerWithInstance.TraceInformation(message)
        };

        MessageIsLoggedForEventTypes(traceMessageActions, TraceEventType.Verbose, TraceEventType.Information);
        MessageIsNotLoggedForEventTypes(traceMessageActions, TraceEventType.Warning);
    }

    [TestMethod]
    public void CheckWarningTraces()
    {
        var traceMessageActions = new Action<string>[]
        {
            (message) => _componentTracer.TraceWarning($"{message}"),
            (message) => _componentTracer.TraceWarning(message),
            (message) => _componentTracerWithPrefix.TraceWarning($"{message}"),
            (message) => _componentTracerWithPrefix.TraceWarning(message),
            (message) => _componentTracerWithInstance.TraceWarning($"{message}"),
            (message) => _componentTracerWithInstance.TraceWarning(message)
        };

        MessageIsLoggedForEventTypes(traceMessageActions, TraceEventType.Information, TraceEventType.Warning);
        MessageIsNotLoggedForEventTypes(traceMessageActions, TraceEventType.Error);
    }

    [TestMethod]
    public void CheckErrorTraces()
    {
        var traceMessageActions = new Action<string>[]
        {
            (message) => _componentTracer.TraceError($"{message}"),
            (message) => _componentTracer.TraceError(message),
            (message) => _componentTracerWithPrefix.TraceError($"{message}"),
            (message) => _componentTracerWithPrefix.TraceError(message),
            (message) => _componentTracerWithInstance.TraceError($"{message}"),
            (message) => _componentTracerWithInstance.TraceError(message)
        };

        MessageIsLoggedForEventTypes(traceMessageActions, TraceEventType.Warning, TraceEventType.Error);
        MessageIsNotLoggedForEventTypes(traceMessageActions, 0);
    }

    private static void MessageIsLoggedForEventTypes(
        Action<string>[] traceMessageActions,
        params TraceEventType?[] appliedEventTypes)
    {
        foreach (var eventType in appliedEventTypes)
        {
            CheckLoggingMessagesForEventType(traceMessageActions, eventType, checkIsLogged: true);
        }
    }

    private static void MessageIsNotLoggedForEventTypes(
        Action<string>[] traceMessageActions,
        params TraceEventType?[] appliedEventTypes)
    {
        foreach (var eventType in appliedEventTypes)
        {
            CheckLoggingMessagesForEventType(traceMessageActions, eventType, checkIsLogged: false);
        }
    }

    private static void CheckLoggingMessagesForEventType(
        Action<string>[] traceMessageActions,
        TraceEventType? appliedEventType,
        bool checkIsLogged)
    {
        var testMessage = Guid.NewGuid().ToString();

        foreach (var traceMessageAction in traceMessageActions)
        {
            CheckLoggingMessageForEventType(traceMessageAction, testMessage, appliedEventType, checkIsLogged);
        }
    }

    private static void CheckLoggingMessageForEventType(
        Action<string> traceMessageAction,
        string testMessage,
        TraceEventType? appliedEventType,
        bool checkIsLogged)
    {
        var oldTraceLevel = ComponentTracer.MaxTraceLevel;

        try
        {
            if (appliedEventType != null)
            {
                ComponentTracer.MaxTraceLevel = appliedEventType.Value;
            }

            var collectedTraces = CollectTracesForAction(() =>
            {
                traceMessageAction(testMessage);
            });

            if (checkIsLogged)
            {
                Assert.IsTrue(collectedTraces.Contains(testMessage),
                    $"Collected traces \"{collectedTraces}\" don't contain expected test message \"{testMessage}\". Applied TraceEventType: {appliedEventType}");
            }
            else
            {
                Assert.IsTrue(!collectedTraces.Contains(testMessage),
                    $"Collected traces \"{collectedTraces}\" contain test message \"{testMessage}\", but it is not expected. Applied TraceEventType: {appliedEventType}");
            }
        }
        finally
        {
            ComponentTracer.MaxTraceLevel = oldTraceLevel;
        }
    }

    private static string CollectTracesForAction(Action action)
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

    private readonly ComponentTracer _componentTracer;
    private readonly ComponentTracer _componentTracerWithPrefix;
    private readonly ComponentTracer _componentTracerWithInstance;
}
