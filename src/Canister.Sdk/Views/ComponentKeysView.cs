// <copyright file="ComponentKeysView.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Canister.Sdk.Cache;
    using Canister.Sdk.Events;

    public sealed class ComponentKeysView
    {
        private readonly Dictionary<Guid, object[]> componentKeyMappings = new Dictionary<Guid, object[]>();
        private readonly IComponentKeyCache cache;

        public ComponentKeysView(IComponentKeyCache cache)
        {
            Guard.Against.Null(() => cache);

            this.cache = cache;
        }

        public void Handle(ComponentRegistered @event)
        {
            Guard.Against.Null(() => @event);

            this.componentKeyMappings.Add(@event.ComponentRegistrationId, new[] { @event.ComponentKey });
            this.cache.SetComponentKeys(GetAllComponentKeys(this.componentKeyMappings));
        }

        public void Handle(ComponentKeysAssigned @event)
        {
            Guard.Against.Null(() => @event);

            this.componentKeyMappings[@event.ComponentRegistrationId] = @event.ComponentKeys;
            this.cache.SetComponentKeys(GetAllComponentKeys(this.componentKeyMappings));
        }

        private static object[] GetAllComponentKeys(Dictionary<Guid, object[]> mappings)
        {
            return mappings.Values
                .SelectMany(componentTypes => componentTypes)
                .Distinct()
                .ToArray();
        }
    }
}
