// <copyright file="Component.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.ReadModel
{
    using System;
    using Canister.Sdk.Model;

    // TODO (Cameron): Add Lifetime, Scope, etc.
    internal sealed class Component
    {
        public int RegistrationCount { get; set; }

        public object[] Keys { get; set; }

        public bool PreserveRegistrations { get; set; }

        public Func<IComponentResolver, object> Factory { get; set; }
    }
}
