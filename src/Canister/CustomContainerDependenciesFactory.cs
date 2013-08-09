// <copyright file="CustomContainerDependenciesFactory.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Factories;
    using Canister.Sdk.Infrastructure;
    using ComponentRegistrationAggregate = Canister.Sdk.Model.ComponentRegistration;

    internal class CustomContainerDependenciesFactory : ContainerDependenciesFactory
    {
        protected override ContainerDependencies CreateContainerDependencies()
        {
            return new CustomContainerDependencies();
        }

        protected override void CreateAndAssignRepositories(ContainerDependencies dependencies)
        {
            base.CreateAndAssignRepositories(dependencies);

            var customDependencies = Convert(dependencies);

            var repository = new CustomRepository<Guid, CustomComponentRegistration, ComponentRegistrationAggregate>(dependencies.Bus, registration => registration.Id);

            customDependencies.CustomComponentRegistrationRepository = repository;
            customDependencies.ComponentRegistrationRepository = repository;
        }

        protected override void CreateAndAssignHandlers(ContainerDependencies dependencies)
        {
            base.CreateAndAssignHandlers(dependencies);

            var customDependencies = Convert(dependencies);

            customDependencies.RegisterComponentHandler = null;
            customDependencies.CustomRegisterComponentHandler = new CustomRegisterComponentHandler(customDependencies.CustomComponentRegistrationRepository);
        }

        protected override void WireUpHandlers(ContainerDependencies dependencies)
        {
            Guard.Against.Null(() => dependencies);
            Guard.Against.Null(() => dependencies.Bus);
            Guard.Against.Null(() => dependencies.AssignComponentKeysHandler);
            Guard.Against.Null(() => dependencies.BeginRequestHandler);
            Guard.Against.Null(() => dependencies.EndRequestHandler);
            Guard.Against.Null(() => dependencies.PreserveExistingRegistrationsHandler);
            Guard.Against.Null(() => dependencies.ResolveAllComponentsHandler);
            Guard.Against.Null(() => dependencies.ResolveComponentHandler);

            var customDependencies = Convert(dependencies);
            if (customDependencies.CustomRegisterComponentHandler == null)
            {
                throw new ArgumentException("Value cannot be null.", "customDependencies.CustomRegisterComponentHandler");
            }

            var bus = customDependencies.Bus;

            bus.Register<AssignComponentKeys>(customDependencies.AssignComponentKeysHandler.Handle);
            bus.Register<BeginRequest>(customDependencies.BeginRequestHandler.Handle);
            bus.Register<EndRequest>(customDependencies.EndRequestHandler.Handle);
            bus.Register<PreserveExistingRegistrations>(customDependencies.PreserveExistingRegistrationsHandler.Handle);
            bus.Register<RegisterComponent>(customDependencies.CustomRegisterComponentHandler.Handle);
            bus.Register<ResolveAllComponents>(customDependencies.ResolveAllComponentsHandler.Handle);
            bus.Register<ResolveComponent>(customDependencies.ResolveComponentHandler.Handle);
        }

        private static CustomContainerDependencies Convert(ContainerDependencies dependencies)
        {
            var customDependencies = dependencies as CustomContainerDependencies;
            if (customDependencies == null)
            {
                throw new ArgumentException("Dependencies must be of type Custom etc...");
            }

            return customDependencies;
        }
    }
}
