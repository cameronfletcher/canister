// <copyright file="ComponentRegistration.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Canister.Sdk.Events;

    public class ComponentRegistration : Aggregate
    {
        private bool existingRegistrationsPreserved;

        public ComponentRegistration(Guid id, object componentKey, ComponentFactory componentFactory)
        {
            Guard.Against.Null(() => componentKey);
            Guard.Against.Null(() => componentFactory);

            var @event = new ComponentRegistered
            {
                ComponentRegistrationId = id,
                ComponentKey = componentKey,
                ComponentFactory = componentFactory,
            };

            this.Apply(@event);

            this.Id = @event.ComponentRegistrationId;
        }

        public Guid Id { get; private set; }

        public virtual void PreserveExistingRegistrations()
        {
            if (this.existingRegistrationsPreserved)
            {
                return;
            }

            var @event = new ExistingRegistrationsPreserved
            {
                ComponentRegistrationId = this.Id,
            };

            this.Apply(@event);

            this.existingRegistrationsPreserved = true;
        }

        public virtual void AssignComponentKeys(IEnumerable<object> componentKeys)
        {
            Guard.Against.Null(() => componentKeys);

            if (!componentKeys.Any())
            {
                throw new ComponentRegistrationException("Cannot assign empty keys to component registration.");
            }

            var @event = new ComponentKeysAssigned
            {
                ComponentRegistrationId = this.Id,
                ComponentKeys = componentKeys.ToArray()
            };

            this.Apply(@event);
        }
    }
}