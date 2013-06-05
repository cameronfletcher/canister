namespace Canister.Events
{
    using System;

    public class ComponentRegistered
    {
        public ComponentRegistered(Guid componentRegistrationId, object originalcomponentKey, Func<IComponentResolver, object> componentFactory)
        {
            this.ComponentRegistrationId = componentRegistrationId;
            this.OriginalComponentKey = originalcomponentKey;
            this.ComponentFactory = componentFactory;
        }

        public Guid ComponentRegistrationId { get; private set; }

        public object OriginalComponentKey { get; private set; }

        public Func<IComponentResolver, object> ComponentFactory { get; private set; }
    }
}
