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

namespace Kaspirin.UI.Framework.Log
{
    /// <summary>
    ///     Message tracer parameters.
    /// </summary>
    public sealed class ComponentTracerParameters
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ComponentTracerParameters" /> class.
        /// </summary>
        /// <param name="traceComponent">
        ///     The traceable component.
        /// </param>
        public ComponentTracerParameters(string traceComponent)
        {
            TraceComponent = traceComponent;

            PrefixFunc = (o) => o.GetType().Name;
            PrefixSource = null;

            HashFunc = (o) => o.GetHashCode().ToString();
            HashSource = null;
        }

        /// <summary>
        ///     The traceable component.
        /// </summary>
        public string TraceComponent { get; set; }

        /// <summary>
        ///     Delegate for calculating the message prefix.
        /// </summary>
        /// <remarks>
        ///     By default:
        ///     <code>PrefixSource.GetType().Name</code>
        /// </remarks>
        public Func<object, string>? PrefixFunc { get; set; }

        /// <summary>
        ///     The object for calculating the message prefix.
        /// </summary>
        public object? PrefixSource { get; set; }

        /// <summary>
        ///     The delegate of the calculation is the hash code of the traceable component.
        /// </summary>
        /// <remarks>
        ///     By default:
        ///     <code>HashSource.GetHashCode().ToString()</code>
        /// </remarks>
        public Func<object, string>? HashFunc { get; set; }

        /// <summary>
        ///     The object of calculating the hash code of the traceable component.
        /// </summary>
        public object? HashSource { get; set; }
    }
}
