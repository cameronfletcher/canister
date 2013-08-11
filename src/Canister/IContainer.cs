// <copyright file="IContainer.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;

    public interface IContainer : IComponentResolver
    {
        IComponentRegistration Register<T>(Func<IComponentResolver, T> componentFactory);
    }
}