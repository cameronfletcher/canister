// <copyright file="ComponentResolver.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Model
{
    using System.Collections.Generic;
    using System.Globalization;

    public class ComponentResolver : IComponentResolver
    {
        private readonly Snapshot snapshot;

        public ComponentResolver(Snapshot snapshot)
        {
            Guard.Against.Null(() => snapshot);

            this.snapshot = snapshot;
        }

        public object Resolve(object componentKey)
        {
            Guard.Against.Null(() => componentKey);

            var componentFactory = this.snapshot.GetComponentFactory(componentKey);
            if (componentFactory == null)
            {
                throw new ComponentResolutionException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot resolve the component with the key {0} (of type {1}) as a registration does not exist in the container.",
                        componentKey.ToString(),
                        componentKey.GetType().Name));
            }

            var component = componentFactory.Invoke(this);

            // cache?
            return component;
        }

        // TODO (Cameron): Do we want to defer execution here?
        public IEnumerable<object> ResolveAll(object componentKey)
        {
            Guard.Against.Null(() => componentKey);

            foreach (var componentFactory in this.snapshot.GetComponentFactories(componentKey))
            {
                var component = componentFactory.Invoke(this);

                // cache?
                yield return component;
            }
        }
    }
}