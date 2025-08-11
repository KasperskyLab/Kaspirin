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
using System.Windows.Controls;

namespace Kaspirin.UI.Framework.UiKit.Controls;

[TemplatePart(Name = PART_ScrollViewer, Type = typeof(ScrollViewer))]
public sealed class TextViewer : Control
{
    public const string PART_ScrollViewer = "PART_ScrollViewer";

    #region Text

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text),
        typeof(string),
        typeof(TextViewer),
        new PropertyMetadata(default(string), OnTextChanged));

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        => ((TextViewer)d).OnTextChanged();

    #endregion

    #region TextChangedBehavior

    public TextViewerTextChangedBehavior TextChangedBehavior
    {
        get => (TextViewerTextChangedBehavior)GetValue(TextChangedBehaviorProperty);
        set => SetValue(TextChangedBehaviorProperty, value);
    }

    public static readonly DependencyProperty TextChangedBehaviorProperty = DependencyProperty.Register(
        nameof(TextChangedBehavior),
        typeof(TextViewerTextChangedBehavior),
        typeof(TextViewer),
        new PropertyMetadata(TextViewerTextChangedBehavior.ScrollOnTop));

    #endregion

    public event Action TextChanged = () => { };

    public event Action TextViewed = () => { };

    public override void OnApplyTemplate()
    {
        _scrollViewer = Guard.EnsureIsInstanceOfType<ScrollViewer>(GetTemplateChild(PART_ScrollViewer));
        _scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
    }

    public void PageDown()
        => _scrollViewer?.PageDown();

    public void PageUp()
        => _scrollViewer?.PageUp();

    public void ScrollToHome()
        => _scrollViewer?.ScrollToHome();

    public void ScrollToBottom()
        => _scrollViewer?.ScrollToBottom();

    public void ScrollToOffset(double offset)
        => _scrollViewer?.ScrollToVerticalOffset(offset);

    private void OnTextChanged()
    {
        _textStartViewed = false;
        _textEndViewed = false;
        _textViewedEventSent = false;

        if (TextChangedBehavior == TextViewerTextChangedBehavior.ScrollOnTop)
        {
            ScrollToHome();
        }

        TextChanged.Invoke();
    }

    private void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
    {
        _scrollViewer?.WhenLoaded(InvalidateTextViewed);
    }

    private void InvalidateTextViewed()
    {
        Guard.IsNotNull(_scrollViewer);

        var isOnTop = _scrollViewer.VerticalOffset.NearlyZero();
        var isOnBottom = (_scrollViewer.VerticalOffset + _scrollViewer.ViewportHeight).NearlyEqual(_scrollViewer.ExtentHeight);

        _textStartViewed = _textStartViewed || isOnTop;
        _textEndViewed = _textEndViewed || isOnBottom;

        if (_textStartViewed && _textEndViewed)
        {
            if (_textViewedEventSent is false)
            {
                _textViewedEventSent = true;

                TextViewed.Invoke();
            }
        }
    }

    private bool _textStartViewed;
    private bool _textEndViewed;
    private bool _textViewedEventSent;

    private ScrollViewer? _scrollViewer;
}
