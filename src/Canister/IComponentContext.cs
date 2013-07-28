// <copyright file="IComponentContext.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System.Collections.Generic;
    using Canister.Sdk;

    public interface IComponentContext : IComponentResolver
    {
        //object Resolve(object componentKey);

        //IEnumerable<object> ResolveAll(object componentKey);
    }
}
