namespace Canister.Cache
{
    public interface IComponentKeyCache
    {
        object[] GetComponentKeys();

        void SetComponentKeys(object[] componentKeys);
    }
}
