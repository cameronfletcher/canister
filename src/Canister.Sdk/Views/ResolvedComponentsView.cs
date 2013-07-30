namespace Canister.Sdk.Views
{
    using System.Linq;
    using Canister.Sdk.Cache;
    using Canister.Sdk.Events;

    public sealed class ResolvedComponentsView
    {
        private readonly IComponentCache cache;

        public ResolvedComponentsView(IComponentCache cache)
        {
            Guard.Against.Null(() => cache);

            this.cache = cache;
        }

        public void Handle(ComponentResolved @event)
        {
            Guard.Against.Null(() => @event);

            var components = this.cache.GetComponents(@event.RequestId);
            this.cache.SetComponents(@event.RequestId, components.Union(new[] { @event.Component }).ToArray());
        }
    }
}
