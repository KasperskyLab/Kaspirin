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

using System;
using System.Collections.Generic;
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals;

internal sealed class InteractivityDialogScope : InteractivityScope<InteractivityDialog>
{
    public static void Register(InteractivityDialog dialog, Action? continueCallback)
    {
        new InteractivityDialogScope().Register(dialog, dialog.ScopeName, continueCallback);
    }

    public static void Unregister(InteractivityDialog dialog, Action? continueCallback)
    {
        new InteractivityDialogScope().Unregister(dialog, dialog.ScopeName, continueCallback);
    }

    protected override InteractivityScope<InteractivityDialog> CreateScopeInstance()
    {
        return new InteractivityDialogScope();
    }

    protected override void OnRegister(InteractivityDialog dialog, List<InteractivityDialog> scopedDialogs, Action? continueCallback)
    {
        scopedDialogs.LastOrDefault()?.HideOverlay();

        dialog.ShowContent(continueCallback);
        dialog.ShowOverlay();

        scopedDialogs.Add(dialog);
    }

    protected override void OnUnregister(InteractivityDialog dialog, List<InteractivityDialog> scopedDialogs, Action? continueCallback)
    {
        var isTopMost = scopedDialogs.Last() == dialog;
        if (isTopMost)
        {
            dialog.HideContent(continueCallback);
            dialog.HideOverlay();
        }
        else
        {
            dialog.HideContent(continueCallback);
        }

        scopedDialogs.Remove(dialog);

        if (isTopMost)
        {
            scopedDialogs.LastOrDefault()?.ShowOverlay();
        }
    }
}