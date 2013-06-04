namespace Canister.Cache
{
    using System;

    public class ComponentTypesCache : IComponentTypesCache
    {
        private Type[] componentTypes = new Type[0];

        public void SetComponentTypes(Type[] componentTypes)
        {
            this.componentTypes = componentTypes;
        }

        public Type[] GetComponentTypes()
        {
            return this.componentTypes;
        }
    }
}
