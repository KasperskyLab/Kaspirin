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

namespace Kaspirin.UI.Framework.UiKit.Converters.TimeConverters
{
    public enum TimeSpanConverterType
    {
        /// <summary>
        /// 4 days 15 hours
        /// </summary>
        TimePassed,

        /// <summary>
        /// About 4 days left...
        /// </summary>
        TimeLeft,

        /// <summary>
        /// 3:06 PM
        /// </summary>
        ShortTime,

        /// <summary>
        /// 15:06
        /// </summary>
        TimeSpan,

        /// <summary>
        /// 06:40
        /// </summary>
        TimeSpanSeconds,

        /// <summary>
        /// 15 hours
        /// </summary>
        ShortTimePast
    }
}
