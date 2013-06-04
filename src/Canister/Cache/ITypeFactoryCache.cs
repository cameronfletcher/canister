namespace Canister.Cache
{
    using System;

    public interface ITypeFactoryCache
    {
        void SetTypeFactory(Type componentType, Func<IComponentResolver, object> componentFactory);

        void Clear();

        Func<IComponentResolver, object> GetTypeFactory(Type componentType);
    }
}
