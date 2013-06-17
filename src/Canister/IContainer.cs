// <copyright file="IContainer.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System;

    public interface IContainer : IComponentResolver
    {
        IComponentRegistration Register<T>(Func<IComponentResolver, T> componentFactory) where T : class;
    }
}
