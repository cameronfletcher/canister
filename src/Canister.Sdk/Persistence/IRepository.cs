// <copyright file="IRepository.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Persistence
{
    using Canister.Sdk.Model;

    public interface IRepository<TKey, TAggregate> where TAggregate : Aggregate
    {
        TAggregate Get(TKey naturalKey);

        void Save(TAggregate aggregate);
    }
}
