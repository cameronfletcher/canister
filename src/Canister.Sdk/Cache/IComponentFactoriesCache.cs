// <copyright file="IComponentFactoriesCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Cache
{
    using System.Collections.Generic;

    public interface IComponentFactoriesCache
    {
        Dictionary<object, ComponentFactory[]> GetComponentFactories();

        void SetComponentFactories(Dictionary<object, ComponentFactory[]> componentFactories);
    }
}