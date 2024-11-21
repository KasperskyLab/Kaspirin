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

using System;

namespace Kaspirin.UI.Framework.UiKit.Input.Filter
{
    public sealed class DelegateInputFilter : IInputFilter
    {
        public DelegateInputFilter(Func<string, string?> filterFunc)
        {
            _filterFunc = Guard.EnsureArgumentIsNotNull(filterFunc);
        }

        public string? FilterInput(string? input)
        {
            if (input is null)
            {
                return null;
            }

            return _filterFunc(input);
        }

        private readonly Func<string, string?> _filterFunc;
    }
}
