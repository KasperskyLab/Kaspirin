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
using System.Windows.Interop;

namespace Kaspirin.UI.Framework.UiKit.Windows
{
    public abstract class WindowHookBase
    {
        protected WindowHookBase(Window window, string hookId, string traceComponent)
        {
            Guard.ArgumentIsNotNull(window);
            Guard.ArgumentIsNotNullOrEmpty(hookId);

            HookId = hookId;
            Tracer = ComponentTracer.Get(traceComponent);

            WindowHandle = window.GetHandle(ensure: true);

            Guard.Assert(WindowHandle != IntPtr.Zero);
            _windowSource = HwndSource.FromHwnd(WindowHandle);
        }

        public string HookId { get; private set; }

        public IntPtr WindowHandle { get; private set; }

        public bool IsHookAttached { get; private set; }

        public abstract void Attach();

        public abstract void Detach();

        protected ComponentTracer Tracer { get; private set; }

        protected void Attach(HwndSourceHook hook)
        {
            Guard.ArgumentIsNotNull(hook);

            if (!IsHookAttached)
            {
                _windowSource.AddHook(hook);

                Tracer.TraceInformation($"{HookId} is attached");
                IsHookAttached = true;
            }
        }

        protected void Detach(HwndSourceHook hook)
        {
            Guard.ArgumentIsNotNull(hook);

            if (IsHookAttached)
            {
                _windowSource.RemoveHook(hook);

                Tracer.TraceInformation($"{HookId} is detached");
                IsHookAttached = false;
            }
        }

        private readonly HwndSource _windowSource;
    }
}
