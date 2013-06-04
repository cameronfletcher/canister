namespace Canister.Cache
{
    using System;
    using System.Collections.Generic;

    public class TypeFactoryCache : ITypeFactoryCache
    {
        private readonly Dictionary<Type, Func<IComponentResolver, object>> typeFactories =
            new Dictionary<Type, Func<IComponentResolver,object>>();

        public void SetTypeFactory(Type componentType, Func<IComponentResolver, object> componentFactory)
        {
            this.typeFactories[componentType] = componentFactory;
        }

        public void Clear()
        {
            this.typeFactories.Clear();
        }

        public Func<IComponentResolver, object> GetTypeFactory(Type componentType)
        {
            Func<IComponentResolver, object> componentFactory = null;
            return this.typeFactories.TryGetValue(componentType, out componentFactory) ? componentFactory : null;
        }
    }
}
