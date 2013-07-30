// <copyright file="AllComponentFactoriesCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Cache
{
    using System.Collections.Generic;

    public class AllComponentFactoriesCache //: IComponentFactoriesCache
    {
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
