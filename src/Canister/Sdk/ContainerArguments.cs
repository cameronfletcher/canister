namespace Canister.Sdk
{
    using Canister.Sdk.Cache;

    internal class ContainerArguments
    {
        public MessageBus MessageBus { get; set; }

        public IComponentsCache ComponentsCache { get; set; }
    }
}
