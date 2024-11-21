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
using System.Collections.Generic;
using System.Linq;

namespace Kaspirin.UI.Framework.UiKit.Navigation
{
    public abstract class BaseRegionBehaviorsRegistry : IRegionBehaviorsRegistry
    {
        protected void AddBehaviorFactory(Func<IRegionBehavior> factory)
        {
            Guard.ArgumentIsNotNull(factory);

            _behaviorFactories.Add(factory);
        }

        public IList<IRegionBehavior> CreateBehaviors()
        {
            return _behaviorFactories.Select(f => f()).ToList();
        }

        private readonly List<Func<IRegionBehavior>> _behaviorFactories = new();
    }
}