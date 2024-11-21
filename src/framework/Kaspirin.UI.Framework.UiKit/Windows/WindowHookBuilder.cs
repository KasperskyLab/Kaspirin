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
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Kaspirin.UI.Framework.UiKit.Windows
{
    public sealed class WindowHookBuilder
    {
        public WindowHookBuilder(string hookId)
        {
            _hookId = hookId;
        }

        public WindowHookBuilder AddHandler(WindowMessage msg, WindowHookDelegate hook)
        {
            Guard.Assert(!_hooksMap.ContainsKey(msg), $"{msg} already added");

            _hooksMap[msg] = (IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) =>
            {
                return hook(hwnd, wParam, lParam, ref handled);
            };

            _syncHooks.Add(_hooksMap[msg]);

            return this;
        }

        public WindowHookBuilder AddHandler<TWParam, TLParam>(WindowMessage msg, WindowHookDelegate<TWParam, TLParam> hook)
            where TWParam : struct
            where TLParam : struct
        {
            Guard.Assert(!_hooksMap.ContainsKey(msg), $"{msg} already added");

            _hooksMap[msg] = (IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) =>
            {
                var wStruct = CastMsgParam<TWParam>(wParam, allowNativePointers: true);
                var lStruct = CastMsgParam<TLParam>(lParam, allowNativePointers: true);

                return hook(hwnd, wParam, wStruct, lParam, lStruct, ref handled);
            };

            _syncHooks.Add(_hooksMap[msg]);

            return this;
        }

        public WindowHookBuilder AddAsyncHandler(WindowMessage msg, WindowHookDelegateAsync hook, bool? shouldHandle = null)
        {
            Guard.Assert(!_hooksMap.ContainsKey(msg), $"{msg} already added");

            _hooksMap[msg] = (IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) =>
            {
                Executers.InUiAsync(() => hook());

                if (shouldHandle != null)
                {
                    handled = shouldHandle.Value;
                }

                return IntPtr.Zero;
            };

            _asyncHooks.Add(_hooksMap[msg]);

            return this;
        }

        public WindowHookBuilder AddAsyncHandler<TWParam, TLParam>(WindowMessage msg, WindowHookDelegateAsync<TWParam, TLParam> hook, bool? shouldHandle = null)
            where TWParam : struct
            where TLParam : struct
        {
            Guard.Assert(!_hooksMap.ContainsKey(msg), $"{msg} already added");

            _hooksMap[msg] = (IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) =>
            {
                var wP = CastMsgParam<TWParam>(wParam, allowNativePointers: false);
                var lP = CastMsgParam<TLParam>(lParam, allowNativePointers: false);

                Executers.InUiAsync(() => hook(wP, lP));

                if (shouldHandle != null)
                {
                    handled = shouldHandle.Value;
                }

                return IntPtr.Zero;
            };

            _asyncHooks.Add(_hooksMap[msg]);

            return this;
        }

        public HwndSourceHook Build()
        {
            return new WindowHookProc(_hookId, _hooksMap, _syncHooks, _asyncHooks).WndProc;
        }

        private static TParam? CastMsgParam<TParam>(IntPtr param, bool allowNativePointers)
            where TParam : struct
        {
            object? tParam;

            if (typeof(TParam) == typeof(IntPtr))
            {
                Guard.Assert(allowNativePointers, "IntPtr is not allowed for params cast");

                tParam = param;
            }
            else if (typeof(TParam) == typeof(int))
            {
                tParam = (int)param.ToInt64();
            }
            else if (typeof(TParam) == typeof(long))
            {
                tParam = param.ToInt64();
            }
            else if (typeof(TParam) == typeof(uint))
            {
                tParam = (uint)param.ToInt64();
            }
            else if (typeof(TParam) == typeof(ulong))
            {
                tParam = (ulong)param.ToInt64();
            }
            else
            {
                tParam = Marshal.PtrToStructure(param, typeof(TParam));
            }

            return (TParam?)tParam;
        }

        private sealed class WindowHookProc
        {
            public WindowHookProc(string hookId, IDictionary<WindowMessage, HwndSourceHook> hooksMap, ICollection<HwndSourceHook> syncHooks, ICollection<HwndSourceHook> asyncHooks)
            {
                _hookId = hookId;
                _hooksMap = new(hooksMap);
                _syncHooks = new(syncHooks);
                _asyncHooks = new(asyncHooks);
            }

            public IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
            {
                if (Enum.TryParse<WindowMessage>(msg.ToString(), out var wm))
                {
                    if (_hooksMap.TryGetValue(wm, out var hook))
                    {
                        if (_asyncHooks.Contains(hook))
                        {
                            _tracer.TraceInformation($"Window Message '{wm}' async processing by '{_hookId}' for window '0x{hwnd:X4}' with params: wParam 0x{wParam:X4}, lParam 0x{lParam:X4}");

                            hook(hwnd, msg, wParam, lParam, ref handled);
                        }
                        else if (_syncHooks.Contains(hook))
                        {
                            _tracer.TraceInformation($"Window Message '{wm}' sync processing by '{_hookId}' for window '0x{hwnd:X4}' with params: wParam 0x{wParam:X4}, lParam 0x{lParam:X4}");

                            return hook(hwnd, msg, wParam, lParam, ref handled);
                        }
                    }
                }

                return IntPtr.Zero;
            }

            private readonly string _hookId;
            private readonly Dictionary<WindowMessage, HwndSourceHook> _hooksMap = new();
            private readonly List<HwndSourceHook> _asyncHooks = new();
            private readonly List<HwndSourceHook> _syncHooks = new();

            private static readonly ComponentTracer _tracer = ComponentTracer.Get(nameof(WindowHookProc));
        }

        private readonly Dictionary<WindowMessage, HwndSourceHook> _hooksMap = new();
        private readonly List<HwndSourceHook> _asyncHooks = new();
        private readonly List<HwndSourceHook> _syncHooks = new();
        private readonly string _hookId;
    }
}
