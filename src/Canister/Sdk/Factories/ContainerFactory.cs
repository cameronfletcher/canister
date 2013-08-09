// <copyright file="ContainerFactory.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Factories
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Canister.Sdk;
    using Canister.Sdk.Cache;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Events;
    using Canister.Sdk.Handlers;
    using Canister.Sdk.Model;
    using Canister.Sdk.Persistence;
    using Canister.Sdk.Views;

    internal class ContainerFactory
    {
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "WIP")]
        public ContainerArguments Create()
        {
            var bus = new MessageBus();

            var componentsCache = new ComponentsCache();
            var componentFactoriesCache = new ComponentFactoriesCache();

            var snapshotService = new SnapshotService(componentFactoriesCache);
            var componentResolverService = new ComponentResolverService();

            var requestRepository = new Repository<Guid, Request>(bus, request => request.Id);
            var componentRegistrationRepository =
                new CustomRepository<Guid, CustomComponentRegistration, Canister.Sdk.Model.ComponentRegistration>(bus, registration => registration.Id);

            var assignComponentKeysHandler = new AssignComponentKeysHandler(componentRegistrationRepository);
            var beginRequestHandler = new BeginRequestHandler(requestRepository, snapshotService);
            var endRequestHandler = new EndRequestHandler(requestRepository);
            var preserveExistingRegistrationsHandler = new PreserveExistingRegistrationsHandler(componentRegistrationRepository);
            var registerComponentHandler = new CustomRegisterComponentHandler(componentRegistrationRepository);
            var resolveAllComponentsHandler = new ResolveAllComponentsHandler(requestRepository, componentResolverService);
            var resolveComponentHandler = new ResolveComponentHandler(requestRepository, componentResolverService);

            bus.Register<AssignComponentKeys>(assignComponentKeysHandler.Handle);
            bus.Register<BeginRequest>(beginRequestHandler.Handle);
            bus.Register<EndRequest>(endRequestHandler.Handle);
            bus.Register<PreserveExistingRegistrations>(preserveExistingRegistrationsHandler.Handle);
            bus.Register<RegisterComponent>(registerComponentHandler.Handle);
            bus.Register<ResolveAllComponents>(resolveAllComponentsHandler.Handle);
            bus.Register<ResolveComponent>(resolveComponentHandler.Handle);

            var componentsView = new ComponentsView(componentsCache);
            var componentFactoriesView = new ComponentFactoriesView(componentFactoriesCache);

            bus.Register<ComponentRegistered>(componentFactoriesView.Handle);
            bus.Register<ComponentKeysAssigned>(componentFactoriesView.Handle);
            bus.Register<ExistingRegistrationsPreserved>(componentFactoriesView.Handle);
            bus.Register<ComponentResolved>(componentsView.Handle);
            bus.Register<RequestEnded>(componentsView.Handle);

            return new ContainerArguments
            {
                MessageBus = bus,
                ComponentsCache = componentsCache,
            };
        }
    }
}
