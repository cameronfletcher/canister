// <copyright file="ComponentResolver.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System;
    using System.Collections.Generic;

    internal sealed class ComponentResolver : IComponentResolver
    {
        private readonly Canister.Sdk.IComponentResolver resolver;

        public ComponentResolver(Canister.Sdk.IComponentResolver resolver)
        {
            Guard.Against.Null(() => resolver);

            this.resolver = resolver;
        }

        public object Resolve(Type componentType)
        {
            return this.resolver.Resolve(componentType);
        }

        public IEnumerable<object> ResolveAll(Type componentType)
        {
            return this.resolver.ResolveAll(componentType);
        }
    }
}
