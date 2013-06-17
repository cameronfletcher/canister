// <copyright file="ComponentRegistered.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Events
{
    using System;

    public class ComponentRegistered
    {
        public ComponentRegistered(Guid componentRegistrationId, object componentKey, Func<IComponentResolver, object> componentFactory)
        {
            this.ComponentRegistrationId = componentRegistrationId;
            this.ComponentKey = componentKey;
            this.ComponentFactory = componentFactory;
        }

        public Guid ComponentRegistrationId { get; private set; }

        public object ComponentKey { get; private set; }

        public Func<IComponentResolver, object> ComponentFactory { get; private set; }
    }
}
