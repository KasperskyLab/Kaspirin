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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;

namespace Kaspirin.UI.Framework.CodeAnalysis.Tests;

public abstract class BaseAnalyzerTests<TAnalyzer>
    where TAnalyzer : DiagnosticAnalyzer, new()
{
    public static DiagnosticResult Diagnostic()
        => CSharpAnalyzerVerifier<TAnalyzer, DefaultVerifier>.Diagnostic();

    public static DiagnosticResult Diagnostic(string diagnosticId)
        => CSharpAnalyzerVerifier<TAnalyzer, DefaultVerifier>.Diagnostic(diagnosticId);

    public static DiagnosticResult Diagnostic(DiagnosticDescriptor descriptor)
        => CSharpAnalyzerVerifier<TAnalyzer, DefaultVerifier>.Diagnostic(descriptor);

    public async Task VerifyAnalyzerAsync(string source, params DiagnosticResult[] expected)
    {
        var test = new AnalyzerTest
        {
            TestCode = source,
        };

        GetAdditionalTypeRefs()?
            .Select(t => MetadataReference.CreateFromFile(t.Assembly.Location))
            .ForEach(r => test.TestState.AdditionalReferences.Add(r));

        test.ExpectedDiagnostics.AddRange(expected);

        await test.RunAsync(CancellationToken.None);
    }

    protected virtual Type[]? GetAdditionalTypeRefs()
    {
        return null;
    }

    private sealed class AnalyzerTest : CSharpAnalyzerTest<TAnalyzer, DefaultVerifier>
    {
        public AnalyzerTest()
        {
            ReferenceAssemblies = ReferenceAssemblies.Net.Net60Windows;

            SolutionTransforms.Add((solution, projectId) =>
            {
                var parseOptions = new CSharpParseOptions(LanguageVersion.CSharp10, DocumentationMode.Diagnose);

                solution = solution.WithProjectParseOptions(projectId, parseOptions);

                return solution;
            });
        }
    }
}
