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
        public PreserveExistingRegistrationsHandler(IRepository<Guid, ComponentRegistration> repository)
        {
            Guard.Against.Null(() => repository);

            this.Repository = repository;
        }

        protected IRepository<Guid, ComponentRegistration> Repository { get; private set; }

        public virtual void Handle(PreserveExistingRegistrations command)
        {
            Guard.Against.Null(() => command);

            var componentRegistration = this.Repository.Get(command.ComponentRegistrationId);
            componentRegistration.PreserveExistingRegistrations();
            this.Repository.Save(componentRegistration);
        }
    }
}