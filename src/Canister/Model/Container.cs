namespace Canister.Model
{
    using System;
    using System.Collections.Generic;
    using Canister.Events;

    public class Container : IContainer
    {
        private readonly TurnkeyBus bus;
        private readonly IComponentResolver componentResolver;

        public Container(TurnkeyBus bus, IComponentResolver componentResolver)
        {
            this.bus = bus;
            this.componentResolver = componentResolver;
        }

        public IComponentRegistration Register<T>(Func<IComponentResolver, T> componentFactory) where T : class
        {
            var @event = new ComponentRegistered(Guid.NewGuid(), typeof(T), componentFactory);

            this.bus.Send(@event);

            return new ComponentRegistration(this.bus, @event.ComponentRegistrationId);
        }

        public object Resolve(object componentKey)
        {
            return this.componentResolver.Resolve(componentKey);
        }

        public IEnumerable<object> ResolveAll(object componentKey)
        {
            return this.ResolveAll(componentKey);
        }
    }
}
