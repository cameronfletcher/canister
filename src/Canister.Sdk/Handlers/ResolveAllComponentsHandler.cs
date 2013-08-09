// <copyright file="ResolveAllComponentsHandler.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Handlers
{
    using System;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Model;
    using Canister.Sdk.Persistence;

    public class ResolveAllComponentsHandler
    {
        private readonly IRepository<Guid, Request> Repository;
        private readonly IComponentResolverService ComponentResolverService;

        public ResolveAllComponentsHandler(IRepository<Guid, Request> requestRepository, IComponentResolverService componentResolverService)
        {
            Guard.Against.Null(() => requestRepository);
            Guard.Against.Null(() => componentResolverService);

            this.Repository = requestRepository;
            this.ComponentResolverService = componentResolverService;
        }

        public virtual void Handle(ResolveAllComponents command)
        {
            Guard.Against.Null(() => command);

            var request = this.Repository.Get(command.RequestId);
            request.ResolveAll(command.ComponentKey, this.ComponentResolverService);
            this.Repository.Save(request);
        }
    }
}