// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 1/10/2024.
// Scope of modification:
//   - Code adaptation to project requirements.

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Interactivity.Core
{
    public abstract class Behavior<T> : Behavior where T : DependencyObject
    {
        protected Behavior()
            : base(typeof(T))
        {
        }

        public new T? AssociatedObject => Guard.EnsureIsInstanceOfType<T?>(base.AssociatedObject);
    }
}
