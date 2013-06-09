namespace Canister
{
    public interface IComponentRegistration
    {
        IComponentRegistration As(object[] componentKeys);

        IComponentRegistration PreserveExistingRegistrations();
    }
}
