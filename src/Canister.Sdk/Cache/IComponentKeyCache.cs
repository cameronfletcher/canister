// <copyright file="IComponentKeyCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Cache
{
    public interface IComponentKeyCache
    {
        object[] GetComponentKeys();

        void SetComponentKeys(object[] componentKeys);
    }
}
