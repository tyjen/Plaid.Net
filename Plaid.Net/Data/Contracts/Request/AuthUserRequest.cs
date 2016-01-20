namespace Plaid.Net.Data.Contracts.Request
{
    using System;
    using Newtonsoft.Json;
    using Plaid.Net.Data.Models;

    /// <summary>
    /// Requests for the /connect/step api.
    /// </summary>
    internal class AuthUserRequest : PlaidRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthUserRequest"/> class.
        /// </summary>
        /// <param name="clientId">The client id provided on signup.</param>
        /// <param name="secret">The client secret provided on signup.</param>
        /// <param name="accessToken">The access token associated with the request.</param>
        public AuthUserRequest(string clientId, string secret, AccessToken accessToken)
            : base(clientId, secret, accessToken?.Value)
        {
        }

        /// <summary>
        /// Gets or sets the mfa value.
        /// </summary>
        [JsonProperty("mfa", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object MfaValue { get; set; }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        [JsonProperty("options", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public SendMethodRequest Options { get; set; }
    }
}