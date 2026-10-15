// Copyright © 2026 AO Kaspersky Lab.
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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace Kaspirin.UI.Framework.Services;

/// <summary>
///     Provides methods for getting empty delegates of any type.
/// </summary>
public static class EmptyDelegate
{
    static EmptyDelegate()
    {
#pragma warning disable format
        _cache = new()
        {
            [typeof(Action)] = (Action)delegate { },
            [typeof(Action<bool>)] = (Action<bool>)delegate { },
            [typeof(Action<string>)] = (Action<string>)delegate { },
            [typeof(Action<int>)] = (Action<int>)delegate { },
            [typeof(Action<double>)] = (Action<double>)delegate { },
            [typeof(EventHandler)] = (EventHandler)delegate { },
            [typeof(NotifyCollectionChangedEventHandler)] = (NotifyCollectionChangedEventHandler)delegate { },
            [typeof(PropertyChangedEventHandler)] = (PropertyChangedEventHandler)delegate { },
        };
#pragma warning restore format
    }

    /// <summary>
    ///     Returns an empty delegate of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of delegate.
    /// </typeparam>
    /// <returns>
    ///     An empty delegate of type <typeparamref name="T" />.
    /// </returns>
    public static T For<T>() where T : Delegate
    {
        if (_cache.TryGetValue(typeof(T), out var d))
        {
            return (T)d;
        }

        lock (_cache)
        {
            if (_cache.TryGetValue(typeof(T), out d))
            {
                return (T)d;
            }

            var invoke = typeof(T).GetMethod("Invoke");
            var parameters = invoke!.GetParameters()
                .Select(p => Expression.Parameter(p.ParameterType))
                .ToArray();

            var lambda = Expression.Lambda<T>(Expression.Empty(), parameters);
            var compiled = lambda.Compile();

            _cache.Add(typeof(T), compiled);

            return compiled;
        }
    }

    /// <summary>
    ///     Returns an empty delegate of type <see cref="EventHandler" />.
    /// </summary>
    /// <returns>
    ///     An empty delegate of the <see cref="EventHandler" /> type.
    /// </returns>
    public static EventHandler ForEventHandler()
        => For<EventHandler>();

    /// <summary>
    ///     Returns an empty delegate of type <see cref="EventHandler{T}" />.
    /// </summary>
    /// <typeparam name="T">
    ///     The type of the delegate argument.
    /// </typeparam>
    /// <returns>
    ///     An empty delegate of type <see cref="EventHandler{T}" />.
    /// </returns>
    public static EventHandler<T> ForEventHandler<T>() where T : EventArgs
        => For<EventHandler<T>>();

    /// <summary>
    ///     Returns an empty delegate of type <see cref="Action" />.
    /// </summary>
    /// <returns>
    ///     An empty delegate of the <see cref="Action" /> type.
    /// </returns>
    public static Action ForAction()
        => For<Action>();

    /// <summary>
    ///     Returns an empty delegate of type <see cref="Action{T}" />.
    /// </summary>
    /// <returns>
    ///     An empty delegate of type <see cref="Action{T}" />.
    /// </returns>
    public static Action<TArg> ForAction<TArg>()
        => For<Action<TArg>>();

    /// <summary>
    ///     Returns an empty delegate of type <see cref="Action{T,T}" />.
    /// </summary>
    /// <returns>
    ///     An empty delegate of type <see cref="Action{T,T}" />.
    /// </returns>
    public static Action<TArg1, TArg2> ForAction<TArg1, TArg2>()
        => For<Action<TArg1, TArg2>>();

    /// <summary>
    ///     Returns an empty delegate of type <see cref="Action{T,T,T}" />.
    /// </summary>
    /// <returns>
    ///     An empty delegate of type <see cref="Action{T,T,T}" />.
    /// </returns>
    public static Action<TArg1, TArg2, TArg3> ForAction<TArg1, TArg2, TArg3>()
        => For<Action<TArg1, TArg2, TArg3>>();

    /// <summary>
    ///     Returns an empty delegate of type <see cref="Action{T,T,T,T}" />.
    /// </summary>
    /// <returns>
    ///     An empty delegate of type <see cref="Action{T,T,T,T}" />.
    /// </returns>
    public static Action<TArg1, TArg2, TArg3, TArg4> ForAction<TArg1, TArg2, TArg3, TArg4>()
        => For<Action<TArg1, TArg2, TArg3, TArg4>>();

    private static readonly Dictionary<Type, Delegate> _cache = new();
}
