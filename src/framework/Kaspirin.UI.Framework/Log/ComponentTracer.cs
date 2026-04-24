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

using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Kaspirin.UI.Framework.Log;

/// <summary>
///     Provides functionality for writing messages to a trace.
/// </summary>
public sealed class ComponentTracer
{
    static ComponentTracer()
    {
        UpdateTraceLevels();
    }

    /// <summary>
    ///     Specifies whether debugging messages are written to the trace.
    /// </summary>
    public static bool CanTraceDebug { get; private set; }

    /// <summary>
    ///     Specifies whether information messages are written to the trace.
    /// </summary>
    public static bool CanTraceInfo { get; private set; }

    /// <summary>
    ///     Specifies whether warning messages are written to the trace.
    /// </summary>
    public static bool CanTraceWarn { get; private set; }

    /// <summary>
    ///     Specifies whether error messages are written to the trace.
    /// </summary>
    public static bool CanTraceError { get; private set; }

    /// <summary>
    ///     Gets or sets the maximum trace level.
    /// </summary>
    /// <remarks>
    ///     Acceptable values:
    ///     <br /><see cref="TraceEventType.Verbose" />
    ///     <br /><see cref="TraceEventType.Information" />
    ///     <br /><see cref="TraceEventType.Warning" />
    ///     <br /><see cref="TraceEventType.Error" />
    ///     <br />To disable tracing, you can set the value to 0, which is not included in the enumeration<see cref="TraceEventType" />.
    /// </remarks>
    public static TraceEventType MaxTraceLevel
    {
        get => _maxTraceLevel;
        set
        {
            Guard.Assert(value.In(_expectedTraceLevels));

            if (_maxTraceLevel != value)
            {
                var oldValue = _maxTraceLevel;
                var newValue = value;

                _maxTraceLevel = value;

                UpdateTraceLevels();

                Get(ComponentTracers.Log).TraceInformation($"Maximal trace level is updated from {oldValue} to {newValue}");
            }
        }
    }

    /// <summary>
    ///     Retrieves the <see cref="ComponentTracer" /> object for the specified trace component.
    /// </summary>
    /// <param name="traceComponent">
    ///     The traceable component.
    /// </param>
    /// <returns>
    ///     The <see cref="ComponentTracer" /> object for the specified component.
    /// </returns>
    public static ComponentTracer Get(string traceComponent)
        => Get(new ComponentTracerParameters(traceComponent));

    /// <summary>
    ///     Retrieves the <see cref="ComponentTracer" /> object for the specified trace component.
    /// </summary>
    /// <param name="source">
    ///     The tracing object.
    /// </param>
    /// <param name="appendHash">
    ///     Add the hash code of the trace object to the message.
    /// </param>
    /// <returns>
    ///     The <see cref="ComponentTracer" /> object for the specified component.
    /// </returns>
    public static ComponentTracer Get(object source, bool appendHash = false)
        => Get(new ComponentTracerParameters(Guard.EnsureArgumentIsNotNull(source).GetType().Name)
        {
            HashSource = appendHash ? source : null,
        });

    /// <summary>
    ///     Retrieves the <see cref="ComponentTracer" /> object for the specified trace component.
    /// </summary>
    /// <param name="traceComponent">
    ///     The traceable component.
    /// </param>
    /// <param name="source">
    ///     The tracing object.
    /// </param>
    /// <param name="appendHash">
    ///     Add the hash code of the trace object to the message.
    /// </param>
    /// <returns>
    ///     The <see cref="ComponentTracer" /> object for the specified component.
    /// </returns>
    public static ComponentTracer Get(string traceComponent, object source, bool appendHash = false)
        => Get(new ComponentTracerParameters(traceComponent)
        {
            PrefixSource = source,
            HashSource = appendHash ? source : null,
        });

    /// <summary>
    ///     Retrieves the <see cref="ComponentTracer" /> object for the specified trace component.
    /// </summary>
    /// <param name="traceComponent">
    ///     The traceable component.
    /// </param>
    /// <param name="prefix">
    ///     The prefix of the message.
    /// </param>
    /// <param name="hash">
    ///     The hash code of the traced component.
    /// </param>
    /// <returns>
    ///     The <see cref="ComponentTracer" /> object for the specified component.
    /// </returns>
    public static ComponentTracer Get(string traceComponent, string prefix, object? hash = null)
        => Get(new ComponentTracerParameters(traceComponent)
        {
            PrefixSource = prefix,
            PrefixFunc = o => (string)o,
            HashSource = hash,
        });

    /// <summary>
    ///     Retrieves the <see cref="ComponentTracer" /> object for the specified trace component.
    /// </summary>
    /// <param name="parameters">
    ///     Tracer parameters.
    /// </param>
    /// <returns>
    ///     The <see cref="ComponentTracer" /> object for the specified component.
    /// </returns>
    public static ComponentTracer Get(ComponentTracerParameters parameters)
    {
        Guard.ArgumentIsNotNull(parameters);

        var hasHash = parameters.HashSource != null && parameters.HashFunc != null;
        if (hasHash)
        {
            return new ComponentTracer(parameters);
        }
        else
        {
            var tracerKey = GetTracerKey(parameters);

            // if tracer has no hash part is message, we can cache tracer instance.
            return _tracersMap.GetOrAdd(tracerKey, _ => new ComponentTracer(parameters));
        }
    }

