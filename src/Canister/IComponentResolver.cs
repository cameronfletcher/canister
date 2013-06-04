namespace Canister
{
    using System;
    using System.Collections.Generic;

    public interface IComponentResolver
    {
        object Resolve(Type registrationType);

        T Resolve<T>() where T : class;

        IEnumerable<object> ResolveAll(Type registrationType);

        IEnumerable<T> ResolveAll<T>() where T : class;
    }
}
