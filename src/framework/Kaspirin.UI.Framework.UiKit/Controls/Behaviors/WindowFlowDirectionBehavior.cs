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

using System.Windows;

namespace Kaspirin.UI.Framework.UiKit.Controls.Behaviors
{
    public sealed class WindowFlowDirectionBehavior : Behavior<Window, WindowFlowDirectionBehavior>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            LocalizationManager.Current.CultureChanged -= OnCultureChanged;
            LocalizationManager.Current.CultureChanged += OnCultureChanged;

            ApplyFlowDirection();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            LocalizationManager.Current.CultureChanged -= OnCultureChanged;
        }

        protected override void OnAssociatedObjectLoaded()
        {
            base.OnAssociatedObjectLoaded();

            LocalizationManager.Current.CultureChanged -= OnCultureChanged;
            LocalizationManager.Current.CultureChanged += OnCultureChanged;

            ApplyFlowDirection();
        }

        protected override void OnAssociatedObjectUnloaded()
        {
            base.OnAssociatedObjectLoaded();

            LocalizationManager.Current.CultureChanged -= OnCultureChanged;
        }

        private void OnCultureChanged()
        {
            ApplyFlowDirection();
        }

        private void ApplyFlowDirection()
        {
            Guard.IsNotNull(AssociatedObject);

            var displayCulture = LocalizationManager.Current.DisplayCulture;

            var flowDirectionInfo = new FlowDirectionInfo(
                displayCulture.CultureInfo.TextInfo.IsRightToLeft,
                displayCulture.XmlLanguage.IetfLanguageTag);

            if (flowDirectionInfo == _currentFlowDirectionInfo)
            {
                return;
            }

            _currentFlowDirectionInfo = flowDirectionInfo;

            AssociatedObject.Language = displayCulture.XmlLanguage;
            AssociatedObject.FlowDirection = displayCulture.CultureInfo.TextInfo.IsRightToLeft
                ? FlowDirection.RightToLeft
                : FlowDirection.LeftToRight;

            AssociatedObject.UpdateLayout();

            var windowId = WindowIdFactory.GetDefaultWindowId(AssociatedObject);

            _tracer.TraceInformation($"Layout updated for {windowId}. FlowDirection: {AssociatedObject.FlowDirection}, Language: {AssociatedObject.Language}.");
        }

        private readonly record struct FlowDirectionInfo(bool IsRTL, string Language);

        private FlowDirectionInfo? _currentFlowDirectionInfo;

        private static readonly ComponentTracer _tracer = ComponentTracer.Get(nameof(WindowFlowDirectionBehavior));
    }
}