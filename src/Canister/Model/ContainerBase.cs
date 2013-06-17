// <copyright file="ContainerBase.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Model
{
    using System;
    using System.Collections.Generic;
    using Canister.Events;

    public class ContainerBase : IContainer
    {
        private readonly TurnkeyBus bus;
        private readonly IComponentResolver componentResolver;

        public ContainerBase(TurnkeyBus bus, IComponentResolver componentResolver)
        {
            Guard.Against.Null(() => bus);
            Guard.Against.Null(() => componentResolver);

            this.bus = bus;
            this.componentResolver = componentResolver;
        }

        public IComponentRegistration Register<T>(Func<IComponentResolver, T> componentFactory) where T : class
        {
            Guard.Against.Null(() => componentFactory);

            var componentRegistrationId = Guid.NewGuid();

            this.bus.Send(new ComponentRegistered(componentRegistrationId, typeof(T), componentFactory));

            return new ComponentRegistration(this.bus, componentRegistrationId);
        }

        public object Resolve(object componentKey)
        {
            return this.componentResolver.Resolve(componentKey);
        }

        public IEnumerable<object> ResolveAll(object componentKey)
        {
            return this.componentResolver.ResolveAll(componentKey);
        }
    }
}
