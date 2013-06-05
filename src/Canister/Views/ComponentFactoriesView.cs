namespace Canister.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Canister.Cache;
    using Canister.Events;

    public class ComponentFactoriesView
    {
        private readonly Dictionary<Guid, Func<IComponentResolver, object>> componentFactories;
        private readonly Dictionary<Guid, object[]> componentKeyMappings;
        private readonly List<Guid> componentRegistrationIds;
        private readonly IComponentFactoriesCache cache;

        public ComponentFactoriesView(IComponentFactoriesCache cache)
        {
            this.componentFactories = new Dictionary<Guid, Func<IComponentResolver, object>>();
            this.componentKeyMappings = new Dictionary<Guid, object[]>();
            this.componentRegistrationIds = new List<Guid>();

            this.cache = cache;
        }

        public void Handle(ComponentRegistered @event)
        {
            this.componentRegistrationIds.Add(@event.ComponentRegistrationId);
            this.componentFactories.Add(@event.ComponentRegistrationId, @event.ComponentFactory);
            this.componentKeyMappings.Add(@event.ComponentRegistrationId, new[] { @event.ComponentKey });
            this.RebuildTypeFactories();
        }

        public void Handle(ComponentKeysAssigned @event)
        {
            this.componentKeyMappings[@event.ComponentRegistrationId] = @event.ComponentKeys;
            this.RebuildTypeFactories();
        }

        private void RebuildTypeFactories()
        {
            this.cache.Clear();

            var allTypeFactories = new Dictionary<object, List<Func<IComponentResolver, object>>>();

            // TODO (Cameron): This could be made more efficient. Probably with a group by statement.
            foreach (var component in this.componentKeyMappings
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
                var factories = allTypeFactories.ContainsKey(component.Type) 
                    ? allTypeFactories[component.Type] 
                    : new List<Func<IComponentResolver, object>>();

                factories.Add(component.Factory);

                allTypeFactories[component.Type] = factories;
            }
        }
    }
}
