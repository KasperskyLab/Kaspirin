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

using System.Threading.Tasks;

namespace Kaspirin.UI.Framework.Threading;

internal static class CompletedTask
{
    static CompletedTask()
    {
#if NETCOREAPP
        Instance = Task.CompletedTask;
#else
        var tcs = new TaskCompletionSource<object?>();
        tcs.SetResult(null);
        Instance = tcs.Task;
#endif
    }

    public static Task Instance { get; }

    public static Task<TResult> FromResult<TResult>(TResult? result = default)
    {
#if NETCOREAPP
        return Task.FromResult(result!);
#else
        var tcs = new TaskCompletionSource<TResult>();
        tcs.SetResult(result!);
        return tcs.Task;
#endif
    }
}
