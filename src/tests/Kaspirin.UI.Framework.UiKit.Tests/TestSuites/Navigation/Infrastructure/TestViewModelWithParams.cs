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

using System.Collections.Generic;
using Kaspirin.UI.Framework.UiKit.Navigation;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Navigation.Infrastructure;

public sealed class TestViewModelWithParams : NavigatableViewModel<TestParameters>
{
    public TestParameters? ReceivedParameters { get; private set; }

    public List<string> InvocationOrder { get; } = new();

    public override void SetParameters(TestParameters parameter)
    {
        ReceivedParameters = parameter;
        InvocationOrder.Add(nameof(SetParameters));
    }

    public override void OnNavigatedTo(NavigationContext context)
    {
        InvocationOrder.Add(nameof(OnNavigatedTo));
    }
}
