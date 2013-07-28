// <copyright file="ComponentRegistered.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Events
{
    using System;
    using Canister.Sdk.Model;

    public class ComponentRegistered
    {
        public Guid ComponentRegistrationId { get; set; }

        public object ComponentKey { get; set; }

        public Func<IComponentResolver, object> ComponentFactory { get; set; }
    }
}
