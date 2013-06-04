// <copyright file="ComponentTypesViewTests.cs" company="Canister contributors">
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

    public class ComponentTypesViewTests
    {
        [Fact]
        public void RegisterSingleComponent()
        {
            // arrange
            var cache = new ComponentTypesCache();
            var view = new ComponentTypesView(cache);

            // arrange (event stream)
            var event1 = new ComponentRegistered(Guid.NewGuid(), typeof(DoStuff), resolver => new DoStuff());

            // act
            view.Handle(event1);

            // assert
            cache.GetComponentTypes().Length.Should().Be(1);
            cache.GetComponentTypes().Should().Contain(event1.OriginalComponentType);
        }

        [Fact]
        public void RegisterSingleComponentWithAssignedTypes()
        {
            // arrange
            var cache = new ComponentTypesCache();
            var view = new ComponentTypesView(cache);

            // arrange (event stream)
            var event1 = new ComponentRegistered(Guid.NewGuid(), typeof(DoEverything), resolver => new DoEverything());
            var event2 = new ComponentTypesAssigned(event1.ComponentRegistrationId, new[] { typeof(IDoStuff), typeof(IDoOtherStuff) });

            // act
            view.Handle(event1);
            view.Handle(event2);

            // assert
            cache.GetComponentTypes().Length.Should().Be(2);
            cache.GetComponentTypes().Should().Contain(event2.ComponentTypes[0]);
            cache.GetComponentTypes().Should().Contain(event2.ComponentTypes[1]);
        }

        [Fact]
        public void RegisterMultipleComponentsWithAssignedTypes()
        {
            // arrange
            var cache = new ComponentTypesCache();
            var view = new ComponentTypesView(cache);

            // arrange (event stream)
            var event1 = new ComponentRegistered(Guid.NewGuid(), typeof(DoStuff), resolver => new DoStuff());
            var event2 = new ComponentTypesAssigned(event1.ComponentRegistrationId, new[] { typeof(IDoStuff) });
            var event3 = new ComponentRegistered(Guid.NewGuid(), typeof(DoOtherStuff), resolver => new DoOtherStuff());
            var event4 = new ComponentTypesAssigned(event3.ComponentRegistrationId, new[] { typeof(IDoOtherStuff) });
            var event5 = new ComponentRegistered(Guid.NewGuid(), typeof(DoEverything), resolver => new DoEverything());
            var event6 = new ComponentTypesAssigned(event5.ComponentRegistrationId, new[] { typeof(IDoStuff), typeof(IDoOtherStuff) });
            var event7 = new ComponentTypesAssigned(event5.ComponentRegistrationId, new[] { typeof(DoEverything) });

            // act
            view.Handle(event1);
            view.Handle(event2);
            view.Handle(event3);
            view.Handle(event4);
            view.Handle(event5);
            view.Handle(event6);
            view.Handle(event7);

            // assert
            cache.GetComponentTypes().Length.Should().Be(3);
            cache.GetComponentTypes().Should().Contain(event2.ComponentTypes[0]);
            cache.GetComponentTypes().Should().Contain(event4.ComponentTypes[0]);
            cache.GetComponentTypes().Should().Contain(event7.ComponentTypes[0]);
        }
    }
}
