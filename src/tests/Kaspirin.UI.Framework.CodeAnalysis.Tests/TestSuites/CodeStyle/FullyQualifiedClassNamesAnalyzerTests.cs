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

using System.Threading.Tasks;
using Kaspirin.UI.Framework.CodeAnalysis.Analyzers.CodeStyle;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kaspirin.UI.Framework.CodeAnalysis.Tests.TestSuites.CodeStyle;

[TestClass]
public sealed class FullyQualifiedClassNamesAnalyzerTests : BaseAnalyzerTests<FullyQualifiedClassNamesAnalyzer>
{
    [TestMethod]
    public async Task FullyQualifiedClassNames_MethodBody_Diagnostics_Find()
    {
        const string source = @"
namespace Sample1
{
    public class Bar
    {
        public System.Int32 Baz(System.Int32 number, System.Collections.Generic.List<int> list)
        {
            return default;
        }
    }
}";

        var expected = new[]
        {
            Diagnostic(DiagnosticRules.FullyQualifiedClassNamesRule)
                .WithSpan(6, 16, 6, 28),
            Diagnostic(DiagnosticRules.FullyQualifiedClassNamesRule)
                .WithSpan(6, 33, 6, 45),
            Diagnostic(DiagnosticRules.FullyQualifiedClassNamesRule)
                .WithSpan(6, 54, 6, 90),
        };

        await VerifyAnalyzerAsync(source, expected);
    }

