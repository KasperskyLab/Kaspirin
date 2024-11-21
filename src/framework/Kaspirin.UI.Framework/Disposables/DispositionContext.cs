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

namespace Kaspirin.UI.Framework.Disposables
{
    /// <summary>
    ///     Adds an event to the <see cref="DependencyObject" /> object <see cref="Disposed" />.
    /// </summary>
    public sealed class DispositionContext
    {
        /// <summary>
        ///     Gets the <see cref="DispositionContext" /> object from <paramref name="target" />.
        /// </summary>
        /// <param name="target">
        ///     The target object.
        /// </param>
        /// <returns>
        ///     The object is <see cref="DispositionContext" /> if it was previously initialized, otherwise <see langword="null" />.
        /// </returns>
        public static DispositionContext? Get(DependencyObject target)
            => GetContext(target);

        /// <summary>
        ///     Initializes the <see cref="DispositionContext" /> object at <paramref name="target" />.
        /// </summary>
        /// <param name="target">
        ///     The target object.
        /// </param>
        public static void Initialize(DependencyObject target)
            => SetContext(target, new DispositionContext());

        /// <summary>
        ///     Throws the <see cref="Disposed" /> event at <paramref name="target" />.
        /// </summary>
        /// <param name="target">
        ///     The target object.
        /// </param>
        public static void Dispose(DependencyObject target)
        {
            var context = GetContext(target);
            if (context == null)
            {
                return;
            }

            context.Dispose();

            SetContext(target, null);
        }

        /// <summary>
        ///     An event about the destruction of an object.
        /// </summary>
        public event EventHandler? Disposed;

        private void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            Disposed?.Invoke(this, EventArgs.Empty);

            _isDisposed = true;
        }

        private static void SetContext(DependencyObject target, DispositionContext? context)
        {
            if (target is FrameworkElement)
            {
                target.SetValue(_frameworkElementDispositionContextProperty, context);
            }
            else
            {
                target.SetValue(_dependencyObjectDispositionContextProperty, context);
            }
        }

        private static DispositionContext? GetContext(DependencyObject target)
        {
            var result = target is FrameworkElement
                ? target.GetValue(_frameworkElementDispositionContextProperty)
                : target.GetValue(_dependencyObjectDispositionContextProperty);

            return (DispositionContext?)result;
        }

        private bool _isDisposed;

        private static readonly DependencyProperty _dependencyObjectDispositionContextProperty = DependencyProperty.Register(
            "DependencyObjectDispositionContext",
            typeof(DispositionContext),
            typeof(DependencyObject));

        private static readonly DependencyProperty _frameworkElementDispositionContextProperty = DependencyProperty.Register(
            "FrameworkElementDispositionContext",
            typeof(DispositionContext),
            typeof(FrameworkElement),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
    }
}
