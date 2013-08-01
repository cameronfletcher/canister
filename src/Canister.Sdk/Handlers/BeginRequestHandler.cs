// <copyright file="BeginRequestHandler.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Handlers
{
    using System;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Model;
    using Canister.Sdk.Persistence;

    public class BeginRequestHandler
    {
        public BeginRequestHandler(IRepository<Guid, Request> repository, ISnapshotService snapshotService)
        {
            Guard.Against.Null(() => repository);
            Guard.Against.Null(() => snapshotService);

            this.Repository = repository;
            this.SnapshotService = snapshotService;
        }

        protected IRepository<Guid, Request> Repository { get; private set; }

        protected ISnapshotService SnapshotService { get; private set; }

        public virtual void Handle(BeginRequest command)
        {
            Guard.Against.Null(() => command);

            var snapshot = this.SnapshotService.GetSnapshot();
            var request = new Request(command.RequestId, snapshot);
            this.Repository.Save(request);
        }
    }
}