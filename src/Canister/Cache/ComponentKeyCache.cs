// <copyright file="ComponentKeyCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Cache
{
    public class ComponentKeyCache : IComponentKeyCache
    {
        private object[] cache = new object[0];

        public object[] GetComponentKeys()
        {
            return this.cache;
        }

        public void SetComponentKeys(object[] componentKeys)
        {
            this.cache = componentKeys;
        }
    }
}
