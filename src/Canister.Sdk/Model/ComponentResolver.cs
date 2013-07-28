// <copyright file="ComponentResolver.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Model
{
    using System.Collections.Generic;

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
                // TODO (Cameron): Fix exception.
                throw new ComponentResolutionException("Cannot resolve!");
            }

            var component = componentFactory.Invoke(this);

            // cache?
            return component;
        }

        public IEnumerable<object> ResolveAll(object componentKey)
        {
            Guard.Against.Null(() => componentKey);

            var componentFactories = this.snapshot.GetComponentFactories(componentKey);
            if (componentFactories == null)
            {
                // TODO (Cameron): Fix exception.
                throw new ComponentResolutionException("Cannot resolve!");
            }

            foreach (var componentFactory in componentFactories)
            {
                var component = componentFactory.Invoke(this);

                // cache?
                yield return component;
            }
        }
    }
}
