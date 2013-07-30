namespace Canister.Sdk.Events
{
    using System;

    public class ComponentResolved
    {
        public Guid RequestId { get; set; }

        public object Component { get; set; }
    }
}
