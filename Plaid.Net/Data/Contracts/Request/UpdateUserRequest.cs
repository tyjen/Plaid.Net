namespace Plaid.Net.Data.Contracts.Request
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Request for updating a user.
    /// </summary>
    internal class UpdateUserRequest : PlaidRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserRequest"/> class. 
        /// </summary>
        /// <param name="clientId">The client id provided on signup.</param>
        /// <param name="secret">The client secret provided on signup.</param>
        /// <param name="accessToken">The access token associated with the request.</param>
        public UpdateUserRequest(string clientId, string secret, string accessToken)
            : base(clientId, secret, accessToken)
        {
        }

        /// <summary>
        /// Gets or sets the request options.
        /// </summary>
        [JsonProperty("options", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public UpdateUserRequestOptions Options { get; set; }

        /// <summary>
        /// Gets or sets the updated password.
        /// </summary>
        [JsonProperty("password", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the updated pin.
        /// </summary>
        [JsonProperty("pin", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Pin { get; set; }

        /// <summary>
        /// Gets or sets the updated username.
        /// </summary>
        [JsonProperty("username", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Username { get; set; }
    }
}