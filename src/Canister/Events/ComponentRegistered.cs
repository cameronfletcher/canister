namespace Canister.Events
{
    using System;

    public class ComponentRegistered
    {
        public ComponentRegistered(Guid componentRegistrationId, Type originalcomponentType, Func<IComponentResolver, object> componentFactory)
        {
            this.ComponentRegistrationId = componentRegistrationId;
            this.OriginalComponentType = originalcomponentType;
            this.ComponentFactory = componentFactory;
        }

        public Guid ComponentRegistrationId { get; private set; }

        public Type OriginalComponentType { get; private set; }

        public Func<IComponentResolver, object> ComponentFactory { get; private set; }
    }
}
