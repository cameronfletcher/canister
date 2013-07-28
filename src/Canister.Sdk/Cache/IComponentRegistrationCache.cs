// <copyright file="IComponentRegistrationCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Cache
{
    using System;
    using System.Collections.Generic;

    public interface IComponentRegistrationCache
    {
        Guid GetComponentRegistrationId(object componentKey);

        IEnumerable<Guid> GetAllComponentRegistrationIds(object componentKey);
    }
}
