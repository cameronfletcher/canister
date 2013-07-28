// <copyright file="Container.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Model
{
    using System;
    using System.Collections.Generic;
    using Canister.Sdk;
    using Canister.Sdk.Cache;
    using Canister.Sdk.Commands;

    public class ContainerBase : IContainer
    {
        private readonly MessageBus bus;
        private readonly IComponentCache componentCache;
        private readonly IComponentRegistrationCache componentRegistrationCache;

        public ContainerBase(MessageBus bus, IComponentCache componentCache, IComponentRegistrationCache componentRegistrationCache)
        {
            Guard.Against.Null(() => bus);
            Guard.Against.Null(() => componentCache);
            Guard.Against.Null(() => componentRegistrationCache);

            this.bus = bus;
            this.componentCache = componentCache;
            this.componentRegistrationCache = componentRegistrationCache;
        }

        public IComponentRegistration Register<T>(Func<IComponentContext, T> componentFactory) where T : class
        {
            Guard.Against.Null(() => componentFactory);

            var componentRegistrationId = Guid.NewGuid();

            this.bus.Send(
                new RegisterComponent
                {
                    ComponentRegistrationId = componentRegistrationId,
                    ComponentKey = typeof(T),
                    ComponentFactory = resolver => componentFactory(new ComponentContext(resolver))
                });

            return new ComponentRegistration(this.bus, componentRegistrationId);
        }

        public object Resolve(object componentKey)
        {
            var requestId = Guid.NewGuid();
            this.bus.Send(new BeginRequest { RequestId = requestId });
            this.bus.Send(
                new ResolveComponent
                {
                    RequestId = requestId,
                    ComponentKey = componentKey
                });

            var components = this.componentCache.GetComponents(requestId);
            this.bus.Send(new EndRequest { RequestId = requestId });

            return components;
        }

        public IEnumerable<object> ResolveAll(object componentKey)
        {
            var requestId = Guid.NewGuid();
            this.bus.Send(new BeginRequest { RequestId = requestId });
            this.bus.Send(
                new ResolveAllComponents
                {
                    RequestId = requestId,
                    ComponentKey = componentKey
                });

            var components = this.componentCache.GetComponents(requestId);
            this.bus.Send(new EndRequest { RequestId = requestId });

            return components;
        }
    }
}