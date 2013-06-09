namespace Canister.Cache
{
    using System;

    public interface IComponentFactoriesCache
    {
        Func<IComponentResolver, object>[] GetComponentFactories(object componentKey);

        void SetComponentFactories(object componentKey, Func<IComponentResolver, object>[] componentFactories);
    }
}
