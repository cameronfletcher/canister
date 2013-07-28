// <copyright file="ResolveComponentHandler.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Handlers
{
    using System;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Model;
    using Canister.Sdk.Persistence;

    public sealed class ResolveComponentHandler
    {
        private readonly IRepository<Guid, Request> requestRepository;
        private readonly IComponentResolverService componentResolverService;

        public ResolveComponentHandler(IRepository<Guid, Request> requestRepository, IComponentResolverService componentResolverService)
        {
            Guard.Against.Null(() => requestRepository);
            Guard.Against.Null(() => componentResolverService);

            this.requestRepository = requestRepository;
            this.componentResolverService = componentResolverService;
        }

        public void Handle(ResolveComponent command)
        {
            Guard.Against.Null(() => command);

            var request = this.requestRepository.Get(command.RequestId);
            var component = this.componentResolverService.Resolve(request.Snapshot, command.ComponentKey);
            request.Track(component);
            this.requestRepository.Save(request);
        }
    }
}