// <copyright file="ComponentResolverService.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Model
{
    using System.Collections.Generic;

    public class ComponentResolverService : IComponentResolverService
    {
        public object Resolve(object componentKey, Snapshot snapshot)
        {
            var compnentResolver = new ComponentResolver(snapshot);
            return compnentResolver.Resolve(componentKey);
        }

        public IEnumerable<object> ResolveAll(object componentKey, Snapshot snapshot)
        {
            var compnentResolver = new ComponentResolver(snapshot);
            return compnentResolver.ResolveAll(componentKey);
        }
    }
}