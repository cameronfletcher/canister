// <copyright file="AssignComponentKeysHandler.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Handlers
{
    using System;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Model;
    using Canister.Sdk.Persistence;

    public sealed class AssignComponentKeysHandler
    {
        private readonly IRepository<Guid, ComponentRegistration> repository;

        public AssignComponentKeysHandler(IRepository<Guid, ComponentRegistration> repository)
        {
            Guard.Against.Null(() => repository);

            this.repository = repository;
        }

        public void Handle(AssignComponentKeys command)
        {
            Guard.Against.Null(() => command);

            var componentRegistration = this.repository.Get(command.ComponentRegistrationId);
            componentRegistration.AssignComponentKeys(command.ComponentKeys);
            this.repository.Save(componentRegistration);
        }
    }
}