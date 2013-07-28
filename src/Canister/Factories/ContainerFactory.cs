//// <copyright file="ContainerFactory.cs" company="Canister contributors">
////  Copyright (c) Canister contributors. All rights reserved.
//// </copyright>

//namespace Canister.Factories
//{
//    using System.Diagnostics.CodeAnalysis;
//    using Canister.Cache;
//    using Canister.Events;
//    using Canister.Model;
//    using Canister.Views;

//    public class ContainerFactory
//    {
//        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "WIP")]
//        public IContainer Create()
//        {
//            var bus = new MessageBus();

//            var factoryCache = new ComponentFactoryCache();
//            var factoriesCache = new ComponentFactoriesCache();

//            var factoryView = new ComponentFactoryView(factoryCache);
//            var factoriesView = new ComponentFactoriesView(factoriesCache);

//            bus.Register<ComponentRegistered>(message => factoryView.Handle(message));
//            bus.Register<ComponentKeysAssigned>(message => factoryView.Handle(message));
//            bus.Register<ExistingRegistrationsPreserved>(message => factoryView.Handle(message));

//            bus.Register<ComponentRegistered>(message => factoriesView.Handle(message));
//            bus.Register<ComponentKeysAssigned>(message => factoriesView.Handle(message));
//            bus.Register<ExistingRegistrationsPreserved>(message => factoriesView.Handle(message));

//            var resolver = new ComponentResolver(factoryCache, factoriesCache);
//            var container = new ContainerBase(bus, resolver);

//            return container;
//        }
//    }
//}
