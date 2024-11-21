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

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_CloseButton, Type = typeof(Button))]
    public class InteractivityDialog : ContentControl
    {
        public const string PART_CloseButton = "PART_CloseButton";

        public InteractivityDialog()
        {
            Loaded += OnLoaded;
        }

        public override void OnApplyTemplate()
        {
            _closeButton = GetTemplateChild(PART_CloseButton) as Button;

            SetCloseAction();
        }

        #region OverlayBehavior

        public InteractivityOverlayBehavior OverlayBehavior
        {
            get => (InteractivityOverlayBehavior)GetValue(OverlayBehaviorProperty);
            set => SetValue(OverlayBehaviorProperty, value);
        }

        public static readonly DependencyProperty OverlayBehaviorProperty = DependencyProperty.Register(
            nameof(OverlayBehavior),
            typeof(InteractivityOverlayBehavior),
            typeof(InteractivityDialog),
            new PropertyMetadata(InteractivityOverlayBehavior.DragWindow));

        #endregion

        #region OverlayCommand

        public ICommand OverlayCommand
        {
            get => (ICommand)GetValue(OverlayCommandProperty);
            set => SetValue(OverlayCommandProperty, value);
        }

        public static readonly DependencyProperty OverlayCommandProperty = DependencyProperty.Register(
            nameof(OverlayCommand),
            typeof(ICommand),
            typeof(InteractivityDialog),
            new PropertyMetadata(default(ICommand)));

        #endregion

        #region DialogSize

        public InteractivityDialogSize DialogSize
        {
            get => (InteractivityDialogSize)GetValue(DialogSizeProperty);
            set => SetValue(DialogSizeProperty, value);
        }

        public static readonly DependencyProperty DialogSizeProperty = DependencyProperty.Register(
            nameof(DialogSize),
            typeof(InteractivityDialogSize),
            typeof(InteractivityDialog),
            new PropertyMetadata(InteractivityDialogSize.Standard));

        #endregion

        #region DialogMargin

        public Thickness DialogMargin
        {
            get => (Thickness)GetValue(DialogMarginProperty);
            set => SetValue(DialogMarginProperty, value);
        }

        public static readonly DependencyProperty DialogMarginProperty = DependencyProperty.Register(
            nameof(DialogMargin),
            typeof(Thickness),
            typeof(InteractivityDialog),
            new PropertyMetadata(default(Thickness)));

        #endregion

        #region Icon

        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            nameof(Icon),
            typeof(ImageSource),
            typeof(InteractivityDialog),
            new PropertyMetadata(default(ImageSource)));

        #endregion

        #region Type

        public InteractivityDialogType Type
        {
            get => (InteractivityDialogType)GetValue(TypeProperty);
            set => SetValue(TypeProperty, value);
        }

        public static readonly DependencyProperty TypeProperty = DependencyProperty.Register(
            nameof(Type),
            typeof(InteractivityDialogType),
            typeof(InteractivityDialog),
            new PropertyMetadata(InteractivityDialogType.Neutral));

        #endregion

        #region Header

        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
            nameof(Header),
            typeof(object),
            typeof(InteractivityDialog),
            new PropertyMetadata(default(object)));

        #endregion

        #region SubHeader

        public object SubHeader
        {
            get => GetValue(SubHeaderProperty);
            set => SetValue(SubHeaderProperty, value);
        }

        public static readonly DependencyProperty SubHeaderProperty = DependencyProperty.Register(
            nameof(SubHeader),
            typeof(object),
            typeof(InteractivityDialog),
            new PropertyMetadata(default(object)));

        #endregion

        #region Description

        public object Description
        {
            get => GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
            nameof(Description),
            typeof(object),
            typeof(InteractivityDialog),
            new PropertyMetadata(default(object)));

        #endregion

        #region UseContentLeftPadding

        public bool UseContentLeftPadding
        {
            get => (bool)GetValue(UseContentLeftPaddingProperty);
            set => SetValue(UseContentLeftPaddingProperty, value);
        }

        public static readonly DependencyProperty UseContentLeftPaddingProperty = DependencyProperty.Register(
            nameof(UseContentLeftPadding),
            typeof(bool),
            typeof(InteractivityDialog),
            new PropertyMetadata(default(bool)));

        #endregion

        #region Footer

        public object Footer
        {
            get => GetValue(FooterProperty);
            set => SetValue(FooterProperty, value);
        }

        public static readonly DependencyProperty FooterProperty = DependencyProperty.Register(
            nameof(Footer),
            typeof(object),
            typeof(InteractivityDialog),
            new PropertyMetadata(default(object)));

        #endregion

        #region HasHelpButton

        public bool HasHelpButton
        {
            get => (bool)GetValue(HasHelpButtonProperty);
            set => SetValue(HasHelpButtonProperty, value);
        }

        public static readonly DependencyProperty HasHelpButtonProperty = DependencyProperty.Register(
            nameof(HasHelpButton),
            typeof(bool),
            typeof(InteractivityDialog),
            new PropertyMetadata(default(bool)));

        #endregion

        #region HelpButtonCommand

        public ICommand HelpButtonCommand
        {
            get => (ICommand)GetValue(HelpButtonCommandProperty);
            set => SetValue(HelpButtonCommandProperty, value);
        }

        public static readonly DependencyProperty HelpButtonCommandProperty = DependencyProperty.Register(
            nameof(HelpButtonCommand),
            typeof(ICommand),
            typeof(InteractivityDialog),
            new PropertyMetadata(default(ICommand)));

        #endregion

        #region HasCloseButton

        public bool HasCloseButton
        {
            get => (bool)GetValue(HasCloseButtonProperty);
            set => SetValue(HasCloseButtonProperty, value);
        }

        public static readonly DependencyProperty HasCloseButtonProperty = DependencyProperty.Register(
            nameof(HasCloseButton),
            typeof(bool),
            typeof(InteractivityDialog),
            new PropertyMetadata(default(bool), OnHasCloseButtonChanged));

        private static void OnHasCloseButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((InteractivityDialog)d).OnHasCloseButtonChanged();

        #endregion

        #region CloseButtonCommand

        public ICommand CloseButtonCommand
        {
            get => (ICommand)GetValue(CloseButtonCommandProperty);
            set => SetValue(CloseButtonCommandProperty, value);
        }

        public static readonly DependencyProperty CloseButtonCommandProperty = DependencyProperty.Register(
            nameof(CloseButtonCommand),
            typeof(ICommand),
            typeof(InteractivityDialog),
            new PropertyMetadata(default(ICommand)));

        #endregion

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            SetCloseAction();
        }

        private void OnHasCloseButtonChanged()
        {
            SetCloseAction();
        }

        private void SetCloseAction()
        {
            if (_closeButton == null)
            {
                return;
            }

            if (HasCloseButton)
            {
                _closeButton.IsCancel = true;

                SuppressCancelButtons();
            }
            else
            {
                _closeButton.IsCancel = false;

                RestoreCancelButtons();
            }
        }

        private void SuppressCancelButtons()
        {
            RestoreCancelButtons();

            _cancelButtons.Clear();
            _cancelButtons.AddRange(GetCancelButtons());
            _cancelButtons.ForEach(b => AccessKeyManager.Unregister(EscKey, b));
        }

        private void RestoreCancelButtons()
        {
            _cancelButtons.ForEach(b => AccessKeyManager.Register(EscKey, b));
        }

        private IEnumerable<Button> GetCancelButtons()
            => this.FindVisualChildren<Button>(b => b.IsCancel).Except(new[] { _closeButton! });

        private Button? _closeButton;

        private readonly List<Button> _cancelButtons = new();

        private const string EscKey = "\u001b";
    }
}
