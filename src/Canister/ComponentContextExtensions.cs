// <copyright file="ComponentContextExtensions.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ComponentContextExtensions
    {
        public static T Resolve<T>(this IComponentContext componentContext) // where T : class
        {
            Guard.Against.Null(() => componentContext);

            return (T)componentContext.Resolve(typeof(T));
        }

        public static IEnumerable<T> ResolveAll<T>(this IComponentContext componentContext) // where T : class
        {
            Guard.Against.Null(() => componentContext);

            return componentContext.ResolveAll(typeof(T)).Cast<T>();
        }
    }
}