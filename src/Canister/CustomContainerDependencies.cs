// <copyright file="CustomContainerDependencies.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System;
    using Canister.Sdk.Factories;
    using Canister.Sdk.Infrastructure;

    internal class CustomContainerDependencies : ContainerDependencies
    {
        public CustomRegisterComponentHandler CustomRegisterComponentHandler { get; set; }

        public IRepository<Guid, CustomComponentRegistration> CustomComponentRegistrationRepository { get; set; }
    }
}
