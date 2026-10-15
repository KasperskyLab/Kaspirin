// Copyright © 2025 AO Kaspersky Lab.
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

using System.Windows.Threading;

namespace Kaspirin.UI.Framework.Threading;

/// <summary>
///     The context of a suspended stack that allows you to resume its execution.
/// </summary>
public sealed class DispatcherFrameContext
{
    internal DispatcherFrameContext(DispatcherFrame dispatcherFrame)
    {
        _dispatcherFrame = dispatcherFrame;
    }

    /// <summary>
    ///     Resumes stack execution from the place where the <see cref="DispatcherFrameAction.Run" /> method was previously called.
    /// </summary>
    public void CloseFrame()
    {
        if (_dispatcherFrame != null)
        {
            _dispatcherFrame.Continue = false;
            _dispatcherFrame = null;
        }
    }

    private DispatcherFrame? _dispatcherFrame;
}
