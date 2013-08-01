// <copyright file="IContainer.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System;
    using System.Collections.Generic;
    using Canister.Sdk;

    public interface IContainer : IComponentContext
    {
        IComponentRegistration Register<T>(Func<IComponentContext, T> componentFactory);
    }
}