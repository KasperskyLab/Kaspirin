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

using System.Collections.Immutable;
using System.Linq;
using Kaspirin.UI.Framework.CodeAnalysis.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Kaspirin.UI.Framework.CodeAnalysis.Analyzers.Threading.Timers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class TimerLambdaUsageAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(DiagnosticRules.TimerLambdaUsageRule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterSyntaxNodeAction(AnalyzeTimerCreation, SyntaxKind.InvocationExpression);
        context.RegisterSyntaxNodeAction(AnalyzeTimerEventsSubscription, SyntaxKind.AddAssignmentExpression);
    }

    private static void AnalyzeTimerCreation(SyntaxNodeAnalysisContext context)
    {
        var invocation = (InvocationExpressionSyntax)context.Node;

        if (context.SemanticModel.GetSymbolInfo(invocation, context.CancellationToken).Symbol is not IMethodSymbol method)
        {
            return;
        }

        if (_timerFactoryName != method.ContainingType.Name)
        {
            return;
        }

        if (_timerFactoryMethodNames.Contains(method.Name) is false)
        {
            return;
        }

        var hasLambdaLikeArgument = invocation.ArgumentList.Arguments
            .Any(arg => arg.Expression is LambdaExpressionSyntax or AnonymousMethodExpressionSyntax);

        if (!hasLambdaLikeArgument)
        {
            return;
        }

        var diagnostic = Diagnostic.Create(
            DiagnosticRules.TimerLambdaUsageRule,
            invocation.Expression.GetLocation(),
            $"{method.ContainingType.ToDisplayString()}.{method.Name}");

        context.ReportDiagnostic(diagnostic);
    }

    private static void AnalyzeTimerEventsSubscription(SyntaxNodeAnalysisContext context)
    {
        var addAssignment = (AssignmentExpressionSyntax)context.Node;

        if (addAssignment.Left is not MemberAccessExpressionSyntax memberAccess)
        {
            return;
        }

        if (addAssignment.Right is not (LambdaExpressionSyntax or AnonymousMethodExpressionSyntax))
        {
            return;
        }

        var symbol = context.SemanticModel.GetSymbolInfo(memberAccess, context.CancellationToken).Symbol;
        if (symbol is not IEventSymbol eventSymbol)
        {
            return;
        }

        if (_timerTypeName != eventSymbol.ContainingType.Name)
        {
            return;
        }

        if (_timerEventNames.Contains(eventSymbol.Name) is false)
        {
            return;
        }

        context.ReportDiagnostic(
            DiagnosticRules.TimerLambdaUsageRule,
            memberAccess.Name.GetLocation(),
            $"{eventSymbol.ContainingType.ToDisplayString()}.{eventSymbol.Name}");
    }

    private static readonly string _timerFactoryName = "TimerFactory";
    private static readonly string[] _timerFactoryMethodNames = new[] { "CreateOnUi", "CreateOnTp" };
    private static readonly string _timerTypeName = "ITimer";
    private static readonly string[] _timerEventNames = new[] { "Tick", "StateChanged" };
}