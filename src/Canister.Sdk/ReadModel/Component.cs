// <copyright file="Component.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.ReadModel
{
    // TODO (Cameron): Add Lifetime, Scope, etc.
    internal sealed class Component
    {
        public int RegistrationCount { get; set; }

        public object[] Keys { get; set; }

        public bool PreserveRegistrations { get; set; }

        public ComponentFactory Factory { get; set; }
    }
}
