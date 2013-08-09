// <copyright file="IComponentFactoriesCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Cache
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public interface IComponentFactoriesCache
    {
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Inappropriate.")]
        Dictionary<object, ComponentFactory[]> GetComponentFactories();

        void SetComponentFactories(Dictionary<object, ComponentFactory[]> componentFactories);
    }
}