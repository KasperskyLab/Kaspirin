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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using Kaspirin.UI.Framework.UiKit.Converters.BooleanConverters;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_ItemIcon, Type = typeof(Icon16))]
    [TemplatePart(Name = PART_ItemImage, Type = typeof(Image))]
    [TemplatePart(Name = PART_ItemHeader, Type = typeof(TextBlock))]
    [TemplatePart(Name = PART_TextInput, Type = typeof(TextInput))]
    [TemplatePart(Name = PART_Placeholder, Type = typeof(TextInput))]
    [TemplatePart(Name = PART_RightBar, Type = typeof(ContentPresenter))]
    internal sealed class SelectPresenter : ButtonBase
    {
        public const string PART_ItemIcon = "PART_ItemIcon";
        public const string PART_ItemImage = "PART_ItemImage";
        public const string PART_ItemHeader = "PART_ItemHeader";
        public const string PART_TextInput = "PART_TextInput";
        public const string PART_Placeholder = "PART_Placeholder";
        public const string PART_RightBar = "PART_RightBar";

        #region Icon

        public UIKitIcon_16 Icon
        {
            get => (UIKitIcon_16)GetValue(_iconProperty);
            private set => SetValue(_iconProperty, value);
        }

        private static readonly DependencyProperty _iconProperty = DependencyProperty.Register(
            nameof(Icon),
            typeof(UIKitIcon_16),
            typeof(SelectPresenter),
            new PropertyMetadata(default(UIKitIcon_16)));

        #endregion

        #region Image

        public ImageSource? Image
        {
            get => (ImageSource?)GetValue(_imageProperty);
            set => SetValue(_imageProperty, value);
        }

        private static readonly DependencyProperty _imageProperty = DependencyProperty.Register(
            nameof(Image),
            typeof(ImageSource),
            typeof(SelectPresenter),
            new PropertyMetadata(default(ImageSource?)));

        #endregion

        #region ImageFlowDirection

        public FlowDirection ImageFlowDirection
        {
            get => (FlowDirection)GetValue(_imageFlowDirectionProperty);
            set => SetValue(_imageFlowDirectionProperty, value);
        }

        private static readonly DependencyProperty _imageFlowDirectionProperty = DependencyProperty.Register(
            nameof(ImageFlowDirection),
            typeof(FlowDirection),
            typeof(SelectPresenter),
            new PropertyMetadata(default(FlowDirection)));

        #endregion

        #region Header

        public string Header
        {
            get => (string)GetValue(_headerProperty);
            private set => SetValue(_headerProperty, value);
        }

        private static readonly DependencyProperty _headerProperty = DependencyProperty.Register(
            nameof(Header),
            typeof(string),
            typeof(SelectPresenter),
            new PropertyMetadata(string.Empty));

        #endregion

        #region FilterText

        public string FilterText
        {
            get => (string)GetValue(_filterTextProperty);
            private set => SetValue(_filterTextProperty, value);
        }

        private static readonly DependencyProperty _filterTextProperty = DependencyProperty.Register(
            nameof(FilterText),
            typeof(string),
            typeof(SelectPresenter),
            new PropertyMetadata(string.Empty, OnFilterTextChanged));

        private static void OnFilterTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((SelectPresenter)d).OnFilterTextChanged();

        #endregion

        #region FilterTextChanged

        public event RoutedEventHandler FilterTextChanged
        {
            add => AddHandler(FilterTextChangedEvent, value);
            remove => RemoveHandler(FilterTextChangedEvent, value);
        }

        public static readonly RoutedEvent FilterTextChangedEvent = EventManager.RegisterRoutedEvent(
            nameof(FilterTextChanged),
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(SelectPresenter));

        #endregion

        #region InputFilter

        public IInputFilter? InputFilter
        {
            get => (IInputFilter?)GetValue(InputFilterProperty);
            set => SetValue(InputFilterProperty, value);
        }

        public static readonly DependencyProperty InputFilterProperty = DependencyProperty.Register(
            nameof(InputFilter),
            typeof(IInputFilter),
            typeof(SelectPresenter));

        #endregion

        #region IsActive

        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register(
            nameof(IsActive),
            typeof(bool),
            typeof(SelectPresenter),
            new PropertyMetadata(false, OnIsActiveChanged));

        private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((SelectPresenter)d).OnIsActiveChanged();

        #endregion

        #region IsEmpty

        public bool IsEmpty
        {
            get => (bool)GetValue(IsEmptyProperty);
            private set => SetValue(_isEmptyPropertyKey, value);
        }

        private static readonly DependencyPropertyKey _isEmptyPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(IsEmpty),
            typeof(bool),
            typeof(SelectPresenter),
            new PropertyMetadata(default(bool)));

        public static readonly DependencyProperty IsEmptyProperty = _isEmptyPropertyKey.DependencyProperty;

        #endregion

        #region IsFilterEnabled

        public bool IsFilterEnabled
        {
            get => (bool)GetValue(IsFilterEnabledProperty);
            set => SetValue(IsFilterEnabledProperty, value);
        }

        public static readonly DependencyProperty IsFilterEnabledProperty = DependencyProperty.Register(
            nameof(IsFilterEnabled),
            typeof(bool),
            typeof(SelectPresenter),
            new PropertyMetadata(default(bool)));

        #endregion

        #region IsInvalidState

        public bool IsInvalidState
        {
            get => (bool)GetValue(IsInvalidStateProperty);
            set => SetValue(IsInvalidStateProperty, value);
        }

        public static readonly DependencyProperty IsInvalidStateProperty = DependencyProperty.Register(
            nameof(IsInvalidState),
            typeof(bool),
            typeof(SelectPresenter),
            new PropertyMetadata(default(bool)));

        #endregion

        #region HasRightBar

        public bool HasRightBar
        {
            get => (bool)GetValue(_hasRightBarPropertyKey.DependencyProperty);
            set => SetValue(_hasRightBarPropertyKey, value);
        }

        private static readonly DependencyPropertyKey _hasRightBarPropertyKey = DependencyProperty.RegisterReadOnly(
            nameof(HasRightBar),
            typeof(bool),
            typeof(SelectPresenter),
            new PropertyMetadata(false));

        public static readonly DependencyProperty HasRightBarProperty = _hasRightBarPropertyKey.DependencyProperty;

        #endregion

        public void UpdatePresenter(SelectItem? itemContainer, bool hasRightBar)
        {
            _isUpdating = true;

            try
            {
                _itemContainer = itemContainer;

                IsEmpty = _itemContainer == null;
                FilterText = _itemContainer?.Header ?? string.Empty;
                HasRightBar = hasRightBar;

                if (!IsEmpty)
                {
                    SetBinding(_iconProperty, new Binding() { Source = _itemContainer, Mode = BindingMode.OneWay, Path = SelectItem.IconProperty.AsPath() });
                    SetBinding(_imageProperty, new Binding() { Source = _itemContainer, Mode = BindingMode.OneWay, Path = SelectItem.ImageProperty.AsPath() });
                    SetBinding(_imageFlowDirectionProperty, new Binding() { Source = _itemContainer, Mode = BindingMode.OneWay, Path = SelectItem.ImageFlowDirectionProperty.AsPath() });
                    SetBinding(_headerProperty, new Binding() { Source = _itemContainer, Mode = BindingMode.OneWay, Path = SelectItem.HeaderProperty.AsPath() });
                }
                else
                {
                    ClearValue(_iconProperty);
                    ClearValue(_imageProperty);
                    ClearValue(_imageFlowDirectionProperty);
                    ClearValue(_headerProperty);
                }
            }
            finally
            {
                _isUpdating = false;
            }
        }

        public override void OnApplyTemplate()
        {
            _icon = (Icon16)GetTemplateChild(PART_ItemIcon);
            _icon.SetBinding(Icon16.IconProperty, new Binding() { Source = this, Path = _iconProperty.AsPath() });

            _image = (Image)GetTemplateChild(PART_ItemImage);
            _image.SetBinding(System.Windows.Controls.Image.SourceProperty, new Binding() { Source = this, Path = _imageProperty.AsPath() });
            _image.SetBinding(System.Windows.Controls.Image.FlowDirectionProperty, new Binding() { Source = this, Path = _imageFlowDirectionProperty.AsPath() });

            _header = (TextBlock)GetTemplateChild(PART_ItemHeader);
            _header.SetBinding(TextBlock.TextProperty, new Binding() { Source = this, Path = _headerProperty.AsPath() });

            _rightBar = (ContentPresenter)GetTemplateChild(PART_RightBar);
            _rightBar.SetBinding(ContentPresenter.ContentProperty, new Binding()
            {
                Path = Select.RightBarProperty.AsPath(),
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor) { AncestorType = typeof(Select) },
            });
            _rightBar.SetBinding(ContentPresenter.ContentTemplateProperty, new Binding()
            {
                Path = Select.RightBarTemplateProperty.AsPath(),
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor) { AncestorType = typeof(Select) },
            });
            _rightBar.SetBinding(VisibilityProperty, new Binding()
            {
                Source = this,
                Path = IsEmptyProperty.AsPath(),
                Converter = new BooleanToVisibilityInverseConverter()
            });

            _textInput = (TextInput)GetTemplateChild(PART_TextInput);
            _textInput.ContextMenuOpening += (o, e) => { e.Handled = true; };
            _textInput.SetBinding(Controls.TextInput.InputFilterProperty, new Binding()
            {
                Source = this,
                Path = InputFilterProperty.AsPath(),
            });
            _textInput.SetBinding(Controls.TextInput.TextProperty, new Binding()
            {
                Source = this,
                Path = _filterTextProperty.AsPath(),
            });
            _textInput.SetBinding(Controls.TextInput.PlaceholderProperty, new Binding()
            {
                Path = Select.PlaceholderProperty.AsPath(),
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor) { AncestorType = typeof(Select) },
                Converter = new DelegateConverter<string>(value => new TextInputStringPlaceholder(value ?? string.Empty))
            });

            _placeholder = (TextBlock)GetTemplateChild(PART_Placeholder);
            _placeholder.SetBinding(TextBlock.TextProperty, new Binding()
            {
                Path = Select.PlaceholderProperty.AsPath(),
                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor) { AncestorType = typeof(Select) },
            });
            _placeholder.SetBinding(TextOptions.TextFormattingModeProperty, new Binding()
            {
                Path = TextOptions.TextFormattingModeProperty.AsPath(),
                Source = _textInput,
            });
            _placeholder.SetBinding(VisibilityProperty, new Binding()
            {
                Source = this,
                Path = IsEmptyProperty.AsPath(),
                Converter = new Converters.BooleanConverters.BooleanToVisibilityConverter()
            });
        }

        private void OnIsActiveChanged()
        {
            if (IsFilterEnabled)
            {
                if (IsActive)
                {
                    if (_textInput != null)
                    {
                        _textInput.SelectAll();

                        Executers.InUiAsync(() => InputFocusManager.SetInputFocus(_textInput));
                    }
                }
                else
                {
                    Focus();
                }
            }
        }

        private void OnFilterTextChanged()
        {
            if (!_isUpdating)
            {
                RaiseEvent(new RoutedEventArgs(FilterTextChangedEvent));
            }
        }

        private Icon16? _icon;
        private Image? _image;
        private TextBlock? _header;
        private TextInput? _textInput;
        private TextBlock? _placeholder;
        private ContentPresenter? _rightBar;
        private SelectItem? _itemContainer;
        private bool _isUpdating;
    }
}