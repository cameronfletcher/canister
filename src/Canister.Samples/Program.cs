namespace Canister.Samples
{
    using Canister.Cache;
    using Canister.Events;
    using Canister.Views;
    using global::Canister.Model;

    class Program
    {
        static void Main(string[] args)
        {
            var bus = new TurnkeyBus();

            var factoryCache = new ComponentFactoryCache();
            var factoriesCache = new ComponentFactoriesCache();

            var factoryView = new ComponentFactoryView(factoryCache);
            var factoriesView = new ComponentFactoriesView(factoriesCache);

            bus.Register<ComponentRegistered>(message => factoryView.Handle(message));
            bus.Register<ComponentKeysAssigned>(message => factoryView.Handle(message));
            bus.Register<ExistingRegistrationsPreserved>(message => factoryView.Handle(message));

            bus.Register<ComponentRegistered>(message => factoriesView.Handle(message));
            bus.Register<ComponentKeysAssigned>(message => factoriesView.Handle(message));
            bus.Register<ExistingRegistrationsPreserved>(message => factoriesView.Handle(message));

            var resolver = new ComponentResolver(factoryCache, factoriesCache);
            var container = new Container(bus, resolver);

            container.Register(e => new Thing()).As(new[] { "new Thing" }).PreserveExistingRegistrations();

            var thing = container.Resolve("new Thing");
        }
    }

    public class Thing
    {
    }
}
