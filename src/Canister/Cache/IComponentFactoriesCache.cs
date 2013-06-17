// <copyright file="IComponentFactoriesCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Cache
{
    using System;

    public interface IComponentFactoriesCache
    {
        Func<IComponentResolver, object>[] GetComponentFactories(object componentKey);

        void SetComponentFactories(object componentKey, Func<IComponentResolver, object>[] componentFactories);
    }
}
