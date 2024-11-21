// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 1/10/2024.
// Scope of modification:
//   - Code adaptation to project requirements.

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Kaspirin.UI.Framework.UiKit.Interactivity.Core
{
    public abstract class EventTriggerBase<TSource> : EventTriggerBase where TSource : class
    {
        protected EventTriggerBase()
            : base(typeof(TSource))
        {
        }

        public new TSource? Source
        {
            get
            {
                return (TSource?)base.Source;
            }
        }
    }
}
