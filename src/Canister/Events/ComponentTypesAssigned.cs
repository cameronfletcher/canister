namespace Canister.Events
{
    using System;

    public class ComponentTypesAssigned
    {
        public ComponentTypesAssigned(Guid componentRegistrationId, Type[] componentTypes)
        {
            this.ComponentRegistrationId = componentRegistrationId;
            this.ComponentTypes = componentTypes;
        }

        public Guid ComponentRegistrationId { get; private set; }

        public Type[] ComponentTypes { get; private set; }
    }
}
