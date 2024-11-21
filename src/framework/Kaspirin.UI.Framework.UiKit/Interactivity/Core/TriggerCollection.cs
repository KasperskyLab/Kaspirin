// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 1/10/2024.
// Scope of modification:
//   - Code adaptation to project requirements.

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Interactivity.Core
{
    public sealed class TriggerCollection : AttachableCollection<TriggerBase>
    {
        internal TriggerCollection()
        {
        }

        internal override void ItemAdded(TriggerBase item)
        {
            if (AssociatedObject != null)
            {
                item.Attach(AssociatedObject);
            }
        }

        internal override void ItemRemoved(TriggerBase item)
        {
            if (item.AssociatedObject != null)
            {
                item.Detach();
            }
        }

        protected override void OnAttached()
        {
            foreach (var trigger in this)
            {
                trigger.Attach(AssociatedObject);
            }
        }

        protected override void OnDetaching()
        {
            foreach (var trigger in this)
            {
                trigger.Detach();
            }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new TriggerCollection();
        }
    }
}
