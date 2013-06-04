namespace Canister.Events
{
    using System;

    public class ExistingRegistrationsPreserved
    {
        public ExistingRegistrationsPreserved(Guid componentRegistrationId)
        {
            this.ComponentRegistrationId = componentRegistrationId;
        }

        public Guid ComponentRegistrationId { get; private set; }
    }
}
