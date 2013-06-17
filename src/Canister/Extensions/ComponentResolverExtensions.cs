// <copyright file="ComponentResolverExtensions.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ComponentResolverExtensions
    {
        public static T Resolve<T>(this IComponentResolver componentResolver) where T : class
        {
            Guard.Against.Null(() => componentResolver);

            return componentResolver.Resolve(typeof(T)) as T;
        }

        public static IEnumerable<T> ResolveAll<T>(this IComponentResolver componentResolver) where T : class
        {
            Guard.Against.Null(() => componentResolver);

            return componentResolver.ResolveAll(typeof(T)).Cast<T>();
        }
    }
}
