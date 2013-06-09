namespace Canister.Model
{
    using System;
    using Canister.Events;

    public class ComponentRegistration : IComponentRegistration
    {
        private readonly TurnkeyBus bus;
        private readonly Guid id;

        public ComponentRegistration(TurnkeyBus bus, Guid id)
        {
            Guard.Against.Null(() => bus);

            this.bus = bus;
            this.id = id;
        }

        public IComponentRegistration As(object[] componentKeys)
        {
            Guard.Against.NullOrEmpty(() => componentKeys);

            var @event = new ComponentKeysAssigned(this.id, componentKeys);
            this.bus.Send(@event);

            return this;
        }

        public IComponentRegistration PreserveExistingRegistrations()
        {
            var @event = new ExistingRegistrationsPreserved(this.id);
            this.bus.Send(@event);

            return this;
        }
    }
}
