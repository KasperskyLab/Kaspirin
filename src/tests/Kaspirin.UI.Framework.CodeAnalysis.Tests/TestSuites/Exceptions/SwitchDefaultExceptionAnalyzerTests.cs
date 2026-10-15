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
using Kaspirin.UI.Framework.CodeAnalysis.Analyzers.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kaspirin.UI.Framework.CodeAnalysis.Tests.TestSuites.Exceptions;

[TestClass]
public sealed class SwitchDefaultExceptionAnalyzerTests : BaseAnalyzerTests<SwitchDefaultExceptionAnalyzer>
{
    [TestMethod]
    public async Task SwitchDefaultThrowsUnexpectedValueException_ShouldNotReportDiagnostic()
    {
        const string testCode = @"
using Kaspirin.UI.Framework.Exceptions;

namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod(int value)
        {
            switch (value)
            {
                case 1:
                    break;
                case 2:
                    break;
                default:
                    throw new UnexpectedValueException(value);
            }
        }
    }
}
";

        await VerifyAnalyzerAsync(testCode);
    }

    [TestMethod]
    public async Task SwitchDefaultThrowsArgumentException_ShouldReportDiagnostic()
    {
        const string testCode = @"
using System;

namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod(int value)
        {
            switch (value)
            {
                case 1:
                    break;
                case 2:
                    break;
                default:
                    throw new ArgumentException(""Invalid value"");
            }
        }
    }
}
";

        var expected = new[]
        {
            Diagnostic(DiagnosticRules.SwitchDefaultExceptionRule)
                .WithSpan(17, 27, 17, 65)
        };

        await VerifyAnalyzerAsync(testCode, expected);
    }

    [TestMethod]
    public async Task SwitchDefaultThrowsInvalidOperationException_ShouldReportDiagnostic()
    {
        const string testCode = @"
using System;

namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod(int value)
        {
            switch (value)
            {
                case 1:
                    break;
                case 2:
                    break;
                default:
                    throw new InvalidOperationException(""Invalid state"");
            }
        }
    }
}
";
        var expected = new[]
        {
            Diagnostic(DiagnosticRules.SwitchDefaultExceptionRule)
                .WithSpan(17, 27, 17, 73)
        };

        await VerifyAnalyzerAsync(testCode, expected);
    }

    [TestMethod]
    public async Task SwitchDefaultThrowsException_ShouldReportDiagnostic()
    {
        const string testCode = @"
using System;

namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod(int value)
        {
            switch (value)
            {
                case 1:
                    break;
                case 2:
                    break;
                default:
                    throw new Exception(""Something went wrong"");
            }
        }
    }
}
";

        var expected = new[]
        {
            Diagnostic(DiagnosticRules.SwitchDefaultExceptionRule)
                .WithSpan(17, 27, 17, 64)
        };

        await VerifyAnalyzerAsync(testCode, expected);
    }

    [TestMethod]
    public async Task SwitchDefaultReturnsValue_ShouldNotReportDiagnostic()
    {
        const string testCode = @"
namespace TestNamespace
{
    public class TestClass
    {
        public int TestMethod(int value)
        {
            switch (value)
            {
                case 1:
                    return 1;
                case 2:
                    return 2;
                default:
                    return 0;
            }
        }
    }
}
";

        await VerifyAnalyzerAsync(testCode);
    }

    [TestMethod]
    public async Task SwitchDefaultEmpty_ShouldNotReportDiagnostic()
    {
        const string testCode = @"
namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod(int value)
        {
            switch (value)
            {
                case 1:
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }
    }
}
";

        await VerifyAnalyzerAsync(testCode);
    }

    [TestMethod]
    public async Task SwitchDefaultNoThrow_ShouldNotReportDiagnostic()
    {
        const string testCode = @"
namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod(int value)
        {
            switch (value)
            {
                case 1:
                    break;
                case 2:
                    break;
                default:
                    System.Console.WriteLine(""Default"");
                    break;
            }
        }
    }
}
";

        await VerifyAnalyzerAsync(testCode);
    }

    [TestMethod]
    public async Task SwitchExpressionDefaultThrowsUnexpectedValueException_ShouldNotReportDiagnostic()
    {
        const string testCode = @"
using Kaspirin.UI.Framework.Exceptions;

namespace TestNamespace
{
    public class TestClass
    {
        public int TestMethod(int value)
        {
            return value switch
            {
                1 => 1,
                2 => 2,
                _ => throw new UnexpectedValueException(value)
            };
        }
    }
}
";

        await VerifyAnalyzerAsync(testCode);
    }

    [TestMethod]
    public async Task SwitchExpressionDefaultThrowsArgumentException_ShouldReportDiagnostic()
    {
        const string testCode = @"
using System;

namespace TestNamespace
{
    public class TestClass
    {
        public int TestMethod(int value)
        {
            return value switch
            {
                1 => 1,
                2 => 2,
                _ => throw new ArgumentException(""Invalid value"")
            };
        }
    }
}
";

        var expectedDiagnostics = new[]
        {
            Diagnostic(DiagnosticRules.SwitchDefaultExceptionRule)
                .WithSpan(14, 28, 14, 66)
        };

        await VerifyAnalyzerAsync(testCode, expectedDiagnostics);
    }

    [TestMethod]
    public async Task SwitchExpressionDefaultReturnsValue_ShouldNotReportDiagnostic()
    {
        const string testCode = @"
namespace TestNamespace
{
    public class TestClass
    {
        public int TestMethod(int value)
        {
            return value switch
            {
                1 => 1,
                2 => 2,
                _ => 0
            };
        }
    }
}
";

        await VerifyAnalyzerAsync(testCode);
    }

    [TestMethod]
    public async Task SwitchNoDefault_ShouldNotReportDiagnostic()
    {
        const string testCode = @"
namespace TestNamespace
{
    public class TestClass
    {
        public void TestMethod(int value)
        {
            switch (value)
            {
                case 1:
                    break;
                case 2:
                    break;
            }
        }
    }
}
";

        await VerifyAnalyzerAsync(testCode);
    }

    protected override Type[]? GetAdditionalTypeRefs()
    {
        return new[] { typeof(UnexpectedValueException) };
    }
}
