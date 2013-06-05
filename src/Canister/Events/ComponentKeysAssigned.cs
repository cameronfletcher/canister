namespace Canister.Events
{
    using System;

    public class ComponentKeysAssigned
    {
        public ComponentKeysAssigned(Guid componentRegistrationId, object[] componentKeys)
        {
            this.ComponentRegistrationId = componentRegistrationId;
            this.ComponentKeys = componentKeys;
        }

        public Guid ComponentRegistrationId { get; private set; }

        public object[] ComponentKeys { get; private set; }
    }
}
