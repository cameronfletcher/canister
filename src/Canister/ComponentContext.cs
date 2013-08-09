namespace Canister
{
    using System;
    using System.Collections.Generic;
    using Canister.Sdk;

    internal sealed class ComponentContext : IComponentContext
    {
        private readonly IComponentResolver resolver;

        public ComponentContext(IComponentResolver resolver)
        {
            Guard.Against.Null(() => resolver);

            this.resolver = resolver;
        }

        public object Resolve(Type componentType)
        {
            return this.resolver.Resolve(componentType);
        }

        public IEnumerable<object> ResolveAll(Type componentType)
        {
            return this.resolver.ResolveAll(componentType);
        }
    }
}
