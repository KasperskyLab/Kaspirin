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
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Kaspirin.UI.Framework.Disposables
{
    /// <summary>
    ///     Creates <see cref="IDisposable" /> objects with the specified action that is performed when <see cref="IDisposable.Dispose" /> is called.
    /// </summary>
    public static class Disposable
    {
        /// <summary>
        ///     Creates an object <see cref="IDisposable" /> that executes <paramref name="disposeAction" />
        ///     after calling <see cref="IDisposable.Dispose" /> for this object.
        /// </summary>
        /// <param name="disposeAction">
        ///     An action to perform.
        /// </param>
        /// <returns>
        ///     The <see cref="IDisposable" /> object.
        /// </returns>
        public static IDisposable Create(Action disposeAction)
        {
            Guard.ArgumentIsNotNull(disposeAction);

            return new DisposableImpl(disposeAction);
        }

        /// <summary>
        ///     Creates an object <see cref="IDisposable" /> that executes <see cref="IDisposable.Dispose" />
        ///     for each element <paramref name="disposables" /> after calling <see cref="IDisposable.Dispose" /> for this object.
        /// </summary>
        /// <param name="disposables">
        ///     An array of objects <see cref="IDisposable" />.
        /// </param>
        /// <returns>
        ///     The <see cref="IDisposable" /> object.
        /// </returns>
        public static IDisposable Composite(params IDisposable[] disposables)
        {
            Guard.ArgumentIsNotNull(disposables);

            return new CompositeDisposableImpl(disposables);
        }

        /// <summary>
        ///     An object <see cref="IDisposable" /> that does not perform any actions when called <see cref="IDisposable.Dispose" />.
        /// </summary>
        public static IDisposable Empty { get; } = new EmptyDisposableImpl();

        private sealed class DisposableImpl : IDisposable
        {
            public DisposableImpl(Action disposeAction)
            {
                _disposeAction = disposeAction;
            }

            public void Dispose() => Interlocked.Exchange(ref _disposeAction, null)?.Invoke();

            private Action? _disposeAction;
        }

        private sealed class CompositeDisposableImpl : IDisposable
        {
            public CompositeDisposableImpl(IEnumerable<IDisposable> disposables)
            {
                _disposables = disposables.ToArray();
            }

            public void Dispose()
            {
                var disposables = Interlocked.Exchange(ref _disposables, null);
                if (disposables != null)
                {
                    foreach (var disposable in disposables)
                    {
                        disposable.Dispose();
                    }
                }
            }

            private IDisposable[]? _disposables;
        }

        private sealed class EmptyDisposableImpl : IDisposable
        {
            public void Dispose() { }
        }
    }
}
