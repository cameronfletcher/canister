namespace Canister.Sdk
{
    using System;
    using System.Collections.Generic;

    public sealed class ComponentContext : IComponentContext
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
