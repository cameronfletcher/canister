// <copyright file="SnapshotService.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Model
{
    using Canister.Sdk.Cache;

    public class SnapshotService : ISnapshotService
    {
        private readonly IComponentFactoriesCache cache;

        public SnapshotService(IComponentFactoriesCache cache)
        {
            this.cache = cache;
        }

        public Snapshot GetSnapshot()
        {
            return new Snapshot(this.cache.GetComponentFactories());
        }
    }
}