﻿// <copyright file="ResolveComponentHandler.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Handlers
{
    using System;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Model;
    using Canister.Sdk.Persistence;

    public class ResolveComponentHandler
    {
        public ResolveComponentHandler(IRepository<Guid, Request> requestRepository, IComponentResolverService componentResolverService)
        {
            Guard.Against.Null(() => requestRepository);
            Guard.Against.Null(() => componentResolverService);

            this.Repository = requestRepository;
            this.ComponentResolverService = componentResolverService;
        }

        protected IRepository<Guid, Request> Repository { get; private set; }

        protected IComponentResolverService ComponentResolverService { get; private set; }

        public virtual void Handle(ResolveComponent command)
        {
            Guard.Against.Null(() => command);

            var request = this.Repository.Get(command.RequestId);
            request.Resolve(command.ComponentKey, this.ComponentResolverService);
            this.Repository.Save(request);
        }
    }
}