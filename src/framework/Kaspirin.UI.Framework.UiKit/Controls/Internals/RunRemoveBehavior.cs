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

using System.Windows.Documents;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal sealed class RunRemoveBehavior : Behavior<Run, RunRemoveBehavior>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            Guard.IsNotNull(AssociatedObject);

            if (!string.IsNullOrEmpty(AssociatedObject.Text))
            {
                return;
            }

            var hasBinding = BindingOperations.GetBindingBase(AssociatedObject, Run.TextProperty) != null;
            if (hasBinding)
            {
                return;
            }

            var root = AssociatedObject.FindParentTextBlock();
            if (root == null)
            {
                return;
            }

            var isComplexTextBlock = string.IsNullOrEmpty(root.Text);
            if (!isComplexTextBlock)
            {
                return;
            }

            root.Inlines.Remove(AssociatedObject);
        }
    }
}
