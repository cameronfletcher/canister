// <copyright file="ComponentRegistration.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Model
{
    using System;
    using Canister.Events;

    public class ComponentRegistration : IComponentRegistration
    {
        private readonly TurnkeyBus bus;
        private readonly Guid id;

        public ComponentRegistration(TurnkeyBus bus, Guid id)
        {
            Guard.Against.Null(() => bus);

            this.bus = bus;
            this.id = id;
        }

        public IComponentRegistration As(object[] componentKeys)
        {
            Guard.Against.NullOrEmpty(() => componentKeys);

            this.bus.Send(new ComponentKeysAssigned(this.id, componentKeys));

            return this;
        }

        public IComponentRegistration PreserveExistingRegistrations()
        {
            this.bus.Send(new ExistingRegistrationsPreserved(this.id));

            return this;
        }
    }
}
