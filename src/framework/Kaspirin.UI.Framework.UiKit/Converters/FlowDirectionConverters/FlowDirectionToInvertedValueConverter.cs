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
using System.Windows.Data;

namespace Kaspirin.UI.Framework.UiKit.Converters.FlowDirectionConverters;

/// <summary>
///     Provides an implementation <see cref="BaseFlowDirectionConverter{T}" /> in which the conversion
///     result is of type <see cref="FlowDirection" />.
/// </summary>
/// <remarks>
///     By default, it converts <see cref="FlowDirection.LeftToRight" /> to <see cref="FlowDirection.RightToLeft" />,
///     and <see cref="FlowDirection.RightToLeft" /> in <see cref="FlowDirection.LeftToRight" />.
/// </remarks>
[ValueConversion(typeof(FlowDirection), typeof(FlowDirection))]
public sealed class FlowDirectionToInvertedValueConverter : BaseFlowDirectionConverter<FlowDirection>
{
    /// <summary>
    ///     Creates an object <see cref="FlowDirectionToInvertedValueConverter" />.
    /// </summary>
    public FlowDirectionToInvertedValueConverter() : base(FlowDirection.RightToLeft, FlowDirection.LeftToRight) { }
}
