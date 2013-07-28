// <copyright file="IComponentCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Cache
{
    using System;
    using System.Collections.Generic;

    public interface IComponentCache
    {
        object[] GetComponents(Guid requiestId);

        void SetComponents(Guid requestId, object[] components);
    }
}
