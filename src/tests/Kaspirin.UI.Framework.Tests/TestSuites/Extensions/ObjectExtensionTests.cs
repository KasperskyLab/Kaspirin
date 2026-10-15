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

using Kaspirin.UI.Framework.Log.Internals;

namespace Kaspirin.UI.Framework.Tests.TestSuites.Extensions;

[TestClass]
public sealed class ObjectExtensionTests
{
    [TestMethod]
    public void Dump_CheckMaxLogDepth()
    {
        var levelMarker = "FD98B9E7-3EA3-49DB-AE2D-89B4D27FBC46";
        var testDto = new TestClass() { Nested = new TestClass { Nested = new TestClass { Nested = new TestClass { Marker = levelMarker } } } };

        CheckIsInDump(testDto.Dump(), levelMarker);
        CheckIsNotInDump(testDto.Dump(maxDepthLevel: 3), levelMarker);
    }

    [TestMethod]
    public void Dump_CheckCircularReference()
    {
        var badDto = new TestClass();
        badDto.Nested = badDto;

        CheckIsInDump(badDto.Dump(), ObjectDumper.CircularReferenceMarker);
    }

    private void CheckIsInDump(string dump, string marker)
    {
        Assert.IsTrue(dump.Contains(marker), $"dump must contain {marker}. Dump: {dump}");
    }

    private void CheckIsNotInDump(string dump, string marker)
    {
        Assert.IsFalse(dump.Contains(marker), $"dump must not contain {marker}. Dump: {dump}");
    }

    private sealed class TestClass
    {
        public string? Marker { get; set; }
        public TestClass? Nested { get; set; }
    }
}
