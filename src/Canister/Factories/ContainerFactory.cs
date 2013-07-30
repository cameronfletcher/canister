// <copyright file="ContainerFactory.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Factories
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Canister.Model;
    using Canister.Sdk;
    using Canister.Sdk.Cache;
    using Canister.Sdk.Commands;
    using Canister.Sdk.Events;
    using Canister.Sdk.Handlers;
    using Canister.Sdk.Model;
    using Canister.Sdk.Persistence;
    using Canister.Sdk.Views;

    public class Repo : Repository<Guid, CustomComponentRegistration>, IRepository<Guid, Canister.Sdk.Model.ComponentRegistration>
    {
        public Repo(MessageBus bus)
            : base(bus, componentRegistration => componentRegistration.Id)
        {
        }

        public new Canister.Sdk.Model.ComponentRegistration Get(Guid naturalKey)
        {
            return base.Get(naturalKey);
        }

        public void Save(Canister.Sdk.Model.ComponentRegistration aggregate)
        {
            var componentRegistration = aggregate as CustomComponentRegistration;
            base.Save(componentRegistration);
        }
    }

    public class ContainerFactory
    {
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "WIP")]
        public IContainer Create()
        {
            var bus = new MessageBus();

            //var factoryCache = new ComponentFactoryCache();
            //var factoriesCache = new ComponentFactoriesCache();
            var componentCache = new ComponentCache();
            var allComponentFactoriesCache = new AllComponentFactoriesCache();

            //var factoryView = new ComponentFactoryView(factoryCache);
            //var factoriesView = new ComponentFactoriesView(factoriesCache);

            var snapshotService = new SnapshotService(allComponentFactoriesCache);
            var componentResolverService = new ComponentResolverService();

            var componentRegistrationRepository = new Repo(bus);

            var requestRepository = new Repository<Guid, Request>(bus, request => request.Id);

            // handlers
            var assignComponentKeysHandler = new AssignComponentKeysHandler(componentRegistrationRepository);
            var beginRequestHandler = new BeginRequestHandler(requestRepository, snapshotService);
            var endRequestHandler = new EndRequestHandler(requestRepository);
            var preserveExistingRegistrationsHandler = new PreserveExistingRegistrationsHandler(componentRegistrationRepository);
            var registerComponentHandler = new CustomRegisterComponentHandler(componentRegistrationRepository);
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
            var allComponentFactoriesView = new AllComponentFactoriesView(allComponentFactoriesCache);
            var componentView = new ResolvedComponentsView(componentCache);

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
