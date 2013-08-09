namespace Canister.Sdk.Factories
{
    using System;
    using Canister.Sdk.Cache;
    using Canister.Sdk.Handlers;
    using Canister.Sdk.Infrastructure;
    using Canister.Sdk.Model;
    using Canister.Sdk.Views;

    public class ContainerDependencies
    {
        // bus
        public MessageBus Bus { get; internal set; }

        // caches
        public IComponentsCache ComponentsCache { get; set; }
        public IComponentFactoriesCache ComponentFactoriesCache { get; set; }

        // services
        public ISnapshotService SnapshotService { get; set; }
        public IComponentResolverService ComponentResolverService { get; set; }

        // repositories
        public IRepository<Guid, Request> RequestRepository { get; set; }
        public IRepository<Guid, ComponentRegistration> ComponentRegistrationRepository { get; set; }

        // handlers
        public AssignComponentKeysHandler AssignComponentKeysHandler { get; set; }
        public BeginRequestHandler BeginRequestHandler { get; set; }
        public EndRequestHandler EndRequestHandler { get; set; }
        public PreserveExistingRegistrationsHandler PreserveExistingRegistrationsHandler { get; set; }
        public RegisterComponentHandler RegisterComponentHandler { get; set; }
        public ResolveAllComponentsHandler ResolveAllComponentsHandler { get; set; }
        public ResolveComponentHandler ResolveComponentHandler { get; set; }

        // views
        public ComponentsView ComponentsView { get; set; }
        public ComponentFactoriesView ComponentFactoriesView { get; set; }
    }
}
