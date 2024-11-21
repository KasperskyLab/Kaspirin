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
using System.Linq;
using System.Text;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core.Translation
{
    public static class CommentGenerator
    {
        public static string AddFileComment(string fileText, string comment)
        {
            var commentBuilder = new StringBuilder();
            comment
                .Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(p => $"// {p.Trim()}")
                .ToList()
                .ForEach(p => commentBuilder.AppendLine(p));

            comment = commentBuilder.ToString();

            return fileText.Replace("@FileComment", comment);
        }
    }
}
