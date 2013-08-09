// <copyright file="IComponentResolver.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System;
    using System.Collections.Generic;

    public interface IComponentResolver
    {
        object Resolve(Type componentType);

        IEnumerable<object> ResolveAll(Type componentType);
    }
}