// <copyright file="Snapshot.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    // value type
    public class Snapshot
    {
        private readonly Dictionary<object, Func<IComponentResolver, object>[]> allComponentFactories;

        public Snapshot(Dictionary<object, Func<IComponentResolver, object>[]> allComponentFactories)
        {
            Guard.Against.Null(() => allComponentFactories);

            this.allComponentFactories = allComponentFactories;
        }

        public Func<IComponentResolver, object> GetComponentFactory(object componentKey)
        {
            var componentFactories = this.GetComponentFactories(componentKey);
            return componentFactories == null ? null : componentFactories.First();
        }

        public Func<IComponentResolver, object>[] GetComponentFactories(object componentKey)
        {
            Func<IComponentResolver, object>[] componentFactories;
            this.allComponentFactories.TryGetValue(componentKey, out componentFactories);
            return componentFactories;
        }
    }
}
