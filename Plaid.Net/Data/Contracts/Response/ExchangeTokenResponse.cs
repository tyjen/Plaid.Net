namespace Plaid.Net.Data.Contracts.Response
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Response contract for ExchangeToken api.
    /// </summary>
    internal class ExchangeTokenResponse
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the bank token.
        /// </summary>
        [JsonProperty("stripe_bank_account_token")]
        public string BankToken { get; set; }
    }
}