// <copyright file="IComponentFactoryCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Cache
{
    using System;

    public interface IComponentFactoryCache
    {
        ComponentFactory GetComponentFactory(object componentKey);

        void SetComponentFactory(object componentKey, ComponentFactory componentFactory);
    }
}
