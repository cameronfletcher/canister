// <copyright file="ResolveComponent.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Commands
{
    using System;

    public sealed class ResolveComponent
    {
        public Guid RequestId { get; set; }

        public object ComponentKey { get; set; }
    }
}