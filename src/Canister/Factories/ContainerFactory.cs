// <copyright file="ContainerFactory.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Factories
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Canister.Model;
    using Canister.Persistence;
    using Canister.Sdk;
    using Canister.Sdk.Cache;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Events;
    using Canister.Sdk.Handlers;
    using Canister.Sdk.Model;
    using Canister.Sdk.Persistence;
    using Canister.Sdk.Views;

    
    public class ContainerFactory
    {
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "WIP")]
        public IContainer Create()
        {
            var bus = new MessageBus();

            var componentCache = new ComponentsCache();
            var componentFactoriesCache = new ComponentFactoriesCache();

            var snapshotService = new SnapshotService(componentFactoriesCache);
            var componentResolverService = new ComponentResolverService();

            var customComponentRegistrationRepository = new CustomComponentRegistrationRepository(bus);
            var requestRepository = new Repository<Guid, Request>(bus, request => request.Id);

            var assignComponentKeysHandler = new AssignComponentKeysHandler(customComponentRegistrationRepository);
            var beginRequestHandler = new BeginRequestHandler(requestRepository, snapshotService);
            var endRequestHandler = new EndRequestHandler(requestRepository);
            var preserveExistingRegistrationsHandler = new PreserveExistingRegistrationsHandler(customComponentRegistrationRepository);
            var registerComponentHandler = new CustomRegisterComponentHandler(customComponentRegistrationRepository);
            //var resolveAllComponentsHandler = new ResolveAllComponentsHandler(componentRegistrationRepository);
            var resolveComponentHandler = new ResolveComponentHandler(requestRepository, componentResolverService);

            // command handlers
            bus.Register<AssignComponentKeys>(assignComponentKeysHandler.Handle);
            bus.Register<BeginRequest>(beginRequestHandler.Handle);
            bus.Register<EndRequest>(endRequestHandler.Handle);
            bus.Register<PreserveExistingRegistrations>(preserveExistingRegistrationsHandler.Handle);
            bus.Register<RegisterComponent>(registerComponentHandler.Handle);
            //bus.Register<ResolveAllComponents>(message => factoryView.Handle(message));
            bus.Register<ResolveComponent>(resolveComponentHandler.Handle);

            // views
            var allComponentFactoriesView = new ComponentFactoriesView(componentFactoriesCache);
            var componentView = new ComponentsView(componentCache);

            //bus.Register<ComponentRegistered>(message => factoriesView.Handle(message));
            //bus.Register<ComponentKeysAssigned>(message => factoriesView.Handle(message));
            //bus.Register<ExistingRegistrationsPreserved>(message => factoriesView.Handle(message));
            bus.Register<ComponentRegistered>(allComponentFactoriesView.Handle);
            bus.Register<ComponentKeysAssigned>(allComponentFactoriesView.Handle);
            bus.Register<ComponentResolved>(componentView.Handle);

            var container = new Container(bus, componentCache);

            return container;
        }
    }
}
