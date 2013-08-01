// <copyright file="ComponentFactoriesCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Cache
{
    using System.Collections.Generic;

    public class ComponentFactoriesCache : IComponentFactoriesCache
    {
        // TODO (Cameron): Check that we aren't passing around a reference to the same object here.
        // TODO (Cameron): Fix the type we're passing round.
        private Dictionary<object, ComponentFactory[]> cache = new Dictionary<object, ComponentFactory[]>();

        public Dictionary<object, ComponentFactory[]> GetComponentFactories()
        {
            return this.cache;
        }

        public void SetComponentFactories(Dictionary<object, ComponentFactory[]> componentFactories)
        {
            this.cache = componentFactories;
        }
    }
}