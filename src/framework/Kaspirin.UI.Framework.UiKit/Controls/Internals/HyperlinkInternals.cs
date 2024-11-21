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
using System.Windows.Documents;
using System.Windows.Input;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
    internal static class HyperlinkInternals
    {
        #region IsTouchBehaviorEnabled

        public static bool GetIsTouchBehaviorEnabled(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsTouchBehaviorEnabledProperty);
        }

        public static void SetIsTouchBehaviorEnabled(DependencyObject obj, bool value)
        {
            obj.SetValue(IsTouchBehaviorEnabledProperty, value);
        }

        public static readonly DependencyProperty IsTouchBehaviorEnabledProperty =
            DependencyProperty.RegisterAttached("IsTouchBehaviorEnabled", typeof(bool), typeof(HyperlinkInternals),
                UIKitPropertyMetadataFactory.CreatePropsMetadata(typeof(Hyperlink), nameof(IsTouchBehaviorEnabledProperty), OnValueChanged));

        #endregion

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var hyperlink = (Hyperlink)d;

            if (e.NewValue is true)
            {
                hyperlink.PreviewTouchDown -= OnPreviewTouchDown;
                hyperlink.PreviewTouchDown += OnPreviewTouchDown;

                hyperlink.PreviewMouseDown -= OnPreviewMouseDown;
                hyperlink.PreviewMouseDown += OnPreviewMouseDown;
            }
            else if (e.NewValue is false)
            {
                hyperlink.PreviewTouchDown -= OnPreviewTouchDown;
                hyperlink.PreviewMouseDown -= OnPreviewMouseDown;
            }
        }

        private static void OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.StylusDevice == null)
            {
                return;
            }

            var hyperlink = (Hyperlink)sender;

            ExecuteCommand(hyperlink, e);
        }

        private static void OnPreviewTouchDown(object? sender, TouchEventArgs e)
        {
            if (e.TouchDevice == null)
            {
                return;
            }

            var hyperlink = Guard.EnsureIsInstanceOfType<Hyperlink>(sender);

            ExecuteCommand(hyperlink, e);
        }

        private static void ExecuteCommand(Hyperlink hyperlink, RoutedEventArgs eventArgs)
        {
            var command = hyperlink.Command;
            var commandParameter = hyperlink.CommandParameter;

            if (command != null)
            {
                eventArgs.Handled = true;

                if (command.CanExecute(commandParameter))
                {
                    command.Execute(commandParameter);
                }
            }
        }
    }
}
