namespace Plaid.Net.Contracts
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Base class for all plaid requests.
    /// </summary>
    internal class PlaidRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaidRequest"/> class.
        /// </summary>
        /// <param name="clientId">The client id provided on signup.</param>
        /// <param name="secret">The client secret provided on signup.</param>
        /// <param name="accessToken">The access token associated with the request.</param>
        public PlaidRequest(string clientId, string secret, string accessToken = null)
        {
            this.ClientId = clientId;
            this.Secret = secret;
            this.AccessToken = accessToken;
        }

        /// <summary>
        /// Gets the access token associated with the request.
        /// </summary>
        [JsonProperty("access_token", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AccessToken { get; private set; }

        /// <summary>
        /// Gets or sets the client id provided on signup.
        /// </summary>
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets the client secret provided on signup.
        /// </summary>
        [JsonProperty("secret")]
        public string Secret { get; private set; }
    }
}