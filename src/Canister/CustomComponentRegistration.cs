// <copyright file="CustomComponentRegistration.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Canister.Sdk;

    internal class CustomComponentRegistration : Canister.Sdk.Model.ComponentRegistration
    {
        private readonly Type componentType;

        public CustomComponentRegistration(Guid id, Type componentType, ComponentFactory componentFactory)
            : base(id, componentType, componentFactory)
        {
            this.componentType = componentType;
        }

        public override void AssignComponentKeys(IEnumerable<object> componentKeys)
        {
            Guard.Against.NullOrEmptyOrNullElements(() => componentKeys);

            if (componentKeys.Any(componentKey => !(componentKey is Type)))
            {
                throw new ComponentRegistrationException("Invalid key type.");
            }

            var firstUnassignableComponentType = componentKeys.Cast<Type>()
                .FirstOrDefault(componentKey => !componentKey.IsAssignableFrom(this.componentType));
            
            if (firstUnassignableComponentType != null)
            {
                throw new ComponentRegistrationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        "Cannot register type of {0} as type of {1}.",
                        this.componentType.Name,
                        firstUnassignableComponentType.Name));
            }

            base.AssignComponentKeys(componentKeys);
        }
    }
}