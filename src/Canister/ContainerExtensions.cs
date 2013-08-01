// <copyright file="ContainerExtensions.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System.Collections.Generic;
    using System.Linq;
    using Canister.Sdk;

    // TODO (Cameron): Make these more relevant eg. ContainerExtensions, on IComponentContext?
    public static class ContainerExtensions
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