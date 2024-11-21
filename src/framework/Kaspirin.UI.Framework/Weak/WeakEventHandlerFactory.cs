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
using System.Linq;
using System.Reflection;

namespace Kaspirin.UI.Framework.Weak
{
    /// <summary>
    ///     Creates a wrapper delegate for the specified delegate and stores a weak reference to it.
    /// </summary>
    /// <typeparam name="TEventHandler">
    ///     The type of delegate.
    /// </typeparam>
    public sealed class WeakEventHandlerFactory<TEventHandler> where TEventHandler : Delegate
    {
        /// <summary>
        ///     Creates a wrapper delegate with a weak reference to <paramref name="EventHandler" />.
        /// </summary>
        /// <param name="eventHandler">
        ///     The original delegate.
        /// </param>
        /// <param name="cleanup">
        ///     A delegate executed when a weak reference to <paramref name="EventHandler" /> is deleted.
        /// </param>
        /// <remarks>
        ///     It cannot be used for static methods.<br /> The weak link is checked every time the delegate
        ///     is called <paramref name="EventHandler" /> and if the weak link is deleted, then the delegate <paramref name="cleanup" /> is called in this case.
        /// </remarks>
        /// <returns>
        ///     The delegate is a wrapper.
        /// </returns>
        public static TEventHandler Create(TEventHandler eventHandler, Action<TEventHandler> cleanup)
        {
            Guard.ArgumentIsNotNull(eventHandler);
            Guard.ArgumentIsNotNull(eventHandler.Target);
            Guard.ArgumentIsNotNull(cleanup);

            var weakEventHandlerImpl = new WeakEventHandlerFactory<TEventHandler>(eventHandler, cleanup);
            return weakEventHandlerImpl._proxyHandler; // Return the delegate to add to the event 
        }

        private WeakEventHandlerFactory(Delegate d, Action<TEventHandler> cleanup)
        {
            var target = Guard.EnsureIsNotNull(d.Target);
            _weakReferenceTarget = new WeakReference(target);
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
            var proxyMethod = typeof(WeakEventHandlerFactory<TEventHandler>)
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .GuardedSingle(o => o.Name == "FuncInvoke" && o.GetGenericArguments().Length == genericArgumentTypes.Length)
                .MakeGenericMethod(genericArgumentTypes);

            _proxyHandler = (TEventHandler)(object)Delegate.CreateDelegate(eventHandlerType, this, proxyMethod);

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
                _ => throw new NotSupportedException()
            };

            var openEventHandlerType = openEventHandlerGenericType.MakeGenericType(genericArgumentTypes);

            _openEventHandler = Delegate.CreateDelegate(openEventHandlerType, null, d.Method);
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
            _cleanup?.Invoke(_proxyHandler);
            _cleanup = null;
        }

        private Action<TEventHandler>? _cleanup;

        private readonly TEventHandler _proxyHandler;
        private readonly WeakReference _weakReferenceTarget;
        private readonly Delegate _openEventHandler;
    }
}
