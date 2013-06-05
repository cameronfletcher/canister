namespace Canister
{
    using System.Collections.Generic;

    public interface IComponentResolver
    {
        object Resolve(object componentKey);

        IEnumerable<object> ResolveAll(object componentKey);
    }
}
