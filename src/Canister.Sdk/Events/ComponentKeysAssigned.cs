// <copyright file="ComponentKeysAssigned.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Events
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class ComponentKeysAssigned
    {
        public Guid ComponentRegistrationId { get; set; }

        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Nonsense.")]
        public object[] ComponentKeys { get; set; }
    }
}