    /// <summary>
    ///     Writes the debug message from <paramref name="interpolatedStringHandler" /> to the trace.
    /// </summary>
    /// <param name="interpolatedStringHandler">
    ///     An interpolated string handler.
    /// </param>
    public void TraceDebug(ref DebugInterpolatedStringHandler interpolatedStringHandler)
        => TraceDebug(interpolatedStringHandler.ToStringAndClear());

    /// <summary>
    ///     Writes the debugging message <paramref name="message" /> to the trace.
    /// </summary>
    /// <param name="message">
    ///     The text of the message.
    /// </param>
    public void TraceDebug(string message)
    {
        if (CanTraceDebug)
        {
            Trace.WriteLine(_tracePrefix + message);
        }
    }

    /// <summary>
    ///     Writes the information message from <paramref name="interpolatedStringHandler" /> to the trace.
    /// </summary>
    /// <param name="interpolatedStringHandler">
    ///     An interpolated string handler.
    /// </param>
    public void TraceInformation(ref InfoInterpolatedStringHandler interpolatedStringHandler)
        => TraceInformation(interpolatedStringHandler.ToStringAndClear());

    /// <summary>
    ///     Writes the information message <paramref name="message" /> to the trace.
    /// </summary>
    /// <param name="message">
    ///     The text of the message.
    /// </param>
    public void TraceInformation(string message)
    {
        if (CanTraceInfo)
        {
            Trace.TraceInformation(_tracePrefix + message);
        }
    }

    /// <summary>
    ///     Writes a warning message from <paramref name="interpolatedStringHandler" /> to the trace.
    /// </summary>
    /// <param name="interpolatedStringHandler">
    ///     An interpolated string handler.
    /// </param>
    public void TraceWarning(ref WarnInterpolatedStringHandler interpolatedStringHandler)
        => TraceWarning(interpolatedStringHandler.ToStringAndClear());

    /// <summary>
    ///     Writes the warning message <paramref name="message" /> to the trace.
    /// </summary>
    /// <param name="message">
    ///     The text of the message.
    /// </param>
    public void TraceWarning(string message)
    {
        if (CanTraceWarn)
        {
            Trace.TraceWarning(_tracePrefix + message);
        }
    }

    /// <summary>
    ///     Writes the error message from <paramref name="interpolatedStringHandler" /> to the trace.
    /// </summary>
    /// <param name="interpolatedStringHandler">
    ///     An interpolated string handler.
    /// </param>
    public void TraceError(ref ErrorInterpolatedStringHandler interpolatedStringHandler)
        => TraceError(interpolatedStringHandler.ToStringAndClear());

    /// <summary>
    ///     Writes the error message <paramref name="message" /> to the trace.
    /// </summary>
    /// <param name="message">
    ///     The text of the message.
    /// </param>
    public void TraceError(string message)
    {
        if (CanTraceError)
        {
            Trace.TraceError(_tracePrefix + message);
        }
    }

    /// <summary>
    ///     Writes a debugging message to the trace with the name of the method.
    /// </summary>
    /// <param name="method">
    ///     The name of the method.
    /// </param>
    public void TraceMethodCall([CallerMemberName] string? method = null)
    {
        if (method is not null)
        {
            TraceDebug(method);
        }
    }

    /// <summary>
    ///     Writes a debugging message to the trace about the start of the method execution.
    /// </summary>
    /// <param name="message">
    ///     The text of the message.
    /// </param>
    /// <param name="method">
    ///     The name of the method.
    /// </param>
    public void TraceMethodStart(string? message = null, [CallerMemberName] string? method = null)
        => TraceDebug($"{method}: method is started.{(message == null ? string.Empty : (" " + message))}");

    /// <summary>
    ///     Writes a debugging message to the trace about the completion of the method execution.
    /// </summary>
    /// <param name="message">
    ///     The text of the message.
    /// </param>
    /// <param name="method">
    ///     The name of the method.
    /// </param>
    public void TraceMethodFinish(string? message = null, [CallerMemberName] string? method = null)
        => TraceDebug($"{method}: method is finished.{(message == null ? string.Empty : (" " + message))}");

    /// <summary>
    ///     Writes a debugging message to the trace with the name of the method.
    /// </summary>
    /// <param name="message">
    ///     The text of the message.
    /// </param>
    /// <param name="method">
    ///     The name of the method.
    /// </param>
    public void TraceMethodDebug(string message, [CallerMemberName] string? method = null)
        => TraceDebug($"{method}: {message}");

    /// <summary>
    ///     Writes a debugging message to the trace using an interpolated string handler, prefixed with
    ///     the name of the method being executed.
    /// </summary>
    /// <param name="interpolatedStringHandler">
    ///     An interpolated string handler.
    /// </param>
    /// <param name="method">
    ///     The name of the method.
    /// </param>
    public void TraceMethodDebug(
        ref DebugInterpolatedStringHandler interpolatedStringHandler,
        [CallerMemberName] string? method = null)
    {
        TraceDebug($"{method}: {interpolatedStringHandler.ToStringAndClear()}");
    }

