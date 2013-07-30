// <copyright file="IComponentFactoriesCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Cache
{
    public interface IComponentFactoriesCache
    {
        ComponentFactory[] GetComponentFactories(object componentKey);

        void SetComponentFactories(object componentKey, ComponentFactory[] componentFactories);
    }
}
