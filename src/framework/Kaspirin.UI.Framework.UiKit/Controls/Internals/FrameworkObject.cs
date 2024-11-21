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
using System.Windows.Markup;

namespace Kaspirin.UI.Framework.UiKit.Controls.Internals
{
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

        public FrameworkElement? FE { get; }
        public FrameworkContentElement? FCE { get; }
        public DependencyObject? DO { get; }

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
                else if (IsFCE)
                {
                    return FCE!.Parent;
                }
                else
                {
                    return null;
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
                else if (IsFCE)
                {
                    return FCE!.TemplatedParent;
                }
                else
                {
                    return null;
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
                else if (IsFCE)
                {
                    return FCE!.Language;
                }
                else
                {
                    return null;
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
                else if (IsFCE)
                {
                    return FCE!.DataContext;
                }
                else
                {
                    return null;
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
                else if (IsFCE)
                {
                    return FCE!.Style;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (IsFE)
                {
                    FE!.Style = value;
                }
                else if (IsFCE)
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
                else if (IsFCE)
                {
                    return FCE!.IsLoaded;
                }
                else
                {
                    return false;
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
                else if (IsFCE)
                {
                    return FCE!.IsInitialized;
                }
                else
                {
                    return false;
                }
            }
        }

        public void WhenLoaded(Action action)
        {
            void OnLoaded(object sender, RoutedEventArgs e)
            {
                new FrameworkObject(Guard.EnsureIsInstanceOfType<DependencyObject>(sender)).Loaded -= OnLoaded;

                action();
            }

            if (IsLoaded)
            {
                action();
            }
            else
            {
                Loaded += OnLoaded;
            }
        }

        public void WhenUnloaded(Action action)
        {
            void OnUnloaded(object sender, RoutedEventArgs e)
            {
                new FrameworkObject(Guard.EnsureIsInstanceOfType<DependencyObject>(sender)).Unloaded -= OnUnloaded;

                action();
            }

            Unloaded += OnUnloaded;
        }

        public void WhenInitialized(Action action)
        {
            void OnInitialized(object? sender, EventArgs e)
            {
                new FrameworkObject(Guard.EnsureIsInstanceOfType<DependencyObject>(sender)).Initialized -= OnInitialized;
                action();
            }

            if (IsInitialized)
            {
                action();
            }
            else
            {
                Initialized += OnInitialized;
            }
        }

        public void RaiseEvent(RoutedEventArgs args)
        {
            if (IsFE)
            {
                FE!.RaiseEvent(args);
            }
            else if (IsFCE)
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
            else if (IsFCE)
            {
                return FCE!.ToString();
            }

            return "Null";
        }

        public event RoutedEventHandler Loaded
        {
            add
            {
                if (IsFE)
                {
                    FE!.Loaded += value;
                }
                else if (IsFCE)
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
                else if (IsFCE)
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
                else if (IsFCE)
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
                else if (IsFCE)
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
                else if (IsFCE)
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
                else if (IsFCE)
                {
                    FCE!.Initialized -= value;
                }
            }
        }
    }
}