    [TestMethod]
    public async Task FullyQualifiedClassNames_MethodBody_Diagnostics_None()
    {
        const string source = @"
namespace Sample1
{
    using System;
    using System.Collections.Generic;

    public class Bar
    {
        public int Baz(int number, List<int> list)
        {
            return default;
        }
    }
}";

        await VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task FullyQualifiedClassNames_VariableDeclaration_Diagnostics_Find()
    {
        const string source = @"
namespace Sample1
{
    public class Bar
    {
        public void Foo()
        {
            System.Int32 myInt;
        }
    }
}";

        var expected = new[]
        {
            Diagnostic(DiagnosticRules.FullyQualifiedClassNamesRule)
                .WithSpan(8, 13, 8, 31),
        };

        await VerifyAnalyzerAsync(source, expected);
    }

    [TestMethod]
    public async Task FullyQualifiedClassNames_VariableDeclaration_Diagnostics_None()
    {
        const string source = @"
namespace Sample1
{
    using System;

    public class Bar
    {
        public void Foo()
        {
            int myInt;
        }
    }
}";

        await VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task FullyQualifiedClassNames_ObjectCreation_Diagnostics_Find()
    {
        const string source = @"
namespace Sample1
{
    public class Bar
    {
        public void Foo()
        {
            var foo = new Sample1.Bar();
            var list = new System.Collections.Generic.List<int>();
        }
    }
}";

        var expected = new[]
        {
            Diagnostic(DiagnosticRules.FullyQualifiedClassNamesRule)
                .WithSpan(8, 27, 8, 38),
            Diagnostic(DiagnosticRules.FullyQualifiedClassNamesRule)
                .WithSpan(9, 28, 9, 64),
        };

        await VerifyAnalyzerAsync(source, expected);
    }

    [TestMethod]
    public async Task FullyQualifiedClassNames_ObjectCreation_Diagnostics_None()
    {
        const string source = @"
namespace Sample1
{
    using System.Collections.Generic;

    public class Bar
    {
        public void Foo()
        {
            var foo = new Bar();
            var list = new List<int>();
        }
    }
}";

        await VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task FullyQualifiedClassNames_FieldReference_Diagnostics_Find()
    {
        const string source = @"
namespace Sample1
{
    public class Bar
    {
        public void Foo()
        {
            var maxValue = System.Int32.MaxValue;
        } 
    }
}";

        var expected = new[]
        {
            Diagnostic(DiagnosticRules.FullyQualifiedClassNamesRule)
                .WithSpan(8, 28, 8, 49),
        };

        await VerifyAnalyzerAsync(source, expected);
    }

    [TestMethod]
    public async Task FullyQualifiedClassNames_FieldReference_Diagnostics_None()
    {
        const string source = @"
namespace Sample1
{
    public class Bar
    {
        public void Foo()
        {
            var maxValue = int.MaxValue;
        } 
    }
}";

        await VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task FullyQualifiedClassNames_Invocation_Arguments_Diagnostics_Find()
    {
        const string source = @"
namespace Sample
{
    using System.Collections.Generic;

    public class MyClass
    {
        public void Bar<T1, T2>()
        {
        }

        public void Foo() 
        {
            Bar<List<List<System.Int32>>, System.Int32>();
        }
    }
}";

        var expected = new[]
        {
            Diagnostic(DiagnosticRules.FullyQualifiedClassNamesRule)
                .WithSpan(14, 27, 14, 39),
            Diagnostic(DiagnosticRules.FullyQualifiedClassNamesRule)
                .WithSpan(14, 43, 14, 55),
        };

        await VerifyAnalyzerAsync(source, expected);
    }

    [TestMethod]
    public async Task FullyQualifiedClassNames_Invocation_Arguments_Diagnostics_None()
    {
        const string source = @"
namespace Sample
{
    using System.Collections.Generic;

    public class MyClass
    {
        public void Bar<T1, T2>()
        {
        }

        public void Foo() 
        {
            Bar<List<List<int>>, int>();
        }
    }
}";

        await VerifyAnalyzerAsync(source);
    }

    [TestMethod]
    public async Task FullyQualifiedClassNames_Invocation_Invoke_Diagnostics_Find_1()
    {
        const string source = @"
namespace Sample
{
    public class MyClass
    {
        public static void Foo() 
        {
        }

        public static void Bar() 
        {
            Sample.MyClass.Foo();
        }
    }
}";

        var expected = new[]
        {
            Diagnostic(DiagnosticRules.FullyQualifiedClassNamesRule)
                .WithSpan(12, 13, 12, 31),
        };

        await VerifyAnalyzerAsync(source, expected);
    }

    [TestMethod]
    public async Task FullyQualifiedClassNames_Invocation_Invoke_Diagnostics_Find_2()
    {
        const string source = @"
namespace Sample
{
    using System.Collections.Generic;

    public class MyClass
    {
        public static void Bar<T1, T2>()
        {
        }

        public void Foo() 
        {
            Sample.MyClass.Bar<List<List<System.Int32>>, System.Int32>();
        }
    }
}";

        var expected = new[]
        {
            Diagnostic(DiagnosticRules.FullyQualifiedClassNamesRule)
                .WithSpan(14, 13, 14, 71),
            Diagnostic(DiagnosticRules.FullyQualifiedClassNamesRule)
                .WithSpan(14, 42, 14, 54),
            Diagnostic(DiagnosticRules.FullyQualifiedClassNamesRule)
                .WithSpan(14, 58, 14, 70),
        };

        await VerifyAnalyzerAsync(source, expected);
    }

    [TestMethod]
    public async Task FullyQualifiedClassNames_Invocation_Invoke_Diagnostics_Find_3()
    {
        const string source = @"
namespace Sample
{
    using System.Collections.Generic;

    public class MyClass
    {
        public static void Bar<T1>()
        {
        }

        public void Foo() 
        {
            MyClass.Bar<Sample.MyClass>();
        }
    }
}";

        var expected = new[]
        {
            Diagnostic(DiagnosticRules.FullyQualifiedClassNamesRule)
                .WithSpan(14, 25, 14, 39),
        };

        await VerifyAnalyzerAsync(source, expected);
    }

    [TestMethod]
    public async Task FullyQualifiedClassNames_Invocation_Invoke_Diagnostics_None()
    {
        const string source = @"
namespace Sample
{
    public class MyClass
    {
        public static void Foo() 
        {
        }

        public static void Bar() 
        {
            Foo();
        }
    }
}";

        await VerifyAnalyzerAsync(source);
    }
}
