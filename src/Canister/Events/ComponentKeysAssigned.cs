// <copyright file="ComponentKeysAssigned.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Events
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class ComponentKeysAssigned
    {
        public ComponentKeysAssigned(Guid componentRegistrationId, object[] componentKeys)
        {
            this.ComponentRegistrationId = componentRegistrationId;
            this.ComponentKeys = componentKeys;
        }

        public Guid ComponentRegistrationId { get; private set; }

        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Nonsense.")]
        public object[] ComponentKeys { get; private set; }
    }
}
