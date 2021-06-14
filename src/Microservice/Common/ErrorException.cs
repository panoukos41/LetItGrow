﻿using System;

namespace LetItGrow.Microservice.Common
{
    /// <summary>
    /// An exception thrown from the RequestHandlers and any part of the app.
    /// These exceptions are considered handled and the error describes what went wrong.
    /// </summary>
    public class ErrorException : Exception
    {
        /// <summary>
        /// Same as the <see cref="Error.Status"/> this is the HTTP status code([RFC7231], Section 6)
        /// generated by the origin server for this occurrence of the problem.
        /// </summary>
        public int Status { get; }

        /// <summary>
        /// An object that contains information of what went wrong.
        /// </summary>
        public Error Error { get; }

        /// <summary>
        /// Initializes a new <see cref="ErrorException"/> with the provided Error.
        /// </summary>
        /// <param name="error">The error that caused this exception.</param>
        public ErrorException(Error error) : base($"{error.Title}: {error.Detail}")
        {
            Status = error.Status;
            Error = error;
        }

        /// <summary>
        /// Initializes a new <see cref="ErrorException"/> with the provided Error.
        /// </summary>
        /// <param name="error">The error that caused this exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference
        /// (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ErrorException(Error error, Exception innerException) : base(error.Title, innerException)
        {
            Status = error.Status;
            Error = error;
        }
    }
}