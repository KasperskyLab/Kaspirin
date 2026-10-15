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
using Kaspirin.UI.Framework.CodeAnalysis.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Kaspirin.UI.Framework.CodeAnalysis.Analyzers.Extensions.Collections;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class CollectionExtensionAddRangeAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(DiagnosticRules.CollectionExtensionAddRangeRule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeInvocation, SyntaxKind.InvocationExpression);
    }

    private static void AnalyzeInvocation(SyntaxNodeAnalysisContext context)
    {
        var invocation = (InvocationExpressionSyntax)context.Node;

        // Check if this is an extension method call (member access expression)
        if (invocation.Expression is not MemberAccessExpressionSyntax memberAccess)
        {
            return;
        }

        // Check if the method name is AddRange
        if (memberAccess.Name.Identifier.Text != "AddRange")
        {
            return;
        }

        // Get the semantic model to determine the type of the receiver
        var semanticModel = context.SemanticModel;
        var symbolInfo = semanticModel.GetSymbolInfo(invocation, context.CancellationToken);

        if (symbolInfo.Symbol is not IMethodSymbol methodSymbol)
        {
            return;
        }

        // Check if this is an extension method
        if (!methodSymbol.IsExtensionMethod)
        {
            return;
        }

        // Check if the method is named AddRange
        if (methodSymbol.Name != "AddRange")
        {
            return;
        }

        // Get the receiver type (the object on which AddRange is called)
        var receiverType = memberAccess.Expression;
        var typeInfo = semanticModel.GetTypeInfo(receiverType, context.CancellationToken);
        var type = typeInfo.Type;

        // Check if the receiver is an array type
        if (type is not IArrayTypeSymbol)
        {
            return;
        }

        context.ReportDiagnostic(
            DiagnosticRules.CollectionExtensionAddRangeRule,
            memberAccess.Name.GetLocation(),
            methodSymbol.Name);
    }
}
