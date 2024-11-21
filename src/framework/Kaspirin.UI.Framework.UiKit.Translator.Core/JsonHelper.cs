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

using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Kaspirin.UI.Framework.UiKit.Translator.Core
{
    internal static class JsonHelper
    {
        public static string Serialize<T>(T value)
        {
            var serializer = new DataContractJsonSerializer(typeof(T));
            using var memoryStream = new MemoryStream();
            serializer.WriteObject(memoryStream, value);
            return Encoding.Default.GetString(memoryStream.ToArray());
        }

        public static T Deserialize<T>(string json)
        {
            using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var serializer = new DataContractJsonSerializer(typeof(T));
            return (T)serializer.ReadObject(memoryStream);
        }
    }
}
