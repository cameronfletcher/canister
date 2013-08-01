namespace Canister.Sdk
{
    using System;
    using System.Collections.Generic;

    public interface IComponentContext
    {
        object Resolve(Type componentType);

        IEnumerable<object> ResolveAll(Type componentType);
    }
}