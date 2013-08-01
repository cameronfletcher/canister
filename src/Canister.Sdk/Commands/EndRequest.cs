// <copyright file="EndRequest.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Commands
{
    using System;

    public sealed class EndRequest
    {
        public Guid RequestId { get; set; }
    }
}