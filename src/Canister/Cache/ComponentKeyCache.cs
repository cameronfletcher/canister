namespace Canister.Cache
{
    public class ComponentKeyCache : IComponentKeyCache
    {
        private object[] componentKeys = new object[0];

        public object[] GetComponentKeys()
        {
            return this.componentKeys;
        }

        public void SetComponentKeys(object[] componentKeys)
        {
            this.componentKeys = componentKeys;
        }
    }
}
