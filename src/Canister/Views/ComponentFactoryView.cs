namespace Canister.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Canister.Cache;
    using Canister.Events;

    public class ComponentFactoryView
    {
        private readonly Dictionary<Guid, Func<IComponentResolver, object>> componentFactories = new Dictionary<Guid, Func<IComponentResolver, object>>();
        private readonly Dictionary<Guid, object[]> componentKeyMappings = new Dictionary<Guid, object[]>();
        private readonly List<Guid> componentRegistrationIds = new List<Guid>();
        
        private readonly IComponentFactoryCache cache;

        public ComponentFactoryView(IComponentFactoryCache cache)
        {
            Guard.Against.Null(() => cache);

            this.cache = cache;
        }

        public void Handle(ComponentRegistered @event)
        {
            Guard.Against.Null(() => @event);

            this.componentRegistrationIds.Add(@event.ComponentRegistrationId);
            this.componentFactories.Add(@event.ComponentRegistrationId, @event.ComponentFactory);
            this.componentKeyMappings.Add(@event.ComponentRegistrationId, new[] { @event.OriginalComponentKey });
            this.RebuildTypeFactories();
        }

        public void Handle(ComponentKeysAssigned @event)
        {
            Guard.Against.Null(() => @event);

            this.componentKeyMappings[@event.ComponentRegistrationId] = @event.ComponentKeys;
            this.RebuildTypeFactories();
        }

        public void Handle(ExistingRegistrationsPreserved @event)
        {
            Guard.Against.Null(() => @event);

            // move this guid to the end (not front)
            this.componentRegistrationIds.Remove(@event.ComponentRegistrationId);
            this.componentRegistrationIds.Insert(0, @event.ComponentRegistrationId);

            this.RebuildTypeFactories();
        }

        private void RebuildTypeFactories()
        {
            this.cache.Clear();

            // TODO (Cameron): This could be made more efficient. Possibly with a group by statement.
            foreach (var component in this.componentKeyMappings
                .SelectMany(
                    map => map.Value,
                    (dictionary, componentKey) =>
                    new
                    {
                        RegistrationId = dictionary.Key,
                        Key = componentKey,
                        Factory = this.componentFactories[dictionary.Key],
                        Order = this.componentRegistrationIds.IndexOf(dictionary.Key)
                    })
                .OrderBy(component => component.Order))
            {
                this.cache.SetComponentFactory(component.Key, component.Factory);
            }
        }
    }
}
