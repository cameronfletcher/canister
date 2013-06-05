namespace Canister.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    public static class ComponentResolverExtensions
    {
        public static T Resolve<T>(this IComponentResolver componentResolver) where T : class
        {
            return componentResolver.Resolve(typeof(T)) as T;
        }


        public static IEnumerable<T> ResolveAll<T>(this IComponentResolver componentResolver) where T : class
        {
            return componentResolver.ResolveAll(typeof(T)).Cast<T>();
        }
    }
}
