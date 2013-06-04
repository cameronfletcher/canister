namespace Canister.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Canister.Cache;
    using Canister.Events;

    public class TypeFactoriesView
    {
        private readonly Dictionary<Guid, Func<IComponentResolver, object>> componentFactories;
        private readonly Dictionary<Guid, Type[]> componentTypeMappings;
        private readonly List<Guid> componentRegistrationIds;
        private readonly ITypeFactoriesCache typeFactories;

        public TypeFactoriesView(ITypeFactoriesCache typeFactories)
        {
            this.componentFactories = new Dictionary<Guid, Func<IComponentResolver, object>>();
            this.componentTypeMappings = new Dictionary<Guid, Type[]>();
            this.componentRegistrationIds = new List<Guid>();

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

            var allTypeFactories = new Dictionary<Type, List<Func<IComponentResolver, object>>>();

            // TODO (Cameron): This could be made more efficient. Probably with a group by statement.
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
                var factories = allTypeFactories.ContainsKey(component.Type) 
                    ? allTypeFactories[component.Type] 
                    : new List<Func<IComponentResolver, object>>();

                factories.Add(component.Factory);

                allTypeFactories[component.Type] = factories;
            }
        }
    }
}
