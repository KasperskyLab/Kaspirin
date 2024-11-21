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

using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.MarkupExtensions.DataBinding
{
    public class CompactMultiBindingExtension : MultiBinding
    {
        public BindingBase Binding1
        {
            set
            {
                if (Bindings.Count > 0)
                {
                    Bindings[0] = value;
                }
                else
                {
                    Bindings.Add(value);
                }
            }
        }

        public BindingBase Binding2
        {
            set
            {
                if (Bindings.Count > 1)
                {
                    Bindings[1] = value;
                }
                else
                {
                    Bindings.Add(value);
                }
            }
        }

        public BindingBase Binding3
        {
            set
            {
                if (Bindings.Count > 2)
                {
                    Bindings[2] = value;
                }
                else
                {
                    Bindings.Add(value);
                }
            }
        }

        public BindingBase Binding4
        {
            set
            {
                if (Bindings.Count > 3)
                {
                    Bindings[3] = value;
                }
                else
                {
                    Bindings.Add(value);
                }
            }
        }
    }
}
