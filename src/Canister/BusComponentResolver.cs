// <copyright file="ComponentResolver.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Model
{
    using System;
    using System.Collections.Generic;
    using Canister.Sdk.Cache;
    using Canister.Sdk.Commands;

    public class BusComponentResolver : IComponentResolver
    {
        private readonly MessageBus bus;
        private readonly IComponentCache componentCache;

        public BusComponentResolver(MessageBus bus, IComponentCache componentCache)
        {
            Guard.Against.Null(() => bus);
            Guard.Against.Null(() => componentCache);

            this.bus = bus;
            this.componentCache = componentCache;
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
