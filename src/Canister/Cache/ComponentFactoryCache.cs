namespace Canister.Cache
{
    using System;
    using System.Collections.Generic;

    public class ComponentFactoryCache : IComponentFactoryCache
    {
        private readonly Dictionary<object, Func<IComponentResolver, object>> typeFactories =
            new Dictionary<object, Func<IComponentResolver,object>>();

        public Func<IComponentResolver, object> GetComponentFactory(object componentKey)
        {
            Func<IComponentResolver, object> componentFactory = null;
            return this.typeFactories.TryGetValue(componentKey, out componentFactory) ? componentFactory : null;
        }

        public void SetComponentFactory(object componentKey, Func<IComponentResolver, object> componentFactory)
        {
            this.typeFactories[componentKey] = componentFactory;
        }

        //public void Clear()
        //{
        //    this.typeFactories.Clear();
        //}
    }
}
