// <copyright file="RegisterComponent.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Commands
{
    using System;
    using Canister.Sdk.Model;

    public sealed class RegisterComponent
    {
        public Guid ComponentRegistrationId { get; set; }

        public object ComponentKey { get; set; }

        public Func<IComponentResolver, object> ComponentFactory { get; set; }
    }
}