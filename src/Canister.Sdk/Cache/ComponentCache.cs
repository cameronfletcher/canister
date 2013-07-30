// <copyright file="ComponentCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Cache
{
    using System;
    using System.Collections.Generic;

    public class ComponentCache : IComponentCache
    {
        private readonly Dictionary<Guid, object[]> cache = new Dictionary<Guid, object[]>();

        public object[] GetComponents(Guid requiestId)
        {
            return this.cache[requiestId];
        }

        public void SetComponents(Guid requestId, object[] components)
        {
            this.cache[requestId] = components;
        }
    }
}
