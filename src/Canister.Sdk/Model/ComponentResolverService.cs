// <copyright file="ComponentResolverService.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Model
{
    public class ComponentResolverService : IComponentResolverService
    {
        public object Resolve(Snapshot snapshot, object componentKey)
        {
            var compnentResolver = new ComponentResolver(snapshot);
            return compnentResolver.Resolve(componentKey);
        }
    }
}
