// <copyright file="ResolveAllComponentsHandler.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Handlers
{
    using System;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Infrastructure;
    using Canister.Sdk.Model;

    public class ResolveAllComponentsHandler
    {
        private readonly IRepository<Guid, Request> repository;
        private readonly IComponentResolverService componentResolverService;

        public ResolveAllComponentsHandler(IRepository<Guid, Request> requestRepository, IComponentResolverService componentResolverService)
        {
            Guard.Against.Null(() => requestRepository);
            Guard.Against.Null(() => componentResolverService);

            this.repository = requestRepository;
            this.componentResolverService = componentResolverService;
        }

        public virtual void Handle(ResolveAllComponents command)
        {
            Guard.Against.Null(() => command);

            var request = this.repository.Get(command.RequestId);
            request.ResolveAll(command.ComponentKey, this.componentResolverService);
            this.repository.Save(request);
        }
    }
}