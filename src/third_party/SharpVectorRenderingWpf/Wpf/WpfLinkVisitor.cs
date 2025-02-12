﻿using System;
using System.Windows.Media;

using SharpVectors.Dom.Svg;

namespace SharpVectors.Renderers.Wpf
{
    public abstract class WpfLinkVisitor : WpfVisitor
    {
        protected WpfLinkVisitor()
        {   
        }

        public abstract bool Aggregates
        {
            get;
        }

        public abstract bool IsAggregate
        {
            get;
        }

        public abstract string AggregatedLayerName
        {
            get;
        }

        public abstract bool Exists(string linkId);

        public abstract void Initialize(DrawingGroup linkGroup, WpfDrawingContext context);

        public abstract void Visit(DrawingGroup group, SvgAElement element, 
            WpfDrawingContext context, float opacity);
    }
}
