namespace Canister.Samples
{
    using Canister.Factories;

    class Program
    {
        static void Main(string[] args)
        {
            var container = new ContainerFactory().Create();

            container.Register(e => new Thing { Name = "first" }).As(new object[] { typeof(IThing), typeof(Thing) });
            //container.Register(e => new Thing { Name = "second" }).As(new object[] { "new Thing", typeof(Thing) }).PreserveExistingRegistrations();
            //container.Register(e => new Thing { Name = "third" }).As(new[] { "new Thing" });

            //var thing = container.Resolve("new Thing");
            //var things = container.ResolveAll("new Thing");

            var thing = container.Resolve(typeof(Thing));
            var otherThing = container.Resolve(typeof(IThing));
        }
    }

    public class Thing : IThing
    {
        public string Name { get; set; }
    }

    public interface IThing
    {
    }
}
