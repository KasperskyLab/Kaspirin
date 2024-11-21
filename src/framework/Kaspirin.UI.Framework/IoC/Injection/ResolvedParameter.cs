// Copyright © 2024 AO Kaspersky Lab.
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

#pragma warning disable KCAIDE0006 // Class should be abstract or sealed

using System;

namespace Kaspirin.UI.Framework.IoC.Injection
{
    /// <summary>
    ///     It is used to provide information about the constructor parameter of the class registered in the IoC container.
    /// </summary>
    public class ResolvedParameter
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ResolvedParameter" /> class.
        /// </summary>
        /// <param name="parameterType">
        ///     The type of the parameter.
        /// </param>
        /// <param name="name">
        ///     The name of the parameter.
        /// </param>
        public ResolvedParameter(Type parameterType, string? name = null)
        {
            ParameterType = Guard.EnsureArgumentIsNotNull(parameterType);
            Name = name;
        }

        /// <summary>
        ///     The type of the parameter.
        /// </summary>
        public Type ParameterType { get; }

        /// <summary>
        ///     The name of the parameter.
        /// </summary>
        public string? Name { get; }
    }
}
