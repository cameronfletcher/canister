// <copyright file="IComponentResolverService.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Model
{
    using System.Collections.Generic;

    public interface IComponentResolverService
    {
        object Resolve(object componentKey, Snapshot snapshot);

        IEnumerable<object> ResolveAll(object componentKey, Snapshot snapshot);
    }
}