// <copyright file="CustomRegisterComponentHandler.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Handlers
{
    using System;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Model;
    using Canister.Sdk.Persistence;

    public class CustomRegisterComponentHandler
    {
        private readonly IRepository<Guid, ComponentRegistration> repository;

        public CustomRegisterComponentHandler(IRepository<Guid, ComponentRegistration> repository)
        {
            Guard.Against.Null(() => repository);

            this.repository = repository;
        }

        public void Handle(RegisterComponent command)
        {
            Guard.Against.Null(() => command);

            var componentRegistration = new CustomComponentRegistration(
                command.ComponentRegistrationId,
                command.ComponentKey as Type,
                command.ComponentFactory);

            this.repository.Save(componentRegistration);
        }
    }
}