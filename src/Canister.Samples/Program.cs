namespace Canister.Samples
{
    using Canister.Factories;

    class Program
    {
        static void Main(string[] args)
        {
            var container = new ContainerFactory().Create();

            container.Register(e => new Thing { Name = "first" }).As(new object[] { "new Thing", typeof(Thing) });
            container.Register(e => new Thing { Name = "second" }).As(new object[] { "new Thing", typeof(Thing) }).PreserveExistingRegistrations();
            container.Register(e => new Thing { Name = "third" }).As(new[] { "new Thing" });

            var thing = container.Resolve("new Thing");
            var things = container.ResolveAll("new Thing");

            var otherThing = container.Resolve(typeof(Thing));
            var otherThings = container.ResolveAll(typeof(Thing));
        }
    }

    public class Thing
    {
        public string Name { get; set; }
    }
}
