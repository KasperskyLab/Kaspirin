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
    ///     It is used to provide information about the constructor parameter of the class registered in the IoC container.
    /// </summary>
    /// <typeparam name="TParameter">
    ///     The type of the parameter.
    /// </typeparam>
    public sealed class ResolvedParameter<TParameter> : ResolvedParameter
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ResolvedParameter{TParameter}" /> class.
        /// </summary>
        public ResolvedParameter()
            : base(typeof(TParameter))
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResolvedParameter{TParameter}" /> class.
        /// </summary>
        /// <param name="name">
        ///     The name of the parameter.
        /// </param>
        public ResolvedParameter(string name)
            : base(typeof(TParameter), name)
        {
        }
    }
}
