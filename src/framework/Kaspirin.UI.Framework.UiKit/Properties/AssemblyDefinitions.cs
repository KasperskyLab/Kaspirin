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

using System.Windows.Markup;

#if NET6_0_OR_GREATER
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/uikit/compatibility/actual", "Kaspirin.UI.Framework.UiKit.Compatibility.Actual")]
#else
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/uikit/compatibility/fw40", "Kaspirin.UI.Framework.UiKit.Compatibility.Fw40")]
#endif

[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/uikit", "Kaspirin.UI.Framework.UiKit")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Accessibility.TextScale")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Animation.Markup")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Animation.Markup.Easing")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Controls")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Controls.Behaviors")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Controls.Internals")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Controls.Properties")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Controls.Selectors")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Controls.VisualStates")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Converters.BooleanConverters")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Converters.CollectionConverters")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Converters.CornerRadiusConverters")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Converters.DictionaryConverters")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Converters.EqualityConverters")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Converters.NumberConverters")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Converters.ThicknessConverters")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Converters.TimeConverters")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Converters.TimeConverters.Validation")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Effects")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Fonts")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Icons")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Input.Filter")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Input.Focus")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Input.Shortcuts")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Interactivity")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Interactivity.Actions")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Interactivity.Core")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.MarkupExtensions.DataBinding")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.MarkupExtensions.ItemIndex")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.MarkupExtensions")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Navigation")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Notifications")]
[assembly: XmlnsDefinition("http://schemas.kaspirin.com/common/visuals", "Kaspirin.UI.Framework.UiKit.Windows")]

[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "Kaspirin.UI.Framework.UiKit.MarkupExtensions.Common")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "Kaspirin.UI.Framework.UiKit.Localization.Markup.Converting")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "Kaspirin.UI.Framework.UiKit.Localization.Markup.Extensions.Files")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "Kaspirin.UI.Framework.UiKit.Localization.Markup.Extensions.Images")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "Kaspirin.UI.Framework.UiKit.Localization.Markup.Extensions.Xaml")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "Kaspirin.UI.Framework.UiKit.Localization.Markup.Extensions.Strings")]

[assembly: XmlnsPrefix("http://schemas.kaspirin.com/common/uikit", "uikit")]
[assembly: XmlnsPrefix("http://schemas.kaspirin.com/common/visuals", "visuals")]