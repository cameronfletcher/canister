namespace Canister.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Canister.Cache;
    using Canister.Events;

    public class ComponentTypesView
    {
        private readonly Dictionary<Guid, Type[]> componentTypeMappings = new Dictionary<Guid, Type[]>();
        private readonly IComponentTypesCache componentTypes;

        public ComponentTypesView(IComponentTypesCache componentTypes)
        {
            this.componentTypes = componentTypes;
        }

        public void Handle(ComponentRegistered @event)
        {
            this.componentTypeMappings.Add(@event.ComponentRegistrationId, new[] { @event.OriginalComponentType });
            this.componentTypes.SetComponentTypes(GetAllComponentTypes(this.componentTypeMappings));
        }

        public void Handle(ComponentTypesAssigned @event)
        {
            this.componentTypeMappings[@event.ComponentRegistrationId] = @event.ComponentTypes;
            this.componentTypes.SetComponentTypes(GetAllComponentTypes(this.componentTypeMappings));
        }

        private static Type[] GetAllComponentTypes(Dictionary<Guid, Type[]> mappings)
        {
            return mappings.Values
                .SelectMany(componentTypes => componentTypes)
                .Distinct()
                .ToArray();
        }
    }
}
