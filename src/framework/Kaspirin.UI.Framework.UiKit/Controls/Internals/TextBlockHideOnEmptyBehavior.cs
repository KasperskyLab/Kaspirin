// Copyright © 2026 AO Kaspersky Lab.
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

using System.Windows.Controls;
using System.Windows.Data;
using Kaspirin.UI.Framework.UiKit.Converters.StringConverters;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals;

internal sealed class TextBlockHideOnEmptyBehavior : Behavior<TextBlock, TextBlockHideOnEmptyBehavior>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        Guard.IsNotNull(AssociatedObject);

        BindingOperations.SetBinding(AssociatedObject, TextBlock.VisibilityProperty, new Binding()
        {
            Source = AssociatedObject,
            Path = TextBlock.TextProperty.AsPath(),
            Converter = new StringToVisibilityConverter()
        });
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        Guard.IsNotNull(AssociatedObject);

        BindingOperations.ClearBinding(AssociatedObject, TextBlock.VisibilityProperty);
    }
}
