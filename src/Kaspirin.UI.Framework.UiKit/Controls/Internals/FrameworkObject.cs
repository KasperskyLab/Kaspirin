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
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Data;
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals;

internal sealed class FrameworkObject
{
    public FrameworkObject(DependencyObject dependencyObject)
    {
        DO = dependencyObject;

        if (dependencyObject is FrameworkElement fe)
        {
            FE = fe;
            FCE = null;
        }
        else if (dependencyObject is FrameworkContentElement fce)
        {
            FE = null;
            FCE = fce;
        }
        else
        {
            Guard.Fail("DependencyObject must be FrameworkElement or FrameworkContentElement");
        }
    }

    public static AutomationPeer CreatePeerForElement(DependencyObject dependencyObject)
    {
        if (dependencyObject is FrameworkElement fe)
        {
            return FrameworkElementAutomationPeer.CreatePeerForElement(fe);
        }
        else if (dependencyObject is FrameworkContentElement fce)
        {
            return FrameworkContentElementAutomationPeer.CreatePeerForElement(fce);
        }
        else
        {
            throw new ArgumentException("DependencyObject must be FrameworkElement or FrameworkContentElement", nameof(dependencyObject));
        }
    }

    public FrameworkElement? FE { get; }
    public FrameworkContentElement? FCE { get; }
    public DependencyObject DO { get; }

    public bool IsFE => FE != null;
    public bool IsFCE => FCE != null;
    public bool IsValid => FE != null || FCE != null;

    public DependencyObject? Parent
    {
        get
        {
            if (IsFE)
            {
                return FE!.Parent;
            }
            else // if (IsFCE)
            {
                return FCE!.Parent;
            }
        }
    }

    public DependencyObject? TemplatedParent
    {
        get
        {
            if (IsFE)
            {
                return FE!.TemplatedParent;
            }
            else // if (IsFCE)
            {
                return FCE!.TemplatedParent;
            }
        }
    }

    public XmlLanguage? Language
    {
        get
        {
            if (IsFE)
            {
                return FE!.Language;
            }
            else // if (IsFCE)
            {
                return FCE!.Language;
            }
        }
    }

    public object? DataContext
    {
        get
        {
            if (IsFE)
            {
                return FE!.DataContext;
            }
            else // if (IsFCE)
            {
                return FCE!.DataContext;
            }
        }
    }

    public Style? Style
    {
        get
        {
            if (IsFE)
            {
                return FE!.Style;
            }
            else // if (IsFCE)
            {
                return FCE!.Style;
            }
        }
        set
        {
            if (IsFE)
            {
                FE!.Style = value;
            }
            else // if (IsFCE)
            {
                FCE!.Style = value;
            }
        }
    }

    public bool IsLoaded
    {
        get
        {
            if (IsFE)
            {
                return FE!.IsLoaded;
            }
            else // if (IsFCE)
            {
                return FCE!.IsLoaded;
            }
        }
    }

    public bool IsInitialized
    {
        get
        {
            if (IsFE)
            {
                return FE!.IsInitialized;
            }
            else // if (IsFCE)
            {
                return FCE!.IsInitialized;
            }
        }
    }

    public bool Focusable
    {
        get
        {
            if (IsFE)
            {
                return FE!.Focusable;
            }
            else // if (IsFCE)
            {
                return FCE!.Focusable;
            }
        }
    }

    public Style? FocusVisualStyle
    {
        get
        {
            if (IsFE)
            {
                return FE!.FocusVisualStyle;
            }
            else // if (IsFCE)
            {
                return FCE!.FocusVisualStyle;
            }
        }
    }

    public void RaiseEvent(RoutedEventArgs args)
    {
        if (IsFE)
        {
            FE!.RaiseEvent(args);
        }
        else // if (IsFCE)
        {
            FCE!.RaiseEvent(args);
        }
    }

    public override string? ToString()
    {
        if (IsFE)
        {
            return FE!.ToString();
        }
        else // if (IsFCE)
        {
            return FCE!.ToString();
        }
    }

    public object FindResource(string resourceKey)
    {
        if (IsFE)
        {
            return FE!.FindResource(resourceKey);
        }
        else // if (IsFCE)
        {
            return FCE!.FindResource(resourceKey);
        }
    }

    public object? GetValue(DependencyProperty property)
    {
        if (IsFE)
        {
            return FE!.GetValue(property);
        }
        else // if (IsFCE)
        {
            return FCE!.GetValue(property);
        }
    }

    public TValue? GetValue<TValue>(DependencyProperty property)
    {
        if (IsFE)
        {
            return FE!.GetValue<TValue>(property);
        }
        else // if (IsFCE)
        {
            return FCE!.GetValue<TValue>(property);
        }
    }

    public void SetValue(DependencyProperty property, object? value)
    {
        if (IsFE)
        {
            FE!.SetValue(property, value);
        }
        else // if (IsFCE)
        {
            FCE!.SetValue(property, value);
        }
    }

    public void SetBinding(DependencyProperty property, BindingBase binding)
    {
        if (IsFE)
        {
            FE!.SetBinding(property, binding);
        }
        else // if (IsFCE)
        {
            FCE!.SetBinding(property, binding);
        }
    }

    public event RoutedEventHandler Loaded
    {
        add
        {
            if (IsFE)
            {
                FE!.Loaded += value;
            }
            else // if (IsFCE)
            {
                FCE!.Loaded += value;
            }
        }
        remove
        {
            if (IsFE)
            {
                FE!.Loaded -= value;
            }
            else // if (IsFCE)
            {
                FCE!.Loaded -= value;
            }
        }
    }

    public event RoutedEventHandler Unloaded
    {
        add
        {
            if (IsFE)
            {
                FE!.Unloaded += value;
            }
            else // if (IsFCE)
            {
                FCE!.Unloaded += value;
            }
        }
        remove
        {
            if (IsFE)
            {
                FE!.Unloaded -= value;
            }
            else // if (IsFCE)
            {
                FCE!.Unloaded -= value;
            }
        }
    }

    public event EventHandler Initialized
    {
        add
        {
            if (IsFE)
            {
                FE!.Initialized += value;
            }
            else // if (IsFCE)
            {
                FCE!.Initialized += value;
            }
        }
        remove
        {
            if (IsFE)
            {
                FE!.Initialized -= value;
            }
            else // if (IsFCE)
            {
                FCE!.Initialized -= value;
            }
        }
    }
}