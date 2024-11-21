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

namespace Kaspirin.UI.Framework.IoC.Injection
{
    /// <summary>
    ///     Sets default values for constructor arguments when creating an object. It is used when registering in the container.
    /// </summary>
    /// <remarks>
    ///     It is suitable for cases when you need to use a certain value for a certain argument, rather
    ///     than getting it from a container.
    /// </remarks>
    public sealed class InjectionConstructor : InjectionMember
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InjectionConstructor" /> class.
        /// </summary>
        /// <param name="resolvedParameters">
        ///     An array of arguments for the implementation constructor, specified in the form <see cref="ResolvedParameter" />.
        /// </param>
        public InjectionConstructor(params ResolvedParameter[] resolvedParameters)
        {
            ResolvedParameters = Guard.EnsureArgumentIsNotNull(resolvedParameters);
        }

        /// <summary>
        ///     An array of arguments for the implementation constructor.
        /// </summary>
        public ResolvedParameter[] ResolvedParameters { get; }
    }
}
