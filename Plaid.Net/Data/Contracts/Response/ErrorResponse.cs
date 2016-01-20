namespace Plaid.Net.Data.Contracts
{
    using System;
    using System.Net;
    using Newtonsoft.Json;
    using Plaid.Net.Data.Models;

    /// <summary>
    /// The error response object returned from Plaid.
    /// </summary>
    internal class ErrorResponse
    {
        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        [JsonProperty("code")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        [JsonProperty("message")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the resolution message.
        /// </summary>
        [JsonProperty("resolve")]
        public string ResolutionMessage { get; set; }

        /// <summary>
        /// Converts this contract into a <see cref="PlaidException"/> model.
        /// </summary>
        /// <param name="httpStatusCode">The http status code returned from Plaid.</param>
        /// <returns>The <see cref="PlaidException"/> model.</returns>
        public PlaidException ToException(int httpStatusCode)
        {
            ErrorCode errorCode = Models.ErrorCode.Unknown;
            Enum.TryParse(this.ErrorCode.ToString(), true, out errorCode);
            return new PlaidException(this.ErrorMessage, this.ResolutionMessage, errorCode, httpStatusCode);
        }

        /// <summary>
        /// Converts this contract into a <see cref="PlaidException"/> model.
        /// </summary>
        /// <param name="httpStatusCode">The http status code returned from Plaid.</param>
        /// <returns>The <see cref="PlaidException"/> model.</returns>
        public PlaidException ToException(HttpStatusCode httpStatusCode)
        {
            return this.ToException((int)httpStatusCode);
        }
    }
}