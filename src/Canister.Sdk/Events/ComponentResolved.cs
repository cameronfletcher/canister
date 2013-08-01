// <copyright file="ComponentResolved.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Events
{
    using System;

    public sealed class ComponentResolved
    {
        public Guid RequestId { get; set; }

        public object Component { get; set; }
    }
}