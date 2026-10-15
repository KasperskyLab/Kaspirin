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
using System.Diagnostics;
using System.Linq;
using Kaspirin.UI.Framework.CodeAnalysis.Extensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Kaspirin.UI.Framework.CodeAnalysis.Analyzers.CodeStyle;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class FullyQualifiedClassNamesAnalyzer : DiagnosticAnalyzer
{
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(DiagnosticRules.FullyQualifiedClassNamesRule);

    public override void Initialize(AnalysisContext context)
    {
        if (!Debugger.IsAttached)
        {
            context.EnableConcurrentExecution();
        }

        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

        var operationKinds = new[]
        {
            OperationKind.VariableDeclaration,
            OperationKind.MethodBodyOperation,
            OperationKind.FieldReference,
            OperationKind.ObjectCreation,
            OperationKind.Invocation,
        };

        context.RegisterOperationAction(AnalyzeOperation, operationKinds);
    }

    private static void AnalyzeOperation(OperationAnalysisContext context)
    {
        var semanticModel = context.Operation.SemanticModel;
        if (semanticModel == null)
        {
            return;
        }

        switch (context.Operation)
        {
            case IInvocationOperation operation:
                {
                    if (operation.Syntax is InvocationExpressionSyntax syntax)
                    {
                        if (HasNamespaceInSyntaxNode<InvokeMethodNamespaceWalker>(semanticModel, syntax))
                        {
                            context.ReportDiagnostic(DiagnosticRules.FullyQualifiedClassNamesRule, syntax.Expression.GetLocation());
                        }

                        var walker = new InvocationArgumentWalker();

                        walker.Visit(syntax);

                        foreach (var argumentSyntax in walker.ArgumentSyntaxNodes())
                        {
                            if (HasNamespaceInSyntaxNode<SignatureNamespaceWalker>(semanticModel, argumentSyntax))
                            {
                                context.ReportDiagnostic(DiagnosticRules.FullyQualifiedClassNamesRule, argumentSyntax.GetLocation());
                            }
                        }
                    }

                    break;
                }

            case IFieldReferenceOperation operation:
                {
                    var syntax = operation.Syntax;

                    if (HasNamespaceInSyntaxNode<SignatureNamespaceWalker>(semanticModel, syntax))
                    {
                        context.ReportDiagnostic(DiagnosticRules.FullyQualifiedClassNamesRule, syntax.GetLocation());
                    }

                    break;
                }

            case IObjectCreationOperation operation:
                {
                    var syntax = operation.Syntax;

                    if (HasNamespaceInSyntaxNode<SignatureNamespaceWalker>(semanticModel, syntax))
                    {
                        var expression = syntax as ObjectCreationExpressionSyntax;

                        var location = expression?.Type.GetLocation() ?? syntax.GetLocation();

                        context.ReportDiagnostic(DiagnosticRules.FullyQualifiedClassNamesRule, location);
                    }

                    break;
                }

            case IVariableDeclarationOperation operation:
                {
                    var syntax = operation.Syntax;

                    if (HasNamespaceInSyntaxNode<SignatureNamespaceWalker>(semanticModel, syntax))
                    {
                        context.ReportDiagnostic(DiagnosticRules.FullyQualifiedClassNamesRule, syntax.GetLocation());
                    }

                    break;
                }

            case IMethodBodyOperation operation:
                {
                    if (operation.Syntax is MethodDeclarationSyntax methodDeclaration)
                    {
                        var returnType = methodDeclaration.ReturnType;

                        if (HasNamespaceInSyntaxNode<SignatureNamespaceWalker>(semanticModel, returnType))
                        {
                            context.ReportDiagnostic(DiagnosticRules.FullyQualifiedClassNamesRule, returnType.GetLocation());
                        }

                        foreach (var parameter in methodDeclaration.ParameterList.Parameters)
                        {
                            var parameterType = parameter.Type;
                            if (parameterType == null)
                            {
                                continue;
                            }

                            if (HasNamespaceInSyntaxNode<SignatureNamespaceWalker>(semanticModel, parameterType))
                            {
                                context.ReportDiagnostic(DiagnosticRules.FullyQualifiedClassNamesRule, parameterType.GetLocation());
                            }
                        }
                    }

                    break;
                }

            default:
                break;
        }
    }

    private static bool HasNamespaceInSyntaxNode<TNamespaceWalker>(SemanticModel semanticModel, SyntaxNode syntax)
        where TNamespaceWalker : NamespaceWalkerBase, new()
    {
        var walker = new TNamespaceWalker();

        walker.Visit(syntax);

        return walker
            .SyntaxNodes()
            .Select(syntaxNode => semanticModel.GetSymbolInfo(syntaxNode.GetReference().GetSyntax()))
            .Any(symbolInfo => symbolInfo.Symbol is { Kind: SymbolKind.Namespace });
    }

    private sealed class InvocationArgumentWalker : CSharpSyntaxWalker
    {
        public ImmutableList<SyntaxNode> ArgumentSyntaxNodes() => _argumentSyntaxNodes.ToImmutableList();

        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
            => Visit(node.Expression);

        public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
            => Visit(node.Name);

        public override void VisitGenericName(GenericNameSyntax node)
            => node.TypeArgumentList.Arguments.ToImmutableList().ForEach(Visit);

        public override void VisitQualifiedName(QualifiedNameSyntax node)
            => _argumentSyntaxNodes.Add(node);

        private readonly List<SyntaxNode> _argumentSyntaxNodes = new();
    }

    private sealed class InvokeMethodNamespaceWalker : NamespaceWalkerBase
    {
        public override void VisitGenericName(GenericNameSyntax node) { }

        public override void VisitIdentifierName(IdentifierNameSyntax node) { }

        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
            => Visit(node.Expression);

        public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
            => Visit(node.Expression);
    }

    private sealed class SignatureNamespaceWalker : NamespaceWalkerBase
    {
        public override void VisitGenericName(GenericNameSyntax node) { }

        public override void VisitIdentifierName(IdentifierNameSyntax node) { }

        public override void VisitQualifiedName(QualifiedNameSyntax node)
            => Visit(node.Left);

        public override void VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
            => Visit(node.Type);

        public override void VisitVariableDeclaration(VariableDeclarationSyntax node)
            => Visit(node.Type);

        public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
            => Visit(node.Expression);
    }

    private abstract class NamespaceWalkerBase : CSharpSyntaxWalker
    {
        public ImmutableList<SyntaxNode> SyntaxNodes() => _syntaxNodes.ToImmutableList();

        public override void Visit(SyntaxNode? node)
        {
            if (node == null)
            {
                return;
            }

            _syntaxNodes.Add(node);
            base.Visit(node);
        }

        private readonly List<SyntaxNode> _syntaxNodes = new();
    }
}