namespace Canister.Sdk.Model
{
    using Canister.Sdk.Cache;

    public class SnapshotService : ISnapshotService
    {
        private readonly AllComponentFactoriesCache cache;

        public SnapshotService(AllComponentFactoriesCache cache)
        {
            this.cache = cache;
        }

        public Snapshot GetSnapshot()
        {
            return new Snapshot(this.cache.GetComponentFactories());
        }
    }
}
