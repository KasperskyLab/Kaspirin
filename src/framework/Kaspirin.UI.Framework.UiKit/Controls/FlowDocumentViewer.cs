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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    public sealed class FlowDocumentViewer : FlowDocumentScrollViewer
    {
        static FlowDocumentViewer()
        {
            MaxZoomProperty.OverrideMetadata(typeof(FlowDocumentViewer),
                new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.None, null, CoerceZoom));
            MinZoomProperty.OverrideMetadata(typeof(FlowDocumentViewer),
                new FrameworkPropertyMetadata(100.0, FrameworkPropertyMetadataOptions.None, null, CoerceZoom));

            EventManager.RegisterClassHandler(
                typeof(FlowDocumentViewer), RequestBringIntoViewEvent, new RequestBringIntoViewEventHandler(OnRequestBringIntoView));
        }

        private static object CoerceZoom(DependencyObject d, object baseValue)
        {
            return 100.0;
        }

        private static void OnRequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
        {
            // If the FlowDocumentViewer is in an outer ScrollViewer, then that ScrollViewer will handle the event and set e.Handled.
            // Otherwise, scrolling in the outer ScrollViewer will only work to the document boundaries, not to the element in focus.
            var flowDocumentViewer = (FlowDocumentViewer)sender;
            (VisualTreeHelper.GetParent(flowDocumentViewer) as UIElement)?.RaiseEvent(e);
        }

        public bool HandlesMouseWheelScrolling { get; set; } = true;

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control)
                || HandlesMouseWheelScrolling)
            {
                base.OnMouseWheel(e);
            }
        }

        protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
        {
            base.OnPreviewMouseWheel(e);

            e.Handled = true;

            RaiseEvent(new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta)
            {
                RoutedEvent = MouseWheelEvent,
                Source = this
            });
        }
    }
}
