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

using System.Collections.Generic;
using System.Globalization;

namespace Kaspirin.UI.Framework.Extensions.CultureInfos
{
    /// <summary>
    ///     Extension methods for <see cref="CultureInfo" />.
    /// </summary>
    public static class CultureInfoExtensions
    {
        /// <summary>
        ///     Returns an enumeration of the parent cultures for <paramref name="CultureInfo" />.
        /// </summary>
        /// <param name="cultureInfo">
        ///     Culture.
        /// </param>
        /// <returns>
        ///     Enumeration for obtaining parent cultures.
        /// </returns>
        public static IEnumerable<CultureInfo> GetParentCultures(this CultureInfo cultureInfo)
        {
            Guard.ArgumentIsNotNull(cultureInfo);

            for (var culture = cultureInfo; !culture.Equals(CultureInfo.InvariantCulture); culture = culture.Parent)
            {
                yield return culture;
            }
        }
    }
}
