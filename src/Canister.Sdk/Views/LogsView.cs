// <copyright file="LogsView.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk.Views
{
    using System.IO;
    using System.Linq;
    using Canister.Sdk.Events;

    public class LogsView
    {
        private readonly TextWriter writer;

        public LogsView(TextWriter writer)
        {
            Guard.Against.Null(() => writer);

            this.writer = writer;
        }

        public void Handle(ComponentKeysAssigned @event)
        {
            Guard.Against.Null(() => @event);

            this.writer.WriteLine(
                "Component {0} has been assigned keys {1}",
                @event.ComponentRegistrationId,
                string.Join(", ", @event.ComponentKeys.Select(componentKey => string.Concat("'", componentKey, "'"))));
        }

        public void Handle(ComponentRegistered @event)
        {
            Guard.Against.Null(() => @event);

            this.writer.WriteLine(
                "Component {0} with key '{1}' has been registered",
                @event.ComponentRegistrationId,
                @event.ComponentKey);
        }

        public void Handle(ComponentResolved @event)
        {
            Guard.Against.Null(() => @event);

            this.writer.WriteLine(
                "Component of type {0} has been resolved as part of request {1}",
                @event.Component.GetType(),
                @event.RequestId);
        }

        public void Handle(ExistingRegistrationsPreserved @event)
        {
            Guard.Against.Null(() => @event);

            this.writer.WriteLine(
                "Component {0} has had its existing registrations preserved",
                @event.ComponentRegistrationId);
        }

        public void Handle(RequestEnded @event)
        {
            Guard.Against.Null(() => @event);

            this.writer.WriteLine(
                "Request {0} has ended",
                @event.RequestId);
        }

        public void Handle(RequestStarted @event)
        {
            Guard.Against.Null(() => @event);

            this.writer.WriteLine(
                "Request {0} has started",
                @event.RequestId);
        }
    }
}
