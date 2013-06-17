// <copyright file="ComponentResolverException.cs" company="Canister contributors">
//  Copyright (c) Canister contributors. All rights reserved.
// </copyright>

namespace Canister
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// The exception that is thrown when there is a problem with configuration.
    /// </summary>
    //// TODO (Cameron): Add componentKey as parameter and property.
    [Serializable]
    public class ComponentResolverException : Exception, ISerializable
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ComponentResolverException" /> class.
        /// </summary>
        public ComponentResolverException()
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ComponentResolverException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ComponentResolverException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ComponentResolverException" /> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public ComponentResolverException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ComponentResolverException" /> class.
        /// </summary>
        /// <param name="info">
        /// The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object 
        /// data about the exception being thrown.
        /// </param>
        /// <param name="context">
        /// The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information 
        /// about the source or destination.
        /// </param>
        protected ComponentResolverException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
