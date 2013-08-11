// <copyright file="ComponentRegistration.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Infrastructure;

    internal class ComponentRegistration : IComponentRegistration
    {
        private readonly MessageBus bus;
        private readonly Guid id;
        private readonly Type originalType;

        public ComponentRegistration(MessageBus bus, Guid id, Type originalType)
        {
            Guard.Against.Null(() => bus);
            Guard.Against.Null(() => originalType);

            this.bus = bus;
            this.id = id;
            this.originalType = originalType;
        }

        public IComponentRegistration As(Type[] componentTypes)
        {
            Guard.Against.NullOrEmpty(() => componentTypes);

            this.bus.Send(
                new AssignComponentKeys 
                { 
                    ComponentRegistrationId = this.id,
                    ComponentKeys = componentTypes
                });

            return this;
        }

        public IComponentRegistration AsImplementedInterfaces()
        {
            this.bus.Send(
                new AssignComponentKeys
                {
                    ComponentRegistrationId = this.id,
                    ComponentKeys = this.originalType.GetInterfaces()
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