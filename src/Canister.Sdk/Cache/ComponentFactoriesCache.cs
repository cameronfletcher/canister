// <copyright file="ComponentFactoriesCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Cache
{
    using System.Collections.Generic;

    public class ComponentFactoriesCache : IComponentFactoriesCache
    {
        private readonly Dictionary<object, ComponentFactory[]> cache = new Dictionary<object, ComponentFactory[]>();

        public ComponentFactory[] GetComponentFactories(object componentKey)
        {
            ComponentFactory[] componentFactories = null;
            return this.cache.TryGetValue(componentKey, out componentFactories) ? componentFactories : null;
        }

        public void SetComponentFactories(object componentKey, ComponentFactory[] componentFactories)
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
