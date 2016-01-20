namespace Plaid.Net.Data.Contracts.Request
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Options for <see cref="GetTransactionsRequest"/>.
    /// </summary>
    internal class TransactionOptionsRequest
    {
        /// <summary>
        /// Collect transactions for a specific account only.
        /// </summary>
        [JsonProperty("account", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Account { get; set; }

        /// <summary>
        /// Collect all recent transactions since and including the given date.
        /// </summary>
        [JsonProperty("gte", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset? GreaterThanDate { get; set; }

        /// <summary>
        /// Collect all transactions up to and including the given date.
        /// </summary>
        [JsonProperty("lte", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTimeOffset? LessThanDate { get; set; }

        /// <summary>
        /// If set to true, transactions from account activities that have not yet posted to the account will be returned.
        /// </summary>
        [JsonProperty("pending", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? Pending { get; set; }
    }
}