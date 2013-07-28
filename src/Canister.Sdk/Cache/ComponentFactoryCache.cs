// <copyright file="ComponentFactoryCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Cache
{
    using System;
    using System.Collections.Generic;
    using Canister.Sdk.Model;

    public class ComponentFactoryCache : IComponentFactoryCache
    {
        private readonly Dictionary<object, Func<IComponentResolver, object>> cache = new Dictionary<object, Func<IComponentResolver, object>>();

        public Func<IComponentResolver, object> GetComponentFactory(object componentKey)
        {
            Func<IComponentResolver, object> componentFactory = null;
            return this.cache.TryGetValue(componentKey, out componentFactory) ? componentFactory : null;
        }

        public void SetComponentFactory(object componentKey, Func<IComponentResolver, object> componentFactory)
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
