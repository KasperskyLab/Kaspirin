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
using System.Linq;
using System.Reflection;

namespace Kaspirin.UI.Framework.Events;

/// <summary>
///     Implements the logic of weak references for event delegates.
/// </summary>
/// <typeparam name="TEventHandler">
///     The type of event handler.
/// </typeparam>
public class BaseWeakEventHandler<TEventHandler> where TEventHandler : Delegate
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="BaseWeakEventHandler{TEventHandler}" /> class.
    /// </summary>
    /// <param name="handler">
    ///     The delegate that runs on the event.
    /// </param>
    /// <param name="cleanup">
    ///     A delegate executed when a weak reference to <paramref name="handler" /> is collected by the garbage collector.
    /// </param>
    internal protected BaseWeakEventHandler(Delegate handler, Action<TEventHandler>? cleanup = null)
    {
        var target = Guard.EnsureIsNotNull(handler.Target);

        _weakReferenceTarget = new WeakReference(target);
        _methodInfo = handler.Method;
        _cleanup = cleanup;

        var targetType = target.GetType();
        var eventHandlerType = typeof(TEventHandler);
        var invokeMethodInfo = eventHandlerType.GetMethod("Invoke");
        if (invokeMethodInfo == null)
        {
            throw new InvalidOperationException($"Invoke method of event handler type {typeof(TEventHandler).FullName} is null");
        }

        var parameterTypes = invokeMethodInfo.GetParameters().Select(o => o.ParameterType).ToArray();
        if (parameterTypes.Length > 7)
        {
            throw new NotSupportedException();
        }

        var genericArgumentTypes = new[] { targetType }.Concat(parameterTypes).ToArray();
        var proxyMethod = typeof(BaseWeakEventHandler<TEventHandler>)
            .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
            .GuardedSingle(o => o.Name == "FuncInvoke" && o.GetGenericArguments().Length == genericArgumentTypes.Length)
            .MakeGenericMethod(genericArgumentTypes);

        ProxyHandler = (TEventHandler)(object)Delegate.CreateDelegate(eventHandlerType, this, proxyMethod);

        var openEventHandlerGenericType = parameterTypes.Length switch
        {
            0 => typeof(Action<>),
            1 => typeof(Action<,>),
            2 => typeof(Action<,,>),
            3 => typeof(Action<,,,>),
            4 => typeof(Action<,,,,>),
            5 => typeof(Action<,,,,,>),
            6 => typeof(Action<,,,,,,>),
            7 => typeof(Action<,,,,,,,>),
            _ => throw new UnexpectedValueException(parameterTypes.Length)
        };

        var openEventHandlerType = openEventHandlerGenericType.MakeGenericType(genericArgumentTypes);

        _openEventHandler = Delegate.CreateDelegate(openEventHandlerType, null, _methodInfo);
    }

    /// <summary>
    ///     Specifies whether a weak reference to the target object of the event handler is valid.
    /// </summary>
    public bool IsAlive => _weakReferenceTarget.IsAlive;

    /// <returns>
    ///     The delegate that needs to be subscribed to the event.
    /// </returns>
    public TEventHandler ProxyHandler { get; }

    /// <summary>
    ///     Checks whether the delegate <paramref name="handler" /> matches the current event handler.
    /// </summary>
    /// <param name="handler">
    ///     The delegate being checked.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the delegate matches, otherwise <see langword="false" />.
    /// </returns>
    public bool Matches(TEventHandler handler)
    {
        if (handler.Method != _methodInfo)
        {
            return false;
        }

        return ReferenceEquals(handler.Target, _weakReferenceTarget.Target);
    }

    /// <summary>
    ///     Calls the event handler.
    /// </summary>
    /// <param name="args">
    ///     Arguments for the event handler.
    /// </param>
    /// <returns>
    ///     Returns <see langword="true" /> if the call was successful, otherwise <see langword="false" />.
    /// </returns>
    public bool Invoke(object?[] args)
    {
        var target = _weakReferenceTarget.Target;
        if (target != null)
        {
            ProxyHandler.DynamicInvoke(args);

            GC.KeepAlive(target);

            return true;
        }

        return false;
    }

    private void FuncInvoke<TTarget>()
        where TTarget : class
    {
        var target = (TTarget?)_weakReferenceTarget.Target;
        if (target != null)
        {
            ((Action<TTarget>)_openEventHandler)(target);
        }
        else
        {
            Cleanup();
        }
    }

    private void FuncInvoke<TTarget, TArg1>(TArg1 arg1)
        where TTarget : class
    {
        var target = (TTarget?)_weakReferenceTarget.Target;
        if (target != null)
        {
            ((Action<TTarget, TArg1>)_openEventHandler)(target, arg1);
        }
        else
        {
            Cleanup();
        }
    }

    private void FuncInvoke<TTarget, TArg1, TArg2>(TArg1 arg1, TArg2 arg2)
        where TTarget : class
    {
        var target = (TTarget?)_weakReferenceTarget.Target;
        if (target != null)
        {
            ((Action<TTarget, TArg1, TArg2>)_openEventHandler)(target, arg1, arg2);
        }
        else
        {
            Cleanup();
        }
    }

    private void FuncInvoke<TTarget, TArg1, TArg2, TArg3>(TArg1 arg1, TArg2 arg2, TArg3 arg3)
        where TTarget : class
    {
        var target = (TTarget?)_weakReferenceTarget.Target;
        if (target != null)
        {
            ((Action<TTarget, TArg1, TArg2, TArg3>)_openEventHandler)(target, arg1, arg2, arg3);
        }
        else
        {
            Cleanup();
        }
    }

    private void FuncInvoke<TTarget, TArg1, TArg2, TArg3, TArg4>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4)
        where TTarget : class
    {
        var target = (TTarget?)_weakReferenceTarget.Target;
        if (target != null)
        {
            ((Action<TTarget, TArg1, TArg2, TArg3, TArg4>)_openEventHandler)(target, arg1, arg2, arg3, arg4);
        }
        else
        {
            Cleanup();
        }
    }

    private void FuncInvoke<TTarget, TArg1, TArg2, TArg3, TArg4, TArg5>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5)
        where TTarget : class
    {
        var target = (TTarget?)_weakReferenceTarget.Target;
        if (target != null)
        {
            ((Action<TTarget, TArg1, TArg2, TArg3, TArg4, TArg5>)_openEventHandler)(target, arg1, arg2, arg3, arg4, arg5);
        }
        else
        {
            Cleanup();
        }
    }

    private void FuncInvoke<TTarget, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6)
        where TTarget : class
    {
        var target = (TTarget?)_weakReferenceTarget.Target;
        if (target != null)
        {
            ((Action<TTarget, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6>)_openEventHandler)(target, arg1, arg2, arg3, arg4, arg5, arg6);
        }
        else
        {
            Cleanup();
        }
    }

    private void FuncInvoke<TTarget, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>(TArg1 arg1, TArg2 arg2, TArg3 arg3, TArg4 arg4, TArg5 arg5, TArg6 arg6, TArg7 arg7)
        where TTarget : class
    {
        var target = (TTarget?)_weakReferenceTarget.Target;
        if (target != null)
        {
            ((Action<TTarget, TArg1, TArg2, TArg3, TArg4, TArg5, TArg6, TArg7>)_openEventHandler)(target, arg1, arg2, arg3, arg4, arg5, arg6, arg7);
        }
        else
        {
            Cleanup();
        }
    }

    private void Cleanup()
    {
        _cleanup?.Invoke(ProxyHandler);
        _cleanup = null;
    }

    private readonly Delegate _openEventHandler;
    private readonly WeakReference _weakReferenceTarget;
    private readonly MethodInfo _methodInfo;

    private Action<TEventHandler>? _cleanup;
}
