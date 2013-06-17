// <copyright file="TurnkeyBus.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

[SuppressMessage("Microsoft.Design", "CA1050:DeclareTypesInNamespaces", Justification = "WIP")]
public sealed class TurnkeyBus
{
    private readonly Dictionary<object, Action<object>[]> registeredHandlers = new Dictionary<object, Action<object>[]>();

    public void Register<T>(Action<T> messageHandler) where T : class
    {
        if (messageHandler == null)
        {
            throw new ArgumentNullException("messageHandler");
        }

        Action<object> handler = message => messageHandler.Invoke((T)message);

        Action<object>[] handlers;
        if (!this.registeredHandlers.TryGetValue(typeof(T), out handlers))
        {
            this.registeredHandlers.Add(typeof(T), new Action<object>[0]);
            handlers = new Action<object>[0];
        }

        this.registeredHandlers[typeof(T)] = handlers.Concat(new[] { handler }).ToArray();
    }

    public void Send(object message)
    {
        if (message == null)
        {
            throw new ArgumentNullException("message");
        }

        Action<object>[] handlers;
        if (!this.registeredHandlers.TryGetValue(message.GetType(), out handlers))
        {
            // no handlers
            return;
        }

        foreach (var handler in handlers)
        {
            // this may throw... what then?
            handler.Invoke(message);
        }
    }
}
