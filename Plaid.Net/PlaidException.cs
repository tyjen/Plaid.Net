namespace Plaid.Net
{
    using System;
    using System.Net;
    using Plaid.Net.Data.Models;

    /// <summary>
    /// The plaid exception.
    /// </summary>
    public class PlaidException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaidException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="resolution">
        /// The resolution.
        /// </param>
        /// <param name="errorCode">
        /// The error code.
        /// </param>
        /// <param name="httpStatusCode">
        /// The http status code.
        /// </param>
        public PlaidException(string message, string resolution, ErrorCode errorCode, HttpStatusCode httpStatusCode)
            : this(message, resolution, errorCode, (int)httpStatusCode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaidException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="resolution">
        /// The resolution.
        /// </param>
        /// <param name="errorCode">
        /// The error code.
        /// </param>
        /// <param name="httpStatusCode">
        /// The http status code.
        /// </param>
        public PlaidException(string message, string resolution, ErrorCode errorCode, int httpStatusCode)
            : base(message)
        {
            this.ErrorCode = errorCode;
            this.Resolution = resolution;
            this.HttpStatusCode = httpStatusCode;
        }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public ErrorCode ErrorCode { get; private set; }

        /// <summary>
        /// Gets or sets the http status code.
        /// </summary>
        public int HttpStatusCode { get; private set; }

        /// <summary>
        /// Gets or sets the resolution.
        /// </summary>
        public string Resolution { get; private set; }
    }
}