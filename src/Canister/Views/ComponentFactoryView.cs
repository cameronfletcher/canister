namespace Canister.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Canister.Cache;
    using Canister.Events;

    public class ComponentFactoryView
    {
        private readonly Dictionary<Guid, Component> components = new Dictionary<Guid, Component>();
        private readonly IComponentFactoryCache cache;

        private int componentRegistrationCount;

        public ComponentFactoryView(IComponentFactoryCache cache)
        {
            Guard.Against.Null(() => cache);

            this.cache = cache;
        }

        public void Handle(ComponentRegistered @event)
        {
            Guard.Against.Null(() => @event);

            this.components.Add(
                @event.ComponentRegistrationId,
                new Component
                {
                    RegistrationCount = componentRegistrationCount++,
                    Keys = new[] { @event.ComponentKey },
                    Factory = @event.ComponentFactory,
                });

            this.RebuildCache(new object[] { @event.ComponentKey });
        }

        public void Handle(ComponentKeysAssigned @event)
        {
            Guard.Against.Null(() => @event);

            var component = this.components[@event.ComponentRegistrationId];
            var affectedComponentKeys = component.Keys.Union(@event.ComponentKeys).ToArray();

            component.Keys = @event.ComponentKeys;

            this.RebuildCache(affectedComponentKeys);
        }

        public void Handle(ExistingRegistrationsPreserved @event)
        {
            Guard.Against.Null(() => @event);

            var component = this.components[@event.ComponentRegistrationId];
            
            component.PreserveRegistrations = true;

            this.RebuildCache(component.Keys);
        }

        private void RebuildCache(object[] componentKeys)
        {
            foreach (var componentKey in componentKeys)
            {
                var componentFactory = this.components.Values
                    .Where(component => component.Keys.Contains(componentKey))
                    .OrderBy(component => component.PreserveRegistrations)
                    .ThenByDescending(component => component.RegistrationCount)
                    .Select(component => component.Factory)
                    .FirstOrDefault();

                this.cache.SetComponentFactory(componentKey, componentFactory);
            }
        }
    }
}
