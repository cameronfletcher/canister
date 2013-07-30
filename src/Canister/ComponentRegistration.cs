// <copyright file="ComponentRegistration.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System;
    using Canister.Sdk;
    using Canister.Sdk.Commands;

    public class ComponentRegistration : IComponentRegistration
    {
        private readonly MessageBus bus;
        private readonly Guid id;

        public ComponentRegistration(MessageBus bus, Guid id)
        {
            Guard.Against.Null(() => bus);

            this.bus = bus;
            this.id = id;
        }

        public IComponentRegistration As(object[] componentKeys)
        {
            Guard.Against.NullOrEmpty(() => componentKeys);

            this.bus.Send(
                new AssignComponentKeys 
                { 
                    ComponentRegistrationId = this.id,
                    ComponentKeys = componentKeys
                });

            return this;
        }

        public IComponentRegistration PreserveExistingRegistrations()
        {
            this.bus.Send(
                new PreserveExistingRegistrations
                {
                    ComponentRegistrationId = this.id
                });

            return this;
        }
    }
}
