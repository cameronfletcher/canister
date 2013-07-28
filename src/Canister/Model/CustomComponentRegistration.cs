namespace Canister.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Canister.Sdk;

    public class CustomComponentRegistration : Canister.Sdk.Model.ComponentRegistration
    {
        private readonly Type componentType;

        public CustomComponentRegistration(Guid id, Type componentType, Func<IComponentResolver, object> componentFactory)
            : base(id, componentType, componentFactory)
        {
            this.componentType = componentType;
        }

        public override void AssignComponentKeys(IEnumerable<object> componentKeys)
        {
            Guard.Against.NullOrEmptyOrNullElements(() => componentKeys);

            if (componentKeys.Any(componentKey => componentKey.GetType() != typeof(Type)))
            {
                throw new ComponentRegistrationException("Invalid key type.");
            }

            if (componentKeys.Cast<Type>().Any(componentKey => !componentType.IsAssignableFrom(componentKey.GetType())))
            {
                // TODO (Cameron): Fix exception message.
                throw new ComponentRegistrationException("Cannot assign component type to original component type.");
            }

            base.AssignComponentKeys(componentKeys);
        }
    }
}