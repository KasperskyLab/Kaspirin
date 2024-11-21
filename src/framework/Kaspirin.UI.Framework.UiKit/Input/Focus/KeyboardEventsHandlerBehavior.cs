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
using System.Windows.Input;

namespace Kaspirin.UI.Framework.UiKit.Input.Focus
{
    public static class KeyboardEventsHandlerBehavior
    {
        #region FocusEventsHandler

        public static readonly DependencyProperty FocusEventsHandlerProperty =
            DependencyProperty.RegisterAttached("FocusEventsHandler", typeof(IKeyboardFocusEventsHandler), typeof(KeyboardEventsHandlerBehavior),
                new PropertyMetadata(OnFocusEventsHandlerChanged));

        public static IKeyboardFocusEventsHandler GetFocusEventsHandler(UIElement uiElement)
        {
            return (IKeyboardFocusEventsHandler)uiElement.GetValue(FocusEventsHandlerProperty);
        }

        public static void SetFocusEventsHandler(UIElement uiElement, IKeyboardFocusEventsHandler keyboardFocusEventsHandler)
        {
            uiElement.SetValue(FocusEventsHandlerProperty, keyboardFocusEventsHandler);
        }

        private static void OnFocusEventsHandlerChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var targetElement = dependencyObject as UIElement;
            if (targetElement == null)
            {
                return;
            }

            var handler = args.NewValue as IKeyboardFocusEventsHandler;

            targetElement.WhenLoaded(() => targetElement.WhenVisible(() =>
            {
                var innerTargetElement = targetElement.FindVisualChildren<UIElement>().GuardedSingleOrDefault(GetIsKeyboardFocusSourceElement);
                if (innerTargetElement != null)
                {
                    targetElement = innerTargetElement;
                }

                if (handler != null)
                {
                    SubscribeToKeyboardFocusEvents(targetElement, handler);
                }
                else if (handler == null)
                {
                    UnsubscribeFromKeyboardFocusEvents(targetElement);
                }
            }));
        }

        #endregion

        #region FocusControllerHandler

        public static readonly DependencyProperty FocusControllerHandlerProperty =
            DependencyProperty.RegisterAttached("FocusControllerHandler", typeof(IKeyboardFocusControllerHandler), typeof(KeyboardEventsHandlerBehavior),
                new PropertyMetadata(OnFocusControllerHandlerChanged));

        public static IKeyboardFocusControllerHandler GetFocusControllerHandler(UIElement uiElement)
        {
            return (IKeyboardFocusControllerHandler)uiElement.GetValue(FocusControllerHandlerProperty);
        }

        public static void SetFocusControllerHandler(UIElement uiElement, IKeyboardFocusControllerHandler keyboardFocusControllerHandler)
        {
            uiElement.SetValue(FocusControllerHandlerProperty, keyboardFocusControllerHandler);
        }

        private static void OnFocusControllerHandlerChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            var targetElement = dependencyObject as UIElement;
            if (targetElement == null)
            {
                return;
            }

            if (args.NewValue is IKeyboardFocusControllerHandler handler)
            {
                handler.OnControllerCreated(new KeyboardFocusController(targetElement));
            }
        }

        #endregion

        #region IsKeyboardFocusSourceElement

        public static bool GetIsKeyboardFocusSourceElement(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsKeyboardFocusSourceElementProperty);
        }

        public static void SetIsKeyboardFocusSourceElement(DependencyObject obj, bool value)
        {
            obj.SetValue(IsKeyboardFocusSourceElementProperty, value);
        }

        public static readonly DependencyProperty IsKeyboardFocusSourceElementProperty =
            DependencyProperty.RegisterAttached("IsKeyboardFocusSourceElement", typeof(bool), typeof(KeyboardEventsHandlerBehavior), new PropertyMetadata(false));

        #endregion

        private static void SubscribeToKeyboardFocusEvents(UIElement inputElement, IKeyboardFocusEventsHandler handler)
        {
            inputElement.SetCurrentValue(FocusEventsHandlerProperty, handler);
            inputElement.GotKeyboardFocus -= OnGotKeyboardFocus;
            inputElement.GotKeyboardFocus += OnGotKeyboardFocus;
            inputElement.LostKeyboardFocus -= OnLostKeyboardFocus;
            inputElement.LostKeyboardFocus += OnLostKeyboardFocus;
        }

        private static void UnsubscribeFromKeyboardFocusEvents(UIElement inputElement)
        {
            inputElement.SetCurrentValue(FocusEventsHandlerProperty, null);
            inputElement.GotKeyboardFocus -= OnGotKeyboardFocus;
            inputElement.LostKeyboardFocus -= OnLostKeyboardFocus;
        }

        private static void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs eventArgs)
        {
            GetFocusEventsHandler((UIElement)sender)?.OnGotKeyboardFocus();
        }

        private static void OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs eventArgs)
        {
            GetFocusEventsHandler((UIElement)sender)?.OnLostKeyboardFocus();
        }

        private sealed class KeyboardFocusController : IKeyboardFocusController
        {
            public KeyboardFocusController(UIElement targetElement)
            {
                _targetElement = new WeakReference<UIElement>(targetElement);
            }

            public void SetFocus()
            {
                Executers.InUiAsync(() =>
                {
                    if (_targetElement.TryGetTarget(out var target))
                    {
                        InputFocusManager.SetInputFocus(target);
                    }
                });
            }

            public void ClearFocus()
            {
                Executers.InUiAsync(() =>
                {
                    if (_targetElement.TryGetTarget(out var target))
                    {
                        InputFocusManager.ClearInputFocus(target);
                    }
                });
            }

            private readonly WeakReference<UIElement> _targetElement;
        }
    }
}
