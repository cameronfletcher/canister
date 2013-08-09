namespace Canister.Sdk.Persistence
{
    using System;
    using System.Globalization;
    using Canister.Sdk.Model;

    public class CustomRepository<TKey, TAggregate, TBase> : Repository<TKey, TAggregate>, IRepository<TKey, TBase>
        where TAggregate : TBase
        where TBase : Aggregate
    {
        public CustomRepository(MessageBus bus, Func<TAggregate, TKey> naturalKeyFunction)
            : base(bus, naturalKeyFunction)
        {
        }

        public new TBase Get(TKey naturalKey)
        {
            return base.Get(naturalKey);
        }

        public void Save(TBase aggregate)
        {
            var aggregateRoot = aggregate as TAggregate;
            if (aggregateRoot == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot save a type of {0} in a repository of type {0}.",
                        typeof(TBase).Name,
                        typeof(TAggregate).Name));
            }

            base.Save(aggregateRoot);
        }
    }
}
