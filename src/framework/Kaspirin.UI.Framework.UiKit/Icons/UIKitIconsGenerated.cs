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

// This file was automatically generated by the 'Kaspirin.UI.Framework.UiKit.Translator'.
// Code generated by the 'Kaspirin.UI.Framework.UiKit.Translator' is owned by the owner of the input file used when generating it.
// The following copyright applies to the portions of the 'Kaspirin.UI.Framework.UiKit.Translator' located in this file: Copyright © 2024 AO Kaspersky Lab.

using System.Runtime.CompilerServices;
using System.Threading;

namespace Kaspirin.UI.Framework.UiKit.Icons
{
    /// <summary>UIKit icons with 12x12 size.</summary>
    public enum UIKitIcon_12
    {
        UIKitUnset = 0,
        Bullet,
        Check,
        IconStub
    }

    /// <summary>UIKit icons with 16x16 size.</summary>
    public enum UIKitIcon_16
    {
        UIKitUnset = 0,
        Add,
        ArrowChange,
        ArrowDown,
        ArrowLeft,
        ArrowRight,
        ArrowUp,
        Calendar,
        Check,
        Clear,
        Copy,
        Cut,
        EyeHide,
        EyeShow,
        IconStub,
        LinkExternal,
        Minus,
        Moon,
        Paste,
        Placeholder,
        Placeholder2,
        RingClock,
        Search,
        Settings,
        StatusDanger,
        StatusDanger2,
        StatusDangerSolid,
        StatusDangerSolid2,
        StatusError,
        StatusErrorSolid,
        StatusInfo,
        StatusInfoSolid,
        StatusPositive,
        StatusPositiveSolid,
        StatusQuestion,
        StatusQuestionSolid,
        StatusWarning,
        StatusWarning2,
        StatusWarningSolid,
        StatusWarningSolid2,
        Sun,
        TextCapsLock,
        TextCapsLockSolid,
        ToggleLightDark
    }

    /// <summary>UIKit icons with 24x24 size.</summary>
    public enum UIKitIcon_24
    {
        UIKitUnset = 0,
        Add,
        ArrowChange,
        ArrowDown,
        ArrowLeft,
        ArrowRight,
        ArrowUp,
        Calendar,
        Check,
        Clear,
        Copy,
        Cut,
        EyeHide,
        EyeShow,
        IconStub,
        LinkExternal,
        Minus,
        Moon,
        Paste,
        Placeholder,
        Placeholder2,
        RingClock,
        Search,
        Settings,
        StatusDanger,
        StatusDanger2,
        StatusDangerSolid,
        StatusDangerSolid2,
        StatusError,
        StatusErrorSolid,
        StatusInfo,
        StatusInfoSolid,
        StatusPositive,
        StatusPositiveSolid,
        StatusQuestion,
        StatusQuestionSolid,
        StatusWarning,
        StatusWarning2,
        StatusWarningSolid,
        StatusWarningSolid2,
        Sun,
        TextCapsLock,
        TextCapsLockSolid,
        ToggleLightDark
    }

    /// <summary>UIKit icons with 32x32 size.</summary>
    public enum UIKitIcon_32
    {
        UIKitUnset = 0,
        IconStub
    }

    /// <summary>UIKit icons with 48x48 size.</summary>
    public enum UIKitIcon_48
    {
        UIKitUnset = 0,
        IconStub
    }

    internal static class UIKitIconMetadataRegistrar
    {
#if !NETFRAMEWORK
        [ModuleInitializer]
#endif
        internal static void RegisterMetadata()
        {
            if (Interlocked.CompareExchange(ref _isRegistered, 1, 0) == 0)
            {
                UIKitIconMetadataStorage.Register(UIKitIcon_16.ArrowChange, isAutoRTL: true, isColorfull: false);
                UIKitIconMetadataStorage.Register(UIKitIcon_16.ArrowLeft, isAutoRTL: true, isColorfull: false);
                UIKitIconMetadataStorage.Register(UIKitIcon_16.ArrowRight, isAutoRTL: true, isColorfull: false);
                UIKitIconMetadataStorage.Register(UIKitIcon_16.StatusQuestion, isAutoRTL: true, isColorfull: false);
                UIKitIconMetadataStorage.Register(UIKitIcon_16.StatusQuestionSolid, isAutoRTL: true, isColorfull: false);
                UIKitIconMetadataStorage.Register(UIKitIcon_24.ArrowChange, isAutoRTL: true, isColorfull: false);
                UIKitIconMetadataStorage.Register(UIKitIcon_24.ArrowLeft, isAutoRTL: true, isColorfull: false);
                UIKitIconMetadataStorage.Register(UIKitIcon_24.ArrowRight, isAutoRTL: true, isColorfull: false);
                UIKitIconMetadataStorage.Register(UIKitIcon_24.StatusQuestion, isAutoRTL: true, isColorfull: false);
                UIKitIconMetadataStorage.Register(UIKitIcon_24.StatusQuestionSolid, isAutoRTL: true, isColorfull: false);
            }
        }

        private static int _isRegistered = 0;
    }
}