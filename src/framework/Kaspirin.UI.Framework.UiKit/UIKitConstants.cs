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
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Kaspirin.UI.Framework.UiKit
{
    internal static class UIKitConstants
    {
        //RefTypes:     public static PropClass PropName => new PropClass();
        //ValueTypes:   public static Thickness PropName { get; } = new Thickness(10);
        //strings:      public static string PropName { get; } = "Value";

        //Internal
        public static string IconsScope { get; } = "Svg";
        public static string PaletteScope { get; } = "Palette";
        public static string LocalizationScope { get; } = "UiKit";

        //BadgeCounter
        public static int BadgeCounterMaxCounter { get; } = 100;

        //Bullet
        public static UIKitIcon_12 BulletIcon { get; } = UIKitIcon_12.Check;

        //Button
        public static double ButtonMinWidth { get; } = 80;
        public static double ButtonGhostMinWidth { get; } = 0;

        //CarouselItem
        public static AnimationProperties CarouselItemAnimationProperties => new()
        {
            Duration = TimeSpan.FromMilliseconds(300),
            Easing = new ExponentialEase() { Exponent = 4, EasingMode = EasingMode.EaseInOut }
        };
        public static Thickness CarouselItemMargin { get; } = new Thickness(4, 0, 4, 0);

        //ChipsItem
        public static Thickness ChipsItemMargin { get; } = new Thickness(0, 6, 8, 6);
        public static int ChipsItemMaxCounter { get; } = 100;

        //ContextMenuPopupDecorator
        public static double ContextMenuPopupDecoratorMaxWidth { get; } = 350;
        public static double ContextMenuPopupDecoratorMinWidth { get; } = 80;

        //DateTimeInput
        public static double DateTimeInputWidth { get; } = 140;

        //ImageGalleryList
        public static AnimationProperties ImageGalleryListAnimationProperties => new()
        {
            Duration = TimeSpan.FromMilliseconds(600),
            Easing = new ExponentialEase() { Exponent = 3, EasingMode = EasingMode.EaseInOut }
        };

        //InteractivityDialog
        public static Thickness InteractivityDialogMargin { get; } = new Thickness(12);

        //InteractivityOverlay
        public static Thickness InteractivityOverlayClipExtent { get; } = new Thickness(4);
        public static double InteractivityOverlayClipCornerRadius { get; } = 6;

        //ListMenu
        public static Thickness ListMenuPadding { get; } = new Thickness(4);

        //MenuItem
        public static double MenuItemIconHeight { get; } = 16;
        public static double MenuItemIconWidth { get; } = 16;

        //PasswordInput
        public static int PasswordInputMaxLength { get; } = 256;

        //Popup
        public static double PopupMaxWidth { get; } = 350;

        //PopupDecorator
        public static double PopupDecoratorOffset { get; } = 4;

        //QrCode
        public static Brush QrCodeBackground => Brushes.White;
        public static Brush QrCodeForeground => Brushes.Black;

        //ScrollBar
        public static double ScrollBarMinLength { get; } = 80;

        //ScrollViewer
        public static double ScrollViewerBorderFadeLength { get; } = 32;
        public static GradientStopCollection ScrollViewerBorderFadeGradient => new()
        {
            new GradientStop() { Color = Colors.Black, Offset = 0 },
            new GradientStop() { Color = Colors.Transparent, Offset = 1 }
        };

        //Search
        public static double SearchWidth { get; } = 150;

        //SelectPopupDecorator
        public static double SelectPopupDecoratorMinHeight { get; } = 80;

        //TabMenuItem
        public static int TabMenuItemMaxCounter { get; } = 100;

        //ToolTip
        public static double ToolTipMaxWidth { get; } = 350;
        public static double ToolTipMaxHeight { get; } = 550;
    }
}
