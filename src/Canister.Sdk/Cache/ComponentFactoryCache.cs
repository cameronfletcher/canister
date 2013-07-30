// <copyright file="ComponentFactoryCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Cache
{
    using System.Collections.Generic;

    public class ComponentFactoryCache : IComponentFactoryCache
    {
        private readonly Dictionary<object, ComponentFactory> cache = new Dictionary<object, ComponentFactory>();

        public ComponentFactory GetComponentFactory(object componentKey)
        {
            ComponentFactory componentFactory = null;
            return this.cache.TryGetValue(componentKey, out componentFactory) ? componentFactory : null;
        }

        public void SetComponentFactory(object componentKey, ComponentFactory componentFactory)
        {
            if (componentFactory == null && this.cache.ContainsKey(componentKey))
            {
                this.cache.Remove(componentKey);
                return;
            }

            this.cache[componentKey] = componentFactory;
        }
    }
}
