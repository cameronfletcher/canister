// <copyright file="RegisterComponentHandler.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Handlers
{
    using System;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Model;
    using Canister.Sdk.Persistence;

    public sealed class RegisterComponentHandler
    {
        private readonly IRepository<Guid, ComponentRegistration> repository;

        public RegisterComponentHandler(IRepository<Guid, ComponentRegistration> repository)
        {
            Guard.Against.Null(() => repository);

            this.repository = repository;
        }

        public void Handle(RegisterComponent command)
        {
            Guard.Against.Null(() => command);

            var componentRegistration = new ComponentRegistration(
                command.ComponentRegistrationId, 
                command.ComponentKey, 
                command.ComponentFactory);
            
            this.repository.Save(componentRegistration);
        }
    }
}
