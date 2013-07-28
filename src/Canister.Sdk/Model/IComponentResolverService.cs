// <copyright file="IComponentResolverService.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Model
{
    public interface IComponentResolverService
    {
        object Resolve(Snapshot snapshot, object componentKey);
    }
}
