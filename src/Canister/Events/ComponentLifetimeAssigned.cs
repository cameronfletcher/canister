// <copyright file="ComponentLifetimeAssigned.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Events
{
    using System;

    public class ComponentLifetimeAssigned
    {
        public ComponentLifetimeAssigned(Guid componentRegistrationId, IComponentLifetime componentLifetime)
        {
            this.ComponentRegistrationId = componentRegistrationId;
            this.ComponentLifetime = componentLifetime;
        }

        public Guid ComponentRegistrationId { get; private set; }

        public IComponentLifetime ComponentLifetime { get; private set; }
    }
}
