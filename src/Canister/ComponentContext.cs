namespace Canister
{
    using System;
    using System.Collections.Generic;
    using Canister.Sdk;

    public class ComponentContext : IComponentContext
    {
        public ComponentContext(IComponentResolver resolver)
        {
        }

        public object Resolve(object componentKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> ResolveAll(object componentKey)
        {
            throw new NotImplementedException();
        }
    }

}
