// Copyright © 2026 AO Kaspersky Lab.
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
using Kaspirin.UI.Framework.UiKit.Navigation;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Navigation.Infrastructure;

public sealed class TestViewModel : NavigatableViewModel
{
    public Action<NavigationContext>? NavigatedToCallback { get; set; }

    public Action<NavigationContext>? NavigatedFromCallback { get; set; }

    public Action<NavigationOptionsBuilder>? BuildOptionsCallback { get; set; }

    public override void OnNavigatedTo(NavigationContext context) => NavigatedToCallback?.Invoke(context);

    public override void OnNavigatedFrom(NavigationContext context) => NavigatedFromCallback?.Invoke(context);

    public override void BuildOptions(NavigationOptionsBuilder builder) => BuildOptionsCallback?.Invoke(builder);
}
