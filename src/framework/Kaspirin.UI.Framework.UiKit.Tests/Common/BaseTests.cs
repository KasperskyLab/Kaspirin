// Copyright © 2025 AO Kaspersky Lab.
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
using System.Runtime.ExceptionServices;

namespace Kaspirin.UI.Framework.UiKit.Tests.Common;
public abstract class BaseTests
{
    [TestInitialize]
    public void TestInitializeBase()
    {
        AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
        LocalizationManager.Initialize("en-US");

        TestInitialize();
    }

    [TestCleanup]
    public void TestCleanupBase()
    {
        LocalizationManager.ChangePrefix(null);
        LocalizationManager.ChangeTheme(null);

        TestCleanup();

        AppDomain.CurrentDomain.FirstChanceException -= CurrentDomain_FirstChanceException;
    }

    protected virtual void TestInitialize()
    {
    }

    protected virtual void TestCleanup()
    {
    }

    protected void InitializeLocalizationManager(string displayCulture, string? formatCulture = default)
    {
        Guard.ArgumentIsNotNull(displayCulture);

        var localizationManagerParameters = new LocalizationParameters(displayCulture)
        {
            FormatCulture = formatCulture ?? displayCulture,
        };

        LocalizationManager.Initialize(localizationManagerParameters);
    }

    private static void CurrentDomain_FirstChanceException(object? sender, FirstChanceExceptionEventArgs args)
    {
        Tracer.TraceError($"First chance exception: {args.Exception}\nCurrent StackTrace: {Environment.StackTrace}");
    }

    protected static ComponentTracer Tracer { get; } = ComponentTracer.Get("Test");
}
