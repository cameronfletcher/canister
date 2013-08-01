// <copyright file="Container.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Canister.Sdk;
    using Canister.Sdk.Cache;
    using Canister.Sdk.Commands;

    public class Container : IContainer
    {
        private readonly MessageBus bus;
        private readonly IComponentsCache componentCache;

        public Container(MessageBus bus, IComponentsCache componentCache)
        {
            Guard.Against.Null(() => bus);
            Guard.Against.Null(() => componentCache);

            this.bus = bus;
            this.componentCache = componentCache;
        }

        public IComponentRegistration Register<T>(Func<IComponentContext, T> componentFactory)
        {
            Guard.Against.Null(() => componentFactory);

            var componentRegistrationId = Guid.NewGuid();

            this.bus.Send(
                new RegisterComponent
                {
                    ComponentRegistrationId = componentRegistrationId,
                    ComponentKey = typeof(T),
                    ComponentFactory = componentResolver => componentFactory.Invoke(new ComponentContext(componentResolver))
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

            var components = this.componentCache.GetComponents(requestId).First();
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

            var components = this.componentCache.GetComponents(requestId);
            this.bus.Send(new EndRequest { RequestId = requestId });

            return components;
        }
    }
}