// <copyright file="Aggregate.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Model
{
    using System.Collections.Generic;

    public abstract class Aggregate
    {
        private readonly Queue<object> events = new Queue<object>();

        internal IEnumerable<object> DequeueEvents()
        {
            while (this.events.Count > 0)
            {
                yield return this.events.Dequeue();
            }
        }

        protected void Apply(object @event)
        {
            this.events.Enqueue(@event);
        }
    }
}
