﻿// <copyright file="IComponentRegistration.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public interface IComponentRegistration
    {
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "As", Justification = "By design.")]
        IComponentRegistration As(Type[] componentTypes);

        IComponentRegistration AsImplementedInterfaces();

        IComponentRegistration PreserveExistingRegistrations();
    }
}