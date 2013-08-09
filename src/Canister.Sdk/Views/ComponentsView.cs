// <copyright file="ComponentsView.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Views
{
    using System.Linq;
    using Canister.Sdk.Cache;
    using Canister.Sdk.Events;

    public sealed class ComponentsView
    {
        private readonly IComponentsCache cache;

        public ComponentsView(IComponentsCache cache)
        {
            Guard.Against.Null(() => cache);

            this.cache = cache;
        }

        public void Handle(ComponentResolved @event)
        {
            Guard.Against.Null(() => @event);

            var components = this.cache.GetComponents(@event.RequestId);
            this.cache.SetComponents(@event.RequestId, components.Union(new[] { @event.Component }).ToArray());
        }

        public void Handle(RequestEnded @event)
        {
            Guard.Against.Null(() => @event);

            // TODO (Cameron): This. Properly.
            this.cache.SetComponents(@event.RequestId, null);
        }
    }
}
