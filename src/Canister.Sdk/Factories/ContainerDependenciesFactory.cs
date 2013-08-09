// <copyright file="ContainerDependenciesFactory.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Factories
{
    using System;
    using Canister.Sdk.Cache;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Events;
    using Canister.Sdk.Handlers;
    using Canister.Sdk.Infrastructure;
    using Canister.Sdk.Model;
    using Canister.Sdk.Views;

    public class ContainerDependenciesFactory
    {
        public virtual ContainerDependencies Create()
        {
            var dependencies = this.CreateContainerDependencies();
            dependencies.Bus = new MessageBus();

            this.CreateAndAssignCaches(dependencies);
            this.CreateAndAssignServices(dependencies);
            this.CreateAndAssignRepositories(dependencies);
            this.CreateAndAssignHandlers(dependencies);
            this.CreateAndAssignViews(dependencies);

            this.WireUpHandlers(dependencies);
            this.WireUpViews(dependencies);

            return dependencies;
        }

        protected virtual ContainerDependencies CreateContainerDependencies()
        {
            return new ContainerDependencies();
        }

        protected virtual void CreateAndAssignCaches(ContainerDependencies dependencies)
        {
            Guard.Against.Null(() => dependencies);

            dependencies.ComponentsCache = new ComponentsCache();
            dependencies.ComponentFactoriesCache = new ComponentFactoriesCache();
        }

        protected virtual void CreateAndAssignServices(ContainerDependencies dependencies)
        {
            Guard.Against.Null(() => dependencies);

            dependencies.SnapshotService = new SnapshotService(dependencies.ComponentFactoriesCache);
            dependencies.ComponentResolverService = new ComponentResolverService();
        }

        protected virtual void CreateAndAssignRepositories(ContainerDependencies dependencies)
        {
            Guard.Against.Null(() => dependencies);

            dependencies.RequestRepository = new DefaultRepository<Guid, Request>(dependencies.Bus, request => request.Id);
            dependencies.ComponentRegistrationRepository = new DefaultRepository<Guid, ComponentRegistration>(dependencies.Bus, registration => registration.Id);
        }

        protected virtual void CreateAndAssignHandlers(ContainerDependencies dependencies)
        {
            Guard.Against.Null(() => dependencies);

            dependencies.AssignComponentKeysHandler = new AssignComponentKeysHandler(dependencies.ComponentRegistrationRepository);
            dependencies.BeginRequestHandler = new BeginRequestHandler(dependencies.RequestRepository, dependencies.SnapshotService);
            dependencies.EndRequestHandler = new EndRequestHandler(dependencies.RequestRepository);
            dependencies.PreserveExistingRegistrationsHandler = new PreserveExistingRegistrationsHandler(dependencies.ComponentRegistrationRepository);
            dependencies.RegisterComponentHandler = new RegisterComponentHandler(dependencies.ComponentRegistrationRepository);
            dependencies.ResolveAllComponentsHandler = new ResolveAllComponentsHandler(dependencies.RequestRepository, dependencies.ComponentResolverService);
            dependencies.ResolveComponentHandler = new ResolveComponentHandler(dependencies.RequestRepository, dependencies.ComponentResolverService);
        }

        protected virtual void CreateAndAssignViews(ContainerDependencies dependencies)
        {
            Guard.Against.Null(() => dependencies);

            dependencies.ComponentsView = new ComponentsView(dependencies.ComponentsCache);
            dependencies.ComponentFactoriesView = new ComponentFactoriesView(dependencies.ComponentFactoriesCache);
        }

        protected virtual void WireUpHandlers(ContainerDependencies dependencies)
        {
            Guard.Against.Null(() => dependencies);
            Guard.Against.Null(() => dependencies.Bus);
            Guard.Against.Null(() => dependencies.AssignComponentKeysHandler);
            Guard.Against.Null(() => dependencies.BeginRequestHandler);
            Guard.Against.Null(() => dependencies.EndRequestHandler);
            Guard.Against.Null(() => dependencies.PreserveExistingRegistrationsHandler);
            Guard.Against.Null(() => dependencies.RegisterComponentHandler);
            Guard.Against.Null(() => dependencies.ResolveAllComponentsHandler);
            Guard.Against.Null(() => dependencies.ResolveComponentHandler);

            var bus = dependencies.Bus;

            bus.Register<AssignComponentKeys>(dependencies.AssignComponentKeysHandler.Handle);
            bus.Register<BeginRequest>(dependencies.BeginRequestHandler.Handle);
            bus.Register<EndRequest>(dependencies.EndRequestHandler.Handle);
            bus.Register<PreserveExistingRegistrations>(dependencies.PreserveExistingRegistrationsHandler.Handle);
            bus.Register<RegisterComponent>(dependencies.RegisterComponentHandler.Handle);
            bus.Register<ResolveAllComponents>(dependencies.ResolveAllComponentsHandler.Handle);
            bus.Register<ResolveComponent>(dependencies.ResolveComponentHandler.Handle);
        }

        protected virtual void WireUpViews(ContainerDependencies dependencies)
        {
            Guard.Against.Null(() => dependencies);
            Guard.Against.Null(() => dependencies.Bus);
            Guard.Against.Null(() => dependencies.ComponentFactoriesView);
            Guard.Against.Null(() => dependencies.ComponentsView);

            var bus = dependencies.Bus;

            bus.Register<ComponentRegistered>(dependencies.ComponentFactoriesView.Handle);
            bus.Register<ComponentKeysAssigned>(dependencies.ComponentFactoriesView.Handle);
            bus.Register<ExistingRegistrationsPreserved>(dependencies.ComponentFactoriesView.Handle);
            bus.Register<ComponentResolved>(dependencies.ComponentsView.Handle);
            bus.Register<RequestEnded>(dependencies.ComponentsView.Handle);
        }
    }
}
