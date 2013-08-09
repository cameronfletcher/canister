// <copyright file="Container.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Canister.Sdk.Factories;
    using Canister.Sdk;
    using Canister.Sdk.Cache;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Infrastructure;

    public class Container : IContainer
    {
        private readonly MessageBus bus;
        private readonly IComponentsCache componentsCache;

        public Container()
            : this(new CustomContainerDependenciesFactory().Create())
        {
        }

        private Container(ContainerDependencies dependencies)
        {
            Guard.Against.Null(() => dependencies);
            Guard.Against.Null(() => dependencies.Bus);
            Guard.Against.Null(() => dependencies.ComponentsCache);

            this.bus = dependencies.Bus;
            this.componentsCache = dependencies.ComponentsCache;
        }

        public IComponentRegistration Register<T>(Func<IComponentResolver, T> componentFactory)
        {
            Guard.Against.Null(() => componentFactory);

            var componentRegistrationId = Guid.NewGuid();

            this.bus.Send(
                new RegisterComponent
                {
                    ComponentRegistrationId = componentRegistrationId,
                    ComponentKey = typeof(T),
                    ComponentFactory = componentResolver => componentFactory.Invoke(new ComponentResolver(componentResolver))
                });

            return new ComponentRegistration(this.bus, componentRegistrationId);
        }

        public object Resolve(Type componentType)
        {
            var requestId = Guid.NewGuid();
            this.bus.Send(new BeginRequest { RequestId = requestId });
            this.bus.Send(
                new ResolveComponent
                {
                    RequestId = requestId,
                    ComponentKey = componentType
                });

            var components = this.componentsCache.GetComponents(requestId).First();
            this.bus.Send(new EndRequest { RequestId = requestId });

            return components;
        }

        public IEnumerable<object> ResolveAll(Type componentType)
        {
            var requestId = Guid.NewGuid();
            this.bus.Send(new BeginRequest { RequestId = requestId });
            this.bus.Send(
                new ResolveAllComponents
                {
                    RequestId = requestId,
                    ComponentKey = componentType
                });

            var components = this.componentsCache.GetComponents(requestId);
            this.bus.Send(new EndRequest { RequestId = requestId });

            return components;
        }
    }
}