namespace Canister.Sdk.Factories
{
    using System;
    using Canister.Sdk.Handlers;
    using Canister.Sdk.Infrastructure;
    using Canister.Sdk.Model;

    internal class CustomContainerDependencies : ContainerDependencies
    {
        public CustomRegisterComponentHandler CustomRegisterComponentHandler { get; set; }

        public IRepository<Guid, CustomComponentRegistration> CustomComponentRegistrationRepository { get; set; }
    }
}
