namespace Canister.Sdk.Persistence
{
    using System;
    using Canister.Sdk.Model;
    using Canister.Sdk;
    using Canister.Sdk.Persistence;
    using ComponentRegistration = Canister.Sdk.Model.ComponentRegistration;

    public class CustomComponentRegistrationRepository : Repository<Guid, CustomComponentRegistration>, IRepository<Guid, ComponentRegistration>
    {
        public CustomComponentRegistrationRepository(MessageBus bus)
            : base(bus, componentRegistration => componentRegistration.Id)
        {
        }

        public new ComponentRegistration Get(Guid naturalKey)
        {
            return base.Get(naturalKey);
        }

        public void Save(ComponentRegistration aggregate)
        {
            var componentRegistration = aggregate as CustomComponentRegistration;
            base.Save(componentRegistration);
        }
    }
}
