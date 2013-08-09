// <copyright file="ComponentsCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Cache
{
    using System;
    using System.Collections.Generic;

    public class ComponentsCache : IComponentsCache
    {
        private readonly Dictionary<Guid, object[]> cache = new Dictionary<Guid, object[]>();

        public object[] GetComponents(Guid requestId)
        {
            return this.cache.ContainsKey(requestId) ? this.cache[requestId] : new object[0];
        }

        public void PutComponents(Guid requestId, object[] components)
        {
            this.cache[requestId] = components;
        }

        public void DeleteComponents(Guid requestId)
        {
            if (this.cache.ContainsKey(requestId))
            {
                this.cache.Remove(requestId);
            }
        }
    }
}