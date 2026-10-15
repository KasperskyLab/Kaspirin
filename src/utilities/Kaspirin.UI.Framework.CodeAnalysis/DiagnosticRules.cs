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

#pragma warning disable RS2008 // Enable analyzer release tracking

using Microsoft.CodeAnalysis;

namespace Kaspirin.UI.Framework.CodeAnalysis;

internal static class DiagnosticRules
{
    public static readonly DiagnosticDescriptor TimerLambdaUsageRule = new(
        id: "KAS001",
        title: "Lambda usage in Timer",
        messageFormat: "Avoid using '{0}' with lambda expressions",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Using lambda expressions in timers may cause memory leaks if the timer holds a strong reference to the delegate, or incorrect behavior if it holds a weak reference.");

    public static readonly DiagnosticDescriptor TimerCreationRule = new(
        id: "KAS002",
        title: "Creating timers is prohibited",
        messageFormat: "Creation of a timer of type '{0}' is prohibited. Use TimerFactory instead.",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "Prohibits the creation of standard .NET timers. Only timers obtained from TimerFactory may be used.");

    public static readonly DiagnosticDescriptor CollectionExtensionAddRangeRule = new(
        id: "KAS003",
        title: "AddRange called on array",
        messageFormat: "AddRange extension method cannot be used on arrays because arrays have fixed size",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "The AddRange extension method from CollectionExtension class is not compatible with arrays. Use List<T> or another mutable collection instead.");

    public static readonly DiagnosticDescriptor SwitchDefaultExceptionRule = new(
        id: "KAS004",
        title: "Switch default must throw UnexpectedValueException",
        messageFormat: "Default branch in switch statement must throw UnexpectedValueException",
        category: "Usage",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true,
        description: "When a switch statement's default branch throws an exception, it must be UnexpectedValueException to maintain consistency.");

    public static readonly DiagnosticDescriptor FullyQualifiedClassNamesRule = new(
        id: "KAS005",
        title: "Fully qualified class name is used",
        messageFormat: "Fully qualified class name reduces code readability. Use using directive instead.",
        category: "CodeStyle",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true,
        description: "Using fully qualified class names reduces code readability. Prefer using directives to import namespaces and simplify class references.");
}
