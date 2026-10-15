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

using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals;

internal sealed class ButtonBaseAcceleratorKeyBehavior : Behavior<ButtonBase, ButtonBaseAcceleratorKeyBehavior>
{
    protected override void OnAttached()
    {
        base.OnAttached();

        Guard.IsNotNull(AssociatedObject);

        if (AssociatedObject is not Button button)
        {
            return;
        }

        button.SetBinding(AutomationProperties.AccessKeyProperty, new Binding()
        {
            Source = button,
            Path = AutomationProperties.AcceleratorKeyProperty.AsPath(),
        });
        button.SetBinding(AutomationProperties.AcceleratorKeyProperty, new MultiBinding()
        {
            Bindings =
            {
                new Binding() { Path = Button.IsDefaultProperty.AsPath(), Source = button },
                new Binding() { Path = Button.IsCancelProperty.AsPath(), Source = button },
            },
            Converter = new DelegateMultiConverter(values =>
            {
                var isDefault = (bool)values[0]!;
                var isCancel = (bool)values[1]!;

                if (isDefault || isCancel)
                {
                    var localizer = LocalizationManager.GetLocalizer<IStringLocalizer>(UIKitConstants.LocalizationScope);

                    if (isDefault && isCancel)
                    {
                        var enterText = localizer.GetString(EnterKey);
                        var escapeText = localizer.GetString(EscapeKey);

                        return $"{enterText}, {escapeText}";
                    }

                    if (isDefault)
                    {
                        return localizer.GetString(EnterKey);
                    }

                    if (isCancel)
                    {
                        return localizer.GetString(EscapeKey);
                    }
                }

                return string.Empty;
            })
        });
    }

    private const string EnterKey = "Button_AcceleratorKeyEnter";
    private const string EscapeKey = "Button_AcceleratorKeyEscape";
}