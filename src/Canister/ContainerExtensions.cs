// <copyright file="ContainerExtensions.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ContainerExtensions
    {
        public static T Resolve<T>(this IComponentResolver componentResolver)
        {
            Guard.Against.Null(() => componentResolver);

            return (T)componentResolver.Resolve(typeof(T));
        }

        public static IEnumerable<T> ResolveAll<T>(this IComponentResolver componentResolver)
        {
            Guard.Against.Null(() => componentResolver);

            return componentResolver.ResolveAll(typeof(T)).Cast<T>();
        }
    }
}