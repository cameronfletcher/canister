// <copyright file="ComponentRegistrationException.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister.Sdk
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The exception that is thrown when there is a problem registering a component.
    /// </summary>
    [Serializable]
    public class ComponentRegistrationException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ComponentRegistrationException" /> class.
        /// </summary>
        public ComponentRegistrationException()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ComponentRegistrationException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ComponentRegistrationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ComponentRegistrationException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ComponentRegistrationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ComponentRegistrationException" /> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object 
        /// data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information 
        /// about the source or destination.
        /// </param>
        protected ComponentRegistrationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
