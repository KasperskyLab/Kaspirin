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

#pragma warning disable CA1724 // Change either name to eliminate the conflict.

using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Kaspirin.UI.Framework.UiKit.Controls.Automation;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

[TemplatePart(Name = PART_ClearButton, Type = typeof(ButtonBase))]
[TemplatePart(Name = PART_SearchInput, Type = typeof(SearchInput))]
public sealed class Search : Control
{
    public const string PART_ClearButton = "PART_ClearButton";
    public const string PART_SearchInput = "PART_SearchInput";

    public Search()
    {
        MouseLeftButtonDown += (o, e) =>
        {
            SetFocus();

            e.Handled = true;
        };

        Loaded += (o, e) =>
        {
            AddFindEventHandler();

            ClearText();

            if (SetFocusFlag)
            {
                SetFocus();
            }

            UpdateIsEmpty();
            UpdateShortcutDisplayText();
        };

        Unloaded += (o, e) =>
        {
            RemoveFindEventHandler();
        };
    }

    #region Text

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text),
        typeof(string),
        typeof(Search),
        new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnTextChanged, CoerceText)
        {
            DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
        });

    private static object CoerceText(DependencyObject d, object baseValue)
        => baseValue ?? string.Empty;

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((Search)d).UpdateIsEmpty();

    #endregion

    #region MaxLength

    public int MaxLength
    {
        get => (int)GetValue(MaxLengthProperty);
        set => SetValue(MaxLengthProperty, value);
    }

    public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.Register(
        nameof(MaxLength),
        typeof(int),
        typeof(Search),
        new FrameworkPropertyMetadata(default(int)), new ValidateValueCallback(MaxLengthValidateValue));

    private static bool MaxLengthValidateValue(object value)
        => (int)value >= 0;

    #endregion

    #region SetFocusFlag

    public bool SetFocusFlag
    {
        get => (bool)GetValue(SetFocusFlagProperty);
        set => SetValue(SetFocusFlagProperty, value);
    }

    public static readonly DependencyProperty SetFocusFlagProperty = DependencyProperty.Register(
        nameof(SetFocusFlag),
        typeof(bool),
        typeof(Search),
        new FrameworkPropertyMetadata(default(bool), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSetFocusFlagChanged));

    private static void OnSetFocusFlagChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var search = (Search)d;
        if (search.SetFocusFlag)
        {
            search.SetFocus();

            Executers.InUiAsync(() => search.SetCurrentValue(SetFocusFlagProperty, false));
        }
    }

    #endregion

    #region GetFocusBehavior

    public InputGetFocusBehaviorType GetFocusBehavior
    {
        get => (InputGetFocusBehaviorType)GetValue(GetFocusBehaviorProperty);
        set => SetValue(GetFocusBehaviorProperty, value);
    }

    public static readonly DependencyProperty GetFocusBehaviorProperty = DependencyProperty.Register(
        nameof(GetFocusBehavior),
        typeof(InputGetFocusBehaviorType),
        typeof(Search),
        new PropertyMetadata(InputGetFocusBehaviorType.Default));

    #endregion

    #region LostFocusBehavior

    public InputLostFocusBehaviorType LostFocusBehavior
    {
        get => (InputLostFocusBehaviorType)GetValue(LostFocusBehaviorProperty);
        set => SetValue(LostFocusBehaviorProperty, value);
    }

    public static readonly DependencyProperty LostFocusBehaviorProperty = DependencyProperty.Register(
        nameof(LostFocusBehavior),
        typeof(InputLostFocusBehaviorType),
        typeof(Search),
        new PropertyMetadata(InputLostFocusBehaviorType.Default));

    #endregion

    #region Placeholder

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public static readonly DependencyProperty PlaceholderProperty = DependencyProperty.Register(
        nameof(Placeholder),
        typeof(string),
        typeof(Search),
        new PropertyMetadata(default(string)));

    #endregion

    #region IsBusy

    public bool IsBusy
    {
        get => (bool)GetValue(IsBusyProperty);
        set => SetValue(IsBusyProperty, value);
    }

    public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(
        nameof(IsBusy),
        typeof(bool),
        typeof(Search),
        new PropertyMetadata(default(bool)));

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
        typeof(Search),
        new PropertyMetadata(true));

    public static readonly DependencyProperty IsEmptyProperty = _isEmptyPropertyKey.DependencyProperty;

    #endregion

    #region Shortcuts

    public ShortcutCollection Shortcuts
    {
        get => (ShortcutCollection)GetValue(ShortcutProperty);
        set => SetValue(ShortcutProperty, value);
    }

    public static readonly DependencyProperty ShortcutProperty = DependencyProperty.Register(
        nameof(Shortcuts),
        typeof(ShortcutCollection),
        typeof(Search),
        new PropertyMetadata(new ShortcutCollection { Guard.EnsureIsNotNull(WindowShortcuts.Search.Shortcut) }, OnShortcutsChanged));

    private static void OnShortcutsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((Search)d).UpdateShortcutDisplayText();

    #endregion

    #region ShowShortcut

    public bool ShowShortcut
    {
        get => (bool)GetValue(ShowShortcutProperty);
        set => SetValue(ShowShortcutProperty, value);
    }

    public static readonly DependencyProperty ShowShortcutProperty = DependencyProperty.Register(
        nameof(ShowShortcut),
        typeof(bool),
        typeof(Search),
        new PropertyMetadata(true, OnShowShortcutChanged));

    private static void OnShowShortcutChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((Search)d).UpdateShortcutDisplayText();

    #endregion

    #region ShortcutDisplayText

    public string? ShortcutDisplayText
    {
        get => (string?)GetValue(ShortcutDisplayTextProperty);
        private set => SetValue(_shortcutDisplayTextPropertyKey, value);
    }

    private static readonly DependencyPropertyKey _shortcutDisplayTextPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(ShortcutDisplayText),
        typeof(string),
        typeof(Search),
        new PropertyMetadata(default(string)));

    public static readonly DependencyProperty ShortcutDisplayTextProperty = _shortcutDisplayTextPropertyKey.DependencyProperty;

    #endregion

    public override void OnApplyTemplate()
    {
        _textInput = (SearchInput)GetTemplateChild(PART_SearchInput);
        _textInput.SetBinding(SearchInput.TextProperty, new Binding() { Source = this, Path = TextProperty.AsPath(), UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
        _textInput.SetBinding(SearchInput.MaxLengthProperty, new Binding() { Source = this, Path = MaxLengthProperty.AsPath() });
        _textInput.SetBinding(SearchInput.GetFocusBehaviorProperty, new Binding() { Source = this, Path = GetFocusBehaviorProperty.AsPath() });
        _textInput.SetBinding(SearchInput.LostFocusBehaviorProperty, new Binding() { Source = this, Path = LostFocusBehaviorProperty.AsPath() });

        var placeholderBinding = new MultiBinding() { Converter = new PlaceholderWithShortcutConverter() };
        placeholderBinding.Bindings.Add(new Binding() { Source = this, Path = PlaceholderProperty.AsPath() });
        placeholderBinding.Bindings.Add(new Binding() { Source = this, Path = _shortcutDisplayTextPropertyKey.AsPath() });
        _textInput.SetBinding(SearchInput.PlaceholderProperty, placeholderBinding);

        _textInput.WhenLoaded(() => _textInput.InputBindings.AddRange(InputBindings));

        _clearButton = (ButtonBase)GetTemplateChild(PART_ClearButton);
        _clearButton.GotKeyboardFocus += OnClearButtonGetKeyboardFocus;
        _clearButton.LostKeyboardFocus += OnClearButtonLostKeyboardFocus;
        _clearButton.Click += OnClearButtonClick;
        _clearButton.Focusable = false;

        GotFocus += OnGotFocus;
        LostFocus += OnLostFocus;
    }

    protected override AutomationPeer OnCreateAutomationPeer()
    {
        return new SearchAutomationPeer(this);
    }

    private void SetFocus()
    {
        var isUnloaded = false;

        this.WhenUnloaded(() => isUnloaded = true);

        Executers.InUiAsync(() =>
        {
            if (isUnloaded)
            {
                return;
            }

            if (_textInput == null)
            {
                return;
            }

            BringIntoView();

            InputFocusManager.SetInputFocus(_textInput);
        },
        DispatcherPriority.Input);
    }

    private void SetFocusFromShortcut(object sender, RoutedEventArgs e)
    {
        if (Shortcuts?.Any() != true)
        {
            return;
        }

        if (e is WindowShortcutEventArgs args && Shortcuts.Any(shortcut => shortcut.IsEquivalentTo(args.KeyGesture)))
        {
            if (_textInput != null && !_textInput.IsFocused)
            {
                e.Handled = InputFocusManager.SetInputFocus(_textInput);
            }
        }
    }

    private void ClearText()
    {
        if (_textInput != null)
        {
            _textInput.Text = string.Empty;
        }
    }

    private void OnLostFocus(object sender, RoutedEventArgs e)
        => AccessKeyManager.Unregister(EscCode, _clearButton);

    private void OnGotFocus(object sender, RoutedEventArgs e)
        => AccessKeyManager.Register(EscCode, _clearButton);

    private void OnClearButtonClick(object sender, RoutedEventArgs e)
    {
        ClearText();

        if (_setFocusOnClear)
        {
            SetFocus();
        }
    }

    private void OnClearButtonGetKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        if (e.OldFocus == _textInput)
        {
            _setFocusOnClear = true;
        }
    }

    private void OnClearButtonLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        => _setFocusOnClear = false;

    private void UpdateIsEmpty()
        => IsEmpty = string.IsNullOrEmpty(Text);

    private void UpdateShortcutDisplayText()
        => ShortcutDisplayText = ShowShortcut && Shortcuts?.Any() == true
            ? Shortcuts.First().ToKeyGesture().GetDisplayStringForCulture(LocalizationManager.DisplayCulture.CultureInfo)
            : null;

    private void AddFindEventHandler()
    {
        var parentWindow = this.GetWindow();
        if (parentWindow != null)
        {
            parentWindow.AddHandler(WindowShortcutBehavior.ShortcutEvent, (RoutedEventHandler)SetFocusFromShortcut);

            _parentWindowRef = new WeakReference(parentWindow);
        }
    }

    private void RemoveFindEventHandler()
    {
        if (_parentWindowRef == null || _parentWindowRef.IsAlive == false)
        {
            return;
        }

        if (_parentWindowRef.Target is Window parentWindow)
        {
            parentWindow.RemoveHandler(WindowShortcutBehavior.ShortcutEvent, (RoutedEventHandler)SetFocusFromShortcut);
        }
    }

    private sealed class PlaceholderWithShortcutConverter : IMultiValueConverter
    {
        public object? Convert(object?[] values, Type targetType, object? parameter, CultureInfo culture)
        {
            Guard.ArgumentIsNotNull(values);
            Guard.Assert(values.Length == 2);

            Guard.IsNotNull(_placeholderTextParameter.ParamSource);
            Guard.IsNotNull(_shortcutParameter.ParamSource);

            var placeholderText = (string?)values[0] ?? string.Empty;
            var shortcut = (string?)values[1] ?? string.Empty;

            var isPlaceholderTextSet = !string.IsNullOrEmpty(placeholderText);
            var isShortcutSet = !string.IsNullOrEmpty(shortcut);

            if (isPlaceholderTextSet)
            {
                if (isShortcutSet)
                {
                    _placeholderTextParameter.ParamSource.Source = placeholderText;
                    _shortcutParameter.ParamSource.Source = shortcut;

                    return new TextInputLocPlaceholder(
                        new LocExtension()
                        {
                            Key = "Search_Pattern_PlaceholderTextAndShortcut",
                            Scope = UIKitConstants.LocalizationScope,
                            Params = new()
                            {
                                _placeholderTextParameter,
                                _shortcutParameter
                            }
                        });
                }
                else
                {
                    _placeholderTextParameter.ParamSource.Source = placeholderText;

                    return new TextInputLocPlaceholder(
                        new LocExtension()
                        {
                            Key = "Search_Pattern_PlaceholderTextOnly",
                            Scope = UIKitConstants.LocalizationScope,
                            Param = _placeholderTextParameter
                        });
                }
            }
            else
            {
                if (isShortcutSet)
                {
                    _shortcutParameter.ParamSource.Source = shortcut;

                    return new TextInputLocPlaceholder(
                        new LocExtension()
                        {
                            Key = "Search_Pattern_ShortcutOnly",
                            Scope = UIKitConstants.LocalizationScope,
                            Param = _shortcutParameter
                        });
                }
                else
                {
                    return new TextInputStringPlaceholder(string.Empty);
                }
            }
        }

        public object?[] ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();

        private readonly LocParameter _placeholderTextParameter = new()
        {
            ParamName = "PlaceholderText",
            ParamSource = new Binding()
        };

        private readonly LocParameter _shortcutParameter = new()
        {
            ParamName = "Shortcut",
            ParamSource = new Binding()
        };
    }

    private const string EscCode = "\x001B";

    private ButtonBase? _clearButton;
    private SearchInput? _textInput;
    private WeakReference? _parentWindowRef;
    private bool _setFocusOnClear;
}
