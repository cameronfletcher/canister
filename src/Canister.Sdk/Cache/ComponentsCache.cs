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

        public object[] GetComponents(Guid requiestId)
        {
            return this.cache.ContainsKey(requiestId) ? this.cache[requiestId] : new object[0];
        }

        public void SetComponents(Guid requestId, object[] components)
        {
            this.cache[requestId] = components;
        }
    }
}