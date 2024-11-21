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

using System.Windows;
using System.Windows.Input;

namespace Kaspirin.UI.Framework.UiKit.Controls.Behaviors
{
    public sealed class WindowMaximizeOnDoubleClickBehavior : Behavior<UIElement, WindowMaximizeOnDoubleClickBehavior>
    {
        protected override void OnAttached()
        {
            Guard.IsNotNull(AssociatedObject);

            AssociatedObject.MouseLeftButtonDown += MaximizeWindow;
        }

        protected override void OnDetaching()
        {
            Guard.IsNotNull(AssociatedObject);

            AssociatedObject.MouseLeftButtonDown -= MaximizeWindow;
        }

        private void MaximizeWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                var currentWindow = AssociatedObject?.GetWindow();
                if (currentWindow?.ResizeMode == ResizeMode.CanResize)
                {
                    WindowCommand.MaximizeOrRestore.Execute(currentWindow);
                }
            }
        }
    }
}
