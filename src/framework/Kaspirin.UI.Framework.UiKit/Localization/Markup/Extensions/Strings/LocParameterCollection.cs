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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Localization.Markup.Extensions.Strings
{
    public class LocParameterCollection : Collection<LocParameter>
    {
        public LocParameterCollection()
        {
        }

        public LocParameterCollection(IDictionary<string, string> constantParameters)
        {
            Guard.ArgumentIsNotNull(constantParameters);

            foreach (var constantParameter in constantParameters)
            {
                var parameter = new LocParameter
                {
                    ParamName = constantParameter.Key,
                    ParamSource = new Binding
                    {
                        Source = constantParameter.Value
                    }
                };

                Add(parameter);
            }
        }

        protected override void InsertItem(int index, LocParameter item)
        {
            Guard.ArgumentIsNotNull(item);
            base.InsertItem(index, item);
        }

        protected override void SetItem(int index, LocParameter item)
        {
            Guard.ArgumentIsNotNull(item);
            base.SetItem(index, item);
        }

        public bool IgnoreUnsetParameters { get; set; }
    }
}
