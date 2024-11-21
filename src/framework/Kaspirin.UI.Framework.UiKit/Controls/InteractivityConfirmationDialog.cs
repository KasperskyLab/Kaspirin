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

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class InteractivityConfirmationDialog : InteractivityDialog
    {
        #region ConfirmButtonCaption

        public string ConfirmButtonCaption
        {
            get => (string)GetValue(ConfirmButtonCaptionProperty);
            set => SetValue(ConfirmButtonCaptionProperty, value);
        }

        public static readonly DependencyProperty ConfirmButtonCaptionProperty = DependencyProperty.Register(
            nameof(ConfirmButtonCaption),
            typeof(string),
            typeof(InteractivityConfirmationDialog),
            new PropertyMetadata(default(string)));

        #endregion

        #region ConfirmButtonCommand

        public ICommand ConfirmButtonCommand
        {
            get => (ICommand)GetValue(ConfirmButtonCommandProperty);
            set => SetValue(ConfirmButtonCommandProperty, value);
        }

        public static readonly DependencyProperty ConfirmButtonCommandProperty = DependencyProperty.Register(
            nameof(ConfirmButtonCommand),
            typeof(ICommand),
            typeof(InteractivityConfirmationDialog),
            new PropertyMetadata(default(ICommand)));

        #endregion

        #region CancelButtonCaption

        public string CancelButtonCaption
        {
            get => (string)GetValue(CancelButtonCaptionProperty);
            set => SetValue(CancelButtonCaptionProperty, value);
        }
        public static readonly DependencyProperty CancelButtonCaptionProperty = DependencyProperty.Register(
             nameof(CancelButtonCaption),
            typeof(string),
            typeof(InteractivityConfirmationDialog),
            new PropertyMetadata(default(string)));

        #endregion

        #region CancelButtonCommand

        public ICommand CancelButtonCommand
        {
            get => (ICommand)GetValue(CancelButtonCommandProperty);
            set => SetValue(CancelButtonCommandProperty, value);
        }

        public static readonly DependencyProperty CancelButtonCommandProperty = DependencyProperty.Register(
            nameof(CancelButtonCommand),
            typeof(ICommand),
            typeof(InteractivityConfirmationDialog),
            new PropertyMetadata(default(ICommand)));

        #endregion
    }
}
