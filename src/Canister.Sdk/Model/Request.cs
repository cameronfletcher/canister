// <copyright file="Request.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Model
{
    using System;
    using Canister.Sdk.Events;

    public class Request : Aggregate
    {
        private readonly Snapshot snapshot;

        private bool hasEnded;

        public Request(Guid id, Snapshot snapshot)
        {
            Guard.Against.Null(() => snapshot);

            var @event = new RequestStarted
            {
                RequestId = id
            };

            this.Apply(@event);

            this.Id = @event.RequestId;
            this.snapshot = snapshot;
        }

        public Guid Id { get; private set; }

        public virtual void End()
        {
            var @event = new RequestEnded
            {
                RequestId = this.Id,
            };

            this.Apply(@event);

            this.hasEnded = true;
        }

        public void Resolve(object componentKey, IComponentResolverService componentResolverService)
        {
            if (this.hasEnded)
            {
                throw new ComponentResolutionException();
            }

            var component = componentResolverService.Resolve(componentKey, this.snapshot);

            var @event = new ComponentResolved
            {
                RequestId = this.Id,
                Component = component,
            };

            this.Apply(@event);
        }

        public void ResolveAll(object componentKey, IComponentResolverService componentResolverService)
        {
            if (this.hasEnded)
            {
                throw new ComponentResolutionException();
            }

            var components = componentResolverService.ResolveAll(componentKey, this.snapshot);

            foreach (var component in components)
            {
                var @event = new ComponentResolved
                {
                    RequestId = this.Id,
                    Component = component,
                };

                this.Apply(@event);
            }
        }
    }
}