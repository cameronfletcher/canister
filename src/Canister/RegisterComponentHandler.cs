// <copyright file="CustomRegisterComponentHandler.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Handlers
{
    using System;
    using Canister.Model;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Persistence;
    using ComponentRegistration = Canister.Sdk.Model.ComponentRegistration;

    public class CustomRegisterComponentHandler : RegisterComponentHandler
    {
        public CustomRegisterComponentHandler(IRepository<Guid, ComponentRegistration> repository)
            : base(repository)
        {
        }

        public override void Handle(RegisterComponent command)
        {
            Guard.Against.Null(() => command);

            var componentRegistration = new CustomComponentRegistration(
                command.ComponentRegistrationId,
                command.ComponentKey as Type,
                command.ComponentFactory);

            this.Repository.Save(componentRegistration);
        }
    }
}