// <copyright file="IRepository.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Infrastructure
{
    using System.Diagnostics.CodeAnalysis;
    using Canister.Sdk.Model;

    public interface IRepository<TKey, TAggregate>
        where TAggregate : Aggregate
    {
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "By design.")]
        TAggregate Get(TKey naturalKey);

        void Save(TAggregate aggregate);
    }
}