// <copyright file="ContainerFactory.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Factories
{
    using System.Diagnostics.CodeAnalysis;
    using Canister.Model;
    using Canister.Sdk;
    using Canister.Sdk.Cache;

    public class ContainerFactory
    {
        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "WIP")]
        public IContainer Create()
        {
            var bus = new MessageBus();

            //var factoryCache = new ComponentFactoryCache();
            //var factoriesCache = new ComponentFactoriesCache();
            var componentCache = new ComponentCache();

            //var factoryView = new ComponentFactoryView(factoryCache);
            //var factoriesView = new ComponentFactoriesView(factoriesCache);

            //bus.Register<ComponentRegistered>(message => factoryView.Handle(message));
            //bus.Register<ComponentKeysAssigned>(message => factoryView.Handle(message));
            //bus.Register<ExistingRegistrationsPreserved>(message => factoryView.Handle(message));

            //bus.Register<ComponentRegistered>(message => factoriesView.Handle(message));
            //bus.Register<ComponentKeysAssigned>(message => factoriesView.Handle(message));
            //bus.Register<ExistingRegistrationsPreserved>(message => factoriesView.Handle(message));

            var container = new Container(bus, componentCache);

            return container;
        }
    }
}
