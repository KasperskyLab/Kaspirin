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

#if NETFRAMEWORK
using System.Collections.Generic;
using System.Reflection;
#endif

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_Image, Type = typeof(Image))]
    public sealed class Illustration : Control
    {
        public const string PART_Image = "PART_Image";

        public Illustration()
        {
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _image = Guard.EnsureArgumentIsNotNull((Image)GetTemplateChild(PART_Image));

            ApplySource(Source);
        }

        #region Source

        public Enum? Source
        {
            get => (Enum?)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(Enum), typeof(Illustration), new PropertyMetadata(OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var illustrationControl = (Illustration)d;
            var illustration = (Enum?)e.NewValue;

#if NETFRAMEWORK
            EnsureMetadataIsRegistered(illustration);
#endif

            illustrationControl.ApplySource(illustration);
        }

        #endregion

        #region IsUnset

        public bool IsUnset
        {
            get { return (bool)GetValue(IsUnsetProperty); }
            private set { SetValue(_isUnsetPropertyKey, value); }
        }

        private static readonly DependencyPropertyKey _isUnsetPropertyKey =
            DependencyProperty.RegisterReadOnly("IsUnset", typeof(bool), typeof(Illustration),
                new PropertyMetadata(false));

        public static readonly DependencyProperty IsUnsetProperty =
            _isUnsetPropertyKey.DependencyProperty;

        #endregion

        private void ApplySource(Enum? illustration)
        {
            if (_image is null)
            {
                return;
            }

            var uiKitIllustration = illustration is not null
                ? new UIKitIllustrationBuilder(illustration)
                : null;

            IsUnset = uiKitIllustration is null || uiKitIllustration.IllustrationName == UIKitIllustrations.Unset;

            if (IsUnset)
            {
                _image.ClearValue(Image.SourceProperty);
            }
            else
            {
                Guard.IsNotNull(uiKitIllustration!.IllustrationName);

                _image.SetValue(Image.SourceProperty, new ImgExtension()
                {
                    Key = uiKitIllustration.IllustrationKey,
                    Scope = uiKitIllustration.IllustrationScope,
                    Mode = ImgExtensionMode.SvgImage
                }.ProvideValue(_image, Image.SourceProperty));
            }

            var illustrationMetadata = IsUnset ? null : UIKitIllustrationMetadataStorage.Get(illustration!);

            ApplySize(illustrationMetadata);
            ApplyFlowDirection(illustrationMetadata);
        }

        private void ApplySize(UIKitIllustrationMetadata? illustrationMetadata)
        {
            if (_image is null)
            {
                return;
            }

            _image.SetValue(HeightProperty, illustrationMetadata?.Height ?? 0);
            _image.SetValue(WidthProperty, illustrationMetadata?.Width ?? 0);
        }

        private void ApplyFlowDirection(UIKitIllustrationMetadata? illustrationMetadata)
        {
            if (_image is null)
            {
                return;
            }

            if (illustrationMetadata is null)
            {
                _image.ClearValue(FlowDirectionProperty);
            }
            else if (illustrationMetadata.IsAutoRTL)
            {
                _image.SetValue(FlowDirectionProperty, LocalizationManager.Current.DisplayCulture.FlowDirection);
            }
            else
            {
                _image.SetValue(FlowDirectionProperty, FlowDirection.LeftToRight);
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            LocalizationManager.Current.CultureChanged += OnCultureChanged;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            LocalizationManager.Current.CultureChanged -= OnCultureChanged;
        }

        private void OnCultureChanged()
        {
            ApplySource(Source);
        }

#if NETFRAMEWORK
        private static void EnsureMetadataIsRegistered(Enum? illustration)
        {
            if (illustration is null)
            {
                return;
            }

            var illustrationType = illustration.GetType();
            var assemblyName = illustrationType.AssemblyQualifiedName;

            if (_metadataRegistrationCache.Add(assemblyName))
            {
                RegisterMetadata(illustrationType);
            }
        }

        private static void RegisterMetadata(Type illustrationType)
        {
            var registarTypeName = $"{illustrationType.Namespace}.UIKitIllustrationMetadataRegistrar";
            var registarType = illustrationType.Assembly.GetType(registarTypeName);
            Guard.IsNotNull(registarType, $"Failed to find '{registarTypeName}' inside the '{illustrationType.Assembly}' assembly");

            const string methodName = "RegisterMetadata";
            var registerMetadataMethodInfo = registarType.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
            Guard.IsNotNull(registerMetadataMethodInfo, $"Failed to get '{methodName}' method inside the '{registarType.FullName}' type");

            registerMetadataMethodInfo.Invoke(default, default);
        }
#endif

        private Image? _image;

#if NETFRAMEWORK
        private static readonly HashSet<string> _metadataRegistrationCache = new();
#endif
    }
}
