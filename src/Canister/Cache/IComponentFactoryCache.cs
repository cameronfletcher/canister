namespace Canister.Cache
{
    using System;

    public interface IComponentFactoryCache
    {
        Func<IComponentResolver, object> GetComponentFactory(object componentKey);

        void SetComponentFactory(object componentKey, Func<IComponentResolver, object> componentFactory);

        //void Clear();
    }
}
