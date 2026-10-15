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

namespace Kaspirin.UI.Framework.UiKit.Controls;

public sealed class BarCodeVector
{
    public BarCodeVector(short[] vector)
    {
        Guard.ArgumentIsNotNull(vector);

        var length = vector.GetLength(0);

        Guard.Assert((length & 1) == 1, $"Invalid vector length {length}. Only odd vector length is supported.");

        Value = vector;
    }

    public short[] Value { get; }
}
