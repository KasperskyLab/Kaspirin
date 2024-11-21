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

using Kaspirin.UI.Framework.UiKit.Controls.Internals;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    [TemplatePart(Name = PART_PathContainer, Type = typeof(Path))]
    public sealed class QrCode : Control
    {
        public const string PART_PathContainer = "PART_PathContainer";

        #region Matrix

        public QrCodeMatrix Matrix
        {
            get { return (QrCodeMatrix)GetValue(MatrixProperty); }
            set { SetValue(MatrixProperty, value); }
        }

        public static readonly DependencyProperty MatrixProperty =
            DependencyProperty.Register("Matrix", typeof(QrCodeMatrix), typeof(QrCode));

        #endregion

        public override void OnApplyTemplate()
        {
            _qrRath = (Path)GetTemplateChild(PART_PathContainer);
            _qrRath.SetBinding(Path.DataProperty, new Binding()
            {
                Source = this,
                Path = MatrixProperty.AsPath(),
                Converter = new DelegateConverter<QrCodeMatrix>(CreateQrCodeGeometry)
            });
        }

        private static StreamGeometry? CreateQrCodeGeometry(QrCodeMatrix? data)
        {
            var matrix = data?.Value;
            if (matrix == null)
            {
                return null;
            }

            var rows = matrix.GetLength(0);
            var columns = matrix.GetLength(1);

            var qrCodeStreamGeometry = new StreamGeometry()
            {
                FillRule = FillRule.EvenOdd
            };

            using var context = qrCodeStreamGeometry.Open();

            int? startingColumn = null;

            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    if (matrix[row, column])
                    {
                        if (startingColumn == null)
                        {
                            // Initialize starting column for current foreground color module.
                            startingColumn = column;
                        }

                        // If this is the last foreground color module in current row, draw rectangle.
                        if (column == columns - 1)
                        {
                            DrawRectGeometry(context, new Int32Rect(startingColumn.Value, row, column - startingColumn.Value + 1, 1));
                            startingColumn = null;
                        }
                    }
                    else if (!matrix[row, column] && startingColumn != null)
                    {
                        // This is the first background color module after a sequence of foreground color modules.
                        // Draw rectangle for this sequence of foreground modules.
                        DrawRectGeometry(context, new Int32Rect(startingColumn.Value, row, column - startingColumn.Value, 1));
                        startingColumn = null;
                    }
                }
            }

            qrCodeStreamGeometry.Freeze();

            return qrCodeStreamGeometry;
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

        private Path? _qrRath;
    }
}
