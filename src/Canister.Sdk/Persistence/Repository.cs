// <copyright file="Repository.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Persistence
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Canister.Sdk.Model;

    public class Repository<TKey, TAggregate> : IRepository<TKey, TAggregate>
        where TAggregate : Aggregate
    {
        private readonly Dictionary<TKey, TAggregate> store = new Dictionary<TKey, TAggregate>();

        private readonly MessageBus bus;
        private readonly Func<TAggregate, TKey> naturalKeyFunction;

        public Repository(MessageBus bus, Func<TAggregate, TKey> naturalKeyFunction)
        {
            Guard.Against.Null(() => bus);
            Guard.Against.Null(() => naturalKeyFunction);

            this.bus = bus;
            this.naturalKeyFunction = naturalKeyFunction;
        }

        public TAggregate Get(TKey naturalKey)
        {
            TAggregate aggregate;
            if (!this.store.TryGetValue(naturalKey, out aggregate))
            {
                throw new NotSupportedException(
                    string.Format(
                        CultureInfo.InvariantCulture, 
                        "The type '{0}' identified by the key '{1}' cannot be found.",
                        typeof(TAggregate).Name,
                        naturalKey.ToString()));
            }

            return aggregate;
        }

        public void Save(TAggregate aggregate)
        {
            Guard.Against.Null(() => aggregate);

            var naturalKey = this.naturalKeyFunction(aggregate);
            
            TAggregate savedAggregate;
            if (this.store.TryGetValue(naturalKey, out savedAggregate) && !object.ReferenceEquals(aggregate, savedAggregate))
            {
                throw new NotSupportedException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "The type '{0}' identified by the key '{1}' already exists.",
                        typeof(TAggregate).Name,
                        naturalKey.ToString()));
            }

            foreach (var @event in aggregate.DequeueEvents())
            {
                this.bus.Send(@event);
            }

            this.store[naturalKey] = aggregate;
        }
    }
}