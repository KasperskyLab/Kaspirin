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

namespace Kaspirin.UI.Framework.UiKit.MarkupExtensions.DataBinding;

public sealed class CompactMultiBindingExtension : MultiBinding
{
    public BindingBase Binding1
    {
        set => SetBinding(index: 0, value);
    }

    public BindingBase Binding2
    {
        set => SetBinding(index: 1, value);
    }

    public BindingBase Binding3
    {
        set => SetBinding(index: 2, value);
    }

    public BindingBase Binding4
    {
        set => SetBinding(index: 3, value);
    }

    public BindingBase Binding5
    {
        set => SetBinding(index: 4, value);
    }

    private void SetBinding(int index, BindingBase binding)
    {
        if (Bindings.Count > index)
        {
            Bindings[index] = binding;
        }
        else
        {
            Bindings.Add(binding);
        }
    }
}
