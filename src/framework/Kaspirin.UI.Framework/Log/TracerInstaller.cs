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
using System.Linq;
using System.Threading;

namespace Kaspirin.UI.Framework.Log;

/// <summary>
///     A class for installing a message tracer.
/// </summary>
public static class TracerInstaller
{
    /// <summary>
    ///     Initializes the message tracer.
    /// </summary>
    /// <remarks>
    ///     If the release mode flag <paramref name="isReleaseMode" /> is <see langword="true" />, then
    ///     the default tracer <see cref="DefaultTraceListener" /> will be disabled.
    /// </remarks>
    /// <param name="isReleaseMode">
    ///     The release mode flag.
    /// </param>
    /// <param name="tracerBackend">
    ///     The internal implementation of the tracer.
    /// </param>
    public static void InstallTracer(bool isReleaseMode, ITracerBackend tracerBackend)
    {
        Trace.Listeners.Add(new FrameworkTraceListener(tracerBackend));
        Trace.AutoFlush = true;

        if (isReleaseMode)
        {
            _tracer.TraceInformation("Release mode: Disable default .Net logger");
            new Thread(() => Trace.Listeners
                                  .OfType<DefaultTraceListener>()
                                  .ToList()
                                  .ForEach(Trace.Listeners.Remove))
                .Start();
        }
    }

    private static readonly ComponentTracer _tracer = ComponentTracer.Get(ComponentTracers.Log);
}
