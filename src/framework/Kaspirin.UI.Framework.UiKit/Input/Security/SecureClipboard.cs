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

namespace Kaspirin.UI.Framework.UiKit.Input.Security;

public class SecureClipboard : ISecureClipboard
{
    public SecureClipboard()
    {
        _clipboardClearAction = new DeferredAction(ClearClipboard);

        Tracer = ComponentTracer.Get(ComponentTracers.Clipboard, this);
    }

    public event Action? OnClipboardCleared;

    public void Copy(string content, SecureClipboardCleanupMode mode = SecureClipboardCleanupMode.Timer)
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
            Tracer.TraceInformation("Some text has been copied to the clipboard");

            UpdateLastClipboardSequenceNumber();
        }
        finally
        {
            if (text.Length > 0)
            {
                if (!clipboardFlushed && mode == SecureClipboardCleanupMode.Timer)
                {
                    // Clipboard flush failed (apparently due to some invasive clipboard monitoring tool, like Punto Switcher).
                    // In this case we shouldn't cleanup the string immediately to let other applications still "lazily" acquire the clipboard content.
                    // So defer string cleanup to the moment when its content is no longer needed.
                    UpdateDeferredCleanupBuffer(text);
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
            RestartClipboardClearTimer();
        }
        else
        {
            CleanupDeferredBuffer();
            _clipboardClearAction.Cancel();
        }
    }

    public string GetClipboardContent()
    {
        try
        {
            return Clipboard.GetText();
        }
        catch (Exception ex)
        {
            ex.TraceException("Failed to get clipboard text");
            return string.Empty;
        }
    }

    protected ComponentTracer Tracer { get; }

    protected virtual TimeSpan? GetClipboardClearInterval()
        => _defaultClipboardClearInterval;

    protected bool IsClipboardClearTimerEnabled => _clipboardClearAction.IsEnabled;

    protected void RestartClipboardClearTimer()
    {
        var interval = GetClipboardClearInterval();

        if (interval.HasValue)
        {
            _clipboardClearAction.Execute(interval.Value);
        }
        else
        {
            _clipboardClearAction.Cancel();
        }
    }

    protected void ClearClipboard()
    {
        CleanupDeferredBuffer();

        var clipboardSequenceNumber = User32Dll.GetClipboardSequenceNumber();
        if (clipboardSequenceNumber != 0 && clipboardSequenceNumber == _lastClipboardSequenceNumber)
        {
            ClearClipboardData();
            SetClipboardText(string.Empty, out _);

            Tracer.TraceMethodInfo("Clipboard is cleared");

            OnClipboardCleared?.Invoke();
        }
        else
        {
            Tracer.TraceMethodInfo("Skip clearing clipboard since it was modified or already cleared");
        }
    }

    protected void UpdateLastClipboardSequenceNumber()
        => _lastClipboardSequenceNumber = User32Dll.GetClipboardSequenceNumber();

    private bool TryOpenClipboard()
    {
        for (var i = 0; i < OpenClipboardRetries; i++)
        {
            if (User32Dll.OpenClipboard(default))
            {
                return true;
            }

            Thread.Sleep(OpenClipboardRetryMillisecondsTimeout);
        }

        Tracer.TraceMethodError($"failed with error 0x{Marshal.GetLastWin32Error():X}");
        return false;
    }

    private void TryCloseClipboard()
    {
        if (!User32Dll.CloseClipboard())
        {
            Tracer.TraceMethodError($"failed with error 0x{Marshal.GetLastWin32Error():X}");
        }
    }

    private unsafe void ClearClipboardData()
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
            SetClipboardTextPrivately(text);
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

    private void UpdateDeferredCleanupBuffer(string text)
    {
        CleanupDeferredBuffer();
        _deferredCleanupBuffer = text;
    }

    private void CleanupDeferredBuffer()
        => _deferredCleanupBuffer?.CleanupMemory();

    private static void SetClipboardTextPrivately(string text)
    {
        var dataObject = new DataObject();
        dataObject.SetText(text);

        // Avoid persisting in Windows 10+ clipboard history.
        var emptyData = new byte[4];
        dataObject.SetData("CanIncludeInClipboardHistory", new MemoryStream(emptyData));
        dataObject.SetData("CanUploadToCloudClipboard", new MemoryStream(emptyData));

        Clipboard.SetDataObject(dataObject, copy: true);
    }

    private string? _deferredCleanupBuffer;
    private int _lastClipboardSequenceNumber;

    private readonly DeferredAction _clipboardClearAction;

    private const int OpenClipboardRetries = 10;
    private const int OpenClipboardRetryMillisecondsTimeout = 100;
    private const long ClipboardErrorCantOpen = 0x800401D0L;
    private const long ClipboardErrorCantClose = 0x800401D4L;

    private static readonly TimeSpan _defaultClipboardClearInterval = TimeSpan.FromMinutes(2);
    private static readonly uint _unicodeTextDataFormatId = (uint)DataFormats.GetDataFormat(DataFormats.UnicodeText).Id;
}