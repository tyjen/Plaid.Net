namespace Plaid.Net.Contracts
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// The add user response.
    /// </summary>
    internal class AddUserResponse
    {
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets the accounts.
        /// </summary>
        [JsonProperty("accounts")]
        public IList<AccountResponse> Accounts { get; set; }

        /// <summary>
        /// Gets or sets the transactions.
        /// </summary>
        [JsonProperty("transactions")]
        public IList<TransactionResponse> Transactions { get; set; }
    }
}