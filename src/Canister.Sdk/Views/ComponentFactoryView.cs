// <copyright file="ComponentFactoryView.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Canister.Sdk.Cache;
    using Canister.Sdk.Events;
    using Canister.Sdk.Model;
    using Canister.Sdk.ReadModel;

    public sealed class ComponentFactoryView
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
                    RegistrationCount = this.componentRegistrationCount++,
                    Keys = new[] { @event.ComponentKey },
                    Factory = @event.ComponentFactory,
                });

            this.RebuildCache(new[] { @event.ComponentKey });
        }

        public void Handle(ComponentKeysAssigned @event)
        {
            Guard.Against.Null(() => @event);

            var component = this.components[@event.ComponentRegistrationId];
            var existingComponentKeys = component.Keys;

            component.Keys = @event.ComponentKeys;

            this.RebuildCache(existingComponentKeys.Union(@event.ComponentKeys).ToArray());
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
                var componentFactory = this.GetComponentFactory(componentKey);
                this.cache.SetComponentFactory(componentKey, componentFactory);
            }
        }

        private Func<IComponentResolver, object> GetComponentFactory(object componentKey)
        {
            return this.components.Values
                .Where(component => component.Keys.Contains(componentKey))
                .OrderBy(component => component.PreserveRegistrations)
                .ThenByDescending(component => component.RegistrationCount)
                .Select(component => component.Factory)
                .FirstOrDefault();
        }
    }
}
