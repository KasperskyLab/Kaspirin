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

using System;
using System.Windows;
using System.Windows.Forms;

namespace Kaspirin.UI.Framework.UiKit.Interactivity.Actions
{
    public sealed class SaveFileAction : TriggerAction<FrameworkElement>
    {
        #region InitialDirectory

        public string InitialDirectory
        {
            get { return (string)GetValue(InitialDirectoryProperty); }
            set { SetValue(InitialDirectoryProperty, value); }
        }

        public static readonly DependencyProperty InitialDirectoryProperty =
            DependencyProperty.Register("InitialDirectory", typeof(string), typeof(SaveFileAction));

        #endregion

        #region FileName

        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileName", typeof(string), typeof(SaveFileAction));

        #endregion

        #region DefaultExt

        public string DefaultExt
        {
            get { return (string)GetValue(DefaultExtProperty); }
            set { SetValue(DefaultExtProperty, value); }
        }

        public static readonly DependencyProperty DefaultExtProperty =
            DependencyProperty.Register("DefaultExt", typeof(string), typeof(SaveFileAction));

        #endregion

        #region Filter

        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(string), typeof(SaveFileAction));

        #endregion

        #region Title

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(SaveFileAction));

        #endregion

        protected override void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestedEventArgs;
            if (args == null)
            {
                return;
            }

            var pathObject = (FileSystemPathObject)args.InteractionObject;

            ServiceLocator.Instance.GetService<INativeDialogLauncher>().ShowDialog(
                    createDialog: () =>
                    {
                        var dialog = new SaveFileDialog
                        {
                            InitialDirectory = InitialDirectory ?? string.Empty,
                            FileName = FileName ?? string.Empty,
                            DefaultExt = DefaultExt ?? string.Empty,
                            Title = Title ?? string.Empty
                        };

                        try
                        {
                            dialog.Filter = Filter ?? string.Empty;
                        }
                        catch (ArgumentException)
                        {
                            _trace.TraceError($"{nameof(SaveFileAction)} try to set invalid Filter={Filter}");
                            dialog.Filter = string.Empty;
                        }

                        return dialog;
                    },
                    showDialog: dialog =>
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            pathObject.Path = dialog.FileName;
                            pathObject.ObjectType = FileSystemPathObjectType.File;
                            pathObject.IsConfirmed = true;
                        }
                    });

            pathObject.Handle();
        }

        private static readonly ComponentTracer _trace = ComponentTracer.Get(UIKitComponentTracers.Interactivity);
    }
}
