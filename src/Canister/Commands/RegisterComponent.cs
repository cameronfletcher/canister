namespace Canister.Commands
{
    using System;

    public class RegisterComponent
    {
        public Guid ComponentRegistrationId { get; set; }

        public object ComponentKey { get; private set; }

        public Func<IComponentResolver, object> ComponentFactory { get; set; }
    }
}