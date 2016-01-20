namespace Plaid.Net.Contracts
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// Options for <see cref="UpdateUserRequest"/>.
    /// </summary>
    internal class UpdateUserRequestOptions
    {
        /// <summary>
        /// Gets or sets the webhook.
        /// </summary>
        [JsonProperty("webhook")]
        public string Webhook { get; set; }
    }
}