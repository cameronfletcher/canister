namespace Canister
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Canister.Cache;

    // TODO (Cameron): This should become part of the default container.
    public class ComponentResolver : IComponentResolver
    {
        private readonly IComponentFactoryCache componentFactoryCache;
        private readonly IComponentFactoriesCache componentFactoriesCache;

        public ComponentResolver(IComponentFactoryCache componentFactoryCache, IComponentFactoriesCache componentFactoriesCache)
        {
            Guard.Against.Null(() => componentFactoryCache);
            Guard.Against.Null(() => componentFactoriesCache);

            this.componentFactoryCache = componentFactoryCache;
            this.componentFactoriesCache = componentFactoriesCache;
        }

        public object Resolve(object componentKey)
        {
            Guard.Against.Null(() => componentKey);

            var componentFactory = this.componentFactoryCache.GetComponentFactory(componentKey);
            if (componentFactory == null)
            {
                // hmm - no such component
                throw new Exception();
            }

            return componentFactory.Invoke(this);
        }

        public IEnumerable<object> ResolveAll(object componentKey)
        {
            Guard.Against.Null(() => componentKey);

            var componentFactories = this.componentFactoriesCache.GetComponentFactories(componentKey);
            if (componentFactories == null)
            {
                // hmm - no such component
                throw new Exception();
            }

            return componentFactories.Select(componentFactory => componentFactory.Invoke(this));
        }
    }
}
