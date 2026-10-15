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
using System.Threading.Tasks;
using Kaspirin.UI.Framework.CodeAnalysis.Analyzers.Extensions.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kaspirin.UI.Framework.CodeAnalysis.Tests.TestSuites.Extensions;

[TestClass]
public sealed class CollectionExtensionAddRangeAnalyzerTests : BaseAnalyzerTests<CollectionExtensionAddRangeAnalyzer>
{
    [TestMethod]
    public async Task AddRangeCalledOnArray_ShouldReportDiagnostic()
    {
        const string testCode = @"
using System.Collections.Generic;
using Kaspirin.UI.Framework.Extensions.Collections;

namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod()
        {
            int[] array = { 1, 2, 3 };
            var items = new List<int> { 4, 5, 6 };
            array.AddRange(items);
        }
    }
}
";

        var expected = new[]
        {
            Diagnostic(DiagnosticRules.CollectionExtensionAddRangeRule)
                .WithSpan(13, 19, 13, 27)
                .WithArguments(nameof(CollectionExtension.AddRange))
        };

        await VerifyAnalyzerAsync(testCode, expected);
    }

    [TestMethod]
    public async Task AddRangeCalledOnTypedArray_ShouldReportDiagnostic()
    {
        const string testCode = @"
using System.Collections.Generic;
using Kaspirin.UI.Framework.Extensions.Collections;

namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod()
        {
            string[] array = { ""a"", ""b"", ""c"" };
            var items = new List<string> { ""d"", ""e"", ""f"" };
            array.AddRange(items);
        }
    }
}
";

        var expected = new[]
        {
            Diagnostic(DiagnosticRules.CollectionExtensionAddRangeRule)
                .WithSpan(13, 19, 13, 27)
                .WithArguments(nameof(CollectionExtension.AddRange))
        };

        await VerifyAnalyzerAsync(testCode, expected);
    }

    [TestMethod]
    public async Task AddRangeCalledOnList_ShouldNotReportDiagnostic()
    {
        const string testCode = @"
using System.Collections.Generic;
using Kaspirin.UI.Framework.Extensions.Collections;

namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod()
        {
            var list = new List<int> { 1, 2, 3 };
            var items = new List<int> { 4, 5, 6 };
            list.AddRange(items);
        }
    }
}
";

        await VerifyAnalyzerAsync(testCode);
    }

    [TestMethod]
    public async Task AddRangeCalledOnCollection_ShouldNotReportDiagnostic()
    {
        const string testCode = @"
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Kaspirin.UI.Framework.Extensions.Collections;

namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod()
        {
            Collection<int> collection = new Collection<int> { 1, 2, 3 };
            var items = new List<int> { 4, 5, 6 };
            collection.AddRange(items);
        }
    }
}
";

        await VerifyAnalyzerAsync(testCode);
    }

    [TestMethod]
    public async Task AddRangeCalledOnImplicitlyTypedArray_ShouldReportDiagnostic()
    {
        const string testCode = @"
using System.Collections.Generic;
using Kaspirin.UI.Framework.Extensions.Collections;

namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod()
        {
            var array = new[] { 1, 2, 3 };
            var items = new List<int> { 4, 5, 6 };
            array.AddRange(items);
        }
    }
}
";

        var expected = new[]
        {
            Diagnostic(DiagnosticRules.CollectionExtensionAddRangeRule)
                .WithSpan(13, 19, 13, 27)
                .WithArguments(nameof(CollectionExtension.AddRange))
        };

        await VerifyAnalyzerAsync(testCode, expected);
    }

    protected override Type[]? GetAdditionalTypeRefs()
    {
        return new[] { typeof(CollectionExtension) };
    }
}