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

using System.Diagnostics;
using System.Text;

namespace Kaspirin.UI.Framework.Log;

/// <summary>
///     Implementation of the trace listener <see cref="TraceListener" /> using the internal logic
///     of the event tracer <see cref="ITracerBackend" />.
/// </summary>
public sealed class FrameworkTraceListener : TraceListener
{
    /// <summary>
    ///     Initializes an instance of <see cref="FrameworkTraceListener" /> with the specified internal logic of the event tracer.
    /// </summary>
    /// <param name="tracer">
    ///     The internal logic of the event tracer.
    /// </param>
    public FrameworkTraceListener(ITracerBackend tracer)
    {
        Guard.ArgumentIsNotNull(tracer);

        _tracer = tracer;
    }

    /// <inheritdoc />
    public override void Close()
    {
        _tracer.Flush();
    }

    /// <inheritdoc />
    public override void Flush()
    {
        if (_message.Length != 0)
        {
            WriteLine(string.Empty);
        }
    }

    /// <inheritdoc />
    public override void Fail(string? message, string? detailMessage)
    {
        Guard.ArgumentIsNotNull(message);

        _tracer.Trace(message, TraceEventType.Critical);
    }

    /// <inheritdoc />
    public override void Write(string? message)
    {
        _message.Append(message);
    }

    /// <inheritdoc />
    public override void WriteLine(string? message)
    {
        Guard.ArgumentIsNotNull(message);

        if (_message.Length != 0)
        {
            _message.Append(message);
            message = _message.ToString();
            _message.Clear();
        }

        _tracer.Trace(message, TraceEventType.Verbose);
    }

    /// <inheritdoc />
    public override void TraceData(TraceEventCache? eventCache, string source, TraceEventType eventType, int id, object? data)
    {
        _tracer.Trace($"Data id={id}: {data}", TraceEventType.Verbose);
    }

    /// <inheritdoc />
    public override void TraceData(TraceEventCache? eventCache, string source, TraceEventType eventType, int id, params object?[]? data)
    {
        _tracer.Trace($"Data id={id}: {data}", TraceEventType.Verbose);
    }

    /// <inheritdoc />
    public override void TraceEvent(TraceEventCache? eventCache, string source, TraceEventType eventType, int id, string? message)
    {
        Guard.ArgumentIsNotNull(message);

        _tracer.Trace(message, eventType);
    }

    /// <inheritdoc />
    public override void TraceEvent(TraceEventCache? eventCache, string source, TraceEventType eventType, int id, string? format, params object?[]? args)
    {
        Guard.ArgumentIsNotNull(format);
        Guard.ArgumentIsNotNull(args);

        _tracer.Trace(string.Format(format, args), eventType);
    }

    private readonly StringBuilder _message = new();
    private readonly ITracerBackend _tracer;
}
