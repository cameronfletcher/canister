// <copyright file="CustomRepository.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Infrastructure
{
    using System;
    using System.Globalization;
    using Canister.Sdk.Model;

    public class CustomRepository<TKey, TAggregate, TAggregateBase> : DefaultRepository<TKey, TAggregate>, IRepository<TKey, TAggregateBase>
        where TAggregate : TAggregateBase
        where TAggregateBase : Aggregate
    {
        public CustomRepository(MessageBus bus, Func<TAggregate, TKey> naturalKeyFunction)
            : base(bus, naturalKeyFunction)
        {
        }

        public new TAggregateBase Get(TKey naturalKey)
        {
            return base.Get(naturalKey);
        }

        public void Save(TAggregateBase aggregate)
        {
            var aggregateRoot = aggregate as TAggregate;
            if (aggregateRoot == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot save a type of {0} in a repository of type {0}.",
                        typeof(TAggregateBase).Name,
                        typeof(TAggregate).Name));
            }

            base.Save(aggregateRoot);
        }
    }
}
