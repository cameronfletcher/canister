// <copyright file="ISnapshotService.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Model
{
    using System.Diagnostics.CodeAnalysis;

    public interface ISnapshotService
    {
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Inappropriate.")]
        Snapshot GetSnapshot();
    }
}