// <copyright file="AssignComponentKeys.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Commands
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public sealed class AssignComponentKeys
    {
        public Guid ComponentRegistrationId { get; set; }

        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Nonsense.")]
        public object[] ComponentKeys { get; set; }
    }
}