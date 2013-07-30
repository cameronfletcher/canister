﻿// <copyright file="IComponentResolver.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk
{
    using System.Collections.Generic;

    public delegate object ComponentFactory(IComponentResolver componentResolver);

    public interface IComponentResolver
    {
        object Resolve(object componentKey);

        IEnumerable<object> ResolveAll(object componentKey);
    }
}
