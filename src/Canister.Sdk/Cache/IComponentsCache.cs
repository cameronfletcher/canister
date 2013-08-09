// <copyright file="IComponentsCache.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Cache
{
    using System;

    // TODO (Cameron): There should be tests to cover the expected functionality of an implementation of this interface.
    public interface IComponentsCache
    {
        // do not throw - return empty array if none
        object[] GetComponents(Guid requestId);

        void PutComponents(Guid requestId, object[] components);

        // do not throw
        void DeleteComponents(Guid requestId);
    }
}
