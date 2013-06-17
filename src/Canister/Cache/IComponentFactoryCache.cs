// <copyright file="IComponentFactoryCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Cache
{
    using System;

    public interface IComponentFactoryCache
    {
        Func<IComponentResolver, object> GetComponentFactory(object componentKey);

        void SetComponentFactory(object componentKey, Func<IComponentResolver, object> componentFactory);
    }
}
