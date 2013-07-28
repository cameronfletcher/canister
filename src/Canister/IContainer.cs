// <copyright file="IContainer.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System;

    public interface IContainer : IComponentContext
    {
        IComponentRegistration Register<T>(Func<IComponentContext, T> componentFactory) where T : class;
    }
}
