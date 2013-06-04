namespace Canister.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Canister.Cache;
    using Canister.Events;

    public class TypeFactoryView
    {
        private readonly Dictionary<Guid, Func<IComponentResolver, object>> componentFactories = 
            new Dictionary<Guid, Func<IComponentResolver, object>>();

        private readonly Dictionary<Guid, Type[]> componentTypeMappings = new Dictionary<Guid, Type[]>();
        private readonly List<Guid> componentRegistrationIds = new List<Guid>();
        private readonly ITypeFactoryCache typeFactories;

        public TypeFactoryView(ITypeFactoryCache typeFactories)
        {
            this.typeFactories = typeFactories;
        }

        public void Handle(ComponentRegistered @event)
        {
            this.componentRegistrationIds.Add(@event.ComponentRegistrationId);
            this.componentFactories.Add(@event.ComponentRegistrationId, @event.ComponentFactory);
            this.componentTypeMappings.Add(@event.ComponentRegistrationId, new[] { @event.OriginalComponentType });
            this.RebuildTypeFactories();
        }

        public void Handle(ComponentTypesAssigned @event)
        {
            this.componentTypeMappings[@event.ComponentRegistrationId] = @event.ComponentTypes;
            this.RebuildTypeFactories();
        }

        private void RebuildTypeFactories()
        {
            this.typeFactories.Clear();

            // TODO (Cameron): This could be made more efficient. Possibly with a group by statement.
            foreach (var component in this.componentTypeMappings
                .SelectMany(
                    map => map.Value,
                    (dictionary, type) =>
                    new
                    {
                        RegistrationId = dictionary.Key,
                        Type = type,
                        Factory = this.componentFactories[dictionary.Key],
                        Order = this.componentRegistrationIds.IndexOf(dictionary.Key)
                    })
                .OrderBy(component => component.Order))
            {
                this.typeFactories.SetTypeFactory(component.Type, component.Factory);
            }
        }
    }
}
