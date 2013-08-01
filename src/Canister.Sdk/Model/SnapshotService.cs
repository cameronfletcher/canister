// <copyright file="SnapshotService.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Model
{
    using Canister.Sdk.Cache;

    public class SnapshotService : ISnapshotService
    {
        private readonly ComponentFactoriesCache cache;

        public SnapshotService(ComponentFactoriesCache cache)
        {
            this.cache = cache;
        }

        public Snapshot GetSnapshot()
        {
            return new Snapshot(this.cache.GetComponentFactories());
        }
    }
}