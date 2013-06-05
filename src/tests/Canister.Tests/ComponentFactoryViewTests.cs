// <copyright file="ComponentFactoryViewTests.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Tests
{
    using System;
    using Canister.Cache;
    using Canister.Events;
    using Canister.Views;
    using FluentAssertions;
    using Xunit;

    public class ComponentFactoryViewTests
    {
        [Fact]
        public void RegisterSingleComponent()
        {
            // arrange
            var cache = new ComponentFactoryCache();
            var view = new ComponentFactoryView(cache);

            // arrange (event stream)
            var event1 = new ComponentRegistered(Guid.NewGuid(), typeof(DoStuff), resolver => new DoStuff());

            // act
            view.Handle(event1);

            // assert
            cache.GetComponentFactory(event1.ComponentKey).Should().Be(event1.ComponentFactory);
        }

        [Fact]
        public void RegisterSingleComponentWithAssignedTypes()
        {
            // arrange
            var cache = new ComponentFactoryCache();
            var view = new ComponentFactoryView(cache);

            // arrange (event stream)
            var event1 = new ComponentRegistered(Guid.NewGuid(), typeof(DoEverything), resolver => new DoEverything());
            var event2 = new ComponentKeysAssigned(event1.ComponentRegistrationId, new[] { typeof(IDoStuff), typeof(IDoOtherStuff) });

            // act
            view.Handle(event1);
            view.Handle(event2);

            // assert
            cache.GetComponentFactory(event2.ComponentKeys[0]).Should().Be(event1.ComponentFactory);
            cache.GetComponentFactory(event2.ComponentKeys[1]).Should().Be(event1.ComponentFactory);
            cache.GetComponentFactory(event1.ComponentKey).Should().Be(null);
        }

        [Fact]
        public void RegisterMultipleComponentsWithAssignedTypes()
        {
            // arrange
            var cache = new ComponentFactoryCache();
            var view = new ComponentFactoryView(cache);

            // arrange (event stream)
            var event1 = new ComponentRegistered(Guid.NewGuid(), typeof(DoStuff), resolver => new DoStuff());
            var event2 = new ComponentKeysAssigned(event1.ComponentRegistrationId, new[] { typeof(IDoStuff) });
            var event3 = new ComponentRegistered(Guid.NewGuid(), typeof(DoOtherStuff), resolver => new DoOtherStuff());
            var event4 = new ComponentKeysAssigned(event3.ComponentRegistrationId, new[] { typeof(IDoOtherStuff) });
            var event5 = new ComponentRegistered(Guid.NewGuid(), typeof(DoEverything), resolver => new DoEverything());
            var event6 = new ComponentKeysAssigned(event5.ComponentRegistrationId, new[] { typeof(IDoStuff), typeof(IDoOtherStuff) });
            var event7 = new ComponentKeysAssigned(event5.ComponentRegistrationId, new[] { typeof(DoEverything) });

            // act
            view.Handle(event1);
            view.Handle(event2);
            view.Handle(event3);
            view.Handle(event4);
            view.Handle(event5);
            view.Handle(event6);
            view.Handle(event7);

            // assert
            cache.GetComponentFactory(event2.ComponentKeys[0]).Should().Be(event1.ComponentFactory);
            cache.GetComponentFactory(event4.ComponentKeys[0]).Should().Be(event3.ComponentFactory);
            cache.GetComponentFactory(event7.ComponentKeys[0]).Should().Be(event5.ComponentFactory);
            cache.GetComponentFactory(event1.ComponentKey).Should().Be(null);
            cache.GetComponentFactory(event3.ComponentKey).Should().Be(null);
        }

        [Fact]
        public void RegisterMultipleComponentsWithAssignedKeysAndPreservedRegistrations()
        {
            // arrange
            var cache = new ComponentFactoryCache();
            var view = new ComponentFactoryView(cache);

            // arrange (event stream)
            var event1 = new ComponentRegistered(Guid.NewGuid(), typeof(DoEverything), resolver => new DoEverything());
            var event2 = new ComponentKeysAssigned(event1.ComponentRegistrationId, new[] { typeof(IDoStuff) });
            var event3 = new ComponentRegistered(Guid.NewGuid(), typeof(DoStuff), resolver => new DoStuff());
            var event4 = new ComponentKeysAssigned(event3.ComponentRegistrationId, new[] { typeof(IDoStuff) });
            var event5 = new ExistingRegistrationsPreserved(event3.ComponentRegistrationId);

            // act
            view.Handle(event1);
            view.Handle(event2);
            view.Handle(event3);
            view.Handle(event4);
            view.Handle(event5);

            // assert
            cache.GetComponentFactory(event4.ComponentKeys[0]).Should().Be(event1.ComponentFactory);
            cache.GetComponentFactory(event1.ComponentKey).Should().Be(null);
            cache.GetComponentFactory(event3.ComponentKey).Should().Be(null);
        }

        [Fact]
        public void RegisterMultipleComponentsWithAssignedKeysAndPreservedRegistrations2()
        {
            // arrange
            var cache = new ComponentFactoryCache();
            var view = new ComponentFactoryView(cache);

            // arrange (event stream)
            var event1 = new ComponentRegistered(Guid.NewGuid(), typeof(DoEverything), resolver => new DoEverything("A"));
            var event2 = new ComponentKeysAssigned(event1.ComponentRegistrationId, new[] { typeof(IDoStuff) });
            var event3 = new ComponentRegistered(Guid.NewGuid(), typeof(DoStuff), resolver => new DoStuff("B"));
            var event4 = new ComponentKeysAssigned(event3.ComponentRegistrationId, new[] { typeof(IDoStuff) });
            var event5 = new ComponentRegistered(Guid.NewGuid(), typeof(DoEverything), resolver => new DoEverything("C"));
            var event6 = new ComponentKeysAssigned(event5.ComponentRegistrationId, new[] { typeof(IDoStuff), typeof(IDoOtherStuff) });
            var event7 = new ExistingRegistrationsPreserved(event5.ComponentRegistrationId);

            // act
            view.Handle(event1);
            view.Handle(event2);
            view.Handle(event3);
            view.Handle(event4);
            view.Handle(event5);
            view.Handle(event6);
            view.Handle(event7);

            // assert
            cache.GetComponentFactory(event6.ComponentKeys[0]).Should().Be(event3.ComponentFactory);
            cache.GetComponentFactory(event1.ComponentKey).Should().Be(null);
            cache.GetComponentFactory(event3.ComponentKey).Should().Be(null);
            cache.GetComponentFactory(event5.ComponentKey).Should().Be(null);
        }
    }
}
