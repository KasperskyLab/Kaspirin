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

using WpfRenderCamability = System.Windows.Media.RenderCapability;

namespace Kaspirin.UI.Framework.UiKit.Animation.Internals;

internal sealed class RenderCapability : IRenderCapability
{
    public RenderCapabilityTier Tier
    {
        get
        {
            Guard.AssertIsUiThread();

            if (WpfRenderCamability.Tier >= 0x00020000)
            {
                return RenderCapabilityTier.Tier2;
            }
            else if (WpfRenderCamability.Tier >= 0x00010000)
            {
                return RenderCapabilityTier.Tier1;
            }
            else
            {
                return RenderCapabilityTier.Tier0;
            }
        }
    }

    public event EventHandler TierChanged
    {
        add
        {
            Guard.AssertIsUiThread();
            WpfRenderCamability.TierChanged += value;
        }
        remove
        {
            Guard.AssertIsUiThread();
            WpfRenderCamability.TierChanged -= value;
        }
    }
}
