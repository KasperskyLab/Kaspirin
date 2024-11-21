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
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.UiKit.Input.Security
{
    public static class SecureClipboard
    {
        static SecureClipboard()
        {
            Executers.InUiAsync(() =>
            {
                _clearClipboardTimer = new DispatcherTimer();
                _clearClipboardTimer.Tick += (s, e) => ClearClipboard();
                _clearClipboardTimer.Interval = _clearClipboardInterval;
            });
        }

        public static event Action? OnClipboardCleared;

        public static void Copy(string content, SecureClipboardCleanupMode mode = SecureClipboardCleanupMode.Timer)
        {
            Guard.ArgumentIsNotNull(content);

            // We're not allowed to cleanup the original string after the copy to clipboard is complete
            // since only the caller is responsible for managing the original string.
            // Therefore we copy the original string, pass the copy to the clipboard and clean it up later on.
            var text =
#if NETCOREAPP
                new string(content);
#else
                string.Copy(content);
#endif

            var clipboardFlushed = true;

            try
            {
                SetClipboardText(text, out clipboardFlushed);
                _tracer.TraceInformation("Some text has been copied to the clipboard");

                UpdateLastClipboardSequenceNumber();
            }
            finally
            {
                if (text.Length > 0)
                {
                    if (!clipboardFlushed && mode == SecureClipboardCleanupMode.Timer)
                    {
                        // Clipboard flush failed (apparently due to some invasive clipboard monitoring tool, like Punto Switcher).
                        // In this case we shouldn't cleanup the string immediately to let other applications still "lazily" aquire the clipboard content.
                        // So defer string cleanup to the moment when its content is no longer needed.
                        UpdateCleanupBuffer(text);
                    }
                    else
                    {
                        // Cleanup string asynchronously to let it be transferred into the clipboard without any issues.
                        Executers.InUiAsync(() => text.CleanupMemory(), DispatcherPriority.ContextIdle);
                    }
                }
            }

            if (mode == SecureClipboardCleanupMode.Timer)
            {
                RestartClipboardTimer();
            }
            else
            {
                CleanupDeferredBuffer();
                _clearClipboardTimer?.Stop();
            }
        }

        public static void Clear()
        {
            if (_clearClipboardTimer?.IsEnabled == true)
            {
                ClearClipboard();
            }
        }

        public static string GetClipboardContent()
        {
            try
            {
                return Clipboard.GetText();
            }
            catch
            {
                return string.Empty;
            }
        }

        public static void UpdateLastClipboardSequenceNumber()
            => _lastClipboardSequenceNumber = User32Dll.GetClipboardSequenceNumber();

        public static void RestartClipboardTimer()
        {
            _clearClipboardTimer?.Stop();
            _clearClipboardTimer?.Start();
        }

        private static bool TryOpenClipboard()
        {
            for (var i = 0; i < OpenClipboardRetries; i++)
            {
                if (User32Dll.OpenClipboard(default))
                {
                    return true;
                }

                Thread.Sleep(OpenClipboardRetryMillisecondsTimeout);
            }

            _tracer.TraceError($"Failed to open clipboard: 0x{Marshal.GetLastWin32Error():X}");
            return false;
        }

        private static void TryCloseClipboard()
        {
            if (!User32Dll.CloseClipboard())
            {
                _tracer.TraceError($"Failed to close clipboard: 0x{Marshal.GetLastWin32Error():X}");
            }
        }

        private static void ClearClipboard()
        {
            _clearClipboardTimer?.Stop();

            CleanupDeferredBuffer();

            var clipboardSequenceNumber = User32Dll.GetClipboardSequenceNumber();
            if (clipboardSequenceNumber != 0 && clipboardSequenceNumber == _lastClipboardSequenceNumber)
            {
                ClearClipboardData();
                SetClipboardText(string.Empty, out _);

                _tracer.TraceInformation("Clipboard is cleared");

                OnClipboardCleared?.Invoke();
            }
            else
            {
                _tracer.TraceInformation("Can't clear clipboard because it already was cleared or updated");
            }
        }

        private static unsafe void ClearClipboardData()
        {
            if (!User32Dll.IsClipboardFormatAvailable(_unicodeTextDataFormatId))
            {
                return;
            }

            if (!TryOpenClipboard())
            {
                return;
            }

            try
            {
                var handle = User32Dll.GetClipboardData(_unicodeTextDataFormatId);
                if (handle == default)
                {
                    return;
                }

                var size = Kernel32Dll.GlobalSize(handle);
                if (size == default)
                {
                    return;
                }

                var pointer = Kernel32Dll.GlobalLock(handle);
                if (pointer != default)
                {
                    try
                    {
                        new Span<byte>((void*)pointer, (int)size).Clear();
                    }
                    finally
                    {
                        Kernel32Dll.GlobalUnlock(handle);
                    }
                }
            }
            finally
            {
                TryCloseClipboard();
            }
        }

        private static void SetClipboardText(string text, out bool clipboardFlushed)
        {
            try
            {
                SetTextPrivately(text);
                clipboardFlushed = true;
            }
            catch (COMException ex)
            {
                unchecked
                {
                    clipboardFlushed =
                        ex.ErrorCode != (int)ClipboardErrorCantOpen &&
                        ex.ErrorCode != (int)ClipboardErrorCantClose;
                }
            }
        }

        private static void UpdateCleanupBuffer(string text)
        {
            CleanupDeferredBuffer();
            _deferredCleanupBuffer = text;
        }

        private static void CleanupDeferredBuffer()
            => _deferredCleanupBuffer?.CleanupMemory();

        private static void SetTextPrivately(string text)
        {
            var dataObject = new DataObject();
            dataObject.SetText(text);

            // Copying past Windows 10+ clipboard history.
            var emptyData = new byte[4];
            dataObject.SetData("CanIncludeInClipboardHistory", new MemoryStream(emptyData));
            dataObject.SetData("CanUploadToCloudClipboard", new MemoryStream(emptyData));

            Clipboard.SetDataObject(dataObject, copy: true);
        }

        private static DispatcherTimer? _clearClipboardTimer;
        private static string? _deferredCleanupBuffer;
        private static int _lastClipboardSequenceNumber;

        private const int OpenClipboardRetries = 10;
        private const int OpenClipboardRetryMillisecondsTimeout = 100;
        private const long ClipboardErrorCantOpen = 0x800401D0L;
        private const long ClipboardErrorCantClose = 0x800401D4L;

        private static readonly TimeSpan _clearClipboardInterval = TimeSpan.FromMinutes(2);
        private static readonly ComponentTracer _tracer = ComponentTracer.Get(ComponentTracers.Clipboard);
        private static readonly uint _unicodeTextDataFormatId = (uint)DataFormats.GetDataFormat(DataFormats.UnicodeText).Id;
    }
}