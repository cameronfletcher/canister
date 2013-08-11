// <copyright file="Snapshot.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Model
{
    using System.Collections.Generic;
    using System.Linq;

    public class Snapshot
    {
        private readonly Dictionary<object, ComponentFactory[]> allComponentFactories;

        // TODO (Cameron): Check that this contains a copy of the dictionary, not a reference to.
        public Snapshot(Dictionary<object, ComponentFactory[]> allComponentFactories)
        {
            Guard.Against.Null(() => allComponentFactories);

            this.allComponentFactories = allComponentFactories;
        }

        public ComponentFactory GetComponentFactory(object componentKey)
        {
            var componentFactories = this.GetComponentFactories(componentKey);
            return componentFactories == null ? null : componentFactories.FirstOrDefault();
        }

        public ComponentFactory[] GetComponentFactories(object componentKey)
        {
            ComponentFactory[] componentFactories;
            this.allComponentFactories.TryGetValue(componentKey, out componentFactories);
            return componentFactories;
        }
    }
}
