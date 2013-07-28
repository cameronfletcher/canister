// <copyright file="ResolveAllComponents.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Commands
{
    using System;

    public sealed class ResolveAllComponents
    {
        public Guid RequestId { get; set; }

        public object ComponentKey { get; set; }
    }
}
