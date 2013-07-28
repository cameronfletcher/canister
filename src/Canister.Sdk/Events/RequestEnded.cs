// <copyright file="RequestEnded.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Events
{
    using System;

    public class RequestEnded
    {
        public Guid RequestId { get; set; }
    }
}
