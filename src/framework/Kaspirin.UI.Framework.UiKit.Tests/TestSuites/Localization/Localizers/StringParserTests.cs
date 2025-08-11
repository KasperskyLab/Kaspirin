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

using System.Globalization;
using System.IO;

namespace Kaspirin.UI.Framework.UiKit.Tests.TestSuites.Localization.Localizers;

[TestClass]
public sealed class StringParserTests
{
    [TestMethod]
    [ExpectedException(typeof(InvalidDataException))]
    public void ParserWithPluralFormInvalid()
    {
        var testString = "'FilesCount = '+$FilesCount+' ' + { $FilesCount : 'file', 'files' } + ''";

        StringResolver.Resolve(testString, s => 595, _ruCulture);
    }

    [TestMethod]
    public void ParserWithPluralFormValid()
    {
        var testString = "'FilesCount = '+$FilesCount+' ' + { $FilesCount : 'file', 'files' } + ''";

        var result = StringResolver.Resolve(testString, s => 595, _enCulture);

        Assert.AreEqual(expected: "FilesCount = 595 files", actual: result);
    }

    private static readonly CultureInfo _ruCulture = CultureInfo.GetCultureInfo("ru-RU");
    private static readonly CultureInfo _enCulture = CultureInfo.GetCultureInfo("en-EU");
}
