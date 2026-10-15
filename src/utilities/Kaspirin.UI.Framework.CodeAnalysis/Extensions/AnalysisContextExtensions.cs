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

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Kaspirin.UI.Framework.CodeAnalysis.Extensions;

internal static class AnalysisContextExtensions
{
    public static void ReportDiagnostic(
        this SyntaxNodeAnalysisContext context,
        DiagnosticDescriptor descriptor,
        Location location,
        params object?[]? messageArgs)
    {
        var diagnostic = Diagnostic.Create(descriptor, location, messageArgs);
        context.ReportDiagnostic(diagnostic);
    }

    public static void ReportDiagnostic(
        this OperationAnalysisContext context,
        DiagnosticDescriptor descriptor,
        Location location,
        params object?[]? messageArgs)
    {
        var diagnostic = Diagnostic.Create(descriptor, location, messageArgs);
        context.ReportDiagnostic(diagnostic);
    }
}
