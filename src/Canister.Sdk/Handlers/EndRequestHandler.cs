// <copyright file="EndRequestHandler.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Handlers
{
    using System;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Model;
    using Canister.Sdk.Persistence;

    public class EndRequestHandler
    {
        public EndRequestHandler(IRepository<Guid, Request> repository)
        {
            Guard.Against.Null(() => repository);

            this.Repository = repository;
        }

        protected IRepository<Guid, Request> Repository { get; private set; }

        public virtual void Handle(EndRequest command)
        {
            Guard.Against.Null(() => command);

            var request = this.Repository.Get(command.RequestId);
            request.End();
            this.Repository.Save(request);
        }
    }
}