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

namespace Kaspirin.UI.Framework.CodeAnalysis.Analyzers.Exceptions;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class SwitchDefaultExceptionAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(DiagnosticRules.SwitchDefaultExceptionRule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterSyntaxNodeAction(AnalyzeSwitchStatement, SyntaxKind.SwitchStatement);
        context.RegisterSyntaxNodeAction(AnalyzeSwitchExpression, SyntaxKind.SwitchExpression);
    }

    private static void AnalyzeSwitchStatement(SyntaxNodeAnalysisContext context)
    {
        var switchStatement = (SwitchStatementSyntax)context.Node;

        var defaultSection = switchStatement.Sections.FirstOrDefault(s => s.Labels.Any(l => l.IsKind(SyntaxKind.DefaultSwitchLabel)));
        if (defaultSection == null)
        {
            return;
        }

        CheckDefaultBranchForException(defaultSection, context);
    }

    private static void AnalyzeSwitchExpression(SyntaxNodeAnalysisContext context)
    {
        var switchExpression = (SwitchExpressionSyntax)context.Node;

        var defaultArm = switchExpression.Arms.FirstOrDefault(a => a.Pattern is DiscardPatternSyntax);
        if (defaultArm == null)
        {
            return;
        }

        CheckDefaultArmForException(defaultArm, context);
    }

    private static void CheckDefaultBranchForException(SwitchSectionSyntax defaultSection, SyntaxNodeAnalysisContext context)
    {
        var statements = defaultSection.Statements;
        if (statements.Count == 0)
        {
            return;
        }

        var throwStatement = statements.OfType<ThrowStatementSyntax>().FirstOrDefault();
        if (throwStatement == null)
        {
            return;
        }

        CheckThrowExpression(throwStatement.Expression, context);
    }

    private static void CheckDefaultArmForException(SwitchExpressionArmSyntax defaultArm, SyntaxNodeAnalysisContext context)
    {
        if (defaultArm.Expression is ThrowExpressionSyntax throwExpression)
        {
            CheckThrowExpression(throwExpression.Expression, context);
        }
    }

    private static void CheckThrowExpression(ExpressionSyntax? throwExpression, SyntaxNodeAnalysisContext context)
    {
        if (throwExpression == null)
        {
            return;
        }

        if (throwExpression is not ObjectCreationExpressionSyntax objectCreation)
        {
            return;
        }

        var exceptionType = context.SemanticModel.GetTypeInfo(objectCreation.Type, context.CancellationToken).Type;
        if (exceptionType == null)
        {
            var typeName = objectCreation.Type.ToString();
            if (!typeName.EndsWith(_exceptionTypeName))
            {
                context.ReportDiagnostic(
                    DiagnosticRules.SwitchDefaultExceptionRule,
                    objectCreation.GetLocation());
            }
        }
        else
        {
            var fullName = exceptionType.ToDisplayString();
            if (!fullName.EndsWith(_exceptionTypeName))
            {
                context.ReportDiagnostic(
                    DiagnosticRules.SwitchDefaultExceptionRule,
                    objectCreation.GetLocation());
            }
        }
    }

    private static readonly string _exceptionTypeName = "UnexpectedValueException";
}
