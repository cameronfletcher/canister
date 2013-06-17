// <copyright file="ComponentFactoriesCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Cache
{
    using System;
    using System.Collections.Generic;

    public class ComponentFactoriesCache : IComponentFactoriesCache
    {
        private readonly Dictionary<object, Func<IComponentResolver, object>[]> cache = new Dictionary<object, Func<IComponentResolver, object>[]>();

        public Func<IComponentResolver, object>[] GetComponentFactories(object componentKey)
        {
            Func<IComponentResolver, object>[] componentFactories = null;
            return this.cache.TryGetValue(componentKey, out componentFactories) ? componentFactories : null;
        }

        public void SetComponentFactories(object componentKey, Func<IComponentResolver, object>[] componentFactories)
        {
            if (componentFactories == null && this.cache.ContainsKey(componentKey))
            {
                this.cache.Remove(componentKey);
                return;
            }

            this.cache[componentKey] = componentFactories;
        }
    }
}
