namespace Canister.Cache
{
    using System;
    using System.Collections.Generic;

    public class ComponentFactoryCache : IComponentFactoryCache
    {
        private readonly Dictionary<object, Func<IComponentResolver, object>> componentFactories =
            new Dictionary<object, Func<IComponentResolver,object>>();

        public Func<IComponentResolver, object> GetComponentFactory(object componentKey)
        {
            Func<IComponentResolver, object> componentFactory = null;
            return this.componentFactories.TryGetValue(componentKey, out componentFactory) ? componentFactory : null;
        }

        public void SetComponentFactory(object componentKey, Func<IComponentResolver, object> componentFactory)
        {
            if (componentFactory == null && this.componentFactories.ContainsKey(componentKey))
            {
                this.componentFactories.Remove(componentKey);
                return;
            }

            this.componentFactories[componentKey] = componentFactory;
        }
    }
}
