// <copyright file="BeginRequestHandler.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Handlers
{
    using System;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Infrastructure;
    using Canister.Sdk.Model;

    public class BeginRequestHandler
    {
        private readonly IRepository<Guid, Request> repository;
        private readonly ISnapshotService snapshotService;

        public BeginRequestHandler(IRepository<Guid, Request> repository, ISnapshotService snapshotService)
        {
            Guard.Against.Null(() => repository);
            Guard.Against.Null(() => snapshotService);

            this.repository = repository;
            this.snapshotService = snapshotService;
        }

        public void Handle(BeginRequest command)
        {
            Guard.Against.Null(() => command);

            var snapshot = this.snapshotService.GetSnapshot();
            var request = new Request(command.RequestId, snapshot);
            this.repository.Save(request);
        }
    }
}