// <copyright file="IContainer.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System;
    using System.Collections.Generic;

    public interface IContainer
    {
        IComponentRegistration Register<T>(Func<IComponentContext, T> componentFactory);

        object Resolve(Type componentType);

        IEnumerable<object> ResolveAll(Type componentType);
    }
}
