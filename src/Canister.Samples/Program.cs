namespace Canister.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainer container = new Container();

            container.Register(e => new Thing { Name = "first" }).As(new[] { typeof(IThing), typeof(Thing) });
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