    /// <summary>
    ///     Writes an information message to the trace with the name of the method.
    /// </summary>
    /// <param name="message">
    ///     The text of the message.
    /// </param>
    /// <param name="method">
    ///     The name of the method.
    /// </param>
    public void TraceMethodInfo(string message, [CallerMemberName] string? method = null)
        => TraceInformation($"{method}: {message}");

    /// <summary>
    ///     Writes an information message to the trace using an interpolated string handler, prefixed with
    ///     the name of the method being executed.
    /// </summary>
    /// <param name="interpolatedStringHandler">
    ///     An interpolated string handler.
    /// </param>
    /// <param name="method">
    ///     The name of the method.
    /// </param>
    public void TraceMethodInfo(
        ref InfoInterpolatedStringHandler interpolatedStringHandler,
        [CallerMemberName] string? method = null)
    {
        TraceInformation($"{method}: {interpolatedStringHandler.ToStringAndClear()}");
    }

    /// <summary>
    ///     Writes a warning message to the trace with the name of the method.
    /// </summary>
    /// <param name="message">
    ///     The text of the message.
    /// </param>
    /// <param name="method">
    ///     The name of the method.
    /// </param>
    public void TraceMethodWarning(string message, [CallerMemberName] string? method = null)
        => TraceWarning($"{method}: {message}");

    /// <summary>
    ///     Writes a warning message to the trace using an interpolated string handler, prefixed with the
    ///     name of the method being executed.
    /// </summary>
    /// <param name="interpolatedStringHandler">
    ///     An interpolated string handler.
    /// </param>
    /// <param name="method">
    ///     The name of the method.
    /// </param>
    public void TraceMethodWarning(
        ref WarnInterpolatedStringHandler interpolatedStringHandler,
        [CallerMemberName] string? method = null)
    {
        TraceWarning($"{method}: {interpolatedStringHandler.ToStringAndClear()}");
    }

    /// <summary>
    ///     Writes an error message to the trace <paramref name="message" /> prefixed with the name of the method being executed.
    /// </summary>
    /// <param name="message">
    ///     An interpolated string handler.
    /// </param>
    /// <param name="method">
    ///     The name of the method.
    /// </param>
    public void TraceMethodError(string message, [CallerMemberName] string? method = null)
        => TraceError($"{method}: {message}");

    /// <summary>
    ///     Writes an error message to the trace using an interpolated string handler, prefixed with the
    ///     name of the method being executed.
    /// </summary>
    /// <param name="interpolatedStringHandler">
    ///     An interpolated string handler.
    /// </param>
    /// <param name="method">
    ///     The name of the method.
    /// </param>
    public void TraceMethodError(
        ref ErrorInterpolatedStringHandler interpolatedStringHandler,
        [CallerMemberName] string? method = null)
    {
        TraceError($"{method}: {interpolatedStringHandler.ToStringAndClear()}");
    }

    private ComponentTracer(ComponentTracerParameters parameters)
    {
        _tracePrefix = GetTracerPrefix(parameters);
    }

    private static void UpdateTraceLevels()
    {
        CanTraceDebug = _maxTraceLevel >= TraceEventType.Verbose;
        CanTraceInfo = _maxTraceLevel >= TraceEventType.Information;
        CanTraceWarn = _maxTraceLevel >= TraceEventType.Warning;
        CanTraceError = _maxTraceLevel >= TraceEventType.Error;
    }

    private static string GetTracerKey(ComponentTracerParameters parameters)
    {
        var key = parameters.TraceComponent;

        if (parameters.PrefixSource != null)
        {
            key += parameters.PrefixSource.GetType().Name;

            if (parameters.PrefixFunc != null)
            {
                key += parameters.PrefixFunc.Invoke(parameters.PrefixSource);
            }
        }

        return key;
    }

    private static string GetTracerPrefix(ComponentTracerParameters parameters)
    {
        var tracePrefix = $"{parameters.TraceComponent}\t";

        if (parameters.HashSource != null)
        {
            var hash = parameters.HashFunc?.Invoke(parameters.HashSource);
            if (hash.IsNotNullOrEmpty())
            {
                tracePrefix += $"hash:{hash}\t";
            }
        }

        if (parameters.PrefixSource != null)
        {
            var prefix = parameters.PrefixFunc?.Invoke(parameters.PrefixSource);
            if (prefix.IsNotNullOrEmpty())
            {
                tracePrefix += $"{prefix} ";
            }
        }

        return tracePrefix;
    }

    private readonly string _tracePrefix;

    private static readonly ConcurrentDictionary<string, ComponentTracer> _tracersMap = new();

    private static readonly TraceEventType[] _expectedTraceLevels =
    {
        0, // Used to disable tracing.
        TraceEventType.Error,
        TraceEventType.Warning,
        TraceEventType.Information,
        TraceEventType.Verbose,
    };

    private static TraceEventType _maxTraceLevel = TraceEventType.Verbose;
}