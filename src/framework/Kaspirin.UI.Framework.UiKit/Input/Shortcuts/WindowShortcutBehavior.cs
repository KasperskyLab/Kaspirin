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
using System.Windows.Input;

namespace Kaspirin.UI.Framework.UiKit.Input.Shortcuts
{
    public sealed class WindowShortcutBehavior : Behavior<Window, WindowShortcutBehavior>
    {
        #region ShortcutCollection

        public static readonly DependencyProperty ShortcutCollectionProperty = DependencyProperty.RegisterAttached(
            "ShortcutCollection",
            typeof(WindowShortcutCollection),
            typeof(WindowShortcutBehavior),
            new PropertyMetadata(default(WindowShortcutCollection), OnShortcutCollectionChanged));

        public static WindowShortcutCollection GetShortcutCollection(DependencyObject obj)
            => (WindowShortcutCollection)obj.GetValue(ShortcutCollectionProperty);

        public static void SetShortcutCollection(DependencyObject obj, WindowShortcutCollection value)
            => obj.SetValue(ShortcutCollectionProperty, value);

        private static void OnShortcutCollectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Window window)
            {
                return;
            }

            if (GetIsEnabled(window))
            {
                GetBehavior(window).UpdateCommandBindings();
            }
        }

        #endregion

        #region ShortcutEvent

        public static readonly RoutedEvent ShortcutEvent = EventManager.RegisterRoutedEvent(
            "ShortcutEvent",
            RoutingStrategy.Bubble,
            typeof(RoutedEventHandler),
            typeof(WindowShortcutBehavior));

        #endregion

        protected override void OnAttached()
        {
            base.OnAttached();

            _commandBindings ??= GetCommandBindings().ToArray();

            AttachCommandBindings();

            _areBindingsAttached = true;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            DetachCommandBindings();

            _areBindingsAttached = false;
        }

        private static void RaiseShortcutEvent(object sender, ExecutedRoutedEventArgs e)
        {
            var shortcutCommand = (RoutedUICommand)e.Command;

            var shortcutEventArgs = new WindowShortcutEventArgs(
                routedEvent: ShortcutEvent,
                keyGesture: shortcutCommand.InputGestures.Cast<KeyGesture>().Single());

            var window = (Window)sender;
            window.RaiseEvent(shortcutEventArgs);
        }

        private void UpdateCommandBindings()
        {
            if (_areBindingsAttached)
            {
                DetachCommandBindings();
            }

            _commandBindings = GetCommandBindings().ToArray();

            if (_areBindingsAttached)
            {
                AttachCommandBindings();
            }
        }

        private void AttachCommandBindings()
        {
            Guard.IsNotNull(AssociatedObject);

            if (_commandBindings is null)
            {
                return;
            }

            foreach (var commandBinding in _commandBindings)
            {
                AssociatedObject.CommandBindings.Add(commandBinding);
            }
        }

        private void DetachCommandBindings()
        {
            Guard.IsNotNull(AssociatedObject);

            if (_commandBindings is null)
            {
                return;
            }

            foreach (var commandBinding in _commandBindings)
            {
                AssociatedObject.CommandBindings.Remove(commandBinding);
            }
        }

        private IEnumerable<CommandBinding> GetCommandBindings()
        {
            yield return GetCommandBinding(WindowShortcuts.Search);

            var shortcutCollection = GetShortcutCollection(Guard.EnsureIsNotNull(AssociatedObject));
            if (shortcutCollection == null)
            {
                yield break;
            }

            foreach (var windowShortcut in shortcutCollection)
            {
                yield return GetCommandBinding(windowShortcut);
            }
        }

        private static CommandBinding GetCommandBinding(WindowShortcut windowShortcut)
        {
            var shortcut = Guard.EnsureIsNotNull(windowShortcut.Shortcut);

            Guard.IsNotNullOrEmpty(windowShortcut.Description, $"Description is required for shortcut '{shortcut}'");
            Guard.IsNotNullOrEmpty(windowShortcut.Name, $"Name is required for shortcut '{shortcut}'");

            var shortcutCommand = new RoutedUICommand(
                windowShortcut.Description,
                windowShortcut.Name,
                typeof(WindowShortcutBehavior),
                new() { shortcut.ToKeyGesture() });

            return new CommandBinding(shortcutCommand, RaiseShortcutEvent);
        }

        private bool _areBindingsAttached;
        private CommandBinding[]? _commandBindings;
    }
}
