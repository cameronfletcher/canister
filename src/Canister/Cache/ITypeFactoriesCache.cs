namespace Canister.Cache
{
    using System;

    public interface ITypeFactoriesCache
    {
        void SetTypeFactories(Type componentType, Func<IComponentResolver, object>[] componentFactories);

        void Clear();

        Func<IComponentResolver, object>[] GetTypeFactories(Type componentType);
    }
}
