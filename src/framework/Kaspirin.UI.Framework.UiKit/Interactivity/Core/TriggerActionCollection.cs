// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 1/10/2024.
// Scope of modification:
//   - Code adaptation to project requirements.

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Interactivity.Core
{
    public class TriggerActionCollection : AttachableCollection<TriggerAction>
    {
        internal TriggerActionCollection()
        {
        }

        internal override void ItemAdded(TriggerAction item)
        {
            if (item.IsHosted)
            {
                throw new InvalidOperationException("Cannot host an instance of a TriggerAction in multiple TriggerCollections simultaneously. Remove it from one TriggerCollection before adding it to another.");
            }

            if (AssociatedObject != null)
            {
                item.Attach(AssociatedObject);
            }

            item.IsHosted = true;
        }

        internal override void ItemRemoved(TriggerAction item)
        {
            Guard.Assert(item.IsHosted, "Item should hosted if it is being removed from a TriggerCollection.");

            if (item.AssociatedObject != null)
            {
                item.Detach();
            }

            item.IsHosted = false;
        }

        protected override void OnAttached()
        {
            foreach (var action in this)
            {
                Guard.Assert(action.IsHosted, "Action must be hosted if it is in the collection.");
                action.Attach(AssociatedObject);
            }
        }

        protected override void OnDetaching()
        {
            foreach (var action in this)
            {
                Guard.Assert(action.IsHosted, "Action must be hosted if it is in the collection.");
                action.Detach();
            }
        }

        protected override Freezable CreateInstanceCore()
        {
            return new TriggerActionCollection();
        }
    }
}
