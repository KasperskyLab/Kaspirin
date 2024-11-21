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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Kaspirin.UI.Framework.UiKit.Localization.Localizer.Strings
{
    public class StringCache
    {
        private const int ClearFrequency = 1000;

        public StringCache()
        {
            _stringCache = new ConcurrentDictionary<int, CircularWeak3StringCollection>();
            _clearNotUsedStringsTask = new SingletonTaskFactory(ClearNotUsedStrings);
        }

        public string GetUniqueString(string str)
        {
            Guard.ArgumentIsNotNull(str);

            Interlocked.Increment(ref _getCounter);
            var hash = str.GetHashCode();

            if (IsNeedToClear())
            {
                Interlocked.Exchange(ref _getCounter, 0);
                _clearNotUsedStringsTask.GetTask();
            }

            if (_stringCache.TryGetValue(hash, out var candidate))
            {
                var existStr = candidate.TryGetItem(str);
                if (existStr != null)
                {
                    return existStr;
                }

                candidate.Add(str);
                return str;
            }

            var strCollection = new CircularWeak3StringCollection();
            strCollection.Add(str);
            _stringCache[hash] = strCollection;

            return str;
        }

        private Task ClearNotUsedStrings()
        {
            return Task.Factory.StartNew(() =>
            {
                var keysToRemove = new List<int>();
                foreach (var itm in _stringCache)
                {
                    if (itm.Value.IsEmpty())
                    {
                        keysToRemove.Add(itm.Key);
                    }
                    else
                    {
                        itm.Value.Cleanup();
                    }
                }

                foreach (var itmKey in keysToRemove)
                {
                    _stringCache.TryRemove(itmKey, out _);
                }
            });
        }

        private bool IsNeedToClear()
        {
            return _getCounter % ClearFrequency == 0;
        }

        #region Nested 

        private struct CircularWeak3StringCollection
        {
            public bool IsEmpty()
            {
                return !(TryGetValue(out _, _str1)
                         || TryGetValue(out _, _str2)
                         || TryGetValue(out _, _str3));
            }

            public string? TryGetItem(string str)
            {
                if (CheckCandidate(out var candidate, str, _str1)
                    || CheckCandidate(out candidate, str, _str2)
                    || CheckCandidate(out candidate, str, _str3))
                {
                    return candidate;
                }

                return null;
            }

            public void Add(string str)
            {
                if (TrySetValue(ref _str1, str)
                    || TrySetValue(ref _str2, str)
                    || TrySetValue(ref _str3, str))
                {
                    return;
                }

                switch (_lastAddedIndex)
                {
                    case 0:
                        ReplaceValue(out _str1, str);
                        break;
                    case 1:
                        ReplaceValue(out _str2, str);
                        break;
                    case 2:
                        ReplaceValue(out _str3, str);
                        break;
                    default:
                        ReplaceValue(out _str1, str);
                        _lastAddedIndex = 0;
                        break;
                }

                Interlocked.Increment(ref _lastAddedIndex);
            }

            public void Cleanup()
            {
                CleanupValue(ref _str1);
                CleanupValue(ref _str2);
                CleanupValue(ref _str3);
                _lastAddedIndex = 0;
            }

            private static bool CheckCandidate([NotNullWhen(true)] out string? candidate, string str, WeakReference? value)
            {
                if (TryGetValue(out var tmpCandidate, value))
                {
                    if (string.Equals(str, tmpCandidate, StringComparison.InvariantCulture))
                    {
                        candidate = tmpCandidate;
                        return true;
                    }
                }
                candidate = null;
                return false;
            }

            private static void CleanupValue(ref WeakReference? value)
            {
                if (value?.Target != null)
                {
                    value = null;
                }
            }

            private static void ReplaceValue(out WeakReference value, string str)
            {
                value = new WeakReference(str);
            }

            private static bool TrySetValue(ref WeakReference? value, string str)
            {
                if (value?.Target == null)
                {
                    value = new WeakReference(str);
                    return true;
                }
                return false;
            }

            private static bool TryGetValue([NotNullWhen(true)] out string? value, WeakReference? str)
            {
                value = null;
                
                var tmp = str?.Target;
                if (tmp == null)
                {
                    return false;
                }

                value = (string)tmp;
                return true;
            }

            private WeakReference? _str1;
            private WeakReference? _str2;
            private WeakReference? _str3;
            private volatile int _lastAddedIndex;
        }

        private class SingletonTaskFactory
        {
            public SingletonTaskFactory(Func<Task> factoryFunction)
            {
                _factoryFunction = factoryFunction;
            }

            public void GetTask()
            {
                lock (_syncObject)
                {
                    if (_task == null)
                    {
                        _task = _factoryFunction();
                        _task.ContinueWith(ClearTask);
                    }
                }
            }

            private void ClearTask(Task task)
            {
                lock (_syncObject)
                {
                    _task = null;
                }
            }

            private Task? _task;
            private readonly object _syncObject = new();
            private readonly Func<Task> _factoryFunction;
        }

        #endregion

        private int _getCounter;

        private readonly ConcurrentDictionary<int, CircularWeak3StringCollection> _stringCache;
        private readonly SingletonTaskFactory _clearNotUsedStringsTask;
    }
}