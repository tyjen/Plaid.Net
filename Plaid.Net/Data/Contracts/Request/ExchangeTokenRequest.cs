namespace Plaid.Net.Data.Contracts.Request
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Request object to exchange banking tokens.
    /// </summary>
    internal class ExchangeTokenRequest : PlaidRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExchangeTokenRequest"/> class.
        /// </summary>
        public ExchangeTokenRequest(string clientId, string secret, string publicToken, string accountId): base(clientId, secret)
        {
            this.PublicToken = publicToken;
            this.AccountId = accountId;
        }

        /// <summary>
        /// The public token from Plaid Link.
        /// </summary>
        [JsonProperty("public_token")]
        public string PublicToken { get; set; }

        /// <summary>
        /// The account identifier from Plaid Link.
        /// </summary>
        [JsonProperty("account_id")]
        public string AccountId { get; set; }
    }
}