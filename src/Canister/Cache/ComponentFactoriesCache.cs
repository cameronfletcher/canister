namespace Canister.Cache
{
    using System;
    using System.Collections.Generic;

    public class ComponentFactoriesCache : IComponentFactoriesCache
    {
        private readonly Dictionary<object, Func<IComponentResolver, object>[]> componentFactories =
            new Dictionary<object, Func<IComponentResolver,object>[]>();

        public Func<IComponentResolver, object>[] GetComponentFactories(object componentKey)
        {
            Func<IComponentResolver, object>[] componentFactories = null;
            return this.componentFactories.TryGetValue(componentKey, out componentFactories) ? componentFactories : null;
        }

        public void SetComponentFactories(object componentKey, Func<IComponentResolver, object>[] componentFactories)
        {
            if (componentFactories == null && this.componentFactories.ContainsKey(componentKey))
            {
                this.componentFactories.Remove(componentKey);
                return;
            }

            this.componentFactories[componentKey] = componentFactories;
        }
    }
}
