using System;
using System.Linq.Expressions;
namespace Canister.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainer container = new Container();

            container.Register(e => new Thing { Name = "first" });
            container.Register(e => new Thing { Name = "second" }).As(new[] { typeof(IThing), typeof(Thing) }).PreserveExistingRegistrations();
            //container.Register(r => new SwampThing((IThing)r.Resolve(typeof(IThing)))).AsImplementedInterfaces();
            container.Register<SwampThing>().AsImplementedInterfaces();
            //container.Register(e => new Thing { Name = "second" }).As(new object[] { "new Thing", typeof(Thing) }).PreserveExistingRegistrations();
            //container.Register(e => new Thing { Name = "third" }).As(new[] { "new Thing" });

            //var thing = container.Resolve("new Thing");
            //var things = container.ResolveAll("new Thing");

            var thing = container.Resolve(typeof(Thing));
            var otherThing = container.Resolve<IThing>();
            var swampThing = container.ResolveAll<SwampThing>();

            var allThings = container.ResolveAll<IThing>();
        }
    }

    public class Thing : IThing 
    {
        public string Name { get; set; }
    }

    public class SwampThing : IThing
    {
        public SwampThing(IThing thing)
        {
            this.Thing = thing;
        }
        
        public IThing Thing { get; set; }
    }

    public interface IThing
    {
    }
}
