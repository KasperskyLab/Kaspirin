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
    public class ChooseFileAction : TriggerAction<FrameworkElement>
    {
        #region InitialDirectory

        public string InitialDirectory
        {
            get { return (string)GetValue(InitialDirectoryProperty); }
            set { SetValue(InitialDirectoryProperty, value); }
        }

        public static readonly DependencyProperty InitialDirectoryProperty =
            DependencyProperty.Register("InitialDirectory", typeof(string), typeof(ChooseFileAction));

        #endregion

        #region Multiselect

        public bool Multiselect
        {
            get { return (bool)GetValue(MultiselectProperty); }
            set { SetValue(MultiselectProperty, value); }
        }

        public static readonly DependencyProperty MultiselectProperty =
            DependencyProperty.Register("Multiselect", typeof(bool), typeof(ChooseFileAction));

        #endregion

        #region CheckFileExists

        public bool CheckFileExists
        {
            get { return (bool)GetValue(CheckFileExistsProperty); }
            set { SetValue(CheckFileExistsProperty, value); }
        }

        public static readonly DependencyProperty CheckFileExistsProperty =
            DependencyProperty.Register("CheckFileExists", typeof(bool), typeof(ChooseFileAction));

        #endregion

        #region Filter

        public string Filter
        {
            get { return (string)GetValue(FilterProperty); }
            set { SetValue(FilterProperty, value); }
        }

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register("Filter", typeof(string), typeof(ChooseFileAction));

        #endregion

        #region Title

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ChooseFileAction));

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
                        var dialog = new OpenFileDialog
                        {
                            Multiselect = Multiselect,
                            CheckFileExists = CheckFileExists,
                            InitialDirectory = InitialDirectory ?? string.Empty,
                            Title = Title ?? string.Empty
                        };

                        try
                        {
                            dialog.Filter = Filter ?? string.Empty;
                        }
                        catch (ArgumentException)
                        {
                            _trace.TraceError($"{nameof(ChooseFileAction)} try to set invalid Filter={Filter}");
                            dialog.Filter = string.Empty;
                        }

                        return dialog;
                    },
                    showDialog: dialog =>
                    {
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            if (dialog.Multiselect)
                            {
                                pathObject.Paths = dialog.FileNames;
                            }
                            else
                            {
                                pathObject.Path = dialog.FileName;
                            }

                            pathObject.ObjectType = FileSystemPathObjectType.File;
                            pathObject.IsConfirmed = true;
                        }
                    });

            pathObject.Handle();
        }

        private static readonly ComponentTracer _trace = ComponentTracer.Get(UIKitComponentTracers.Interactivity);
    }
}
