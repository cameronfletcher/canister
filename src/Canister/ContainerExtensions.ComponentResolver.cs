// <copyright file="ContainerExtensions.ComponentResolver.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System.Collections.Generic;
    using System.Linq;

    /// <content>Container extension methods (for component resolver).</content>
    public static partial class ContainerExtensions
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