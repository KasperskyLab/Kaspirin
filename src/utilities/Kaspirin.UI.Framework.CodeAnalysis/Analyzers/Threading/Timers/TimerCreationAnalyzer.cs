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
using System.Collections.Immutable;
using Kaspirin.UI.Framework.CodeAnalysis.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Kaspirin.UI.Framework.CodeAnalysis.Analyzers.Threading.Timers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class TimerCreationAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(DiagnosticRules.TimerCreationRule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterOperationAction(AnalyzeObjectCreation, OperationKind.ObjectCreation);
    }

    private static void AnalyzeObjectCreation(OperationAnalysisContext context)
    {
        var objectCreation = (IObjectCreationOperation)context.Operation;
        var createdType = objectCreation.Type;

        if (createdType is null)
        {
            return;
        }

        if (!_forbiddenTimerMetadataNames.Contains(createdType.ToDisplayString()))
        {
            return;
        }

        context.ReportDiagnostic(
            DiagnosticRules.TimerCreationRule,
            objectCreation.Syntax.GetLocation(),
            createdType.ToDisplayString());
    }

    private static readonly HashSet<string> _forbiddenTimerMetadataNames = new()
    {
        "System.Threading.Timer",
        "System.Timers.Timer",
        "System.Windows.Forms.Timer",
        "System.Windows.Threading.DispatcherTimer"
    };
}
