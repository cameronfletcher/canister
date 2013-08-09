namespace Canister
{
    using System;
    using Canister.Sdk.Factories;
    using Canister.Sdk.Infrastructure;

    internal class CustomContainerDependencies : ContainerDependencies
    {
        public CustomRegisterComponentHandler CustomRegisterComponentHandler { get; set; }

        public IRepository<Guid, CustomComponentRegistration> CustomComponentRegistrationRepository { get; set; }
    }
}
