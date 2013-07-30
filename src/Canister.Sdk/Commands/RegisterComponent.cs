// <copyright file="RegisterComponent.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Commands
{
    using System;

    public sealed class RegisterComponent
    {
        public Guid ComponentRegistrationId { get; set; }

        public object ComponentKey { get; set; }

        public ComponentFactory ComponentFactory { get; set; }
    }
}