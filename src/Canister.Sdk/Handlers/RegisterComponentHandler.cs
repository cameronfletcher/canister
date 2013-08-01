﻿// <copyright file="RegisterComponentHandler.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Handlers
{
    using System;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Model;
    using Canister.Sdk.Persistence;

    public class RegisterComponentHandler
    {
        public RegisterComponentHandler(IRepository<Guid, ComponentRegistration> repository)
        {
            Guard.Against.Null(() => repository);

            this.Repository = repository;
        }

        protected IRepository<Guid, ComponentRegistration> Repository { get; private set; }

        public virtual void Handle(RegisterComponent command)
        {
            Guard.Against.Null(() => command);

            var componentRegistration = new ComponentRegistration(
                command.ComponentRegistrationId, 
                command.ComponentKey, 
                command.ComponentFactory);
            
            this.Repository.Save(componentRegistration);
        }
    }
}