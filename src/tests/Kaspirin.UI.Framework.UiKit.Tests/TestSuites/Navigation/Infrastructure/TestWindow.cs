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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Navigation.Infrastructure;

public sealed class TestWindow : IDisposable
{
    public TestWindow()
    {
        Host = new ContentControl();

        _window = new Window
        {
            WindowStartupLocation = WindowStartupLocation.Manual,
            WindowStyle = WindowStyle.None,
            ShowInTaskbar = false,
            ShowActivated = false,
            Left = -10_000,
            Top = -10_000,
            Width = 100,
            Height = 100,
            Content = Host,
        };

        _window.Show();

        PumpDispatcher();
    }

    public ContentControl Host { get; }

    public void DetachHost()
    {
        _window.Content = null;

        PumpDispatcher();
    }

    public void AttachHost()
    {
        _window.Content = Host;

        PumpDispatcher();
    }

    public void Dispose()
    {
        _window.Close();

        PumpDispatcher();
    }

    public static void PumpDispatcher()
    {
        var frame = new DispatcherFrame();

        Dispatcher.CurrentDispatcher.BeginInvoke(
            DispatcherPriority.ContextIdle,
            new Action(() => frame.Continue = false));

        Dispatcher.PushFrame(frame);
    }

    private readonly Window _window;
}
