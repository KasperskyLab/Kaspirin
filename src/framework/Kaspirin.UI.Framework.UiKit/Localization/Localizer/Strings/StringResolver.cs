// Copyright © 2024 AO Kaspersky Lab.
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
using System.Globalization;
using Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing;
using Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Evaluators.ValueExpression;
using Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings.Parsing.Expressions;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings
{
    public static class StringResolver
    {
        public static string Resolve(string localizableString, Func<string, object?> variableResolver, CultureInfo cultureInfo)
        {
            Guard.ArgumentIsNotNull(localizableString);
            Guard.ArgumentIsNotNull(variableResolver);

            var valueExpression = (ValueExpression)Parser.ParseExpression(
                localizableString,
                new ValueExpressionEvaluator(cultureInfo),
                filePath: localizableString); // Use the content as the filename so that it appears in the exception.

            return valueExpression.Resolve(_ => new ValueExpression(
#if NETCOREAPP
                Array.Empty<IExpression>()
#else
                new IExpression[0]
#endif
                ), variableResolver);
        }
    }
}