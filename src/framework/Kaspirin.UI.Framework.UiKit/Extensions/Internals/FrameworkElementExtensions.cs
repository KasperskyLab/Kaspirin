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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Threading;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Extensions.Internals
{
    internal static class FrameworkElementExtensions
    {
        public static void LinkTextStyle(this FrameworkElement target, TextBlock source)
        {
            target.SetBinding(TextBlock.LineHeightProperty, new Binding() { Source = source, Path = TextBlock.LineHeightProperty.AsPath() });
            target.SetBinding(TextBlock.LineStackingStrategyProperty, new Binding() { Source = source, Path = TextBlock.LineStackingStrategyProperty.AsPath() });
            target.SetBinding(TextBlock.FontFamilyProperty, new Binding() { Source = source, Path = TextBlock.FontFamilyProperty.AsPath() });
            target.SetBinding(TextBlock.FontSizeProperty, new Binding() { Source = source, Path = TextBlock.FontSizeProperty.AsPath() });
            target.SetBinding(TextBlock.FontStyleProperty, new Binding() { Source = source, Path = TextBlock.FontStyleProperty.AsPath() });
            target.SetBinding(TextBlock.FontWeightProperty, new Binding() { Source = source, Path = TextBlock.FontWeightProperty.AsPath() });
            target.SetBinding(TextBlock.ForegroundProperty, new Binding() { Source = source, Path = TextBlock.ForegroundProperty.AsPath() });
        }

        public static bool CoerceWhenLoaded(this FrameworkElement target, DependencyProperty valueProperty)
        {
            if (target.IsLoaded is false)
            {
                target.WhenLoaded(() => Executers.InUiAsync(() => target.CoerceValue(valueProperty), DispatcherPriority.Send));
                return false;
            }

            return true;
        }
    }
}
