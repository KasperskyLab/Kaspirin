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

using Kaspirin.UI.Framework.Mvvm;

namespace Kaspirin.UI.Framework.UiKit.Controls
{
    internal sealed class DateTimePopupItem : ViewModelBase
    {
        public static DateTimePopupItem Empty { get; } = new DateTimePopupItem(0, "Empty") { IsVisible = false };
        public static DateTimePopupItem Stub { get; } = new DateTimePopupItem(0, "Stub") { IsVisible = false };

        public DateTimePopupItem(int value, string? displayText = null)
        {
            _value = value;
            _displayText = displayText ?? _value.ToString() ?? string.Empty;
            _isVisible = true;
        }

        public int Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    RaisePropertyChanged(nameof(Value));
                }
            }
        }

        public string DisplayText
        {
            get { return _displayText; }
            set
            {
                if (_displayText != value)
                {
                    _displayText = value;
                    RaisePropertyChanged(nameof(DisplayText));
                }
            }
        }

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    RaisePropertyChanged(nameof(IsVisible));
                }
            }
        }

        private string _displayText;
        private int _value;
        private bool _isVisible;
    }
}
