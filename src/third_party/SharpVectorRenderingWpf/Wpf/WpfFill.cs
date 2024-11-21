// Copyright © 2024 AO Kaspersky Lab.

// This file has been modified by AO Kaspersky Lab in 1/22/2021.
// Scope of modification:
//   - Code adaptation to project requirements.

using System;
using System.Xml;
using System.Windows;
using System.Windows.Media;

using SharpVectors.Dom.Svg;
using System.Globalization;

namespace SharpVectors.Renderers.Wpf
{
    public enum WpfFillType
    {
        None     = 0,
        Solid    = 1,
        Gradient = 2,
        Pattern  = 3
    }

    public abstract class WpfFill : DependencyObject
    {
        #region Constructors and Destructor

        protected WpfFill()
        {
        }

        #endregion

        #region Public Properties

        public abstract bool IsUserSpace
        {
            get;
        }

        public abstract WpfFillType FillType
        {
            get;
        }

        #endregion

        #region Public Methods

        public abstract Brush GetBrush(Rect elementBounds, WpfDrawingContext context, Transform viewTransform);

        public static WpfFill CreateFill(SvgDocument document, string absoluteUri)
        {
            XmlNode node = document.GetNodeByUri(absoluteUri);

            SvgGradientElement gradientNode = node as SvgGradientElement;
            if (gradientNode != null)
            {
                return new WpfGradientFill(gradientNode);
            }

            SvgPatternElement patternNode = node as SvgPatternElement;
            if (patternNode != null)
            {
                return new WpfPatternFill(patternNode);
            }

            SvgSolidColorElement solidColorNode = node as SvgSolidColorElement;
            if (solidColorNode != null)
            {
                return new WpfSolidColorFill(solidColorNode);
            }

            return null;
        }

        public static Brush CreateViewportBrush(SvgStyleableElement svgElm)
        {
            string prop = svgElm.GetAttribute("viewport-fill");
            string opacity = svgElm.GetAttribute("viewport-fill-opacity"); // no auto-inherit
            if (string.Equals(prop, "inherit", StringComparison.OrdinalIgnoreCase)) // if explicitly defined...
            {
                prop = svgElm.GetPropertyValue("viewport-fill");
            }
            if (string.Equals(opacity, "inherit", StringComparison.OrdinalIgnoreCase)) // if explicitly defined...
            {
                opacity = svgElm.GetPropertyValue("viewport-fill-opacity");
            }
            if (!string.IsNullOrWhiteSpace(prop))
            {
                WpfSvgColor svgColor = new WpfSvgColor(svgElm, "viewport-fill");
                Color color = svgColor.Color;

                if (color.A == 255)
                {
                    double alpha = 255;

                    if (!string.IsNullOrWhiteSpace(opacity))
                    {
                        alpha *= SvgNumber.ParseNumber(opacity);
                    }

                    alpha = Math.Min(alpha, 255);
                    alpha = Math.Max(alpha, 0);

                    color = Color.FromArgb((byte)Convert.ToInt32(alpha), color.R, color.G, color.B);
                }

                var brush = new SolidColorBrush(color);
                double opacityValue = 1;
                if (!string.IsNullOrWhiteSpace(opacity) && double.TryParse(opacity, NumberStyles.Float, NumberFormatInfo.InvariantInfo, out opacityValue))
                {
                    brush.Opacity = opacityValue;
                }

                return brush;
            }
            else
            {
                Color color = Colors.Black; // the default color...
                double alpha = 255;

                if (!string.IsNullOrWhiteSpace(opacity))
                {
                    alpha *= SvgNumber.ParseNumber(opacity);
                }

                alpha = Math.Min(alpha, 255);
                alpha = Math.Max(alpha, 0);

                color = Color.FromArgb((byte)Convert.ToInt32(alpha), color.R, color.G, color.B);

                return new SolidColorBrush(color);
            }
        }

        #endregion
    }
}
