// Copyright © 2025 AO Kaspersky Lab.
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
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using Kaspirin.UI.Framework.UiKit.Controls.Internals;

namespace Kaspirin.UI.Framework.UiKit.Controls;

[TemplatePart(Name = PART_PathContainer, Type = typeof(Path))]
public sealed class BarCode : Control
{
    public const string PART_PathContainer = "PART_PathContainer";

    #region Vector

    public BarCodeVector Vector
    {
        get => (BarCodeVector)GetValue(VectorProperty);
        set => SetValue(VectorProperty, value);
    }

    public static readonly DependencyProperty VectorProperty = DependencyProperty.Register(
        nameof(Vector),
        typeof(BarCodeVector),
        typeof(BarCode),
        new PropertyMetadata(default(BarCodeVector)));

    #endregion

    public override void OnApplyTemplate()
    {
        _barRath = (Path)GetTemplateChild(PART_PathContainer);
        _barRath.SetBinding(Path.DataProperty, new Binding()
        {
            Source = this,
            Path = VectorProperty.AsPath(),
            Converter = new DelegateConverter<BarCodeVector>(CreateBarCodeGeometry)
        });
    }

    private static StreamGeometry? CreateBarCodeGeometry(BarCodeVector? data)
    {
        var vector = data?.Value;
        if (vector == null)
        {
            return null;
        }

        var length = vector.GetLength(0);

        var barCodeStreamGeometry = new StreamGeometry()
        {
            FillRule = FillRule.EvenOdd
        };

        using var context = barCodeStreamGeometry.Open();

        var offsetX = 0;
        var offsetY = 0;

        for (var i = 0; i < length; i++)
        {
            var barWidth = vector[i];
            var barHeight = 1;

            var barRect = new Int32Rect(
                x: offsetX,
                y: offsetY,
                width: barWidth,
                height: barHeight);

            if ((i & 1) == 0)
            {
                //draw only odd bars
                DrawRectGeometry(context, barRect);
            }

            offsetX += barWidth;
        }

        barCodeStreamGeometry.Freeze();

        return barCodeStreamGeometry;
    }

    private static void DrawRectGeometry(StreamGeometryContext context, Int32Rect rect)
    {
        if (rect.IsEmpty)
        {
            return;
        }

        context.BeginFigure(new Point(rect.X, rect.Y), true, true);
        context.LineTo(new Point(rect.X, rect.Y + rect.Height), false, false);
        context.LineTo(new Point(rect.X + rect.Width, rect.Y + rect.Height), false, false);
        context.LineTo(new Point(rect.X + rect.Width, rect.Y), false, false);
    }

    private Path? _barRath;
}
