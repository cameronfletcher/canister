// <copyright file="EndRequestHandler.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Handlers
{
    using System;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Model;
    using Canister.Sdk.Persistence;

    public sealed class EndRequestHandler
    {
        private readonly IRepository<Guid, Request> repository;

        public EndRequestHandler(IRepository<Guid, Request> repository)
        {
            Guard.Against.Null(() => repository);

            this.repository = repository;
        }

        public void Handle(EndRequest command)
        {
            Guard.Against.Null(() => command);

            var request = this.repository.Get(command.RequestId);
            request.End();
            this.repository.Save(request);
        }
    }
}