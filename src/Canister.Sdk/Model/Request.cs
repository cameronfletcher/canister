// <copyright file="Request.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Model
{
    using System;
    using Canister.Sdk.Events;

    public class Request : Aggregate
    {
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
            this.Snapshot = snapshot;
        }

        public Guid Id { get; private set; }

        public Snapshot Snapshot { get; private set; }

        public virtual void End()
        {
            var @event = new RequestEnded
            {
                RequestId = this.Id,
            };

            this.Apply(@event);

            this.hasEnded = true;
        }

        //public void Track(object component)
        //{
        //    if (this.hasEnded)
        //    {
        //        throw new ComponentResolutionException();
        //    }
        //}

        public void Resolve(object componentKey, IComponentResolverService componentResolverService)
        {
            if (this.hasEnded)
            {
                throw new ComponentResolutionException();
            }

            var component = componentResolverService.Resolve(this.Snapshot, componentKey);

            var @event = new ComponentResolved
            {
                RequestId = this.Id,
                Component = component,
            };

            this.Apply(@event);
        }
    }
}
