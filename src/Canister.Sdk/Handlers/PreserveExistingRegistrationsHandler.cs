// <copyright file="PreserveExistingRegistrationsHandler.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Handlers
{
    using System;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Model;
    using Canister.Sdk.Persistence;

    public class PreserveExistingRegistrationsHandler
    {
        private readonly IRepository<Guid, ComponentRegistration> repository;

        public PreserveExistingRegistrationsHandler(IRepository<Guid, ComponentRegistration> repository)
        {
            Guard.Against.Null(() => repository);

            this.repository = repository;
        }

        public void Handle(PreserveExistingRegistrations command)
        {
            Guard.Against.Null(() => command);

            var componentRegistration = this.repository.Get(command.ComponentRegistrationId);
            componentRegistration.PreserveExistingRegistrations();
            this.repository.Save(componentRegistration);
        }
    }
}