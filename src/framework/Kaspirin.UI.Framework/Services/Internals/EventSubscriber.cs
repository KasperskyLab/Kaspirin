// Copyright © 2025 AO Kaspersky Lab.
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

namespace Kaspirin.UI.Framework.Services.Internals;

internal static class EventSubscriber
{
    public static void OnceOrNow<TSource>(
        TSource source,
        Func<TSource, bool> condition,
        Action<EventHandler> subscribeCallback,
        Action<EventHandler> unsubscribeCallback,
        Action action)
    {
        Guard.ArgumentIsNotNull(source);
        Guard.ArgumentIsNotNull(condition);
        Guard.ArgumentIsNotNull(subscribeCallback);
        Guard.ArgumentIsNotNull(unsubscribeCallback);
        Guard.ArgumentIsNotNull(action);

        var sync = new object();

        void OnEvent(object? sender, EventArgs e)
        {
            lock (sync)
            {
                var source = Guard.EnsureArgumentIsInstanceOfType<TSource>(sender);
                if (action != null && condition(source))
                {
                    action.Invoke();
                    action = null!;

                    unsubscribeCallback(OnEvent);
                }
            }
        }

        lock (sync)
        {
            subscribeCallback(OnEvent);

            if (action != null && condition(source))
            {
                action.Invoke();
                action = null!;

                unsubscribeCallback(OnEvent);
            }
        }
    }

    public static void OnceOrNow<TSource>(
        TSource source,
        Func<TSource, bool> condition,
        Action<RoutedEventHandler> subscribeCallback,
        Action<RoutedEventHandler> unsubscribeCallback,
        Action action)
    {
        Guard.ArgumentIsNotNull(source);
        Guard.ArgumentIsNotNull(condition);
        Guard.ArgumentIsNotNull(subscribeCallback);
        Guard.ArgumentIsNotNull(unsubscribeCallback);
        Guard.ArgumentIsNotNull(action);

        var sync = new object();

        void OnEvent(object? sender, EventArgs e)
        {
            lock (sync)
            {
                var source = Guard.EnsureArgumentIsInstanceOfType<TSource>(sender);
                if (action != null && condition(source))
                {
                    action.Invoke();
                    action = null!;

                    unsubscribeCallback(OnEvent);
                }
            }
        }

        lock (sync)
        {
            subscribeCallback(OnEvent);

            if (action != null && condition(source))
            {
                action.Invoke();
                action = null!;

                unsubscribeCallback(OnEvent);
            }
        }
    }

    public static void OnceOrNow<TSource>(
        TSource source,
        Func<TSource, bool> condition,
        Action<DependencyPropertyChangedEventHandler> subscribeCallback,
        Action<DependencyPropertyChangedEventHandler> unsubscribeCallback,
        Action action)
    {
        Guard.ArgumentIsNotNull(source);
        Guard.ArgumentIsNotNull(condition);
        Guard.ArgumentIsNotNull(subscribeCallback);
        Guard.ArgumentIsNotNull(unsubscribeCallback);
        Guard.ArgumentIsNotNull(action);

        var sync = new object();

        void OnEvent(object? sender, DependencyPropertyChangedEventArgs e)
        {
            lock (sync)
            {
                var source = Guard.EnsureArgumentIsInstanceOfType<TSource>(sender);
                if (action != null && condition(source))
                {
                    action.Invoke();
                    action = null!;

                    unsubscribeCallback(OnEvent);
                }
            }
        }

        lock (sync)
        {
            subscribeCallback(OnEvent);

            if (action != null && condition(source))
            {
                action.Invoke();
                action = null!;

                unsubscribeCallback(OnEvent);
            }
        }
    }

    public static void Once<TSource>(
        TSource source,
        Action<RoutedEventHandler> subscribeCallback,
        Action<RoutedEventHandler> unsubscribeCallback,
        Action action)
    {
        Guard.ArgumentIsNotNull(source);
        Guard.ArgumentIsNotNull(subscribeCallback);
        Guard.ArgumentIsNotNull(unsubscribeCallback);
        Guard.ArgumentIsNotNull(action);

        var sync = new object();

        void OnEvent(object? sender, EventArgs e)
        {
            lock (sync)
            {
                if (action != null)
                {
                    action.Invoke();
                    action = null!;
                }

                unsubscribeCallback(OnEvent);
            }
        }

        subscribeCallback(OnEvent);
    }
}
