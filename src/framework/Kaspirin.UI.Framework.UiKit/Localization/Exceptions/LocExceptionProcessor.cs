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

using Kaspirin.UI.Framework.UiKit.Localization.LocResources;
using System;

namespace Kaspirin.UI.Framework.UiKit.Localization.Exceptions
{
    public static class LocExceptionProcessor
    {
        public static void SetHandlers(
            LocExceptionHandler markupExceptionHandler,
            LocExceptionHandler localizerExceptionHandler,
            LocExceptionHandler resourceExceptionHandler)
        {
            _weakMarkupExceptionHandler = new WeakReference(markupExceptionHandler);
            _weakLocalizerExceptionHandler = new WeakReference(localizerExceptionHandler);
            _weakResourceProviderExceptionHandler = new WeakReference(resourceExceptionHandler);
        }

        public static void ProcessAsMarkupException(this Exception exception, string message)
        {
            Guard.ArgumentIsNotNull(exception);
            Guard.ArgumentIsNotNull(message);

            if (exception is LocalizationMarkupException)
            {
                exception.ProcessException(message);
            }
            else
            {
                exception = new LocalizationMarkupException(message, exception);
                exception.ProcessException(message);
            }
        }

        public static void ProcessAsLocalizerException(this Exception exception, string message)
        {
            Guard.ArgumentIsNotNull(exception);
            Guard.ArgumentIsNotNull(message);

            if (exception is LocalizerException)
            {
                exception.ProcessException(message);
            }
            else
            {
                exception = new LocalizerException(message, exception);
                exception.ProcessException(message);
            }
        }

        public static void ProcessAsResourceProviderException(this Exception exception, string message)
        {
            Guard.ArgumentIsNotNull(exception);
            Guard.ArgumentIsNotNull(message);

            if (exception is ResourceProviderException)
            {
                exception.ProcessException(message);
            }
            else
            {
                exception = new ResourceProviderException(message, exception);
                exception.ProcessException(message);
            }
        }

        private static void ProcessException(this Exception exception, string message)
        {
            Guard.ArgumentIsNotNull(exception);
            Guard.ArgumentIsNotNull(message);

            var exceptionArgs = new LocExceptionArgs(exception, message);
            var exceptionHandler = (LocExceptionHandler?)null;

            if (exception is ResourceProviderException)
            {
                exceptionHandler = _weakResourceProviderExceptionHandler?.Target as LocExceptionHandler;
            }
            else if (exception is LocalizerException)
            {
                exceptionHandler = _weakLocalizerExceptionHandler?.Target as LocExceptionHandler;
            }
            else if (exception is LocalizationMarkupException)
            {
                exceptionHandler = _weakMarkupExceptionHandler?.Target as LocExceptionHandler;
            }

            exceptionHandler?.Invoke(exceptionArgs);

            if (exceptionArgs.Handled)
            {
                return;
            }

            throw new LocException(exceptionArgs.Description, exceptionArgs.Exception);
        }

        private static WeakReference? _weakResourceProviderExceptionHandler;
        private static WeakReference? _weakLocalizerExceptionHandler;
        private static WeakReference? _weakMarkupExceptionHandler;
    }
}