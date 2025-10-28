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

namespace Kaspirin.UI.Framework.Tests.TestSuites.Mvvm;

internal sealed class TestViewModel : BaseViewModel
{
    public string? TestStringProperty
    {
        get => _testStringProperty;
        set => SetProperty(ref _testStringProperty, value);
    }

    public bool TestBooleanProperty
    {
        get => _testBooleanProperty;
        set => SetProperty(ref _testBooleanProperty, value);
    }

    public bool OnPropertyChangedCalled { get; private set; }

    public void RaisePropertyChangedForTest(params string[] propertyNames)
        => RaisePropertyChanged(propertyNames);

    public void RaiseAllPropertiesChangedForTest()
        => RaiseAllPropertiesChanged();

    protected override void OnPropertyChanged(string? propertyName)
    {
        OnPropertyChangedCalled = true;
        base.OnPropertyChanged(propertyName);
    }

    private string? _testStringProperty;
    private bool _testBooleanProperty;
}
