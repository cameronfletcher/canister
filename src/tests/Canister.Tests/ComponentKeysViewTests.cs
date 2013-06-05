// <copyright file="ComponentKeysViewTests.cs" company="Canister contributors">
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

    public class ComponentKeysViewTests
    {
        [Fact]
        public void RegisterSingleComponent()
        {
            // arrange
            var cache = new ComponentKeyCache();
            var view = new ComponentKeysView(cache);

            // arrange (event stream)
            var event1 = new ComponentRegistered(Guid.NewGuid(), typeof(DoStuff), resolver => new DoStuff());

            // act
            view.Handle(event1);

            // assert
            cache.GetComponentKeys().Length.Should().Be(1);
            cache.GetComponentKeys().Should().Contain(event1.OriginalComponentKey);
        }

        [Fact]
        public void RegisterSingleComponentWithAssignedKeys()
        {
            // arrange
            var cache = new ComponentKeyCache();
            var view = new ComponentKeysView(cache);

            // arrange (event stream)
            var event1 = new ComponentRegistered(Guid.NewGuid(), typeof(DoEverything), resolver => new DoEverything());
            var event2 = new ComponentKeysAssigned(event1.ComponentRegistrationId, new[] { typeof(IDoStuff), typeof(IDoOtherStuff) });

            // act
            view.Handle(event1);
            view.Handle(event2);

            // assert
            cache.GetComponentKeys().Length.Should().Be(2);
            cache.GetComponentKeys().Should().Contain(event2.ComponentKeys[0]);
            cache.GetComponentKeys().Should().Contain(event2.ComponentKeys[1]);
        }

        [Fact]
        public void RegisterMultipleComponentsWithAssignedKeys()
        {
            // arrange
            var cache = new ComponentKeyCache();
            var view = new ComponentKeysView(cache);

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
            cache.GetComponentKeys().Length.Should().Be(3);
            cache.GetComponentKeys().Should().Contain(event2.ComponentKeys[0]);
            cache.GetComponentKeys().Should().Contain(event4.ComponentKeys[0]);
            cache.GetComponentKeys().Should().Contain(event7.ComponentKeys[0]);
        }
    }
}
