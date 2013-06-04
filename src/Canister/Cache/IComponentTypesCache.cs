namespace Canister.Cache
{
    using System;

    public interface IComponentTypesCache
    {
        void SetComponentTypes(Type[] componentTypes);

        Type[] GetComponentTypes();
    }
}
