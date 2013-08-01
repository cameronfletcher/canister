// <copyright file="AssignComponentKeysHandler.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Handlers
{
    using System;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Model;
    using Canister.Sdk.Persistence;

    public class AssignComponentKeysHandler
    {
        public AssignComponentKeysHandler(IRepository<Guid, ComponentRegistration> repository)
        {
            Guard.Against.Null(() => repository);

            this.Repository = repository;
        }

        protected IRepository<Guid, ComponentRegistration> Repository { get; private set; }

        public virtual void Handle(AssignComponentKeys command)
        {
            Guard.Against.Null(() => command);

            var componentRegistration = this.Repository.Get(command.ComponentRegistrationId);
            componentRegistration.AssignComponentKeys(command.ComponentKeys);
            this.Repository.Save(componentRegistration);
        }
    }
}