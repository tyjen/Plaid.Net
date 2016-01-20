namespace Plaid.Net.Data.Contracts.Request
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// The get transactions request.
    /// </summary>
    internal class GetTransactionsRequest : PlaidRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTransactionsRequest"/> class.
        /// </summary>
        /// <param name="clientId">The client id provided on signup.</param>
        /// <param name="secret">The client secret provided on signup.</param>
        /// <param name="accessToken">The access token associated with the request.</param>
        public GetTransactionsRequest(string clientId, string secret, string accessToken = null)
            : base(clientId, secret, accessToken)
        {
        }

        /// <summary>
        /// Gets or sets the options.
        /// </summary>
        [JsonProperty("options", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public TransactionOptionsRequest Options { get; set; }
    }
}