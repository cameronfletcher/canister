// <copyright file="ContainerExtensions.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Canister.Sdk;

    /// <content>Container extension methods.</content>
    public static partial class ContainerExtensions
    {
        public static IComponentRegistration Register<T>(this IContainer container)
        {
            Guard.Against.Null(() => container);

            var componentFactory = CreateComponentFactory<T>();
            
            return container.Register(componentFactory);
        }

        // TODO (Cameron): Check behaviour here with ResolveAll.
        private static Func<IComponentResolver, T> CreateComponentFactory<T>()
        {
            var chosenConstructor = typeof(T).GetConstructors()
                .Where(constructor => constructor.GetParameters().All(parameter => !parameter.ParameterType.IsValueType))
                .OrderBy(constructor => constructor.GetParameters().Count())
                .FirstOrDefault();

            if (chosenConstructor == null)
            {
                throw new ComponentRegistrationException(
                    string.Format(
                        "Registration failed for type {1} as a suitable constructor could not be found.",
                        typeof(T)));
            }

            var componentResolver = Expression.Parameter(typeof(IComponentResolver), "componentResolver");
            var componentResolverMethod = typeof(ContainerExtensions).GetMethod("Resolve");
            var arguments = chosenConstructor.GetParameters()
                .Select(parameter => parameter.ParameterType)
                .Select(parameterType => Expression.Call(componentResolverMethod.MakeGenericMethod(parameterType), componentResolver))
                .ToArray();
            
            var call = Expression.New(chosenConstructor, arguments);
            var lambda = Expression.Lambda(call, componentResolver);

            return lambda.Compile() as Func<IComponentResolver, T>;
        }
    }
}